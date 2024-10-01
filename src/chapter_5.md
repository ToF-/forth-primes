# An improved sieve program

We can improve our sieve program if we consider that

- except for number 2, all even bits in all positions are zero bits
- except for number 5, all bits corresponding to a multiple of 5 are zero bits

Why then store boolean information for these numbers ? The same question is valid of course for 3 and 7 (and so on), but taking those factors into account might complicate our scheme too much.

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

If N is nor a multiple of 2 nor a multiple of 5, we lookup the position N/160 in the sieve, and we examine the bit B where B depends on the value N%160 :

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

The matching relies on the fact that we never look for a multiple of 2 or a multiple of 5 in this table.


For instance, with N = 4807, we examine position 240 and find 01100001, meaning that only 4801, 4813 and 4817 are prime numbers, while 4803, 4807, 4807, 4811 and 4819 are not. N%20 = 7, which is the value stored at bit #2. Bit #2 is zero, so 4807 is not prime.

