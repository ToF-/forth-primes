INCLUDE ffl/tst.fs
INCLUDE bitset.fs

CR ." bitset" CR CR
T{ ." bitset is initially empty" CR
    100 BITSET MY-SET
    MY-SET 0 IS-INCLUDED? ?FALSE
}T

T{ ." after include! bitset includes bit n" CR
    3 MY-SET INCLUDE!
    4 MY-SET INCLUDE!
    41 MY-SET INCLUDE!
    97 MY-SET INCLUDE!

    MY-SET 0 IS-INCLUDED? ?FALSE

    MY-SET 3 IS-INCLUDED? ?TRUE
    MY-SET 4 IS-INCLUDED? ?TRUE
     MY-SET 41 IS-INCLUDED? ?TRUE
     MY-SET 97 IS-INCLUDED? ?TRUE
}T

T{ ." after bitset-fill all bits are included" CR
    MY-SET BITSET-FILL
    MY-SET 49 IS-INCLUDED? ?TRUE
    MY-SET 99 IS-INCLUDED? ?TRUE
}T

T{ ." after exclude! bitset does not include bit n" CR
    0  MY-SET EXCLUDE!
    1  MY-SET EXCLUDE!
    2  MY-SET EXCLUDE!
    3  MY-SET EXCLUDE!
    42 MY-SET EXCLUDE!
    MY-SET 42 IS-INCLUDED? ?FALSE
}T

T{ ." after bitset-erase all bits are excluded" CR
    MY-SET BITSET-ERASE
    MY-SET 49 IS-INCLUDED? ?FALSE
    MY-SET 99 IS-INCLUDED? ?FALSE
}T
