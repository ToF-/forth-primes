
: IS-PRIME? ( n -- f )
    TRUE SWAP
    2 BEGIN
        2DUP DUP * >= WHILE
            2DUP MOD 0= IF
                ROT DROP FALSE -ROT
            THEN
        1+
    REPEAT
    2DROP ;

: TEST
    1000000 2 DO I DUP IS-PRIME? IF . CR ELSE DROP THEN LOOP ;

TEST BYE
