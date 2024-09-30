# An improved trial program

Now we can use the values in the `SMALL-PRIMES` instead of a sequence of divisors :

<pre><span style="color:#669999; font-weight:bold;">\</span> <span style="color:#669999; font-weight:bold;">p-trial.fs   compute primes by trial division algorithm, using prime divisors
</span>
<span style="color:#3D3D5C; font-weight:bold;">REQUIRE</span> <span style="color:#800000; font-weight:bold;">small-primes.fs</span>

<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">SQUARED</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">n -- n² )</span>
    <span style="color:#009999; font-weight:bold;">DUP</span> <span style="color:#CC6600; font-weight:bold;">*</span> <span style="color:#993300; font-weight:bold;">;</span>

<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">IS-MULTIPLE?</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">m,n -- f )</span>
    <span style="color:#CC6600; font-weight:bold;">MOD</span> <span style="color:#CC6600; font-weight:bold;">0=</span> <span style="color:#993300; font-weight:bold;">;</span>

<span style="color:#800000; font-weight:bold;">1000</span> <span style="color:#336699; font-weight:bold;">SQUARED</span> <span style="color:#F07F00; font-weight:bold;">CONSTANT</span> <span style="color:#336699; font-weight:bold;">PRIME-LIMIT</span>

<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">IS-PRIME?</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">n -- f )</span>
    <span style="color:#3D3D5C; font-weight:bold;">ASSERT(</span> <span style="color:#009999; font-weight:bold;">DUP</span> <span style="color:#336699; font-weight:bold;">PRIME-LIMIT</span> <span style="color:#CC6600; font-weight:bold;">&lt;=</span> <span style="color:#3D3D5C; font-weight:bold;">)</span>
    <span style="color:#CC6600; font-weight:bold;">TRUE</span> <span style="color:#009999; font-weight:bold;">SWAP</span>
    <span style="color:#800000; font-weight:bold;">SMALL-PRIMES-MAX</span> <span style="color:#800000; font-weight:bold;">0</span> <span style="color:#993300; font-weight:bold;">DO</span>
        <span style="color:#993300; font-weight:bold;">I</span> <span style="color:#800000; font-weight:bold;">NTH-PRIME</span>
        <span style="color:#009999; font-weight:bold;">2DUP</span> <span style="color:#336699; font-weight:bold;">SQUARED</span> <span style="color:#CC6600; font-weight:bold;">&lt;</span> <span style="color:#993300; font-weight:bold;">IF</span>
            <span style="color:#009999; font-weight:bold;">DROP</span> <span style="color:#993300; font-weight:bold;">LEAVE</span>
        <span style="color:#993300; font-weight:bold;">ELSE</span>
            <span style="color:#009999; font-weight:bold;">OVER</span> <span style="color:#009999; font-weight:bold;">SWAP</span> <span style="color:#336699; font-weight:bold;">IS-MULTIPLE?</span> <span style="color:#993300; font-weight:bold;">IF</span>
                <span style="color:#009999; font-weight:bold;">NIP</span> <span style="color:#CC6600; font-weight:bold;">FALSE</span> <span style="color:#009999; font-weight:bold;">SWAP</span> <span style="color:#993300; font-weight:bold;">LEAVE</span>
            <span style="color:#993300; font-weight:bold;">THEN</span>
        <span style="color:#993300; font-weight:bold;">THEN</span>
    <span style="color:#993300; font-weight:bold;">LOOP</span> <span style="color:#009999; font-weight:bold;">DROP</span> <span style="color:#993300; font-weight:bold;">;</span>
</pre>
Since we are only using prime divisors < 1000, we cannot extend the prime test beyond 1000000. As before, the word starts by setting a result flag to `TRUE`, then proceed to loop over the small primes table, trying all prime divisors. If D² > N, we leave the loop, and the flag stays true. If if N is a multiple of D then we drop the flag, replacing it with `FALSE`, 

The top level words are unchanged.
<pre>
<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">.PRIMES</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">n -- )</span>
    <span style="color:#CC6600; font-weight:bold;">1+</span> <span style="color:#800000; font-weight:bold;">2</span> <span style="color:#993300; font-weight:bold;">DO</span>
        <span style="color:#993300; font-weight:bold;">I</span> <span style="color:#336699; font-weight:bold;">IS-PRIME?</span> <span style="color:#993300; font-weight:bold;">IF</span>
            <span style="color:#993300; font-weight:bold;">I</span> <span style="color:#CC6600; font-weight:bold;">.</span> <span style="color:#3D3D5C; font-weight:bold;">CR</span>
        <span style="color:#993300; font-weight:bold;">THEN</span>
   <span style="color:#993300; font-weight:bold;">LOOP</span> <span style="color:#993300; font-weight:bold;">;</span>

<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">PRIME-COUNT</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">n -- t )</span>
    <span style="color:#800000; font-weight:bold;">0</span> <span style="color:#009999; font-weight:bold;">SWAP</span>
    <span style="color:#CC6600; font-weight:bold;">1+</span> <span style="color:#800000; font-weight:bold;">2</span> <span style="color:#993300; font-weight:bold;">DO</span>
        <span style="color:#993300; font-weight:bold;">I</span> <span style="color:#336699; font-weight:bold;">IS-PRIME?</span> <span style="color:#993300; font-weight:bold;">IF</span>
            <span style="color:#CC6600; font-weight:bold;">1+</span>
        <span style="color:#993300; font-weight:bold;">THEN</span>
    <span style="color:#993300; font-weight:bold;">LOOP</span> <span style="color:#993300; font-weight:bold;">;</span>
</pre>
Counting the primes up to 1000000 takes 1.6 seconds.
