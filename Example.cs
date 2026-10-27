using System;
using System.Collections.Generic;
using MathExtensions;

namespace MathExApp;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("==================================================");
        Console.WriteLine("           MathEx - Demonstracja i Testy          ");
        Console.WriteLine("==================================================\n");

        RunPrimeTests();
        RunGcdTests();
        RunLcmTests();

        Console.WriteLine("\n==================================================");
        Console.WriteLine("  Wszystkie testy i przypadki brzegowe zaliczone! ");
        Console.WriteLine("==================================================");
    }

    private static void RunPrimeTests()
    {
        Console.WriteLine("--- [1] Liczby Pierwsze (Primes) ---");

        // Przypadki brzegowe
        Assert(!MathEx.IsPrime(-10), "Liczba ujemna -10 nie jest pierwsza");
        Assert(!MathEx.IsPrime(-1),  "Liczba ujemna -1 nie jest pierwsza");
        Assert(!MathEx.IsPrime(0),   "0 nie jest pierwsze");
        Assert(!MathEx.IsPrime(1),   "1 nie jest pierwsze");
        Assert(MathEx.IsPrime(2),    "2 jest liczbą pierwszą (jedyna parzysta)");
        Assert(MathEx.IsPrime(3),    "3 jest liczbą pierwszą");
        Assert(!MathEx.IsPrime(4),   "4 nie jest pierwsze");
        Assert(MathEx.IsPrime(997),  "997 jest liczbą pierwszą");
        Assert(MathEx.IsPrime(104729L), "104729 jest liczbą pierwszą (long)");

        Console.WriteLine("  ✓ IsPrime: Poprawnie obsłużono liczby ujemne, 0, 1, 2 oraz duże liczby.");

        // Sito Eratostenesa
        List<int> primesTo30 = MathEx.GetPrimesUpTo(30);
        Console.WriteLine($"  Liczby pierwsze do 30: [{string.Join(", ", primesTo30)}]");
        Assert(primesTo30.SequenceEqual([2, 3, 5, 7, 11, 13, 17, 19, 23, 29]), "Sito do 30 zgodne z oczekiwaniem");

        List<int> primesInRange = MathEx.GetPrimesInRange(10, 25);
        Console.WriteLine($"  Liczby pierwsze w przedziale [10, 25]: [{string.Join(", ", primesInRange)}]");
        Assert(primesInRange.SequenceEqual([11, 13, 17, 19, 23]), "Przedział [10, 25] zgodny z oczekiwaniem");

        // Rozkład na czynniki pierwsze
        List<long> factors360 = MathEx.PrimeFactors(360);
        Console.WriteLine($"  Czynniki pierwsze dla 360: {string.Join(" * ", factors360)}");
        Assert(factors360.SequenceEqual([2, 2, 2, 3, 3, 5]), "Faktoryzacja 360 poprawna");

        List<long> factorsEdge = MathEx.PrimeFactors(1);
        Assert(factorsEdge.Count == 0, "Faktoryzacja dla 1 zwraca pustą listę");
    }

    private static void RunGcdTests()
    {
        Console.WriteLine("\n--- [2] NWD (Największy Wspólny Dzielnik / GCD) ---");

        // Standardowe przypadki
        Assert(MathEx.GCD(48, 18) == 6, "NWD(48, 18) == 6");
        Assert(MathEx.GCD(54, 24) == 6, "NWD(54, 24) == 6");
        Console.WriteLine($"  NWD(48, 18) = {MathEx.GCD(48, 18)}");

        // Przypadki brzegowe z zerem
        Assert(MathEx.GCD(0, 15) == 15, "NWD(0, 15) == 15");
        Assert(MathEx.GCD(15, 0) == 15, "NWD(15, 0) == 15");
        Assert(MathEx.GCD(0, 0) == 0,   "NWD(0, 0) == 0");
        Console.WriteLine("  ✓ Zero: NWD(0, 15) = 15, NWD(0, 0) = 0");

        // Przypadki ujemne
        Assert(MathEx.GCD(-24, 36) == 12, "NWD(-24, 36) == 12");
        Assert(MathEx.GCD(-24, -36) == 12, "NWD(-24, -36) == 12");
        Console.WriteLine("  ✓ Liczby ujemne: NWD(-24, -36) = 12 (wynik zawsze dodatni)");

        // Pułapka int.MinValue (Math.Abs(int.MinValue) wyrzucałby błąd)
        int gcdWithMin = MathEx.GCD(int.MinValue, 16);
        Assert(gcdWithMin == 16, "NWD(int.MinValue, 16) == 16");
        Console.WriteLine($"  ✓ int.MinValue: NWD(int.MinValue, 16) = {gcdWithMin} (brak przepełnienia!)");

        // NWD dla wielu liczb
        int multiGcd = MathEx.GCD(100, 75, 25, 50);
        Assert(multiGcd == 25, "NWD(100, 75, 25, 50) == 25");
        Console.WriteLine($"  NWD(100, 75, 25, 50) = {multiGcd}");
    }

    private static void RunLcmTests()
    {
        Console.WriteLine("\n--- [3] NWW (Najmniejsza Wspólna Wielokrotność / LCM) ---");

        Assert(MathEx.LCM(12, 18) == 36, "NWW(12, 18) == 36");
        Console.WriteLine($"  NWW(12, 18) = {MathEx.LCM(12, 18)}");

        // Przypadki brzegowe
        Assert(MathEx.LCM(0, 10) == 0, "NWW(0, 10) == 0");
        Assert(MathEx.LCM(-6, 8) == 24, "NWW(-6, 8) == 24");
        Console.WriteLine("  ✓ Liczby ujemne i zero dla NWW obsłużone poprawnie");

        long multiLcm = MathEx.LCM(4, 5, 6);
        Assert(multiLcm == 60, "NWW(4, 5, 6) == 60");
        Console.WriteLine($"  NWW(4, 5, 6) = {multiLcm}");
    }

    private static void Assert(bool condition, string message)
    {
        if (!condition)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"  ✗ BŁĄD ASERCJI: {message}");
            Console.ResetColor();
            throw new Exception($"Test nie powiódł się: {message}");
        }
    }
}
