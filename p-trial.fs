\ p-trial.fs   compute primes by trial division algorithm, using prime divisors

REQUIRE small-primes.fs

: SQUARED ( n -- n² )
    DUP * ;

: IS-MULTIPLE? ( m,n -- f )
    MOD 0= ;

1000 SQUARED CONSTANT PRIME-LIMIT

: IS-PRIME? ( n -- f )
    DUP PRIME-LIMIT > IF ABORT" number too large" THEN
    SMALL-PRIMES-MAX 0 DO
        I NTH-PRIME
        2DUP SQUARED < IF
            DROP LEAVE
        ELSE OVER -ROT IS-MULTIPLE? IF
            DROP FALSE LEAVE
        THEN THEN
    LOOP ;

: .PRIMES ( n -- )
    1+ 2 DO
        I IS-PRIME? IF
            I . CR
        THEN
   LOOP ;

: PRIME-COUNT ( n -- t )
    0 SWAP
    1+ 2 DO
        I IS-PRIME? IF
            1+
        THEN
    LOOP ;
