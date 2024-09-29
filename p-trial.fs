\ p-trial.fs   compute primes by trial division algorithm, using prime divisors

REQUIRE small-primes.fs

: SQUARED ( n -- n² )
    DUP * ;

: IS-MULTIPLE? ( m,n -- f )
    MOD 0= ;

1000 SQUARED CONSTANT PRIME-LIMIT

: IS-PRIME? ( n -- f )
    ASSERT( DUP PRIME-LIMIT <= )
    TRUE SWAP
    SMALL-PRIMES-MAX 0 DO
        I NTH-PRIME
        2DUP SQUARED < IF
            DROP LEAVE
        ELSE
            OVER SWAP IS-MULTIPLE? IF
                NIP FALSE SWAP
                LEAVE
            THEN
        THEN
    LOOP DROP ;

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
