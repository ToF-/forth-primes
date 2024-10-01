# An improved sieve program

We can improve our sieve program if we consider that

- except for number 2, all even bits in all positions are zero bits
- except for number 5, all bits corresponding to a multiple of 5 are zero bits

Why then store boolean information for these numbers ? These values will always be false. The same question is valid of course for 3 and 7 (and so on), but taking those factors into account might complicate our scheme too much.

By eliminating the need to store information for multiples of 2 and multiples of 5 in the sieve, a single byte can store information for 20 numbers instead of 8. A 64 bit cell can store information for 160 numbers.

For example bits 0 to 7 at position 1 in the sieve gives information for numbers 21 to 39 respectively: 01011010

| bit | number | value | info |
| --- | ------ | ----- | ---- |
| 0   | 21     |  0    | not prime |
| 1   | 23     |  1    | prime |
| 2   | 27     |  0    | not prime |
| 3   | 29     |  1    | prime |
| 4   | 31     |  1    | prime |
| 5   | 33     |  0    | not prime |
| 6   | 37     |  1    | prime |
| 7   | 39     |  0    | not prime |

To find if number N is prime, we first ask if N is even : if it's the case then N = 2 or a non-prime. If N is not even, is it a multiple of 5 ? If yes, N = 5 or a non-prime.

If N is not a multiple of 2 nor a multiple of 5, we lookup the position N/160 in the sieve, and we examine the bit B where B depends on the value N%160 :

| N%160 |  b |
| ----- | -- |
|   1   |  0 |
|   3   |  1 |
|   7   |  2 |
|   9   |  3 |
|  11   |  4 |
|  …    |  … |
| 149   | 59 |
| 151   | 60 |
| 153   | 61 |
| 157   | 62 |
| 159   | 63 |

The correctess of this matching relies on the fact that we never look for a multiple of 2 or a multiple of 5 in this table.

We start by defining some utility words :
<pre><span style="color:#669999; font-weight:bold;">\</span> <span style="color:#669999; font-weight:bold;">p-sieve.fs compuiting primes with improved sieve 
</span>
<span style="color:#3D3D5C; font-weight:bold;">REQUIRE</span> <span style="color:#800000; font-weight:bold;">small-primes.fs</span>

<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">SQUARED</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">n -- n² )</span>
    <span style="color:#009999; font-weight:bold;">DUP</span> <span style="color:#CC6600; font-weight:bold;">*</span> <span style="color:#993300; font-weight:bold;">;</span>

<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">IS-MULTIPLE?</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">n,m -- f )</span>
    <span style="color:#CC6600; font-weight:bold;">MOD</span> <span style="color:#CC6600; font-weight:bold;">0=</span> <span style="color:#993300; font-weight:bold;">;</span>

<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">NOT-2-OR-5-MULTIPLE?</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">n -- f )</span>
    <span style="color:#009999; font-weight:bold;">DUP</span> <span style="color:#800000; font-weight:bold;">2</span> <span style="color:#CC6600; font-weight:bold;">MOD</span> <span style="color:#993300; font-weight:bold;">IF</span>
        <span style="color:#800000; font-weight:bold;">5</span> <span style="color:#CC6600; font-weight:bold;">MOD</span>
    <span style="color:#993300; font-weight:bold;">ELSE</span>
        <span style="color:#009999; font-weight:bold;">DROP</span> <span style="color:#CC6600; font-weight:bold;">FALSE</span>
    <span style="color:#993300; font-weight:bold;">THEN</span> <span style="color:#993300; font-weight:bold;">;</span>

<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">REVERSE</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">b -- b )</span>
    <span style="color:#800000; font-weight:bold;">255</span> <span style="color:#CC6600; font-weight:bold;">XOR</span> <span style="color:#993300; font-weight:bold;">;</span>

<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">BITMASK</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">n -- v )</span>
    <span style="color:#800000; font-weight:bold;">1</span> <span style="color:#009999; font-weight:bold;">SWAP</span> <span style="color:#CC6600; font-weight:bold;">LSHIFT</span> <span style="color:#993300; font-weight:bold;">;</span>
</pre>
Then we define the `SIEVE` table, which will store information for primes below 1000000. Finding the position of a number in the sieve is done by calculating the offset of its cell. A cell can store flags for 160 (odd and non-multiples of 5) numbers and occupies (`CELL`) 8 bytes. Hence P = S+⌊N/160]x8 where S is the base address of the `SIEVE` table.

The bitmask to apply in order to extract the bit value is 2^B.
Where B = ⌊M/10⌋x4 + P/2 where P = (M%10) if M ≤ 5, (M%10)-2 if M > 5,
and M = N%160.
<pre>
<span style="color:#800000; font-weight:bold;">1000</span> <span style="color:#336699; font-weight:bold;">SQUARED</span> <span style="color:#F07F00; font-weight:bold;">CONSTANT</span> <span style="color:#336699; font-weight:bold;">PRIME-LIMIT</span>
<span style="color:#800000; font-weight:bold;">160</span> <span style="color:#F07F00; font-weight:bold;">CONSTANT</span> <span style="color:#336699; font-weight:bold;">FLAGS/CELL</span>

<span style="color:#336699; font-weight:bold;">PRIME-LIMIT</span> <span style="color:#336699; font-weight:bold;">FLAGS/CELL</span> <span style="color:#CC6600; font-weight:bold;">/</span> <span style="color:#CC3300; font-weight:bold;">CELLS</span> <span style="color:#F07F00; font-weight:bold;">CONSTANT</span> <span style="color:#336699; font-weight:bold;">SIEVE-SIZE</span>

<span style="color:#F07F00; font-weight:bold;">CREATE</span> <span style="color:#336699; font-weight:bold;">SIEVE</span> <span style="color:#336699; font-weight:bold;">SIEVE-SIZE</span> <span style="color:#CC3300; font-weight:bold;">ALLOT</span>

<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">SIEVE-POS</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">n -- addr )</span>
    <span style="color:#336699; font-weight:bold;">FLAGS/CELL</span> <span style="color:#CC6600; font-weight:bold;">/</span> <span style="color:#CC3300; font-weight:bold;">CELLS</span> <span style="color:#336699; font-weight:bold;">SIEVE</span> <span style="color:#CC6600; font-weight:bold;">+</span> <span style="color:#993300; font-weight:bold;">;</span>

<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">SIEVE-MASK</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">n -- bitmask )</span>
    <span style="color:#336699; font-weight:bold;">FLAGS/CELL</span> <span style="color:#CC6600; font-weight:bold;">MOD</span>
    <span style="color:#800000; font-weight:bold;">10</span> <span style="color:#CC6600; font-weight:bold;">/MOD</span> <span style="color:#800000; font-weight:bold;">4</span> <span style="color:#CC6600; font-weight:bold;">*</span>
    <span style="color:#009999; font-weight:bold;">SWAP</span> <span style="color:#009999; font-weight:bold;">DUP</span> <span style="color:#800000; font-weight:bold;">5</span> <span style="color:#CC6600; font-weight:bold;">&gt;</span> <span style="color:#993300; font-weight:bold;">IF</span> <span style="color:#800000; font-weight:bold;">2</span> <span style="color:#CC6600; font-weight:bold;">-</span> <span style="color:#993300; font-weight:bold;">THEN</span>
    <span style="color:#CC6600; font-weight:bold;">2/</span> <span style="color:#CC6600; font-weight:bold;">+</span> <span style="color:#336699; font-weight:bold;">BITMASK</span> <span style="color:#993300; font-weight:bold;">;</span>

<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">SIEVE!</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">n -- )</span>
    <span style="color:#009999; font-weight:bold;">DUP</span> <span style="color:#336699; font-weight:bold;">SIEVE-MASK</span> <span style="color:#CC6600; font-weight:bold;">INVERT</span>
    <span style="color:#009999; font-weight:bold;">SWAP</span> <span style="color:#336699; font-weight:bold;">SIEVE-POS</span>
    <span style="color:#009999; font-weight:bold;">DUP</span> <span style="color:#CC3300; font-weight:bold;">@</span> <span style="color:#009999; font-weight:bold;">ROT</span> <span style="color:#CC6600; font-weight:bold;">AND</span> <span style="color:#009999; font-weight:bold;">SWAP</span> <span style="color:#CC3300; font-weight:bold;">!</span> <span style="color:#993300; font-weight:bold;">;</span>
</pre>
Marking a number as composite is done by finding its position and bitmask in the sieve, and setting its bit to zero by `AND`ing the inverted mask to the current cell value.

To initialize the whole sieve, we start filling it with ones, mark the number 1 as composite, and then proceed to mark all the multiples of the small primes P², P²+P, P²+2P… starting with 3.

