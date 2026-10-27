namespace MathExtensions;

/// <summary>
/// Zestaw rozszerzonych metod matematycznych (liczby pierwsze, NWD, NWW)
/// z pełną obsługą przypadków brzegowych.
/// </summary>
public static class MathEx
{
    #region Liczby Pierwsze (Prime Numbers)

    /// <summary>
    /// Sprawdza, czy podana liczba całkowita jest liczbą pierwszą.
    /// Zwraca false dla liczb ujemnych, 0 i 1.
    /// Złożoność czasowa: O(sqrt(n)).
    /// </summary>
    public static bool IsPrime(int n)
    {
        if (n <= 1) return false;
        if (n <= 3) return true; // 2 i 3 są pierwsze
        if (n % 2 == 0 || n % 3 == 0) return false;

        // Sprawdzanie dzielników w postaci 6k ± 1
        for (int i = 5; (long)i * i <= n; i += 6)
        {
            if (n % i == 0 || n % (i + 2) == 0)
                return false;
        }

        return true;
    }

    /// <summary>
    /// Sprawdza, czy podana liczba 64-bitowa (long) jest liczbą pierwszą.
    /// Bezpieczna obsługa przed przepełnieniem zakresu.
    /// </summary>
    public static bool IsPrime(long n)
    {
        if (n <= 1) return false;
        if (n <= 3) return true;
        if (n % 2 == 0 || n % 3 == 0) return false;

        long limit = (long)Math.Sqrt(n);
        for (long i = 5; i <= limit; i += 6)
        {
            if (n % i == 0 || n % (i + 2) == 0)
                return false;
        }

        return true;
    }

    /// <summary>
    /// Zwraca listę wszystkich liczb pierwszych mniejszych lub równych podanemu limitowi.
    /// Wykorzystuje zoptymalizowane Sito Eratostenesa o złożoności O(n log log n).
    /// </summary>
    public static List<int> GetPrimesUpTo(int limit)
    {
        if (limit < 2) return [];

        var isPrime = new bool[limit + 1];
        Array.Fill(isPrime, true, 2, limit - 1);

        for (int p = 2; (long)p * p <= limit; p++)
        {
            if (isPrime[p])
            {
                for (int multiple = p * p; multiple <= limit; multiple += p)
                {
                    isPrime[multiple] = false;
                }
            }
        }

        var primes = new List<int>();
        for (int i = 2; i <= limit; i++)
        {
            if (isPrime[i])
            {
                primes.Add(i);
            }
        }

        return primes;
    }

    /// <summary>
    /// Zwraca listę liczb pierwszych z podanego przedziału [start, end].
    /// </summary>
    public static List<int> GetPrimesInRange(int start, int end)
    {
        if (start > end || end < 2) return [];

        int effectiveStart = Math.Max(start, 2);
        var primesUpToEnd = GetPrimesUpTo(end);

        return primesUpToEnd.Where(p => p >= effectiveStart).ToList();
    }

    /// <summary>
    /// Dokonuje rozkładu liczby dodatniej n na czynniki pierwsze (faktoryzacja).
    /// Np. dla 360 zwraca [2, 2, 2, 3, 3, 5].
    /// Dla liczb mniejszych lub równych 1 zwraca pustą listę.
    /// </summary>
    public static List<long> PrimeFactors(long n)
    {
        if (n <= 1) return [];

        var factors = new List<long>();

        while (n % 2 == 0)
        {
            factors.Add(2);
            n /= 2;
        }

        while (n % 3 == 0)
        {
            factors.Add(3);
            n /= 3;
        }

        long limit = (long)Math.Sqrt(n);
        for (long i = 5; i <= limit; i += 6)
        {
            while (n % i == 0)
            {
                factors.Add(i);
                n /= i;
                limit = (long)Math.Sqrt(n);
            }

            while (n % (i + 2) == 0)
            {
                factors.Add(i + 2);
                n /= (i + 2);
                limit = (long)Math.Sqrt(n);
            }
        }

        if (n > 1)
        {
            factors.Add(n);
        }

        return factors;
    }

    #endregion

    #region NWD (Największy Wspólny Dzielnik / GCD)

    /// <summary>
    /// Oblicza Największy Wspólny Dzielnik (NWD / GCD) dla dwóch liczb całkowitych 32-bitowych.
    /// Bezpiecznie obsługuje liczby ujemne, zero oraz przypadek brzegowy int.MinValue.
    /// </summary>
    public static int GCD(int a, int b)
    {
        // Używamy konwersji na 64-bitowy long, aby Math.Abs(int.MinValue) nie powodowało OverflowException
        long x = Math.Abs((long)a);
        long y = Math.Abs((long)b);

        while (y != 0)
        {
            long temp = y;
            y = x % y;
            x = temp;
        }

        if (x > int.MaxValue)
        {
            throw new OverflowException("Wynik NWD przekracza int.MaxValue (występuje tylko dla GCD(int.MinValue, int.MinValue)).");
        }

        return (int)x;
    }

    /// <summary>
    /// Oblicza Największy Wspólny Dzielnik (NWD / GCD) dla dwóch liczb całkowitych 64-bitowych.
    /// Bezpiecznie obsługuje liczby ujemne, zero oraz przypadek brzegowy long.MinValue.
    /// </summary>
    public static long GCD(long a, long b)
    {
        ulong uA = (a == long.MinValue) ? ((ulong)long.MaxValue + 1UL) : (ulong)Math.Abs(a);
        ulong uB = (b == long.MinValue) ? ((ulong)long.MaxValue + 1UL) : (ulong)Math.Abs(b);

        while (uB != 0)
        {
            ulong temp = uB;
            uB = uA % uB;
            uA = temp;
        }

        if (uA > (ulong)long.MaxValue)
        {
            throw new OverflowException("Wynik NWD przekracza long.MaxValue (występuje tylko dla GCD(long.MinValue, long.MinValue)).");
        }

        return (long)uA;
    }

    /// <summary>
    /// Oblicza Największy Wspólny Dzielnik (NWD) dla tablicy lub listy liczb całkowitych.
    /// Zawiera optymalizację: natychmiastowe przerwanie, gdy NWD osiągnie 1.
    /// </summary>
    public static int GCD(params int[] numbers)
    {
        if (numbers == null || numbers.Length == 0)
            throw new ArgumentException("Wymagana jest co najmniej jedna liczba do obliczenia NWD.", nameof(numbers));

        int result = numbers[0];
        for (int i = 1; i < numbers.Length; i++)
        {
            result = GCD(result, numbers[i]);
            if (result == 1) return 1; // Optymalizacja: mniejszego NWD niż 1 już nie będzie
        }

        return Math.Abs(result);
    }

    /// <summary>
    /// Oblicza Największy Wspólny Dzielnik (NWD) dla tablicy lub listy liczb 64-bitowych.
    /// </summary>
    public static long GCD(params long[] numbers)
    {
        if (numbers == null || numbers.Length == 0)
            throw new ArgumentException("Wymagana jest co najmniej jedna liczba do obliczenia NWD.", nameof(numbers));

        long result = numbers[0];
        for (int i = 1; i < numbers.Length; i++)
        {
            result = GCD(result, numbers[i]);
            if (result == 1) return 1;
        }

        return Math.Abs(result);
    }

    #endregion

    #region NWW (Najmniejsza Wspólna Wielokrotność / LCM)

    /// <summary>
    /// Oblicza Najmniejszą Wspólną Wielokrotność (NWW / LCM) dwóch liczb całkowitych.
    /// Dzielenie przed mnożeniem chroni przed przedwczesnym przepełnieniem (overflow).
    /// </summary>
    public static long LCM(long a, long b)
    {
        if (a == 0 || b == 0) return 0;

        long gcd = GCD(a, b);
        checked
        {
            // Dzielimy najpierw, aby zapobiec niepotrzebnemu przepełnieniu przy mnożeniu
            return (Math.Abs(a) / gcd) * Math.Abs(b);
        }
    }

    /// <summary>
    /// Oblicza Najmniejszą Wspólną Wielokrotność (NWW / LCM) dla zestawu liczb.
    /// </summary>
    public static long LCM(params long[] numbers)
    {
        if (numbers == null || numbers.Length == 0)
            throw new ArgumentException("Wymagana jest co najmniej jedna liczba do obliczenia NWW.", nameof(numbers));

        long result = numbers[0];
        for (int i = 1; i < numbers.Length; i++)
        {
            result = LCM(result, numbers[i]);
            if (result == 0) return 0;
        }

        return Math.Abs(result);
    }

    #endregion
}
