\ print-init-168-primes.fs output code for prime divisor table init
REQUIRE trial.fs

: .PRIME-TABLE
    0 1000 2 DO
        I IS-PRIME? IF
            I 4 .R ."  ,"
            1+
            DUP 14 MOD 0= IF
                CR
            THEN
        THEN
    LOOP DROP CR ;

.PRIME-TABLE
BYE
