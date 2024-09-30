\ sieve.fs compute primes with the sieve of Eratosthenes algorithm

REQUIRE small-primes.fs

: SQUARED ( n -- n² )
    DUP * ;

1000 SQUARED CONSTANT PRIME-LIMIT

PRIME-LIMIT 8 / CONSTANT SIEVE-SIZE

CREATE SIEVE SIEVE-SIZE ALLOT

: SIEVE-POS ( n -- addr )
    64 / CELLS SIEVE + ;

: SIEVE-BITMASK ( n -- bitmask )
    64 MOD 1 SWAP LSHIFT ;

: SIEVE! ( n -- )
    ASSERT( DUP PRIME-LIMIT <= )
    DUP SIEVE-BITMASK INVERT
    SWAP SIEVE-POS DUP @
    ROT AND SWAP ! ;


: INIT-SIEVE
    SIEVE SIEVE-SIZE 255 FILL
    0 SIEVE! 1 SIEVE!
    SMALL-PRIMES-MAX 0 DO
        I NTH-PRIME
        DUP DUP SQUARED
        BEGIN
            DUP PRIME-LIMIT <= WHILE
            DUP SIEVE!
            OVER +
        REPEAT
        2DROP DROP
    LOOP ;

: IS-PRIME? ( n -- f )
    ASSERT( DUP PRIME-LIMIT <= )
    DUP SIEVE-BITMASK
    SWAP SIEVE-POS @ AND ;

INIT-SIEVE

: .PRIMES ( n -- )
    1+ 2 DO I IS-PRIME? IF I . CR THEN LOOP ;

: PRIME-COUNT ( n -- t )
    0 SWAP
    1+ 2 DO
        I IS-PRIME? IF
            1+
        THEN
    LOOP ;
