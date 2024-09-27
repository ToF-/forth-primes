# Finding primes with a sieve

An efficient way to find primes is to use the Sieve of Eratosthenes algorithm.

For every prime number *p*, we mark *p.p*, *(p+1)p*, *(p+2)p*, … *(p+n)p* as composite.

For instance, prime numbers 2,3,5, and 7 allow the marking of all composites ≤ 100 :

- 2 : {4, 6, 8, 10, 12, …, 100}
- 3 : {9, 15, 21, 27, 33, 39, 45, 51, 57, 63, 66, 69, 75, 81, 87, 93, 99}
- 5 : {25, 30, 35, 55, 65, 85, 95}
- 7 : {49, 77, 91}

<pre style="color:#000000;background:#F2F2F2;"><span style="color:#669999; font-weight:bold;">\</span> <span style="color:#669999; font-weight:bold;">sieve.fs compute primes with the sieve of Eratosthenes algorithm
</span>
<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">SQUARED</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">n -- n² )</span>
    <span style="color:#009999; font-weight:bold;">DUP</span> <span style="color:#CC6600; font-weight:bold;">*</span> <span style="color:#993300; font-weight:bold;">;</span>

<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">REVERSE</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">bits -- bits )</span>
    <span style="color:#800000; font-weight:bold;">255</span> <span style="color:#CC6600; font-weight:bold;">XOR</span> <span style="color:#993300; font-weight:bold;">;</span>

<span style="color:#F07F00; font-weight:bold;">CREATE</span> <span style="color:#336699; font-weight:bold;">PRIMES</span>
  <span style="color:#800000; font-weight:bold;">2</span> <span style="color:#CC3300; font-weight:bold;">C,</span>   <span style="color:#800000; font-weight:bold;">3</span> <span style="color:#CC3300; font-weight:bold;">C,</span>   <span style="color:#800000; font-weight:bold;">5</span> <span style="color:#CC3300; font-weight:bold;">C,</span>   <span style="color:#800000; font-weight:bold;">7</span> <span style="color:#CC3300; font-weight:bold;">C,</span>  <span style="color:#800000; font-weight:bold;">11</span> <span style="color:#CC3300; font-weight:bold;">C,</span>  <span style="color:#800000; font-weight:bold;">13</span> <span style="color:#CC3300; font-weight:bold;">C,</span>  <span style="color:#800000; font-weight:bold;">17</span> <span style="color:#CC3300; font-weight:bold;">C,</span>  <span style="color:#800000; font-weight:bold;">19</span> <span style="color:#CC3300; font-weight:bold;">C,</span>  <span style="color:#800000; font-weight:bold;">23</span> <span style="color:#CC3300; font-weight:bold;">C,</span>
 <span style="color:#800000; font-weight:bold;">29</span> <span style="color:#CC3300; font-weight:bold;">C,</span>  <span style="color:#800000; font-weight:bold;">31</span> <span style="color:#CC3300; font-weight:bold;">C,</span>  <span style="color:#800000; font-weight:bold;">37</span> <span style="color:#CC3300; font-weight:bold;">C,</span>  <span style="color:#800000; font-weight:bold;">41</span> <span style="color:#CC3300; font-weight:bold;">C,</span>  <span style="color:#800000; font-weight:bold;">43</span> <span style="color:#CC3300; font-weight:bold;">C,</span>  <span style="color:#800000; font-weight:bold;">47</span> <span style="color:#CC3300; font-weight:bold;">C,</span>  <span style="color:#800000; font-weight:bold;">53</span> <span style="color:#CC3300; font-weight:bold;">C,</span>  <span style="color:#800000; font-weight:bold;">59</span> <span style="color:#CC3300; font-weight:bold;">C,</span>  <span style="color:#800000; font-weight:bold;">61</span> <span style="color:#CC3300; font-weight:bold;">C,</span>
 <span style="color:#800000; font-weight:bold;">67</span> <span style="color:#CC3300; font-weight:bold;">C,</span>  <span style="color:#800000; font-weight:bold;">71</span> <span style="color:#CC3300; font-weight:bold;">C,</span>  <span style="color:#800000; font-weight:bold;">73</span> <span style="color:#CC3300; font-weight:bold;">C,</span>  <span style="color:#800000; font-weight:bold;">79</span> <span style="color:#CC3300; font-weight:bold;">C,</span>  <span style="color:#800000; font-weight:bold;">83</span> <span style="color:#CC3300; font-weight:bold;">C,</span>  <span style="color:#800000; font-weight:bold;">89</span> <span style="color:#CC3300; font-weight:bold;">C,</span>  <span style="color:#800000; font-weight:bold;">97</span> <span style="color:#CC3300; font-weight:bold;">C,</span> <span style="color:#800000; font-weight:bold;">101</span> <span style="color:#CC3300; font-weight:bold;">C,</span> <span style="color:#800000; font-weight:bold;">103</span> <span style="color:#CC3300; font-weight:bold;">C,</span>
<span style="color:#800000; font-weight:bold;">107</span> <span style="color:#CC3300; font-weight:bold;">C,</span> <span style="color:#800000; font-weight:bold;">109</span> <span style="color:#CC3300; font-weight:bold;">C,</span> <span style="color:#800000; font-weight:bold;">113</span> <span style="color:#CC3300; font-weight:bold;">C,</span> <span style="color:#800000; font-weight:bold;">127</span> <span style="color:#CC3300; font-weight:bold;">C,</span> <span style="color:#800000; font-weight:bold;">131</span> <span style="color:#CC3300; font-weight:bold;">C,</span> <span style="color:#800000; font-weight:bold;">137</span> <span style="color:#CC3300; font-weight:bold;">C,</span> <span style="color:#800000; font-weight:bold;">139</span> <span style="color:#CC3300; font-weight:bold;">C,</span> <span style="color:#800000; font-weight:bold;">149</span> <span style="color:#CC3300; font-weight:bold;">C,</span> <span style="color:#800000; font-weight:bold;">151</span> <span style="color:#CC3300; font-weight:bold;">C,</span>
<span style="color:#800000; font-weight:bold;">157</span> <span style="color:#CC3300; font-weight:bold;">C,</span> <span style="color:#800000; font-weight:bold;">163</span> <span style="color:#CC3300; font-weight:bold;">C,</span> <span style="color:#800000; font-weight:bold;">167</span> <span style="color:#CC3300; font-weight:bold;">C,</span> <span style="color:#800000; font-weight:bold;">173</span> <span style="color:#CC3300; font-weight:bold;">C,</span> <span style="color:#800000; font-weight:bold;">179</span> <span style="color:#CC3300; font-weight:bold;">C,</span> <span style="color:#800000; font-weight:bold;">181</span> <span style="color:#CC3300; font-weight:bold;">C,</span> <span style="color:#800000; font-weight:bold;">191</span> <span style="color:#CC3300; font-weight:bold;">C,</span> <span style="color:#800000; font-weight:bold;">193</span> <span style="color:#CC3300; font-weight:bold;">C,</span> <span style="color:#800000; font-weight:bold;">197</span> <span style="color:#CC3300; font-weight:bold;">C,</span>
<span style="color:#800000; font-weight:bold;">199</span> <span style="color:#CC3300; font-weight:bold;">C,</span> <span style="color:#800000; font-weight:bold;">211</span> <span style="color:#CC3300; font-weight:bold;">C,</span> <span style="color:#800000; font-weight:bold;">223</span> <span style="color:#CC3300; font-weight:bold;">C,</span> <span style="color:#800000; font-weight:bold;">227</span> <span style="color:#CC3300; font-weight:bold;">C,</span> <span style="color:#800000; font-weight:bold;">229</span> <span style="color:#CC3300; font-weight:bold;">C,</span> <span style="color:#800000; font-weight:bold;">233</span> <span style="color:#CC3300; font-weight:bold;">C,</span> <span style="color:#800000; font-weight:bold;">239</span> <span style="color:#CC3300; font-weight:bold;">C,</span> <span style="color:#800000; font-weight:bold;">241</span> <span style="color:#CC3300; font-weight:bold;">C,</span> <span style="color:#800000; font-weight:bold;">251</span> <span style="color:#CC3300; font-weight:bold;">C,</span>

<span style="color:#800000; font-weight:bold;">54</span> <span style="color:#F07F00; font-weight:bold;">CONSTANT</span> <span style="color:#336699; font-weight:bold;">MAX-PRIMES</span>

<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">NTH-PRIME</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">n -- p )</span>
    <span style="color:#336699; font-weight:bold;">PRIMES</span> <span style="color:#CC6600; font-weight:bold;">+</span> <span style="color:#CC3300; font-weight:bold;">C@</span> <span style="color:#993300; font-weight:bold;">;</span>

<span style="color:#800000; font-weight:bold;">256</span> <span style="color:#F07F00; font-weight:bold;">CONSTANT</span> <span style="color:#336699; font-weight:bold;">PRIME-LIMIT</span>

<span style="color:#336699; font-weight:bold;">PRIME-LIMIT</span> <span style="color:#336699; font-weight:bold;">SQUARED</span> <span style="color:#F07F00; font-weight:bold;">CONSTANT</span> <span style="color:#336699; font-weight:bold;">SIEVE-LIMIT</span>
<span style="color:#336699; font-weight:bold;">SIEVE-LIMIT</span> <span style="color:#800000; font-weight:bold;">8</span> <span style="color:#CC6600; font-weight:bold;">/</span> <span style="color:#F07F00; font-weight:bold;">CONSTANT</span> <span style="color:#336699; font-weight:bold;">SIEVE-SIZE</span>

<span style="color:#F07F00; font-weight:bold;">CREATE</span> <span style="color:#336699; font-weight:bold;">SIEVE</span> <span style="color:#336699; font-weight:bold;">SIEVE-SIZE</span> <span style="color:#CC3300; font-weight:bold;">ALLOT</span>

<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">SIEVE-POS</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">n -- addr )</span>
    <span style="color:#800000; font-weight:bold;">8</span> <span style="color:#CC6600; font-weight:bold;">/</span> <span style="color:#336699; font-weight:bold;">SIEVE</span> <span style="color:#CC6600; font-weight:bold;">+</span> <span style="color:#993300; font-weight:bold;">;</span>

<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">SIEVE-BITMASK</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">n -- bitmask )</span>
    <span style="color:#800000; font-weight:bold;">8</span> <span style="color:#CC6600; font-weight:bold;">MOD</span> <span style="color:#800000; font-weight:bold;">1</span> <span style="color:#009999; font-weight:bold;">SWAP</span> <span style="color:#CC6600; font-weight:bold;">LSHIFT</span> <span style="color:#993300; font-weight:bold;">;</span>

<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">SIEVE!</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">n -- )</span>
    <span style="color:#3D3D5C; font-weight:bold;">ASSERT(</span> <span style="color:#009999; font-weight:bold;">DUP</span> <span style="color:#336699; font-weight:bold;">SIEVE-LIMIT</span> <span style="color:#CC6600; font-weight:bold;">&lt;</span> <span style="color:#3D3D5C; font-weight:bold;">)</span>
    <span style="color:#009999; font-weight:bold;">DUP</span> <span style="color:#336699; font-weight:bold;">SIEVE-BITMASK</span> <span style="color:#336699; font-weight:bold;">REVERSE</span>
    <span style="color:#009999; font-weight:bold;">SWAP</span> <span style="color:#336699; font-weight:bold;">SIEVE-POS</span>
    <span style="color:#009999; font-weight:bold;">DUP</span> <span style="color:#CC3300; font-weight:bold;">C@</span> <span style="color:#009999; font-weight:bold;">ROT</span> <span style="color:#CC6600; font-weight:bold;">AND</span>
    <span style="color:#009999; font-weight:bold;">SWAP</span> <span style="color:#CC3300; font-weight:bold;">C!</span> <span style="color:#993300; font-weight:bold;">;</span>


<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">INIT-SIEVE</span>
    <span style="color:#336699; font-weight:bold;">SIEVE</span> <span style="color:#336699; font-weight:bold;">SIEVE-SIZE</span> <span style="color:#800000; font-weight:bold;">255</span> <span style="color:#CC3300; font-weight:bold;">FILL</span>
    <span style="color:#800000; font-weight:bold;">1</span> <span style="color:#336699; font-weight:bold;">SIEVE!</span>
    <span style="color:#336699; font-weight:bold;">MAX-PRIMES</span> <span style="color:#800000; font-weight:bold;">0</span> <span style="color:#993300; font-weight:bold;">DO</span>
        <span style="color:#993300; font-weight:bold;">I</span> <span style="color:#336699; font-weight:bold;">NTH-PRIME</span>
        <span style="color:#009999; font-weight:bold;">DUP</span> <span style="color:#009999; font-weight:bold;">DUP</span> <span style="color:#336699; font-weight:bold;">SQUARED</span>
        <span style="color:#993300; font-weight:bold;">BEGIN</span>
            <span style="color:#009999; font-weight:bold;">DUP</span> <span style="color:#336699; font-weight:bold;">SIEVE-LIMIT</span> <span style="color:#CC6600; font-weight:bold;">&lt;</span> <span style="color:#993300; font-weight:bold;">WHILE</span>
            <span style="color:#009999; font-weight:bold;">DUP</span> <span style="color:#336699; font-weight:bold;">SIEVE!</span>
            <span style="color:#009999; font-weight:bold;">OVER</span> <span style="color:#CC6600; font-weight:bold;">+</span>
        <span style="color:#993300; font-weight:bold;">REPEAT</span>
        <span style="color:#009999; font-weight:bold;">2DROP</span> <span style="color:#009999; font-weight:bold;">DROP</span>
    <span style="color:#993300; font-weight:bold;">LOOP</span> <span style="color:#993300; font-weight:bold;">;</span>

<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">IS-PRIME?</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">n -- f )</span>
    <span style="color:#3D3D5C; font-weight:bold;">ASSERT(</span> <span style="color:#009999; font-weight:bold;">DUP</span> <span style="color:#336699; font-weight:bold;">SIEVE-LIMIT</span> <span style="color:#CC6600; font-weight:bold;">&lt;</span> <span style="color:#3D3D5C; font-weight:bold;">)</span>
    <span style="color:#009999; font-weight:bold;">DUP</span> <span style="color:#336699; font-weight:bold;">SIEVE-BITMASK</span>
    <span style="color:#009999; font-weight:bold;">SWAP</span> <span style="color:#336699; font-weight:bold;">SIEVE-POS</span> <span style="color:#CC3300; font-weight:bold;">C@</span> <span style="color:#CC6600; font-weight:bold;">AND</span> <span style="color:#993300; font-weight:bold;">;</span>

<span style="color:#336699; font-weight:bold;">INIT-SIEVE</span>

<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">.PRIMES</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">n -- )</span>
    <span style="color:#800000; font-weight:bold;">2</span> <span style="color:#993300; font-weight:bold;">DO</span> <span style="color:#993300; font-weight:bold;">I</span> <span style="color:#336699; font-weight:bold;">IS-PRIME?</span> <span style="color:#993300; font-weight:bold;">IF</span> <span style="color:#993300; font-weight:bold;">I</span> <span style="color:#CC6600; font-weight:bold;">.</span> <span style="color:#3D3D5C; font-weight:bold;">CR</span> <span style="color:#993300; font-weight:bold;">THEN</span> <span style="color:#993300; font-weight:bold;">LOOP</span> <span style="color:#993300; font-weight:bold;">;</span>
</pre>
We initialize a table of the prime numbers between 2 and 256, then a sieve that will store a flag for each number. 

Information for number *n* can be found in the sieve at position *n/8*, at bit *n%8*. We use bit masks to read and write that information. 

For example the byte at position 0 in the sieve is `10101100` meaning that numbers 2,3,5 and 7 are primes, while 0,1,4 and 6 are not.

To mark the number 4807 in the sieve, we find its position : 4807/8 = 600, and its bitmask 2^(4807%8) = 2^7 = 128 or 10000000. Then reading the byte at position 600, we *AND* it with a reverse of the bitmask (01111111), setting bit#7 to zero without changing the other flags at position 600.

Position #600 in the sieve is 00000010, meaning that in the range 4800 to 4807, only 4801 is prime.

After filling the whole sieve with ones, and eliminating numbers 0 and 1, we loop on the small primes table. For each prime *p*, starting with *p.p*, we mark the positions *p.p*, *p.p+p*, *p.p+2p*, and so on up to the sieve limit.

