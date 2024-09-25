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
</pre>

The `IS-PRIME?` word expects a number on the stack and leaves a boolean flag. It starts by tucking a `TRUE` value behind the number argument, then tries divisors from 2 to *√n* (or as the code says, as long as *n ≥ d²*). If a divisor is found, it sets the result flat to `FALSE` and swap *n* and *d* so that the loop ist forced to stop. Otherwise, it goes on with the next divisor. We don't need to try _all_ divisors, only divisors up to *√N* because if *xy = n* and *x > √n*, then *y < √n* and thus would have been found before we get to try *x*. At the end of the loop, *n* and *d* are dropped, leaving the reasult flag.


