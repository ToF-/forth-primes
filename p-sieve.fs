\ p-sieve.fs compuiting primes with improved sieve 

REQUIRE small-primes.fs

: SQUARED ( n -- n² )
    DUP * ;

: IS-MULTIPLE? ( n,m -- f )
    MOD 0= ;

: NOT-2-OR-5-MULTIPLE? ( n -- f )
    DUP 2 IS-MULTIPLE?
    SWAP 5 IS-MULTIPLE? OR 0=  ;

1000 SQUARED CONSTANT PRIME-LIMIT

PRIME-LIMIT 20 / CONSTANT SIEVE-SIZE

CREATE SIEVE SIEVE-SIZE ALLOT

: NOR ( f,g -- ! [f or g] )
    OR 0= ;

: REVERSE ( b -- b )
    255 XOR ;

160 CONSTANT FLAGS/CELL

: BITMASK ( n -- v )
    1 SWAP LSHIFT ;

: INIT-SIEVE-MASKS
    0
    160 0 DO
        I NOT-2-OR-5-MULTIPLE? IF
            DUP BITMASK ,
            1+
        ELSE
            0 ,
        THEN
    LOOP DROP ;

CREATE SIEVE-MASKS
INIT-SIEVE-MASKS

: SIEVE-POS ( n -- addr )
    FLAGS/CELL / CELLS SIEVE + ;

: SIEVE-MASK ( n -- bitmask )
    FLAGS/CELL MOD CELLS SIEVE-MASKS + @ ;

: SIEVE! ( n -- )
    DUP NOT-2-OR-5-MULTIPLE? IF
        DUP SIEVE-MASK INVERT
        SWAP SIEVE-POS
        DUP @ ROT AND SWAP !
    ELSE DROP THEN ;

: INIT-SIEVE
    SIEVE SIEVE-SIZE 255 FILL
    1 SIEVE!
    SMALL-PRIMES-MAX 1 DO
        I NTH-PRIME DUP
        DUP SQUARED
        BEGIN
            DUP PRIME-LIMIT <= WHILE
            DUP SIEVE!
            OVER +
        REPEAT
        2DROP DROP
    LOOP ;

INIT-SIEVE

: IS-PRIME? ( n -- f )
    ASSERT( DUP PRIME-LIMIT <= )
    ASSERT( DUP 1 > )
    DUP 2 IS-MULTIPLE? IF 2 =
    ELSE
        DUP 5 IS-MULTIPLE? IF 5 =
    ELSE
        DUP SIEVE-MASK SWAP
        SIEVE-POS @ SWAP AND
    THEN THEN ;

CREATE #BIT-NUMBERS 1 C, 3 C, 7 C, 9 C, 11 C, 13 C, 17 C, 19 C,

\ : PRIME-POS>NUMBER ( n -- p )
\     8 /MOD FLAGS/BYTE *
\     SWAP #BIT-NUMBERS + C@ + ;
\ 
\ : NEXT-PRIME-POS ( n -- n' )
\     BEGIN
\         1+ DUP 8 /MOD
\         SIEVE + C@
\         1 ROT LSHIFT AND
\     UNTIL ;
\ 
\ -2 CONSTANT PRIME-POS-2
\ -1 CONSTANT PRIME-POS-3
\  0 CONSTANT PRIME-POS-5
\ 
\ : NEXT-PRIME ( c -- c',p)
\          DUP PRIME-POS-2 = IF 1+ 2
\     ELSE DUP PRIME-POS-3 = IF 1+ 3
\     ELSE DUP PRIME-POS-5 = IF 1+ 5
\     ELSE
\         NEXT-PRIME-POS
\         DUP PRIME-POS>NUMBER
\     THEN THEN THEN ;
\ 
\ : .ALL-PRIMES ( n -- )
\     >R PRIME-POS-2
\     BEGIN
\         NEXT-PRIME
\         DUP R@ < WHILE
\         . CR
\     REPEAT
\     DROP R> DROP ;

: .PRIMES ( n -- )
    1+ 2 DO I IS-PRIME? IF I . CR THEN LOOP ;

: PRIME-COUNT ( n -- n )
    0 SWAP
    1+ 2 DO
        I IS-PRIME? IF
            1+
        THEN
    LOOP ;
