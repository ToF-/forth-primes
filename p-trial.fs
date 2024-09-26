\ p-trial.fs   compute primes by trial division algorithm, using prime divisors

: SQUARED ( n -- n² )
    DUP * ;

: IS-MULTIPLE? ( m,n -- f )
    MOD 0= ;

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

: IS-PRIME? ( n -- f )
    ASSERT( DUP 65536 < )
    TRUE SWAP
    MAX-PRIMES 0 DO
        I NTH-PRIME
        2DUP SQUARED < IF
            DROP LEAVE
        ELSE OVER SWAP IS-MULTIPLE? IF
            NIP FALSE SWAP LEAVE
        THEN THEN
    LOOP DROP ;

: .PRIMES ( n -- )
    2 DO I DUP IS-PRIME? IF . CR ELSE DROP THEN LOOP ;

: #PRIMES ( n -- t )
    0 SWAP 2 DO I IS-PRIME? IF 1+ THEN LOOP ;

