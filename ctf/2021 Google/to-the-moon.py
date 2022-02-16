# I made a super secret encoder. I remember using:
# - a weird base, much higher than base64
# - a language named after a painter: Piet
# - a language that is the opposite of good
# - a language that looks like a rainbow cat
# - a language that is too vulgar to write here
# - a language that ended in 'ary' but I don't remember the full name

# I also use gzip and zlib (to compress the stuff) and I like hiding things in files...

#==============================================================================

characters = []

with open("chall.txt", "r") as file:
    while line := file.readline():
        for c in line:
            characters.append(c)

# There are 256 distinct characters in chall.txt so we essentially create a
# a chart of encodings like e.g.:
# https://upload.wikimedia.org/wikipedia/commons/1/1b/ASCII-Table-wide.svg)
sorted_distinct_characters = sorted(    # sort the list
  list(                                 # cast them to a list
    set(characters)                     # capture unique values (256)
  )
)

# We then index into our chart to derive values 0-255 which we can cast to
# hex values
values = []
for c in characters:
  values.append(hex(sorted_distinct_characters.index(c)))

print(values[:12]);

# The initial hex ends up as:              FF 8D FF E0 00 10 A4 64 94 64 00 10
# which is close to JPEGs file signature:  FF D8 FF E0 00 10 4A 46 49 46 00 01
# Doing a nibble swap will produce data that can be read as a jpeg file
swapped = []
for n in values:
  x = int(n,16)
  swapped.append((
    (x & 0xF0) >> 4)        # get the high nibble and shift right
    |                       # this OR ensures both swaps happen
    (x & 0x0F) << 4         # get the low nibble and shift left
  )

print([hex(x) for x in swapped[:12]])   # jpeg signature

# save this image to file
# we'll then run it through a Piet interpreter
with open("piet_file.jpg", "wb") as file:
    file.write(bytes(swapped))