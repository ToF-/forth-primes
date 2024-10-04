# A simple, inefficient program to compute if a number is prime

To know if a number N is prime, we examine if N is a multiple of D with D varying from from 2 to √N.

<pre><span style="color:#669999; font-weight:bold;">\</span> <span style="color:#669999; font-weight:bold;">trial.fs   compute primes by trial division algorithm
</span>
<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">SQUARED</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">n -- n² )</span>
    <span style="color:#009999; font-weight:bold;">DUP</span> <span style="color:#CC6600; font-weight:bold;">*</span> <span style="color:#993300; font-weight:bold;">;</span>

<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">IS-MULTIPLE?</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">m,n -- f )</span>
    <span style="color:#CC6600; font-weight:bold;">MOD</span> <span style="color:#CC6600; font-weight:bold;">0=</span> <span style="color:#993300; font-weight:bold;">;</span>

<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">IS-PRIME?</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">n -- n|0 )</span>
    <span style="color:#800000; font-weight:bold;">2</span> <span style="color:#993300; font-weight:bold;">BEGIN</span>
        <span style="color:#009999; font-weight:bold;">2DUP</span> <span style="color:#336699; font-weight:bold;">SQUARED</span> <span style="color:#CC6600; font-weight:bold;">&gt;=</span> <span style="color:#993300; font-weight:bold;">WHILE</span>
        <span style="color:#009999; font-weight:bold;">2DUP</span> <span style="color:#336699; font-weight:bold;">IS-MULTIPLE?</span> <span style="color:#993300; font-weight:bold;">IF</span> <span style="color:#009999; font-weight:bold;">NIP</span> <span style="color:#800000; font-weight:bold;">0</span> <span style="color:#009999; font-weight:bold;">SWAP</span> <span style="color:#993300; font-weight:bold;">ELSE</span> <span style="color:#CC6600; font-weight:bold;">1+</span> <span style="color:#993300; font-weight:bold;">THEN</span>
    <span style="color:#993300; font-weight:bold;">REPEAT</span> <span style="color:#009999; font-weight:bold;">DROP</span> <span style="color:#993300; font-weight:bold;">;</span>

<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">.PRIMES</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">n -- )</span>
    <span style="color:#CC6600; font-weight:bold;">1+</span> <span style="color:#800000; font-weight:bold;">2</span> <span style="color:#993300; font-weight:bold;">DO</span> <span style="color:#993300; font-weight:bold;">I</span> <span style="color:#336699; font-weight:bold;">IS-PRIME?</span> <span style="color:#993300; font-weight:bold;">IF</span> <span style="color:#993300; font-weight:bold;">I</span> <span style="color:#CC6600; font-weight:bold;">.</span> <span style="color:#3D3D5C; font-weight:bold;">CR</span> <span style="color:#993300; font-weight:bold;">THEN</span> <span style="color:#993300; font-weight:bold;">LOOP</span> <span style="color:#993300; font-weight:bold;">;</span>

<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">PRIME-COUNT</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">n -- t )</span>
    <span style="color:#800000; font-weight:bold;">0</span> <span style="color:#009999; font-weight:bold;">SWAP</span>
    <span style="color:#CC6600; font-weight:bold;">1+</span> <span style="color:#800000; font-weight:bold;">2</span> <span style="color:#993300; font-weight:bold;">DO</span> <span style="color:#993300; font-weight:bold;">I</span> <span style="color:#336699; font-weight:bold;">IS-PRIME?</span> <span style="color:#993300; font-weight:bold;">IF</span> <span style="color:#CC6600; font-weight:bold;">1+</span> <span style="color:#993300; font-weight:bold;">THEN</span> <span style="color:#993300; font-weight:bold;">LOOP</span> <span style="color:#993300; font-weight:bold;">;</span>
</pre>
As we are dealing with integers, D going from 2 to √N is easier to achieve by looping until D² > N so we define a word i`SQUARED` that multiplies the number on the stack by itself.

If N mod D = O for 1 < D ≤ √N then N is a multiple of D, hence the word `IS-MULTIPLE?`.

The central word in our program, `IS-PRIME?` expects a number N on the stack, and will return N if N is prime, or 0 is N is composite.
Starting with D = 2, and as long as N ≥ D², it checks if N is a multiple of D. The loop is delimited with the words `BEGIN` and `REPEAT`, and controlled by a `WHILE` condition test.

Inside the loop, we duplicate both N and D, and test that N ≥ D². If that's not the case, we immediately exit the loop.

We duplicate N and D again. If N is a multiple of D, in which case N is removed and replaced with 0. This will force the loop to exit since 0 is not ≥ D. Otherwise, D is increased by one and the loop goes on.

At the end of the loop, the stack will be in either states:

0, D : a divisor has been found

N, D : the last divisor has been tested

We can then just drop the divisor D from the stack, leaving  either 0 (i.e false) if N is composite, or the positive number N (i.e true) is N is prime.

The word `.PRIMES` will print all primes up to a number by looping from 2 to N+1 excluded, checking the loop index and printing it if it's prime.

Finally the word `PRIME-COUNT` will count how many primes exist up to a number.

Let's try our program:
```
> gforth trial.fs -e "25 .PRIMES BYE"
2
3
5
7
11
13
17
19
23
> gforth trials.fs -e "1000000 PRIME-COUNT . BYE"
78498
```
This last result takes more than 5 seconds to appear on my machine, so some improvement are in order.

