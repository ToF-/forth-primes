# Finding primes by eliminating composites

The idea of using primes to find primes can be improved further with the Sieve of Eratosthenes : since we know that a number P is prime, we can alreday mark P.P, (P+1)P, (P+2)P, (P+3)P, … as non primes. Here's an instance of this strategy with primes 2 to 7 :

| prime | marked composites |
| ----- | ------------------|
| 2    | { 4, 6, 8, 10, 12, 14, …, 96, 98, 100 }|
| 3    | { 9, 15, 21, 27, 33, 36, 39, 45, 51, 57, 63, 66, 69, 75, 81, 87, 93, 99 }|
| 5    | { 25, 35, 55, 65, 85, 95} |
| 7    | { 49, 77, 91} |

The remaining numbers smaller than 100 that are non marked in this process: 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97 are all prime numbers.

The Sieve of Eratosthenes, used with primes from 2 to P, can be used to determine primes up to P². What we need is a table storing a boolean value for each number in the range.

<pre><span style="color:#669999; font-weight:bold;">\</span> <span style="color:#669999; font-weight:bold;">sieve.fs compute primes with the sieve of Eratosthenes algorithm
</span>
<span style="color:#3D3D5C; font-weight:bold;">REQUIRE</span> <span style="color:#800000; font-weight:bold;">small-primes.fs</span>

<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">SQUARED</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">n -- n² )</span>
    <span style="color:#009999; font-weight:bold;">DUP</span> <span style="color:#CC6600; font-weight:bold;">*</span> <span style="color:#993300; font-weight:bold;">;</span>

<span style="color:#800000; font-weight:bold;">1000</span> <span style="color:#336699; font-weight:bold;">SQUARED</span> <span style="color:#F07F00; font-weight:bold;">CONSTANT</span> <span style="color:#336699; font-weight:bold;">PRIME-LIMIT</span>

<span style="color:#336699; font-weight:bold;">PRIME-LIMIT</span> <span style="color:#800000; font-weight:bold;">8</span> <span style="color:#CC6600; font-weight:bold;">/</span> <span style="color:#F07F00; font-weight:bold;">CONSTANT</span> <span style="color:#336699; font-weight:bold;">SIEVE-SIZE</span>

<span style="color:#F07F00; font-weight:bold;">CREATE</span> <span style="color:#336699; font-weight:bold;">SIEVE</span> <span style="color:#336699; font-weight:bold;">SIEVE-SIZE</span> <span style="color:#CC3300; font-weight:bold;">ALLOT</span>
</pre>
The largest number we will inspect is 1000000, so 125000 bytes are `ALLOT`ed. Our language allows for fetching (via `@`) and storing (via `!`  memory cells of 64 bits. Therefore the flag value about number N can be found by reading bit N%64 at position N/64 in the table. The way to read a specific bit B can be computed by left shifting a value of 1 B times :

| bit (b) | bit mask ( 2^b) |
| --- | -------- |
|  0  | 0000000000000000000000000000000000000000000000000000000000000001 |
|  1  | 0000000000000000000000000000000000000000000000000000000000000010 |
|  2  | 0000000000000000000000000000000000000000000000000000000000000100 |
|  …  |  … |
| 63  | 1000000000000000000000000000000000000000000000000000000000000000 |

Inverting this bit mask and `AND`ing it with the current cell value will put the specific bit to zero. (e.g. 11011011 & !(2^0) = 11011011 & 11111110 = 11011010)
<pre>
<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">SIEVE-POS</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">n -- addr )</span>
    <span style="color:#800000; font-weight:bold;">64</span> <span style="color:#CC6600; font-weight:bold;">/</span> <span style="color:#CC3300; font-weight:bold;">CELLS</span> <span style="color:#336699; font-weight:bold;">SIEVE</span> <span style="color:#CC6600; font-weight:bold;">+</span> <span style="color:#993300; font-weight:bold;">;</span>

<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">SIEVE-BITMASK</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">n -- bitmask )</span>
    <span style="color:#800000; font-weight:bold;">64</span> <span style="color:#CC6600; font-weight:bold;">MOD</span> <span style="color:#800000; font-weight:bold;">1</span> <span style="color:#009999; font-weight:bold;">SWAP</span> <span style="color:#CC6600; font-weight:bold;">LSHIFT</span> <span style="color:#993300; font-weight:bold;">;</span>

<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">SIEVE!</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">n -- )</span>
    <span style="color:#3D3D5C; font-weight:bold;">ASSERT(</span> <span style="color:#009999; font-weight:bold;">DUP</span> <span style="color:#336699; font-weight:bold;">PRIME-LIMIT</span> <span style="color:#CC6600; font-weight:bold;">&lt;=</span> <span style="color:#3D3D5C; font-weight:bold;">)</span>
    <span style="color:#009999; font-weight:bold;">DUP</span> <span style="color:#336699; font-weight:bold;">SIEVE-BITMASK</span> <span style="color:#CC6600; font-weight:bold;">INVERT</span>
    <span style="color:#009999; font-weight:bold;">SWAP</span> <span style="color:#336699; font-weight:bold;">SIEVE-POS</span> <span style="color:#009999; font-weight:bold;">DUP</span> <span style="color:#CC3300; font-weight:bold;">@</span>
    <span style="color:#009999; font-weight:bold;">ROT</span> <span style="color:#CC6600; font-weight:bold;">AND</span> <span style="color:#009999; font-weight:bold;">SWAP</span> <span style="color:#CC3300; font-weight:bold;">!</span> <span style="color:#993300; font-weight:bold;">;</span>
</pre>
To initialize the sieve, we start filling it entrirely with ones, meaning that all numbers are considered primes. Then for each prime divisor P found in the small primes table, we "sieve" the position P², P²+P, P²+2P, … until the sieve limit is reached.
<pre>
<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">INIT-SIEVE</span>
    <span style="color:#336699; font-weight:bold;">SIEVE</span> <span style="color:#336699; font-weight:bold;">SIEVE-SIZE</span> <span style="color:#800000; font-weight:bold;">255</span> <span style="color:#CC3300; font-weight:bold;">FILL</span>
    <span style="color:#800000; font-weight:bold;">0</span> <span style="color:#336699; font-weight:bold;">SIEVE!</span> <span style="color:#800000; font-weight:bold;">1</span> <span style="color:#336699; font-weight:bold;">SIEVE!</span>
    <span style="color:#800000; font-weight:bold;">SMALL-PRIMES-MAX</span> <span style="color:#800000; font-weight:bold;">0</span> <span style="color:#993300; font-weight:bold;">DO</span>
        <span style="color:#993300; font-weight:bold;">I</span> <span style="color:#800000; font-weight:bold;">NTH-PRIME</span>
        <span style="color:#009999; font-weight:bold;">DUP</span> <span style="color:#009999; font-weight:bold;">DUP</span> <span style="color:#336699; font-weight:bold;">SQUARED</span>
        <span style="color:#993300; font-weight:bold;">BEGIN</span>
            <span style="color:#009999; font-weight:bold;">DUP</span> <span style="color:#336699; font-weight:bold;">PRIME-LIMIT</span> <span style="color:#CC6600; font-weight:bold;">&lt;=</span> <span style="color:#993300; font-weight:bold;">WHILE</span>
            <span style="color:#009999; font-weight:bold;">DUP</span> <span style="color:#336699; font-weight:bold;">SIEVE!</span>
            <span style="color:#009999; font-weight:bold;">OVER</span> <span style="color:#CC6600; font-weight:bold;">+</span>
        <span style="color:#993300; font-weight:bold;">REPEAT</span>
        <span style="color:#009999; font-weight:bold;">2DROP</span> <span style="color:#009999; font-weight:bold;">DROP</span>
    <span style="color:#993300; font-weight:bold;">LOOP</span> <span style="color:#993300; font-weight:bold;">;</span>
</pre>
To find if the number N is prime, fetch the value at cell N/64, `AND` it with the bit mask value of bit N%64 : if the result is not zero, N is prime.
<pre>
<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">IS-PRIME?</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">n -- f )</span>
    <span style="color:#3D3D5C; font-weight:bold;">ASSERT(</span> <span style="color:#009999; font-weight:bold;">DUP</span> <span style="color:#336699; font-weight:bold;">PRIME-LIMIT</span> <span style="color:#CC6600; font-weight:bold;">&lt;=</span> <span style="color:#3D3D5C; font-weight:bold;">)</span>
    <span style="color:#009999; font-weight:bold;">DUP</span> <span style="color:#336699; font-weight:bold;">SIEVE-BITMASK</span>
    <span style="color:#009999; font-weight:bold;">SWAP</span> <span style="color:#336699; font-weight:bold;">SIEVE-POS</span> <span style="color:#CC3300; font-weight:bold;">@</span> <span style="color:#CC6600; font-weight:bold;">AND</span> <span style="color:#993300; font-weight:bold;">;</span>
</pre>
After initializing the sieve, the top level words are unchanged.
<pre>
<span style="color:#336699; font-weight:bold;">INIT-SIEVE</span>

<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">.PRIMES</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">n -- )</span>
    <span style="color:#CC6600; font-weight:bold;">1+</span> <span style="color:#800000; font-weight:bold;">2</span> <span style="color:#993300; font-weight:bold;">DO</span> <span style="color:#993300; font-weight:bold;">I</span> <span style="color:#336699; font-weight:bold;">IS-PRIME?</span> <span style="color:#993300; font-weight:bold;">IF</span> <span style="color:#993300; font-weight:bold;">I</span> <span style="color:#CC6600; font-weight:bold;">.</span> <span style="color:#3D3D5C; font-weight:bold;">CR</span> <span style="color:#993300; font-weight:bold;">THEN</span> <span style="color:#993300; font-weight:bold;">LOOP</span> <span style="color:#993300; font-weight:bold;">;</span>

<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">PRIME-COUNT</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">n -- t )</span>
    <span style="color:#800000; font-weight:bold;">0</span> <span style="color:#009999; font-weight:bold;">SWAP</span>
    <span style="color:#CC6600; font-weight:bold;">1+</span> <span style="color:#800000; font-weight:bold;">2</span> <span style="color:#993300; font-weight:bold;">DO</span>
        <span style="color:#993300; font-weight:bold;">I</span> <span style="color:#336699; font-weight:bold;">IS-PRIME?</span> <span style="color:#993300; font-weight:bold;">IF</span>
            <span style="color:#CC6600; font-weight:bold;">1+</span>
        <span style="color:#993300; font-weight:bold;">THEN</span>
    <span style="color:#993300; font-weight:bold;">LOOP</span> <span style="color:#993300; font-weight:bold;">;</span>
</pre>
Now our quest to count the primes up to 1000000 takes 0.6 seconds.
