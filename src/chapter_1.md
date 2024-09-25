# A simple, inefficient program

To know if a number N is prime, we divide it by numbers from 2 to √N : if none of these numbers divide N, then N is prime.
```
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
```
