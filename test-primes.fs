INCLUDE ffl/tst.fs
INCLUDE primes.fs

CR ." primes" CR CR
T{ ." 1 is not prime" CR
    1 IS-PRIME? ?FALSE
}T

T{ ." 2 is prime" CR
    2 IS-PRIME? ?TRUE
}T

T{ ." 3 is prime" CR
    3 IS-PRIME? ?TRUE
}T

T{ ." 4 is not prime" CR
    4 IS-PRIME? ?FALSE
}T

T{ ." 5 is prime" CR
    5 IS-PRIME? ?TRUE
}T

T{ ." 17 is prime" CR
    17 IS-PRIME? ?TRUE
}T

T{ ." 4807 is not prime" CR
    4807 IS-PRIME? ?FALSE
}T

T{ ." 99999787 is prime" CR
   99999787 IS-PRIME? ?TRUE
}T

BYE
