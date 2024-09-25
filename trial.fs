\ trial.fs   compute primes by trial division algorithm
: SQUARED ( n -- n² )
    DUP * ;

: IS-MULTIPLE? ( m,n -- f )
    MOD 0= ;

: IS-PRIME? ( n -- f )
    TRUE SWAP
    2 BEGIN
        2DUP SQUARED >= WHILE
        2DUP IS-MULTIPLE? IF
            ROT DROP FALSE -ROT
            SWAP
        THEN
        1+
    REPEAT
    2DROP ;

: TEST
    1000000 2 DO I DUP IS-PRIME? IF . CR ELSE DROP THEN LOOP ;

TEST BYE
