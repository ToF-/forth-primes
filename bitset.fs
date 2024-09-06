CELL 8 * CONSTANT BITS-PER-CELL

: BITSET-SIZE ( n -- n )
    BITS-PER-CELL / 1+ CELLS ;

: BITSET ( n <name> -- )
    CREATE DUP ,
    BITSET-SIZE ALLOT ;

: BITSET-OFFSET ( bitset,n -- adr )
    OVER @ OVER < ABORT" OUT OF BITSET SIZE"
    BITS-PER-CELL / CELLS SWAP CELL+ + ;

: BITMASK ( n -- mask )
    1 SWAP LSHIFT ;

: IS-INCLUDED? ( bitset,n -- f )
    DUP BITMASK
    -ROT BITSET-OFFSET @
    AND ;

: INCLUDE! ( n,bitset -- )
    OVER BITSET-OFFSET DUP @
    ROT BITMASK OR
    SWAP ! ;

: EXCLUDE! ( n,bitset -- )
    OVER BITSET-OFFSET DUP @
    ROT BITMASK -1 XOR AND
    SWAP ! ;

: BITSET-FILL ( bitset -- )
    DUP @ BITSET-SIZE
    SWAP CELL+ DUP -ROT + SWAP DO
        -1 I !
        CELL +LOOP ;

: BITSET-ERASE ( bitset -- )
    DUP @ BITSET-SIZE
    SWAP CELL+ SWAP ERASE ;

