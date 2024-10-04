\ trial.fs   compute primes by trial division algorithm

: SQUARED ( n -- n² )
    DUP * ;

: IS-MULTIPLE? ( m,n -- f )
    MOD 0= ;

: IS-PRIME? ( n -- n|0 )
    2 BEGIN
        2DUP SQUARED >= WHILE
        2DUP IS-MULTIPLE? IF NIP 0 SWAP THEN
        1+
    REPEAT
    DROP ;

: .PRIMES ( n -- )
    1+ 2 DO
        I IS-PRIME? IF I . CR THEN
   LOOP ;

: PRIME-COUNT ( n -- t )
    0 SWAP
    1+ 2 DO
        I IS-PRIME? IF 1+ THEN
    LOOP ;
