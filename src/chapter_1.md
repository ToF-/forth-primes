# A simple, inefficient program

<pre><span style="color:#669999; font-weight:bold;">\</span> <span style="color:#669999; font-weight:bold;">trial.fs   compute primes by trial division algorithm</span></pre>
To know if a number N is prime, we divide it by numbers from 2 to √N : if N is not a multiple of any of these divisors then N is prime. 

Here's a word that takes N and D on the stack, and leaves `TRUE` if N%D == 0, `FALSE` otherwise.

<pre> <span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">IS-MULTIPLE?</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">n,d -- f )</span>
    <span style="color:#CC6600; font-weight:bold;">MOD</span> <span style="color:#CC6600; font-weight:bold;">0=</span> <span style="color:#993300; font-weight:bold;">;</span>
</pre>

As we are dealing with integers, D going from 2 to √N is easier to achieve by looping until D² > N. Hence a word to square a number:

<pre><span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">SQUARED</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">n -- n² )</span>
    <span style="color:#009999; font-weight:bold;">DUP</span> <span style="color:#CC6600; font-weight:bold;">*</span> <span style="color:#993300; font-weight:bold;">;</span>
</pre>

The word `IS-PRIME?` expects a number on the stack, and will a boolean result. 

<pre><span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">IS-PRIME?</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">n -- f )</span>
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

It starts with tucking a `TRUE` result flag behind the number argument. Starting with 2, and as long as N ≥ D², it checks if N is a multiple of D. If that's the case, the result flag is dropped and replaced with `FALSE`, and N and D are swapped on the stack to force an exit on the next loop. Otherwise, D is increased by 1.

A word `.PRIMES` will print all primes up to a number by `LOOP`ing from 2 to N+1 excluded, checking if the loop index is prime, printing it if that's the case.

<pre>
<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">.PRIMES</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">n -- )</span>
    <span style="color:#CC6600; font-weight:bold;">1+</span> <span style="color:#800000; font-weight:bold;">2</span> <span style="color:#993300; font-weight:bold;">DO</span>
        <span style="color:#993300; font-weight:bold;">I</span> <span style="color:#336699; font-weight:bold;">IS-PRIME?</span> <span style="color:#993300; font-weight:bold;">IF</span>
            <span style="color:#993300; font-weight:bold;">I</span> <span style="color:#CC6600; font-weight:bold;">.</span> <span style="color:#3D3D5C; font-weight:bold;">CR</span>
        <span style="color:#993300; font-weight:bold;">THEN</span>
   <span style="color:#993300; font-weight:bold;">LOOP</span> <span style="color:#993300; font-weight:bold;">;</span>
</pre>
Let's try our program:
```
> gforth trial.fs -e "100 .PRIMES BYE"
2
3
5
7
11
13
17
19
23
29
31
37
41
43
47
53
59
61
67
71
73
79
83
89
97
```
It works!

Finally the word `PRIME-COUNT` will count how many primes exist up to a number.
<pre>
<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">PRIME-COUNT</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">n -- t )</span>
    <span style="color:#800000; font-weight:bold;">0</span> <span style="color:#009999; font-weight:bold;">SWAP</span>
    <span style="color:#CC6600; font-weight:bold;">1+</span> <span style="color:#800000; font-weight:bold;">2</span> <span style="color:#993300; font-weight:bold;">DO</span>
        <span style="color:#993300; font-weight:bold;">I</span> <span style="color:#336699; font-weight:bold;">IS-PRIME?</span> <span style="color:#993300; font-weight:bold;">IF</span>
            <span style="color:#CC6600; font-weight:bold;">1+</span>
        <span style="color:#993300; font-weight:bold;">THEN</span>
    <span style="color:#993300; font-weight:bold;">LOOP</span> <span style="color:#993300; font-weight:bold;">;</span>
</pre>
Using this word on my 3.3 Ghz Intel mac to find how many primes to 1000000:
```
gforth trials.fs -e "1000000 PRIME-COUNT . BYE"
```
takes about 5 seconds, so there must be a better way. 
