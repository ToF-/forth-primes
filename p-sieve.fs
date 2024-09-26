\ sieve.fs compute primes with a sieve 

: SQUARED ( n -- n² )
    DUP * ;

: MULTIPLE ( n,m -- f )
    MOD 0= ;

: NOR ( f,g -- ! [f or g] )
    OR 0= ;

: REVERSE ( b -- b )
    255 XOR ;

: P, ( w -- )
    DUP 255 AND C,
    8 RSHIFT C, ;

: P@ ( n -- p )
    DUP C@
    SWAP 1+ C@
    8 LSHIFT OR ;

1000 CONSTANT PRIMES-LIMIT
CREATE PRIMES
  2 P,   3 P,   5 P,   7 P,  11 P,  13 P,  17 P,  19 P,  23 P,  29 P,  31 P,  37 P,
 41 P,  43 P,  47 P,  53 P,  59 P,  61 P,  67 P,  71 P,  73 P,  79 P,  83 P,  89 P,
 97 P, 101 P, 103 P, 107 P, 109 P, 113 P, 127 P, 131 P, 137 P, 139 P, 149 P, 151 P,
157 P, 163 P, 167 P, 173 P, 179 P, 181 P, 191 P, 193 P, 197 P, 199 P, 211 P, 223 P,
227 P, 229 P, 233 P, 239 P, 241 P, 251 P, 257 P, 263 P, 269 P, 271 P, 277 P, 281 P,
283 P, 293 P, 307 P, 311 P, 313 P, 317 P, 331 P, 337 P, 347 P, 349 P, 353 P, 359 P,
367 P, 373 P, 379 P, 383 P, 389 P, 397 P, 401 P, 409 P, 419 P, 421 P, 431 P, 433 P,
439 P, 443 P, 449 P, 457 P, 461 P, 463 P, 467 P, 479 P, 487 P, 491 P, 499 P, 503 P,
509 P, 521 P, 523 P, 541 P, 547 P, 557 P, 563 P, 569 P, 571 P, 577 P, 587 P, 593 P,
599 P, 601 P, 607 P, 613 P, 617 P, 619 P, 631 P, 641 P, 643 P, 647 P, 653 P, 659 P,
661 P, 673 P, 677 P, 683 P, 691 P, 701 P, 709 P, 719 P, 727 P, 733 P, 739 P, 743 P,
751 P, 757 P, 761 P, 769 P, 773 P, 787 P, 797 P, 809 P, 811 P, 821 P, 823 P, 827 P,
829 P, 839 P, 853 P, 857 P, 859 P, 863 P, 877 P, 881 P, 883 P, 887 P, 907 P, 911 P,
919 P, 929 P, 937 P, 941 P, 947 P, 953 P, 967 P, 971 P, 977 P, 983 P, 991 P, 997 P,

168 CONSTANT MAX-PRIMES

: NTH-PRIME ( n -- p )
    2* PRIMES + P@ ;

PRIMES-LIMIT SQUARED CONSTANT SIEVE-LIMIT
20 CONSTANT FLAGS/BYTE

SIEVE-LIMIT FLAGS/BYTE / 1+ CONSTANT SIEVE-SIZE

\ stores primality flags for numbers except multiples of 2 and 5
\ a byte at pos n stores flags for n+1, n+3, n+7, n+9, n+11, n+13, n+17, n+19
CREATE SIEVE SIEVE-SIZE ALLOT

: 0,
    0 C, ;

CREATE SIEVE-MASKS

\    1       3             7       9       11       13             17         19
  0, 1 C, 0, 2 C, 0, 0, 0, 4 C, 0, 8 C, 0, 16 C, 0, 32 C, 0, 0, 0, 64 C,  0, 128 C,

\ e.g at SIEVE#0 : 11110110 = numbers 19,17,13,11,7,3 are primes, 9 and 1 are not prime,
\ 0,2,4,5,6,8,10,12,14,15,16,18 flags are not stored
\ at     SIEVE#1 : 01011010 = numbers 37,31,29,23 are primes, 39,23,27 and 21 are not prime

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

: #PRIMES ( n -- m )
    0 SWAP 2 DO I IS-PRIME? IF 1+ THEN LOOP ;

