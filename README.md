A lightweight, optimized, and fault-tolerant mathematical extension library in C# (.NET 10).
The project addresses common algorithmic problems and pitfalls related to prime numbers, GCD (Greatest Common Divisor), and LCM (Least Common Multiple).
🚀 Implemented Features
1. Prime Numbers
MathEx.IsPrime(int n) and MathEx.IsPrime(long n)
An algorithm with \(O(\sqrt{n})\) complexity that checks candidates in the form of \(6k \pm 1\).
Edge cases:
Numbers \(\le 1\) (\(0\), \(1\), and negative numbers) return false.
\(2\) and \(3\) return true.
The loop is protected against integer overflow for large long numbers.
MathEx.GetPrimesUpTo(int limit)
An efficient Sieve of Eratosthenes with \(O(n \log \log n)\) complexity.
MathEx.GetPrimesInRange(int start, int end)
Returns a list of prime numbers within a specified, inclusive range.
MathEx.PrimeFactors(long n)
Prime factorization (e.g., returns [2, 2, 2, 3, 3, 5] for \(360\)).
2. GCD – Greatest Common Divisor
MathEx.GCD(int a, int b) and MathEx.GCD(long a, long b)
Iterative Euclidean algorithm (no risk of recursion stack overflow).
Negative value safety: the GCD result is always non-negative (GCD(-24, -36) == 12).
Zero handling: GCD(0, 15) == 15, GCD(0, 0) == 0.
The int.MinValue pitfall: Standard Math.Abs(int.MinValue) in C# throws an OverflowException (since the absolute value of \(-2^{31}\) does not fit into a standard positive int). This has been safely handled in MathEx.
MathEx.GCD(params int[] numbers) and MathEx.GCD(params long[] numbers)
GCD for an arbitrary number of arguments, featuring an early-exit optimization (if the current GCD reaches \(1\)).
3. LCM – Least Common Multiple
MathEx.LCM(long a, long b) and MathEx.LCM(params long[] numbers)
Calculated using the formula: \(\frac{\vert{}a\vert{}}{\text{GCD}(a, b)} \cdot \vert{}b\vert{}\).
Division before multiplication prevents unnecessary arithmetic overflow.
🛠️ Running the Examples
Simply execute the following command in the project directory:
dotnet run
This will run the program from the Example.cs file, which tests all methods and verifies edge-case assertions.
