\ p-sieve.fs compute primes with a sieve

: SQUARED ( n -- n² )
    DUP * ;

: MULTIPLE ( n,m -- f )
    MOD 0= ;

: NOR ( f,g -- ! [f or g] )
    OR 0= ;

: REVERSE ( b -- b )
    255 XOR ;

1000 CONSTANT PRIMES-LIMIT
CREATE PRIMES
  2 ,   3 ,   5 ,   7 ,  11 ,  13 ,  17 ,  19 ,  23 ,  29 ,  31 ,  37 ,
 41 ,  43 ,  47 ,  53 ,  59 ,  61 ,  67 ,  71 ,  73 ,  79 ,  83 ,  89 ,
 97 , 101 , 103 , 107 , 109 , 113 , 127 , 131 , 137 , 139 , 149 , 151 ,
157 , 163 , 167 , 173 , 179 , 181 , 191 , 193 , 197 , 199 , 211 , 223 ,
227 , 229 , 233 , 239 , 241 , 251 , 257 , 263 , 269 , 271 , 277 , 281 ,
283 , 293 , 307 , 311 , 313 , 317 , 331 , 337 , 347 , 349 , 353 , 359 ,
367 , 373 , 379 , 383 , 389 , 397 , 401 , 409 , 419 , 421 , 431 , 433 ,
439 , 443 , 449 , 457 , 461 , 463 , 467 , 479 , 487 , 491 , 499 , 503 ,
509 , 521 , 523 , 541 , 547 , 557 , 563 , 569 , 571 , 577 , 587 , 593 ,
599 , 601 , 607 , 613 , 617 , 619 , 631 , 641 , 643 , 647 , 653 , 659 ,
661 , 673 , 677 , 683 , 691 , 701 , 709 , 719 , 727 , 733 , 739 , 743 ,
751 , 757 , 761 , 769 , 773 , 787 , 797 , 809 , 811 , 821 , 823 , 827 ,
829 , 839 , 853 , 857 , 859 , 863 , 877 , 881 , 883 , 887 , 907 , 911 ,
919 , 929 , 937 , 941 , 947 , 953 , 967 , 971 , 977 , 983 , 991 , 997 ,

168 CONSTANT MAX-PRIMES

: NTH-PRIME ( n -- p )
    CELLS PRIMES + @ ;

PRIMES-LIMIT SQUARED CONSTANT SIEVE-LIMIT
20 CONSTANT FLAGS/BYTE

SIEVE-LIMIT FLAGS/BYTE / CONSTANT SIEVE-SIZE

CREATE SIEVE SIEVE-SIZE ALLOT

: ,,
    0 C, ;

CREATE SIEVE-MASKS

\    1       3              7       9
  ,, 1 C, ,, 2 C, ,, ,, ,,  4 C, ,, 8 C,

\   11       13             17         19
,, 16 C, ,, 32 C, ,, ,, ,, 64 C,  ,, 128 C,

: SIEVE-POS ( n -- addr )
    FLAGS/BYTE / SIEVE + ;

: SIEVE-MASK ( n -- bitmask )
    FLAGS/BYTE MOD SIEVE-MASKS + C@ ;

: SIEVE! ( n -- )
    DUP 2 MULTIPLE OVER 5 MULTIPLE NOR IF
        DUP SIEVE-MASK REVERSE
        SWAP SIEVE-POS
        DUP C@ ROT AND SWAP C!
    ELSE DROP THEN ;

: INIT-SIEVE
    SIEVE SIEVE-SIZE 255 FILL
    1 SIEVE!
    MAX-PRIMES 1 DO
        I NTH-PRIME DUP
        DUP SQUARED
        BEGIN
            DUP SIEVE-LIMIT < WHILE
            DUP SIEVE!
            OVER +
        REPEAT
        2DROP DROP
    LOOP ;

INIT-SIEVE

: IS-PRIME? ( n -- f )
    ASSERT( DUP SIEVE-LIMIT < )
    ASSERT( DUP 1 > )
    DUP 2 MULTIPLE IF 2 =
    ELSE
        DUP 5 MULTIPLE IF 5 =
    ELSE
        DUP SIEVE-MASK SWAP
        SIEVE-POS C@ SWAP AND
    THEN THEN ;

: .PRIMES ( m,n -- )
    SWAP DO I IS-PRIME? IF I . CR THEN LOOP ;

