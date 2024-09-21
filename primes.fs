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
\ multiples of 2 and 5 and never stored, each nibble stores informations for numbers 10*b to 10*b+9
\ a 64 bits cell can thus store information for 10*b to 10*b+159
\ to retrieve if a number n is prime :
\ if n is even then f = n == 2
\ if n%5 == 0 then f = n == 5
\ cell offset to consider:  i = n / 160
\ nibble to consider in that cell: j = 15 - (n % 160) / 10
\ bit to consider in that nibble: k = (n % 160) % 9=0 ? 0   … 7 ? 1 … 3 ? 2  … 1 ? 3
\ e.g  n % 160  | j
\            1  | 63
\            3  | 62
\            7  | 61
\            9  | 60
\           11  | 59
\           13  | 61
\           17  | 63
\           19  | 62

\ if n is even or multiple of 5  
: PRIMES, ( n -- )
    2 DO I IS-PRIME? IF I , THEN LOOP ;

10000 CONSTANT MAX-PRIMES
CREATE PRIMES
HERE
MAX-PRIMES PRIMES,
HERE SWAP - CELL / CONSTANT #PRIMES

: OFFSET-P ( a, p -- -a%p )
    SWAP NEGATE SWAP MOD ;



: TEST #PRIMES 0 DO PRIMES I CELLS + @ . LOOP ;

TEST
BYE

\ swap negate swap mod

\ initialize a table of primes from 2 to √t
\ then use it to create a table of primes from 2 to t
\ then use 

