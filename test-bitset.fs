INCLUDE ffl/tst.fs
INCLUDE bitset.fs

T{ ." bitset is initially empty" CR
    100 BITSET MY-SET
    0 MY-SET INCLUDE? ?FALSE
    MY-SET 9 DUMP
}T

T{ ." after include! bitset includes bit n" CR
    3 MY-SET INCLUDE!
    4 MY-SET INCLUDE!
    41 MY-SET INCLUDE!
    97 MY-SET INCLUDE!
    99 MY-SET INCLUDE!
    2 MY-SET INCLUDE? ?FALSE
    3 MY-SET INCLUDE? ?TRUE
    4 MY-SET INCLUDE? ?TRUE
    41 MY-SET INCLUDE? ?TRUE
    97 MY-SET INCLUDE! ?TRUE
    99 MY-SET INCLUDE? ?TRUE
}T

