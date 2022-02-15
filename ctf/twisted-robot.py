#
#  Challenge Download:
#  https://storage.googleapis.com/gctf-2021-attachments-project/8d19115532225f6ab25ed208e355b37d55476dfc2c1996cbe81f6e82c96f79a20756d5d53fac7f90bc7841aedab34d0686335bafcdbe2cf07333163719ecff9b
#

# python's `random.getrandbits()` uses the Marsesene Twister algorithm which can be predicted with
# a sufficient pool of consecutive outputs (624)
# I'll make use of an existing package to power the preditions:
# from https://github.com/kmyk/mersenne-twister-predictor
#
# I'm reusing the main .py code from the challenge, just implementing the decryption

import random
import sys
from mt19937predictor import MT19937Predictor


def formatNumber(n):
    n = str(n)
    return f'{n[:3]}-{n[3:6]}-{n[6:]}'

# This generates random phone numbers because it's easy to find a lot of people!
# Our number generator is not great so we had to hack it a bit to make sure we can
# reach folks in Philly (area code 215)


def generateRandomNumbers():
    arr = []
    for i in range(624):
        arr.append(formatNumber(random.getrandbits(32) + (1 << 31)))
    return arr


def encodeSecret(s):
    key = [random.getrandbits(8) for i in range(len(s))]
    return bytes([a ^ b for a, b in zip(key, list(s.encode()))])


# solution
def decodeSecret():
    predictor = MT19937Predictor()
    with open('robo_numbers_list.txt') as file:
        while line := file.readline():
            # we need to reverse the operations of `generateRandomNumbers()`
            predictor.setrandbits(
                # strip the formating from `formatNumber()` and convert the string to an int
                (int(line.rstrip().replace('-', '')))
                # subtract 1 << 31 (equivalent to 2**31 or 2147483648)
                - (1 << 31), 32)

    # then we need to reverse `encodeSecret()`
    with open('secret.enc', 'rb') as file:
        secret = []
        key = []
        while (byte := file.read(1)):
            secret.append(int.from_bytes(byte, sys.byteorder))
        key = [predictor.getrandbits(8) for i in range(len(secret))]
        print('secret: {}\nkey: {}\n'.format(secret, key))
        return ''.join(
            map(
                lambda x: chr(x),
                [a ^ b for a, b in zip(secret, key)]))


def menu():
    print("""\n\nWelcome to the RoboCaller!! What would you like to do?
1: generate a new list of numbers
2: encrypt a super secret (in beta)cd
3: decrypt a super secret (coming soon!!)
4: exit""")
    choice = ''
    while choice not in ['1', '2', '3']:
        choice = input('>')
        if choice == '1':
            open('robo_numbers_list.txt', 'w').write(
                '\n'.join(generateRandomNumbers()))
            print("...done! list saved under 'robo_numbers_list.txt'")
        elif choice == '2':
            secret = input(
                'give me your secret and I\'ll save it as "secret.enc"')
            open('secret.enc', 'wb').write(encodeSecret(secret))
        elif choice == '3':
            print(decodeSecret())
            print("\n\n#boom\n")
            sys.exit()
        elif choice == '4':
            print("Thank you for using RoboCaller1337!")
            sys.exit()
    return


def main():
    while True:
        menu()


if __name__ == "__main__":
    main()
