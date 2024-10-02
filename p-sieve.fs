\ p-sieve.fs compuiting primes with improved sieve 

REQUIRE small-primes.fs

: SQUARED ( n -- n² )
    DUP * ;

: IS-MULTIPLE? ( n,m -- f )
    MOD 0= ;

: NOT-2-OR-5-MULTIPLE? ( n -- f )
    DUP 2 MOD IF
        5 MOD
    ELSE
        DROP FALSE
    THEN ;

: REVERSE ( b -- b )
    255 XOR ;

: BITMASK ( n -- v )
    1 SWAP LSHIFT ;

1000 SQUARED CONSTANT PRIME-LIMIT
160 CONSTANT FLAGS/CELL

PRIME-LIMIT FLAGS/CELL / CELLS CONSTANT SIEVE-SIZE

CREATE SIEVE SIEVE-SIZE ALLOT

: SIEVE-POS ( n -- addr )
    FLAGS/CELL / CELLS SIEVE + ;

: SIEVE-MASK ( n -- bitmask )
    FLAGS/CELL MOD
    10 /MOD 4 *
    SWAP DUP 5 > IF 2 - THEN
    2/ + BITMASK ;

: SIEVE! ( n -- )
    DUP SIEVE-MASK INVERT
    SWAP SIEVE-POS
    DUP @ ROT AND SWAP ! ;

: INIT-SIEVE
    SIEVE SIEVE-SIZE 255 FILL
    1 SIEVE!
    SMALL-PRIMES-MAX 1 DO
        I NTH-PRIME DUP
        DUP SQUARED
        BEGIN
            DUP PRIME-LIMIT <= WHILE
            DUP NOT-2-OR-5-MULTIPLE? IF
                DUP SIEVE!
            THEN
            OVER +
        REPEAT
        2DROP DROP
    LOOP ;

INIT-SIEVE

: BIT#>UNITS ( b -- n )
    4 MOD
    DUP 2 AND
    SWAP 2*
    1 + + ;

: BIT#>TENS ( b -- n )
    4 / 10 * ;

: BIT#>NUMBER ( b -- n )
    64 /MOD
    FLAGS/CELL *
    SWAP DUP BIT#>TENS
    SWAP BIT#>UNITS
    + + ;

: BIT#>IS-PRIME? ( b -- f )
    64 /MOD
    CELLS SIEVE + @
    SWAP BITMASK AND ;


: NEXT-SMALL-PRIME ( b -- b',p )
    DUP 0 = IF 1+ 2 ELSE
    DUP 1  = IF 1+ 3 ELSE
    DUP 2  = IF 1+ 5 ELSE
    1+ 7 THEN THEN THEN ;

: NEXT-PRIME ( b -- b',p )
    DUP 4 < IF
        NEXT-SMALL-PRIME
    ELSE
        BEGIN
            DUP BIT#>NUMBER
            DUP PRIME-LIMIT <=
            ROT DUP BIT#>IS-PRIME? 0=
            ROT AND
        WHILE
            NIP 1+
        REPEAT
        1+ SWAP
    THEN ;

: .PRIMES ( n -- )
    >R 0
    BEGIN
        NEXT-PRIME
        DUP R@ <= WHILE
        . CR
    REPEAT R> DROP ;

: PRIME-COUNT ( n -- n )
    >R 0 0
    BEGIN
        NEXT-PRIME
        R@ <= WHILE
        SWAP 1+ SWAP
    REPEAT R> DROP DROP ;
    
