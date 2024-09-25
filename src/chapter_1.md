# A simple, inefficient program

To know if a number N is prime, we divide it by numbers from 2 to √N : if none of these numbers divide N, then N is prime.

```
: IS-PRIME? ( n -- f )
    TRUE SWAP
    2 BEGIN
        2DUP DUP * >= WHILE
            2DUP MOD 0= IF
                ROT DROP FALSE -ROT
                SWAP
            THEN
        1+
    REPEAT
    2DROP ;
```

The `IS-PRIME?` word expects a number on the stack and leaves a `TRUE` if this number is prime. 

A default value of `TRUE` is tucked behind the argument, then we begin the division loop starting with divisor D = 2.

This loop should stop as soon as the divisor is ≥ √N, in other words while N is greater than D * D, the loop goes on.

If N mod D is zero, we have found a divisor smaller than N, so we drop the `TRUE` flag and replace it with `FALSE`. Then we swap N and D, so that the loop stops at the next comparison ( whatever D is, N² > D).

If N mod D is not zero, we increment the divisor and repeat the loop. The word ends with dropping N and D, leaving the flag on the stack.


