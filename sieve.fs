\ sieve.fs compute primes with the sieve of Eratosthenes algorithm

: SQUARED ( n -- n² )
    DUP * ;

: IS-MULTIPLE? ( m,n -- f )
    MOD 0= ;

: REVERSE ( bits -- bits )
    255 XOR ;

CREATE PRIMES
  2 C,   3 C,   5 C,   7 C,  11 C,  13 C,  17 C,  19 C,  23 C,
 29 C,  31 C,  37 C,  41 C,  43 C,  47 C,  53 C,  59 C,  61 C,
 67 C,  71 C,  73 C,  79 C,  83 C,  89 C,  97 C, 101 C, 103 C,
107 C, 109 C, 113 C, 127 C, 131 C, 137 C, 139 C, 149 C, 151 C,
157 C, 163 C, 167 C, 173 C, 179 C, 181 C, 191 C, 193 C, 197 C,
199 C, 211 C, 223 C, 227 C, 229 C, 233 C, 239 C, 241 C, 251 C,

54 CONSTANT MAX-PRIMES

: NTH-PRIME ( n -- p )
    PRIMES + C@ ;

256 CONSTANT PRIME-LIMIT

PRIME-LIMIT SQUARED CONSTANT SIEVE-LIMIT
SIEVE-LIMIT 8 / CONSTANT SIEVE-SIZE

CREATE SIEVE SIEVE-SIZE ALLOT

: SIEVE-POS ( n -- addr )
    8 / SIEVE + ;

: SIEVE-BITMASK ( n -- bitmask )
    8 MOD 1 SWAP LSHIFT ;

: SIEVE! ( n -- )
    ASSERT( DUP SIEVE-LIMIT < )
    DUP SIEVE-BITMASK REVERSE
    SWAP SIEVE-POS
    DUP C@ ROT AND
    SWAP C! ;


: INIT-SIEVE
    SIEVE SIEVE-SIZE 255 FILL
    1 SIEVE!
    MAX-PRIMES 0 DO
        I NTH-PRIME
        DUP DUP SQUARED
        BEGIN
            DUP SIEVE-LIMIT < WHILE
            DUP SIEVE!
            OVER +
        REPEAT
        2DROP DROP
    LOOP ;

: IS-PRIME? ( n -- f )
    ASSERT( DUP SIEVE-LIMIT < )
    DUP SIEVE-BITMASK
    SWAP SIEVE-POS C@ AND ;

INIT-SIEVE

: .PRIMES ( n -- )
    2 DO I IS-PRIME? IF I . CR THEN LOOP ;
