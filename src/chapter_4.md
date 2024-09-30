# Finding primes by eliminating composites

The idea of using primes to find primes can be improved further with the Sieve of Eratosthenes : since we know that a number P is prime, we can alreday mark P.P, (P+1)P, (P+2)P, (P+3)P, … as non primes. Here's an instance of this strategy with primes 2 to 7 :

| prime | marked composites |
| ----- | ------------------|
| 2    | { 4, 6, 8, 10, 12, 14, …, 96, 98, 100 }|
| 3    | { 9, 15, 21, 27, 33, 36, 39, 45, 51, 57, 63, 66, 69, 75, 81, 87, 93, 99 }|
| 5    | { 25, 35, 55, 65, 85, 95} |
| 7    | { 49, 77, 91} |

The remaining non marked numbers smaller than 100 are 11,1



