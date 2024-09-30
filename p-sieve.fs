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


: BIT#>NUMBER ( b -- n )
    DUP 4 / 10 *
    SWAP 4 MOD
    DUP 2* 1+
    SWAP 2/ 2*
    + + ;


: SIEVE-NUMBER ( c -- n )
    DUP 64 /MOD
    FLAGS/CELL *
    SWAP BIT#>NUMBER + ;

: BIT-VALUE-OFFSET ( n -- o )
    4 MOD
    2 = IF
        2
    ELSE
        0
    THEN 2 + ;

: NEXT-SIEVE-VALUE ( n,v -- n',v' )
    2DUP 2 3 D= IF 2DROP 2 5 ELSE
    2DUP 2 5 D= IF 2DROP 3 7 ELSE
    OVER BIT-VALUE-OFFSET + SWAP 1+ SWAP 
    THEN THEN ;

: NEXT-PRIME ( n,p -- n',p' )
    BEGIN
        NEXT-SIEVE-VALUE           \ n,p
        DUP PRIME-LIMIT <= >R      \ n,p
        OVER FLAGS/CELL MOD 1 SWAP LSHIFT
        OVER SIEVE-POS
        @ SWAP AND 0=
        R> AND WHILE
    REPEAT ;

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
