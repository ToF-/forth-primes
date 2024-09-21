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

