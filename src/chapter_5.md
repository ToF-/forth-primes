# An improved sieve program

We can improve our sieve program if we consider that

- except for number 2, all even bits in all positions are zero bits
- except for number 5, all bits corresponding to a multiple of 5 are zero bits

Why then store boolean information for these numbers ? The same question is valid of course for 3 and 7 (and so on), but taking those factors into account might complicate our scheme too much.

By eliminating the need to store information for multiples of 2 and multiples of 5 in the sieve, a single byte can store information for 20 numbers instead of 8.

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


To find if number N is prime, we first ask if N is even : if it's the case then N = 2 or a non-prime.
If N is not even, is it a multiple of 5 ? If yes, N = 5 or a non-prime.
If N is not a multiple of 2 or 5, we lookup the position N/20 in the sieve, and determine the bitmask corresponding N%20. Here N%20 is one of { 1, 3, 7, 9, 11, 13, 17, 19 }, so there are 8 bitmask possible : { 1, 10, 100, 1000, 10000, 100000, 1000000, 10000000 }:

For instance, with N = 4807, we examine position 240 and find 01100001, meaning that only 4801, 4813 and 4817 are prime numbers, while 4803, 4807, 4807, 4811 and 4819 are not. N%20 = 7, which is the value stored at bit #2. Bit #2 is zero, so 4807 is not prime.

For a quick mapping between N%20 and the corresponding bit mask, we initialize a table of bitmasks where only positions 1,3,7,9,11,13,17 and 19 are significant. The other positions are not to be searched, since we multiples of 2 and multiples of 5 are eliminated early in our search.

<pre style="color:#000000;background:#F2F2F2;"><span style="color:#669999; font-weight:bold;">\</span> <span style="color:#669999; font-weight:bold;">p-sieve.fs compuiting primes with improved sieve 
</span>
<span style="color:#3D3D5C; font-weight:bold;">REQUIRE</span> <span style="color:#800000; font-weight:bold;">small-primes.fs</span>

<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">SQUARED</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">n -- n² )</span>
    <span style="color:#009999; font-weight:bold;">DUP</span> <span style="color:#CC6600; font-weight:bold;">*</span> <span style="color:#993300; font-weight:bold;">;</span>

<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">IS-MULTIPLE?</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">n,m -- f )</span>
    <span style="color:#CC6600; font-weight:bold;">MOD</span> <span style="color:#CC6600; font-weight:bold;">0=</span> <span style="color:#993300; font-weight:bold;">;</span>

<span style="color:#800000; font-weight:bold;">1000</span> <span style="color:#336699; font-weight:bold;">SQUARED</span> <span style="color:#F07F00; font-weight:bold;">CONSTANT</span> <span style="color:#336699; font-weight:bold;">PRIME-LIMIT</span>

<span style="color:#336699; font-weight:bold;">PRIME-LIMIT</span> <span style="color:#800000; font-weight:bold;">20</span> <span style="color:#CC6600; font-weight:bold;">/</span> <span style="color:#F07F00; font-weight:bold;">CONSTANT</span> <span style="color:#336699; font-weight:bold;">SIEVE-SIZE</span>

<span style="color:#F07F00; font-weight:bold;">CREATE</span> <span style="color:#336699; font-weight:bold;">SIEVE</span> <span style="color:#336699; font-weight:bold;">SIEVE-SIZE</span> <span style="color:#CC3300; font-weight:bold;">ALLOT</span>

<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">NOR</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">f,g -- ! [f or g] )</span>
    <span style="color:#CC6600; font-weight:bold;">OR</span> <span style="color:#CC6600; font-weight:bold;">0=</span> <span style="color:#993300; font-weight:bold;">;</span>

<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">REVERSE</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">b -- b )</span>
    <span style="color:#800000; font-weight:bold;">255</span> <span style="color:#CC6600; font-weight:bold;">XOR</span> <span style="color:#993300; font-weight:bold;">;</span>

<span style="color:#800000; font-weight:bold;">160</span> <span style="color:#F07F00; font-weight:bold;">CONSTANT</span> <span style="color:#336699; font-weight:bold;">FLAGS/CELL</span>

<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">BITMASK</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">n -- v )</span>
    <span style="color:#800000; font-weight:bold;">1</span> <span style="color:#009999; font-weight:bold;">SWAP</span> <span style="color:#CC6600; font-weight:bold;">LSHIFT</span> <span style="color:#993300; font-weight:bold;">;</span>

<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">INIT-SIEVE-MASKS</span>
    <span style="color:#800000; font-weight:bold;">0</span>
    <span style="color:#800000; font-weight:bold;">160</span> <span style="color:#800000; font-weight:bold;">0</span> <span style="color:#993300; font-weight:bold;">DO</span>
        <span style="color:#993300; font-weight:bold;">I</span> <span style="color:#800000; font-weight:bold;">2</span> <span style="color:#336699; font-weight:bold;">IS-MULTIPLE?</span> <span style="color:#CC6600; font-weight:bold;">0=</span>
        <span style="color:#993300; font-weight:bold;">I</span> <span style="color:#800000; font-weight:bold;">5</span> <span style="color:#336699; font-weight:bold;">IS-MULTIPLE?</span> <span style="color:#CC6600; font-weight:bold;">0=</span> 
        <span style="color:#CC6600; font-weight:bold;">AND</span> <span style="color:#993300; font-weight:bold;">IF</span>
            <span style="color:#009999; font-weight:bold;">DUP</span> <span style="color:#336699; font-weight:bold;">BITMASK</span> <span style="color:#CC3300; font-weight:bold;">,</span>
            <span style="color:#CC6600; font-weight:bold;">1+</span>
        <span style="color:#993300; font-weight:bold;">ELSE</span>
            <span style="color:#800000; font-weight:bold;">0</span> <span style="color:#CC3300; font-weight:bold;">,</span>
        <span style="color:#993300; font-weight:bold;">THEN</span>
    <span style="color:#993300; font-weight:bold;">LOOP</span> <span style="color:#009999; font-weight:bold;">DROP</span> <span style="color:#993300; font-weight:bold;">;</span>

<span style="color:#F07F00; font-weight:bold;">CREATE</span> <span style="color:#336699; font-weight:bold;">SIEVE-MASKS</span>

<span style="color:#336699; font-weight:bold;">INIT-SIEVE-MASKS</span>

<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">SIEVE-POS</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">n -- addr )</span>
    <span style="color:#336699; font-weight:bold;">FLAGS/CELL</span> <span style="color:#CC6600; font-weight:bold;">/</span> <span style="color:#CC3300; font-weight:bold;">CELLS</span> <span style="color:#336699; font-weight:bold;">SIEVE</span> <span style="color:#CC6600; font-weight:bold;">+</span> <span style="color:#993300; font-weight:bold;">;</span>

<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">SIEVE-MASK</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">n -- bitmask )</span>
    <span style="color:#336699; font-weight:bold;">FLAGS/CELL</span> <span style="color:#CC6600; font-weight:bold;">MOD</span> <span style="color:#336699; font-weight:bold;">SIEVE-MASKS</span> <span style="color:#CC6600; font-weight:bold;">+</span> <span style="color:#CC3300; font-weight:bold;">@</span> <span style="color:#993300; font-weight:bold;">;</span>

<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">SIEVE!</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">n -- )</span>
    <span style="color:#009999; font-weight:bold;">DUP</span> <span style="color:#800000; font-weight:bold;">2</span> <span style="color:#336699; font-weight:bold;">IS-MULTIPLE?</span> <span style="color:#009999; font-weight:bold;">OVER</span> <span style="color:#800000; font-weight:bold;">5</span> <span style="color:#336699; font-weight:bold;">IS-MULTIPLE?</span> <span style="color:#336699; font-weight:bold;">NOR</span> <span style="color:#993300; font-weight:bold;">IF</span>
        <span style="color:#009999; font-weight:bold;">DUP</span> <span style="color:#336699; font-weight:bold;">SIEVE-MASK</span> <span style="color:#CC6600; font-weight:bold;">INVERT</span>
        <span style="color:#009999; font-weight:bold;">SWAP</span> <span style="color:#336699; font-weight:bold;">SIEVE-POS</span>
        <span style="color:#009999; font-weight:bold;">DUP</span> <span style="color:#CC3300; font-weight:bold;">@</span> <span style="color:#009999; font-weight:bold;">ROT</span> <span style="color:#CC6600; font-weight:bold;">AND</span> <span style="color:#009999; font-weight:bold;">SWAP</span> <span style="color:#CC3300; font-weight:bold;">!</span>
    <span style="color:#993300; font-weight:bold;">ELSE</span> <span style="color:#009999; font-weight:bold;">DROP</span> <span style="color:#993300; font-weight:bold;">THEN</span> <span style="color:#993300; font-weight:bold;">;</span>

<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">INIT-SIEVE</span>
    <span style="color:#336699; font-weight:bold;">SIEVE</span> <span style="color:#336699; font-weight:bold;">SIEVE-SIZE</span> <span style="color:#800000; font-weight:bold;">255</span> <span style="color:#CC3300; font-weight:bold;">FILL</span>
    <span style="color:#800000; font-weight:bold;">1</span> <span style="color:#336699; font-weight:bold;">SIEVE!</span>
    <span style="color:#800000; font-weight:bold;">SMALL-PRIMES-MAX</span> <span style="color:#800000; font-weight:bold;">1</span> <span style="color:#993300; font-weight:bold;">DO</span>
        <span style="color:#993300; font-weight:bold;">I</span> <span style="color:#800000; font-weight:bold;">NTH-PRIME</span> <span style="color:#009999; font-weight:bold;">DUP</span>
        <span style="color:#009999; font-weight:bold;">DUP</span> <span style="color:#336699; font-weight:bold;">SQUARED</span>
        <span style="color:#993300; font-weight:bold;">BEGIN</span>
            <span style="color:#009999; font-weight:bold;">DUP</span> <span style="color:#336699; font-weight:bold;">PRIME-LIMIT</span> <span style="color:#CC6600; font-weight:bold;">&lt;=</span> <span style="color:#993300; font-weight:bold;">WHILE</span>
            <span style="color:#009999; font-weight:bold;">DUP</span> <span style="color:#336699; font-weight:bold;">SIEVE!</span>
            <span style="color:#009999; font-weight:bold;">OVER</span> <span style="color:#CC6600; font-weight:bold;">+</span>
        <span style="color:#993300; font-weight:bold;">REPEAT</span>
        <span style="color:#009999; font-weight:bold;">2DROP</span> <span style="color:#009999; font-weight:bold;">DROP</span>
    <span style="color:#993300; font-weight:bold;">LOOP</span> <span style="color:#993300; font-weight:bold;">;</span>

<span style="color:#336699; font-weight:bold;">INIT-SIEVE</span>

<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">IS-PRIME?</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">n -- f )</span>
    <span style="color:#3D3D5C; font-weight:bold;">ASSERT(</span> <span style="color:#009999; font-weight:bold;">DUP</span> <span style="color:#336699; font-weight:bold;">PRIME-LIMIT</span> <span style="color:#CC6600; font-weight:bold;">&lt;=</span> <span style="color:#3D3D5C; font-weight:bold;">)</span>
    <span style="color:#3D3D5C; font-weight:bold;">ASSERT(</span> <span style="color:#009999; font-weight:bold;">DUP</span> <span style="color:#800000; font-weight:bold;">1</span> <span style="color:#CC6600; font-weight:bold;">&gt;</span> <span style="color:#3D3D5C; font-weight:bold;">)</span>
    <span style="color:#009999; font-weight:bold;">DUP</span> <span style="color:#800000; font-weight:bold;">2</span> <span style="color:#336699; font-weight:bold;">IS-MULTIPLE?</span> <span style="color:#993300; font-weight:bold;">IF</span> <span style="color:#800000; font-weight:bold;">2</span> <span style="color:#CC6600; font-weight:bold;">=</span>
    <span style="color:#993300; font-weight:bold;">ELSE</span>
        <span style="color:#009999; font-weight:bold;">DUP</span> <span style="color:#800000; font-weight:bold;">5</span> <span style="color:#336699; font-weight:bold;">IS-MULTIPLE?</span> <span style="color:#993300; font-weight:bold;">IF</span> <span style="color:#800000; font-weight:bold;">5</span> <span style="color:#CC6600; font-weight:bold;">=</span>
    <span style="color:#993300; font-weight:bold;">ELSE</span>
        <span style="color:#009999; font-weight:bold;">DUP</span> <span style="color:#336699; font-weight:bold;">SIEVE-MASK</span> <span style="color:#009999; font-weight:bold;">SWAP</span>
        <span style="color:#336699; font-weight:bold;">SIEVE-POS</span> <span style="color:#CC3300; font-weight:bold;">@</span> <span style="color:#009999; font-weight:bold;">SWAP</span> <span style="color:#CC6600; font-weight:bold;">AND</span>
    <span style="color:#993300; font-weight:bold;">THEN</span> <span style="color:#993300; font-weight:bold;">THEN</span> <span style="color:#993300; font-weight:bold;">;</span>

<span style="color:#F07F00; font-weight:bold;">CREATE</span> <span style="color:#336699; font-weight:bold;">#BIT-NUMBERS</span> <span style="color:#800000; font-weight:bold;">1</span> <span style="color:#CC3300; font-weight:bold;">C,</span> <span style="color:#800000; font-weight:bold;">3</span> <span style="color:#CC3300; font-weight:bold;">C,</span> <span style="color:#800000; font-weight:bold;">7</span> <span style="color:#CC3300; font-weight:bold;">C,</span> <span style="color:#800000; font-weight:bold;">9</span> <span style="color:#CC3300; font-weight:bold;">C,</span> <span style="color:#800000; font-weight:bold;">11</span> <span style="color:#CC3300; font-weight:bold;">C,</span> <span style="color:#800000; font-weight:bold;">13</span> <span style="color:#CC3300; font-weight:bold;">C,</span> <span style="color:#800000; font-weight:bold;">17</span> <span style="color:#CC3300; font-weight:bold;">C,</span> <span style="color:#800000; font-weight:bold;">19</span> <span style="color:#CC3300; font-weight:bold;">C,</span>

<span style="color:#669999; font-weight:bold;">\</span> <span style="color:#669999; font-weight:bold;">: PRIME-POS&gt;NUMBER ( n -- p )
</span><span style="color:#669999; font-weight:bold;">\</span> <span style="color:#669999; font-weight:bold;">    8 /MOD FLAGS/BYTE *
</span><span style="color:#669999; font-weight:bold;">\</span> <span style="color:#669999; font-weight:bold;">    SWAP #BIT-NUMBERS + C@ + ;
</span><span style="color:#669999; font-weight:bold;">\</span> <span style="color:#669999; font-weight:bold;">
</span><span style="color:#669999; font-weight:bold;">\</span> <span style="color:#669999; font-weight:bold;">: NEXT-PRIME-POS ( n -- n' )
</span><span style="color:#669999; font-weight:bold;">\</span> <span style="color:#669999; font-weight:bold;">    BEGIN
</span><span style="color:#669999; font-weight:bold;">\</span> <span style="color:#669999; font-weight:bold;">        1+ DUP 8 /MOD
</span><span style="color:#669999; font-weight:bold;">\</span> <span style="color:#669999; font-weight:bold;">        SIEVE + C@
</span><span style="color:#669999; font-weight:bold;">\</span> <span style="color:#669999; font-weight:bold;">        1 ROT LSHIFT AND
</span><span style="color:#669999; font-weight:bold;">\</span> <span style="color:#669999; font-weight:bold;">    UNTIL ;
</span><span style="color:#669999; font-weight:bold;">\</span> <span style="color:#669999; font-weight:bold;">
</span><span style="color:#669999; font-weight:bold;">\</span> <span style="color:#669999; font-weight:bold;">-2 CONSTANT PRIME-POS-2
</span><span style="color:#669999; font-weight:bold;">\</span> <span style="color:#669999; font-weight:bold;">-1 CONSTANT PRIME-POS-3
</span><span style="color:#669999; font-weight:bold;">\</span> <span style="color:#669999; font-weight:bold;"> 0 CONSTANT PRIME-POS-5
</span><span style="color:#669999; font-weight:bold;">\</span> <span style="color:#669999; font-weight:bold;">
</span><span style="color:#669999; font-weight:bold;">\</span> <span style="color:#669999; font-weight:bold;">: NEXT-PRIME ( c -- c',p)
</span><span style="color:#669999; font-weight:bold;">\</span> <span style="color:#669999; font-weight:bold;">         DUP PRIME-POS-2 = IF 1+ 2
</span><span style="color:#669999; font-weight:bold;">\</span> <span style="color:#669999; font-weight:bold;">    ELSE DUP PRIME-POS-3 = IF 1+ 3
</span><span style="color:#669999; font-weight:bold;">\</span> <span style="color:#669999; font-weight:bold;">    ELSE DUP PRIME-POS-5 = IF 1+ 5
</span><span style="color:#669999; font-weight:bold;">\</span> <span style="color:#669999; font-weight:bold;">    ELSE
</span><span style="color:#669999; font-weight:bold;">\</span> <span style="color:#669999; font-weight:bold;">        NEXT-PRIME-POS
</span><span style="color:#669999; font-weight:bold;">\</span> <span style="color:#669999; font-weight:bold;">        DUP PRIME-POS&gt;NUMBER
</span><span style="color:#669999; font-weight:bold;">\</span> <span style="color:#669999; font-weight:bold;">    THEN THEN THEN ;
</span><span style="color:#669999; font-weight:bold;">\</span> <span style="color:#669999; font-weight:bold;">
</span><span style="color:#669999; font-weight:bold;">\</span> <span style="color:#669999; font-weight:bold;">: .ALL-PRIMES ( n -- )
</span><span style="color:#669999; font-weight:bold;">\</span> <span style="color:#669999; font-weight:bold;">    &gt;R PRIME-POS-2
</span><span style="color:#669999; font-weight:bold;">\</span> <span style="color:#669999; font-weight:bold;">    BEGIN
</span><span style="color:#669999; font-weight:bold;">\</span> <span style="color:#669999; font-weight:bold;">        NEXT-PRIME
</span><span style="color:#669999; font-weight:bold;">\</span> <span style="color:#669999; font-weight:bold;">        DUP R@ &lt; WHILE
</span><span style="color:#669999; font-weight:bold;">\</span> <span style="color:#669999; font-weight:bold;">        . CR
</span><span style="color:#669999; font-weight:bold;">\</span> <span style="color:#669999; font-weight:bold;">    REPEAT
</span><span style="color:#669999; font-weight:bold;">\</span> <span style="color:#669999; font-weight:bold;">    DROP R&gt; DROP ;
</span>
<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">.PRIMES</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">n -- )</span>
    <span style="color:#CC6600; font-weight:bold;">1+</span> <span style="color:#800000; font-weight:bold;">2</span> <span style="color:#993300; font-weight:bold;">DO</span> <span style="color:#993300; font-weight:bold;">I</span> <span style="color:#336699; font-weight:bold;">IS-PRIME?</span> <span style="color:#993300; font-weight:bold;">IF</span> <span style="color:#993300; font-weight:bold;">I</span> <span style="color:#CC6600; font-weight:bold;">.</span> <span style="color:#3D3D5C; font-weight:bold;">CR</span> <span style="color:#993300; font-weight:bold;">THEN</span> <span style="color:#993300; font-weight:bold;">LOOP</span> <span style="color:#993300; font-weight:bold;">;</span>

<span style="color:#F07F00; font-weight:bold;">:</span> <span style="color:#336699; font-weight:bold;">PRIME-COUNT</span> <span style="color:#669999; font-weight:bold;">(</span> <span style="color:#669999; font-weight:bold;">n -- n )</span>
    <span style="color:#800000; font-weight:bold;">0</span> <span style="color:#009999; font-weight:bold;">SWAP</span>
    <span style="color:#CC6600; font-weight:bold;">1+</span> <span style="color:#800000; font-weight:bold;">2</span> <span style="color:#993300; font-weight:bold;">DO</span>
        <span style="color:#993300; font-weight:bold;">I</span> <span style="color:#336699; font-weight:bold;">IS-PRIME?</span> <span style="color:#993300; font-weight:bold;">IF</span>
            <span style="color:#CC6600; font-weight:bold;">1+</span>
        <span style="color:#993300; font-weight:bold;">THEN</span>
    <span style="color:#993300; font-weight:bold;">LOOP</span> <span style="color:#993300; font-weight:bold;">;</span>
</pre>
