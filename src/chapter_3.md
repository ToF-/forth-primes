# Finding primes with a sieve

An efficient way to find primes is to use the Sieve of Eratosthenes algorithm. For each prime number *p* that is already known, we mark the numbers *2p*, *3p*, …, *np* as composite. After we have marked all composite numbers from 2.2 to *kp* all the remaining unkmarked numbers are known to be prime numbers.

We could have each number in the sieve take one bit of information, but we can save space by not storing information for multiples of 2 and 5. If a number is a multiple of 2, this number is either 2 or a composite. The same goes for 5. We could apply that rule for multiples of 3, but that would make the storing more complicated. 

Thus for each byte at position *n* in the sieve we can store information about *n+1*, *n+3*, *n+7*, *n+9*, *n+11*, *n+13*, *n+17* and *n+19*. A sieve of 1000000 numbers occupy only 50000 bytes. A mapping between numbers and bitmask will help write and retrieve the information. 

For instance the the byte at position 0 of the sieve `11110110` meaning that number 19,17,13,11,7 and 3 are primes. Flags for numbers 0,2,4,5,6,8,10,13,14,15,16,18 are not stored. At position 1 of the sieve, `01011010` means that only 37,31,29 and 23 are primes.

