# A simple, inefficient program to compute if a number is prime

To know if a number N is prime, we examine if N is a multiple of D with D varying from from 2 to √N.

As we dealing with integers, D going from 2 to √N is easier to achieve by looping until D² > N. Let's define a word to get the square of a number.

<pre><span style="color:#669999; font-weight:bold;">\</span> <span style="color:#669999; font-weight:bold;">trial.fs   compute primes by trial division algorithm

<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">SQUARED</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">n -- n² )</span>
    <span style="color:#009999; font-weight:bold;">DUP</span> <span style="color:#CC6600; font-weight:bold;">*</span> <span style="color:#993300; font-weight:bold;">;</span>
</span></pre>

We define a new forth word called `SQUARED`. A word definition is introduced with `:` and ends with `;`. Parameters are pushed and manipulated through the _parameter stack_. The comment `( n -- n² )` explains what is expected on the stack and what happens after `SQUARED` is called. To calculate the square of a number, we `DUP`licate the number and multiply it by itself. 

Let's launch gforth with our script and try this definition:
```
> gforth trial.fs -e "42 SQUARED . BYE"
1764
```

To determine if N is a multiple of D, we examine N mod D:
<pre>
<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">IS-MULTIPLE?</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">m,n -- f )</span>
    <span style="color:#CC6600; font-weight:bold;">MOD</span> <span style="color:#CC6600; font-weight:bold;">0=</span> <span style="color:#993300; font-weight:bold;">;</span>
</pre>
Our word takes two integers N and M on the stack and leaves a flag set to true if N mod M is zero:
```
> gforth trial.fs -e "49 7 IS-MULTIPLE? . 47 7 IS-MULTIPLE? . BYE"
-1 0
```
The central word in our program, `IS-PRIME?` expects a number N on the stack, and will return N if N is prime, 0 is N is composite.
<pre>
<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">IS-PRIME?</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">n -- n|0 )</span>
    <span style="color:#800000; font-weight:bold;">2</span> <span style="color:#993300; font-weight:bold;">BEGIN</span>
        <span style="color:#009999; font-weight:bold;">2DUP</span> <span style="color:#336699; font-weight:bold;">SQUARED</span> <span style="color:#CC6600; font-weight:bold;">&gt;=</span> <span style="color:#993300; font-weight:bold;">WHILE</span>
        <span style="color:#009999; font-weight:bold;">2DUP</span> <span style="color:#336699; font-weight:bold;">IS-MULTIPLE?</span> <span style="color:#993300; font-weight:bold;">IF</span> <span style="color:#009999; font-weight:bold;">NIP</span> <span style="color:#800000; font-weight:bold;">0</span> <span style="color:#009999; font-weight:bold;">SWAP</span> <span style="color:#993300; font-weight:bold;">ELSE</span> <span style="color:#CC6600; font-weight:bold;">1+</span> <span style="color:#993300; font-weight:bold;">THEN</span>
    <span style="color:#993300; font-weight:bold;">REPEAT</span> <span style="color:#009999; font-weight:bold;">DROP</span> <span style="color:#993300; font-weight:bold;">;</span>
</pre>
Starting with D = 2, and as long as N ≥ D², it checks if N is a multiple of D, through a loop. The loop is delimited with the words `BEGIN` and `REPEAT`, and controlled by a `WHILE` condition test.

Inside the loop, we duplicate both N and D, and test that N ≥ D². If that's not the case, we immediately exit the loop. 

We duplicate N and D again to check if N is a multiple of D in which case N is removed and replaced with 0. This will force the loop to exit, since 0 ≥ D² is false.

Otherwise, D is increased by one and the loop goes on.

At the end of the loop, the stack will be in either states:
- 0, D : a divisor has been found
- N, D : the last divisor has been tested

Thus if we drop the divisor D from the stack, we are left with 0 (i.e false) if N is composite, and the positive number N (i.e true) otherwise.

A word `.PRIMES` will print all primes up to a number by `LOOP`ing from 2 to N+1 excluded, checking if the loop index is prime, printing it if that's the case.

<pre>
<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">.PRIMES</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">n -- )</span>
    <span style="color:#CC6600; font-weight:bold;">1+</span> <span style="color:#800000; font-weight:bold;">2</span> <span style="color:#993300; font-weight:bold;">DO</span> <span style="color:#993300; font-weight:bold;">I</span> <span style="color:#336699; font-weight:bold;">IS-PRIME?</span> <span style="color:#993300; font-weight:bold;">IF</span> <span style="color:#993300; font-weight:bold;">I</span> <span style="color:#CC6600; font-weight:bold;">.</span> <span style="color:#3D3D5C; font-weight:bold;">CR</span> <span style="color:#993300; font-weight:bold;">THEN</span> <span style="color:#993300; font-weight:bold;">LOOP</span> <span style="color:#993300; font-weight:bold;">;</span>
</pre>
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
```
It works!

Finally the word `PRIME-COUNT` will count how many primes exist up to a number.
<pre>
<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">PRIME-COUNT</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">n -- t )</span>
    <span style="color:#800000; font-weight:bold;">0</span> <span style="color:#009999; font-weight:bold;">SWAP</span>
    <span style="color:#CC6600; font-weight:bold;">1+</span> <span style="color:#800000; font-weight:bold;">2</span> <span style="color:#993300; font-weight:bold;">DO</span> <span style="color:#993300; font-weight:bold;">I</span> <span style="color:#336699; font-weight:bold;">IS-PRIME?</span> <span style="color:#993300; font-weight:bold;">IF</span> <span style="color:#CC6600; font-weight:bold;">1+</span> <span style="color:#993300; font-weight:bold;">THEN</span> <span style="color:#993300; font-weight:bold;">LOOP</span> <span style="color:#993300; font-weight:bold;">;</span>
</pre>
Using this word on my 3.3 Ghz Intel mac to find how many primes to 1000000:
```
time gforth trials.fs -e "1000000 PRIME-COUNT . BYE"
78498
real    0m5.341s
user    0m4.975s
sys     0m0.044s
```
takes more than 5 seconds, so there must be a better way.
