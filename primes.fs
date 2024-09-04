
: NAIVE-IS-PRIME? ( n -- f )
    0 SWAP
    DUP 1+ 1 DO
        DUP I MOD 0= IF
            SWAP 1+ SWAP
        THEN
    LOOP DROP
    2 = ;

: SQUARED ( n -- n )
    DUP * ;

: OPTIMIZED-NAIVE-IS-PRIME? ( n -- f )
    TRUE >R 2
    BEGIN
        2DUP SQUARED >= R@ AND WHILE
        2DUP MOD 0= IF
            R> DROP FALSE >R
        THEN
        1+
    REPEAT 2DROP R> ;

: IS-PRIME? ( n -- f )
    DUP 2 < IF
        DROP FALSE
    ELSE
       OPTIMIZED-NAIVE-IS-PRIME?
    THEN ;
