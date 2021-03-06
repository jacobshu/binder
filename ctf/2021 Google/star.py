
 # from https://gist.github.com/jsbueno/1c2a2a74490702215597ea54dccf2a6e
from __future__ import print_function
version = __version__ = '1.1'
# Author- Ross Tucker
# Thanks to Marc Majcher for his project, Piet.pl



from PIL import Image   # Python Imaging Library
from operator import itemgetter
import sys

HEX_BLACK = '0x000000'
HEX_WHITE = '0xffffff'

hex2tuple = {
    '0xffc0c0':{'color':'light red','abbv':'lR','hue':0,'dark':0},
    '0xffffc0':{'color':'light yellow','abbv':'lY','hue':1,'dark':0},
    '0xc0ffc0':{'color':'light green','abbv':'lG','hue':2,'dark':0},
    '0xc0ffff':{'color':'light cyan','abbv':'lC','hue':3,'dark':0},
    '0xc0c0ff':{'color':'light blue','abbv':'lB','hue':4,'dark':0},
    '0xffc0ff':{'color':'light magenta','abbv':'lM','hue':5,'dark':0},
    '0xff0000':{'color':'red','abbv':' R','hue':0,'dark':1},
    '0xffff00':{'color':'yellow','abbv':' Y','hue':1,'dark':1},
    '0x00ff00':{'color':'green','abbv':' G','hue':2,'dark':1},
    '0x00ffff':{'color':'cyan','abbv':' C','hue':3,'dark':1},
    '0x0000ff':{'color':'blue','abbv':' B','hue':4,'dark':1},
    '0xff00ff':{'color':'magenta','abbv':' M','hue':5,'dark':1},
    '0xc00000':{'color':'dark red','abbv':'dR','hue':0,'dark':2},
    '0xc0c000':{'color':'dark yellow','abbv':'dY','hue':1,'dark':2},
    '0x00c000':{'color':'dark green','abbv':'dG','hue':2,'dark':2},
    '0x00c0c0':{'color':'dark cyan','abbv':'dC','hue':3,'dark':2},
    '0x0000c0':{'color':'dark blue','abbv':'dB','hue':4,'dark':2},
    '0xc000c0':{'color':'dark magenta','abbv':'dM','hue':5,'dark':2},
    '0xffffff':{'color':'white','abbv':'Wt','hue':-1,'dark':-1},
    '0x000000':{'color':'black','abbv':'Bk','hue':-1,'dark':-1}
    }

do_arr = [["NOOP", "PUSH", "POP"],
          ["ADD",  "SUB",  "MULT"],
          ["DIV",  "MOD",  "NOT"],
          ["GTR",  "PNTR", "SWCH"],
          ["DUP",  "ROLL", "N_IN"],
          ["C_IN", "NOUT", "COUT"]]

class PVM(object):
    def __init__(self):
        self.dp = 0
        self.cc = -1
        self.stack = []
        self.block_value = 1

    def NOOP(self):
        pass

    def PUSH(self):
        self.stack.append(self.block_value)

    def POP(self):
        if len(self.stack) < 1:
            return
        self.stack.pop()

    def ADD(self):
        if len(self.stack) < 2:
            return
        top = self.stack.pop()
        next = self.stack.pop()
        self.stack.append(next + top)

    def SUB(self):
        if len(self.stack) < 2:
            return
        top = self.stack.pop()
        next = self.stack.pop()
        self.stack.append(next - top)

    def MULT(self):
        if len(self.stack) < 2:
            return
        top = self.stack.pop()
        next = self.stack.pop()
        self.stack.append(next*top)

    def DIV(self):
        if len(self.stack) < 2:
            return
        top = self.stack.pop()
        next = self.stack.pop()
        self.stack.append(next // top)

    def MOD(self):
        if len(self.stack) < 2:
            return
        top = self.stack.pop()
        next = self.stack.pop()
        self.stack.append(next % top)

    def NOT(self):
        if len(self.stack) < 1:
            return
        top = self.stack.pop()
        self.stack.append(int(not top))

    def GTR(self):
        if len(self.stack) < 2:
            return
        top = self.stack.pop()
        next = self.stack.pop()
        self.stack.append(int(next > top))

    def PNTR(self):
        if len(self.stack) < 1:
            return
        top = self.stack.pop()
        self.dp = (self.dp + top) % 4

    def SWCH(self):
        if len(self.stack) < 1:
            return
        top = self.stack.pop()
        self.cc *= (-1) ** (top % 2)

    def DUP(self):
        if len(self.stack) < 1:
            return
        self.stack.append(self.stack[-1])

    def ROLL(self):
        if len(self.stack) < 2:
            return
        num = self.stack.pop()
        depth = self.stack.pop()
        num %= depth
        if depth <= 0 or num == 0:
            return
        x = -abs(num) + depth * (num < 0)
        self.stack[-depth:] = self.stack[x:] + self.stack[-depth:x]

    def N_IN(self):
        n = int(input("Enter an integer: "))
        self.stack.append(n)

    def C_IN(self):
        c = ord(input("Enter a character: "))
        self.stack.append(c)

    def NOUT(self):
        if len(self.stack) < 1:
            return
        top = self.stack.pop()
        sys.stdout.write(str(top))

    def COUT(self):
        if len(self.stack) < 1:
            return
        top = self.stack.pop()
        sys.stdout.write(chr(top))


class Interpreter(object):
    def __init__(self, filename, pvm, debug=False):
        self.x, self.y = 0, 0
        self.pvm = pvm
        self.debug = debug
        self.step_number = 1
        self.block = (0,0)
        self.filename = filename
        self._image = Image.open(self.filename).convert("RGB")
        self.cols, self.rows = self._image.size
        self.matrix = [[0 for x in range(self.cols)] \
                        for y in range(self.rows)]
        for j in range(self.rows):
            for i in range(self.cols):
                r,g,b = self._image.getpixel((i,j))
                self.matrix[j][i] = "0x%02x%02x%02x" % (r,g,b)

    def _is_valid(self,x,y):
        return 0 <= x < self.cols and 0 <= y < self.rows and \
               self.matrix[y][x] != HEX_BLACK

    def neighbors(self,x,y):
        for (dx,dy) in ((0,1),(0,-1),(1,0),(-1,0)):
            if self._is_valid(x+dx,y+dy) and \
                   (x+dx,y+dy) not in self.block and \
                   self.matrix[y][x] == self.matrix[y+dy][x+dx]:
                self.block.append((x+dx,y+dy))
                self.neighbors(x+dx,y+dy)

    def dmesg(self, mesg):
        if self.debug:
            print(mesg, file=sys.stderr)

    def get_edge(self):
        k_1 = int(not(self.pvm.dp % 2))
        r_1 = int(not(int(self.pvm.dp % 2) - int(self.pvm.cc < 0)))
        k_2 = int(self.pvm.dp % 2)
        r_2 = int(self.pvm.dp < 2)
        self.block.sort(key=itemgetter(k_1), reverse=r_1)
        self.block.sort(key=itemgetter(k_2), reverse=r_2)
        return self.block[0]

    def get_next_valid(self, x, y):
        if self.pvm.dp == 0:
            x += 1
        elif self.pvm.dp == 1:
            y += 1
        elif self.pvm.dp == 2:
            x -= 1
        elif self.pvm.dp == 3:
            y -= 1
        else:
            raise ValueError("Invalid DP value")
            sys.exit(1)
        return x,y

    def run(self):
        for step in self: #zip(self, range(10)):
            pass

    def step(self):
        while True:
            self.dmesg("\n-- STEP: %s" % self.step_number)
            self.block = [(self.x, self.y)]
            self.neighbors(self.x, self.y) # modifies self.block
            self.pvm.block_value = len(self.block)
            i = 1
            seen_white = 0
            ex, ey = self.get_edge()
            while i <= 8:
                nx, ny = self.get_next_valid(ex, ey)
                if not self._is_valid(nx, ny):
                    i += 1
                    if i % 2:
                        self.pvm.dp = (self.pvm.dp + 1) % 4
                    else:
                        self.pvm.cc *= -1
                    self.dmesg("Trying again at %s, %s. DP: %s CC: %s" % \
                            (nx, ny, self.pvm.dp, self.pvm.cc))
                    if self.matrix[ey][ex] != HEX_WHITE:
                        self.block = [(ex, ey)]
                        self.neighbors(ex, ey) # modifies self.block
                        ex, ey = self.get_edge()
                elif self.matrix[ny][nx] == HEX_WHITE:
                    if not seen_white:
                        seen_white = 1
                        i = 0
                        self.dmesg("Entering white; sliding thru")
                    ex, ey = nx, ny
                else: # next color is a color
                    self.dmesg("%s @ (%s,%s) -> %s @ (%s,%s) DP:%s CC:%s" % \
                            (hex2tuple[self.matrix[self.y][self.x]]['color'], \
                                self.x, self.y, \
                                hex2tuple[self.matrix[ny][nx]]['color'], \
                                nx, ny,\
                                self.pvm.dp, self.pvm.cc))
                    if not seen_white:
                        dH = hex2tuple[self.matrix[ny][nx]]['hue'] - \
                            hex2tuple[self.matrix[self.y][self.x]]['hue']
                        dD = hex2tuple[self.matrix[ny][nx]]['dark'] - \
                            hex2tuple[self.matrix[self.y][self.x]]['dark']
                        op = getattr(self.pvm, do_arr[dH][dD])
                        op()
                        #exec "self.pvm.%s" % do_arr[dH][dD]
                        self.dmesg("OPER: %s" % (do_arr[dH][dD]))

                    self.dmesg("STACK: %s" % self.pvm.stack)
                    self.x, self.y = nx, ny
                    self.step_number += 1
                    yield None
                    break
            else:
                self.dmesg("Execution trapped, program terminates")
                return

    __iter__ = step

if __name__ == "__main__":
    debug = False
    if "--debug" in sys.argv:
        debug = True
        sys.argv.remove("--debug")
    i = Interpreter(sys.argv[1], PVM(), debug)
    i.run()