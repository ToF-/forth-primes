: NAIVE-IS-PRIME? ( n -- f )
    0 SWAP
    DUP 1+ 1 DO
        DUP I MOD 0= IF
            SWAP 1+ SWAP
        THEN
    LOOP DROP
    2 = ;
        
: IS-PRIME? ( n -- f )
    DUP 2 < IF
        DROP FALSE
    ELSE
        NAIVE-IS-PRIME?
    THEN ;
