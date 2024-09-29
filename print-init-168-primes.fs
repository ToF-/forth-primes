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
    LOOP CR . ;

." \ small-primes.fs" CR CR
." CREATE SMALL-PRIMES" CR CR
.PRIME-TABLE
." CONSTANT SMALL-PRIMES-MAX" CR CR
." : NTH-PRIME ( n -- p )" CR
."     CELLS SMALL-PRIMES + @ ;" CR
BYE
