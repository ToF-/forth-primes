REQUIRE bitset.fs

: TRIAL-PRIME? ( n -- f )
    TRUE >R                          \ result ← false
    2                                \  n,i
    BEGIN
        2DUP DUP * >=              \ while i ≤ √n and no multiple found
        R@ AND WHILE
        2DUP MOD 0=
        IF R> DROP FALSE >R THEN     \ result ← false
        1+                           \ next
    REPEAT 2DROP R> ;

: IS-PRIME? ( n -- f )
    DUP 2 < IF
        DROP FALSE
    ELSE
       TRIAL-PRIME?
    THEN ;

\ a 4-bit nibble can store prime info for numbers b+1,b+3,b+7,b+9, where b = 10*n
\ for instance with n = 2, the nibble 0101 stores 21 is composite, 23 is prime, 27 is composite, 29 is prime
\ multiples of 2 and 5 and never stored, each nibble stores information for numbers 10*b to 10*b+9
\ thus a byte stores information from numbers 20*b to 20*b+19
\ to store info about primes from 0 to 1000000 takes 500000 bytes

50000 CONSTANT MAX-PTABLE
CREATE PTABLE MAX-PTABLE ALLOT
PTABLE MAX-PTABLE ERASE

: P-OFFSET-BIT ( n -- b )
    DUP 20 MOD 5 / 2* 
    SWAP 10 MOD 3 MOD 0= IF 1 ELSE 0 THEN + ;

\ to find which byte stores info about number n, o = n/20
\ to find which bit stores info about number n at offset o, i = 7-j
\ where j = (n%20)/5 * 2 + ((n%10)%3==0) ? 1 : 0

: P-TABLE-OFFSET ( p -- n,b ) \ p is prime, 5 < p < 1000000
    DUP 20 /
    SWAP P-OFFSET-BIT ;

: P-TABLE-PRIME! ( p -- ) \ p is prime, 5 < p < 1000000
    P-TABLE-OFFSET
    1 SWAP LSHIFT
    SWAP PTABLE +
    DUP C@ ROT OR SWAP C! ;

: P-TABLE-PRIME? ( n -- f )
    DUP 2 MOD 0= IF 2 = ELSE DUP 5 MOD 0= IF 5 =
    ELSE
        P-TABLE-OFFSET
        SWAP PTABLE + C@
        1 ROT LSHIFT AND
    THEN THEN ;

: INIT-PTABLE
    ." initializing ptable.."
    100000 1 DO
        I IS-PRIME? IF
            I P-TABLE-PRIME!
        THEN
    LOOP ;

CR INIT-PTABLE

: .H hex 0 <<# 16 0 DO # LOOP #> TYPE SPACE #>> DECIMAL ;

: .PTABLE
    HEX
    PTABLE  MAX-PTABLE CELLS OVER + SWAP DO I @ DUP IF .H CRELSE DROP THEN CELL +LOOP ;

: TEST-PTABLE
    100000 2 DO
        I P-TABLE-PRIME? IF I . CR THEN
    LOOP ;

\ CR TEST-PTABLE
CR .PTABLE
\ where j = (n%20)/5 * 2 + (n%20)&8/8
\ let h = ((n%20) -1)/ 5* 2       

\ 0,1,2,3,4,5 → 
\ a 64 bits cell can thus store information for 10*b to 10*b+159
\ to retrieve if a number n is prime :
\ if n is even then f = n == 2
\ if n%5 == 0 then f = n == 5
\ cell offset to consider:  i = n / 160
\ nibble to consider in that cell: j = 15 - (n % 160) / 10
\ bit to consider in that nibble: k = (n % 160) % 10 =0 ? 0   … 7 ? 1 … 3 ? 2  … 1 ? 3
\ e.g  n % 160  | j
\            1  | 63
\            3  | 62
\            7  | 61
\            9  | 60
\           11  | 59
\           …   | …
\          157  |  2
\          158  |  1
\          159  |  0

\ to find if n is prime:
\ if n is even or multiple of 5  
BYE
