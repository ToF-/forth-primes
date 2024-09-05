CELL 8 * CONSTANT BITS-PER-CELL

: BITSET-SIZE ( n -- n )
    BITS-PER-CELL / 1+ CELLS ;

: BITSET ( n <name> -- )
    CREATE
    BITSET-SIZE ALLOT ;

: BITSET-OFFSET ( bitset,n -- adr )
    BITS-PER-CELL / CELLS + ;
    
: INCLUDE? ( n,bitset -- f )
    OVER BITSET-OFFSET @
    SWAP 1 SWAP LSHIFT AND ;

: INCLUDE! ( n,bitset -- )
    OVER BITSET-OFFSET DUP @
    ROT 1 SWAP LSHIFT OR
    SWAP ! ;
