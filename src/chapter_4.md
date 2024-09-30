# Finding primes by eliminating composites

The idea of using primes to find primes can be improved further with the Sieve of Eratosthenes : since we know that a number P is prime, we can alreday mark P.P, (P+1)P, (P+2)P, (P+3)P, … as non primes. Here's an instance of this strategy with primes 2 to 7 :

| prime | marked composites |
| ----- | ------------------|
| 2    | { 4, 6, 8, 10, 12, 14, …, 96, 98, 100 }|
| 3    | { 9, 15, 21, 27, 33, 36, 39, 45, 51, 57, 63, 66, 69, 75, 81, 87, 93, 99 }|
| 5    | { 25, 35, 55, 65, 85, 95} |
| 7    | { 49, 77, 91} |

The remaining numbers smaller than 100 that are non marked in this process: 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97 are all prime numbers.

The Sieve of Eratosthenes, used with primes from 2 to P, can be used to determines primes up to P². What we need is a table storing a boolean value for each number in the range.




