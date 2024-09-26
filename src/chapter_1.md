# A simple, inefficient program

To know if a number N is prime, we divide it by numbers from 2 to √N : if none of these numbers divide N, then N is prime.



<pre style="color:#000000;background:#F2F2F2;"><span style="color:#669999; font-weight:bold;">\</span> <span style="color:#669999; font-weight:bold;">trial.fs   compute primes by trial division algorithm
</span><span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">SQUARED</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">n -- n² )</span>
    <span style="color:#009999; font-weight:bold;">DUP</span> <span style="color:#CC6600; font-weight:bold;">*</span> <span style="color:#993300; font-weight:bold;">;</span>

<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">IS-MULTIPLE?</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">m,n -- f )</span>
    <span style="color:#CC6600; font-weight:bold;">MOD</span> <span style="color:#CC6600; font-weight:bold;">0=</span> <span style="color:#993300; font-weight:bold;">;</span>

<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">IS-PRIME?</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">n -- f )</span>
    <span style="color:#CC6600; font-weight:bold;">TRUE</span> <span style="color:#009999; font-weight:bold;">SWAP</span>
    <span style="color:#800000; font-weight:bold;">2</span> <span style="color:#993300; font-weight:bold;">BEGIN</span>
        <span style="color:#009999; font-weight:bold;">2DUP</span> <span style="color:#336699; font-weight:bold;">SQUARED</span> <span style="color:#CC6600; font-weight:bold;">&gt;=</span> <span style="color:#993300; font-weight:bold;">WHILE</span>
        <span style="color:#009999; font-weight:bold;">2DUP</span> <span style="color:#336699; font-weight:bold;">IS-MULTIPLE?</span> <span style="color:#993300; font-weight:bold;">IF</span>
            <span style="color:#009999; font-weight:bold;">ROT</span> <span style="color:#009999; font-weight:bold;">DROP</span> <span style="color:#CC6600; font-weight:bold;">FALSE</span> <span style="color:#009999; font-weight:bold;">-ROT</span>
            <span style="color:#009999; font-weight:bold;">SWAP</span>
        <span style="color:#993300; font-weight:bold;">THEN</span>
        <span style="color:#CC6600; font-weight:bold;">1+</span>
    <span style="color:#993300; font-weight:bold;">REPEAT</span>
    <span style="color:#009999; font-weight:bold;">2DROP</span> <span style="color:#993300; font-weight:bold;">;</span>

<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">.PRIMES</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">n -- )</span>
    <span style="color:#800000; font-weight:bold;">2</span> <span style="color:#993300; font-weight:bold;">DO</span> <span style="color:#993300; font-weight:bold;">I</span> <span style="color:#009999; font-weight:bold;">DUP</span> <span style="color:#336699; font-weight:bold;">IS-PRIME?</span> <span style="color:#993300; font-weight:bold;">IF</span> <span style="color:#CC6600; font-weight:bold;">.</span> <span style="color:#3D3D5C; font-weight:bold;">CR</span> <span style="color:#993300; font-weight:bold;">ELSE</span> <span style="color:#009999; font-weight:bold;">DROP</span> <span style="color:#993300; font-weight:bold;">THEN</span> <span style="color:#993300; font-weight:bold;">LOOP</span> <span style="color:#993300; font-weight:bold;">;</span>

</pre>
The `IS-PRIME?` word expects a number on the stack. It starts by tucking a `TRUE` result flag behind the number argument, then tries divisors from 2 to *√n* (stopping when *d² > n*) If a divisor is found, the result flag is set to `FALSE` and *n* and *d* are swapped, forcing the loop to stop. Otherwise, the loop  goes on with the next divisor. We don't need to try _all_ divisors, only divisors up to *√N* because if *xy = n* and *x > √n*, then *y < √n* and thus would have been found before we get to try *x*. At the end of the loop, *n* and *d* are dropped, leaving the result flag.

Calling the `.PRIMES` word with 65536 on the stack : 
```
gforth trials.fs -e "65536 .PRIMES BYE"
```
takes about 0.172ms on my machine (a 3,3 intel cole i5 mac). 
