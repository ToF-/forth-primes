# Introduction

In this series of posts, we explore how to efficiently find prime numbers using _gforth_.

In Forth, programs explicitely make use of an array of _cells_ structured as a Last In First Out container called the _parameter stack_. In _gforth_ each element of this array is a 64 bit integer.

Forth use _words_ to manipulate the stack and perform arithmetic and other kinds of operations. Words are read from the standard input, or from a _gforth_ script file, are separated by space, and directly executed.

Here are some of these words :

| forth word | effect on the stack | behavior |
| ---------- | ------------------- | -------- |
| `DUP` | a → a,a  | duplicates the value at the top of the stack |
| `OVER` | a,b → a,b,a  | copy the 2d value over the top |
| `SWAP` | a,b → b,a  | echange the top value with the 2d one |
| `ROT` | a,b,c → b,c,a  | rotate the 3d value to the top |
| `2DUP` | a,b → a,b,a,b  | equivalent of `OVER OVER` |
| `DROP` | a,b → a  | remove the first value from the stack |
| `NIP` | a,b → b  | remove the 2d value from the stack |
| `+` | a,b → a+b  | addition |
| `-` | a,b → a-b  | subtraction |
| `*` | a,b → a*b  | multiplication |
| `/` | a,b → ⌊a/b⌋ | integer division |
| `MOD` | a,b → a mod b  | modulo|
| `.` | a → _ | display the top value, removing it from the stack |
| `.R` | n,p → _ | display _\<n>_ on _\<p\>_ positions (left padding) |
| `CR` | _ | outputs a CRLF on the standard output |
| `." …"` |  | display characters until " |

More interesting words will reveal useful as we go forward in our programming task.
