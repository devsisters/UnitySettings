#!/usr/local/bin/python3
import sys

if __name__ == '__main__':
    filename = sys.argv[1]
    text = ''
    i = 0
    with open(filename, 'rb') as f:
        for b in f.read():
            text += str(b) + ','
            i += 1
            if i % 16 == 0: text += '\n'
    print(text)
