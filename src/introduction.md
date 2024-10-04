# Introduction

In this series of posts, we explore how to efficiently find prime numbers using _gforth_.

In Forth, programs explicitely make use of an array of _cells_ structured as a Last In First Out container called the _parameter stack_. In _gforth_ each element of this array is a 64 bit integer.

Forth use _words_ to manipulate the stack and perform arithmetic and other kinds of operations. Words are read from the standard input, or from a _gforth_ script file, are separated by space, and directly executed.

Here are some of these words :

- `DUP ( a -- a,a )` : duplicates the value at the top of the stack
- `OVER ( a,b -- a,b,a )` : copy the 2d value over the top
- `SWAP ( a,b -- b,a )` : echange the top value with the 2d one
- `ROT ( a,b,c -- b,c,a )` : rotate the 3d value to the top
- `2DUP ( a,b -- a,b,a,b )` : equivalent of `OVER OVER`
- `NIP` ( a,b -- b )` : remove the 2d value from the stack
- `DROP ( a,b -- a )` : remove the first value from the stack
- `+ ( a,b -- a+b )` : adds the top and 2d values on the stack, leaving the sum
- `- ( a,b -- a-b )` : subtract the value at the top from the 2d, leaving the difference
- `* ( a,b -- a*b )` : multiply the values at the top, leaving the product
- `/ ( a,b -- ⌊a/b⌋` : divide the 2d value by the 1st, leaving the (integer) quotient
- `MOD ( a,b -- a mod b )` : leaves the modulo b of a
- `. ( a -- )` : display the top value, removing it from the stack
- `( xyz )` : start a comment that will end when a `)` is met; (the space after `(` is required)
- ." xyz"` : print the following sequence of chars, until " is met

Some special words are use to create *new* words :
- `VARIABLE xyz` : create a variable called xyz. When executed, xyz will leave its address on the stack
- `CONSTANT xyz ( n -- ) ` : create a constant named xyz with value n. When executed, xyz will leave the value on the stack
- `: xyz` : start the compilation of a definition for a new word xyz
- `;` : end the compilation of a new word

Some words structure the control flow and are used only in definitions :
- `IF ( c -- )` : if the value on the stack is true (different from 0), continue with the following words; if it's not jump after the `ELSE` marker, or after the `THEN` marker if there is no `ELSE` in the flow
- `ELSE` : marks the beginning of the else words to jump to when evaluating a `IF`
- `THEN` : marks the end of a conditionnal structure , where to jump when evaluating false on a `IF … THEN` structure or after the IF part of an `IF … ELSE … THEN` structure.
- `BEGIN` : marks the beginning of a loop
- `REPEAT` : marks the end of the loop, and execution back to the word that follows the `BEGIN` marker
- `WHILE ( c -- )` : if the value on the cell is true (different from 0), execution exits the loop, to the first word that follows the `REPEAT` marker
- `DO ( b,a -- )` : marks the beginning of a loop that will iterate from a to b-1 included
- `LOOP` : marks the end of the loop, and execution back to the word that follows the `DO` marker
- `I: ( -- n ) ` : inside a `DO … LOOP` , push the index value of the loop on the stack

Some words manipulate a memory zone called the _dictionary_, which is where words definitions are stored, as well as data values.
- `CREATE xyz ` : create a new zone called xyz in the available space in the dictionary. When xyz is executed, it will leave the address of the memory zone
- `ALLOT ( n -- )` : reserve n bytes in the available space in the dictionary
- `, ( n -- )` : compile the cell value in the available space in the dictionary
- `@ ( addr -- n )` : fetch the cell value stored at the given address
- `! ( n,addr --)` : store the cell value n at the given address 
