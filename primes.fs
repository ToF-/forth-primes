1024 CONSTANT SMALL-PRIMES-LIMIT
CREATE SMALL-PRIMES
HERE
2 , 3 , 5 , 7 , 11 , 13 , 17 , 19 , 23 , 29 , 31 , 37 , 41 , 43 , 47 , 53 , 59 , 61 , 67 ,
71 , 73 , 79 , 83 , 89 , 97 , 101 , 103 , 107 , 109 , 113 , 127 , 131 , 137 , 139 , 149 ,
151 , 157 , 163 , 167 , 173 , 179 , 181 , 191 , 193 , 197 , 199 , 211 , 223 , 227 , 229 ,
233 , 239 , 241 , 251 , 257 , 263 , 269 , 271 , 277 , 281 , 283 , 293 , 307 , 311 , 313 ,
317 , 331 , 337 , 347 , 349 , 353 , 359 , 367 , 373 , 379 , 383 , 389 , 397 , 401 , 409 ,
419 , 421 , 431 , 433 , 439 , 443 , 449 , 457 , 461 , 463 , 467 , 479 , 487 , 491 , 499 ,
503 , 509 , 521 , 523 , 541 , 547 , 557 , 563 , 569 , 571 , 577 , 587 , 593 , 599 , 601 ,
607 , 613 , 617 , 619 , 631 , 641 , 643 , 647 , 653 , 659 , 661 , 673 , 677 , 683 , 691 ,
701 , 709 , 719 , 727 , 733 , 739 , 743 , 751 , 757 , 761 , 769 , 773 , 787 , 797 , 809 ,
811 , 821 , 823 , 827 , 829 , 839 , 853 , 857 , 859 , 863 , 877 , 881 , 883 , 887 , 907 ,
911 , 919 , 929 , 937 , 941 , 947 , 953 , 967 , 971 , 977 , 983 , 991 , 997 , 1009 , 1013 ,
1019 , 1021 ,
HERE SWAP - CELL / CONSTANT MAX-SMALL-PRIMES

SMALL-PRIMES-LIMIT DUP * CONSTANT MEDIUM-PRIMES-LIMIT
20 CONSTANT P-INFO-PER-BYTE
MEDIUM-PRIMES-LIMIT P-INFO-PER-BYTE / 1+ CONSTANT MEDIUM-PRIMES-TABLE-SIZE

CREATE MEDIUM-PRIMES-TABLE MEDIUM-PRIMES-TABLE-SIZE ALLOT

: MEDIUM-PRIMES-ADDRESS ( n -- addr )
    20 / MEDIUM-PRIMES-TABLE + ;

: MEDIUM-PRIMES-BIT# ( n -- bit# )
    DUP 20 MOD 5 / 2*
    SWAP 10 MOD 3 MOD 0= NEGATE + ;

: MEDIUM-PRIMES-MOD-20 ( bit# -- n )
    DUP 4 / 10 *
    OVER 4 MOD 2/ 6 * 1+ +
    SWAP 2 MOD 2* + ;

: BIT-MASK ( bit# -- c )
    1 SWAP LSHIFT ;

: MARK-AS-COMPOSITE ( n -- )
    DUP 2 MOD 0=
    OVER 5 MOD 0= OR IF
        DROP
    ELSE
        DUP MEDIUM-PRIMES-BIT# BIT-MASK 255 XOR SWAP
        MEDIUM-PRIMES-ADDRESS DUP C@ ROT AND SWAP C!
    THEN ;

: INIT-MEDIUM-PRIMES-TABLE
    MEDIUM-PRIMES-TABLE MEDIUM-PRIMES-TABLE-SIZE 255 FILL
    1 MARK-AS-COMPOSITE
    MAX-SMALL-PRIMES 1 DO
        SMALL-PRIMES I CELLS + @ DUP
        BEGIN
            OVER +
            DUP MEDIUM-PRIMES-LIMIT < WHILE
            DUP MARK-AS-COMPOSITE
        REPEAT 2DROP
    LOOP ;

: IS-MEDIUM-PRIME? ( n -- f )
    ASSERT( DUP MEDIUM-PRIMES-LIMIT < )
    ASSERT( DUP 1 > )
    DUP 2 MOD 0= IF 2 = ELSE DUP 5 MOD 0= IF 5 = ELSE
        DUP MEDIUM-PRIMES-BIT# BIT-MASK SWAP
        MEDIUM-PRIMES-ADDRESS C@ SWAP AND
    THEN THEN ;

INIT-MEDIUM-PRIMES-TABLE

: TEST-MEDIUM-PRIMES
    MEDIUM-PRIMES-LIMIT 2 DO
        I IS-MEDIUM-PRIME? IF I . CR THEN
    LOOP ;

: NEXT-PRIME-POINTER ( addr -- addr )
    BEGIN
        1+ DUP 8 /MOD
        SWAP BIT-MASK
        SWAP C@ AND
    UNTIL ;

: PRIME-POINTER-TO-PRIME ( addr -- n )
    8 /MOD
    SWAP MEDIUM-PRIMES-MOD-20 
    SWAP MEDIUM-PRIMES-TABLE - 20 * + ;

: TEST-PRIMES
    1 . 2 . CR
    2 . 3 . CR
    3 . 5 . CR
    MEDIUM-PRIMES-TABLE 8 *
    NEXT-PRIME-POINTER
    78497 3 DO
        I 1+ . 
        NEXT-PRIME-POINTER
        DUP PRIME-POINTER-TO-PRIME . CR
    LOOP DROP ;

TEST-PRIMES BYE
