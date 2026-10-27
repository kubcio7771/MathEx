# MathEx

Lekka, zoptymalizowana i odporna na błędy biblioteka rozszerzeń matematycznych w C# (.NET 10).

Projekt rozwiązuje częste problemy i pułapki algorytmiczne związane z liczbami pierwszymi, NWD (Największym Wspólnym Dzielnikiem) oraz NWW (Najmniejszą Wspólną Wielokrotnością).

---

## 🚀 Zaimplementowane funkcjonalności

### 1. Liczby pierwsze (`Prime Numbers`)
- `MathEx.IsPrime(int n)` oraz `MathEx.IsPrime(long n)`
  - Algorytm o złożoności $O(\sqrt{n})$ sprawdzający kandydatów w postaci $6k \pm 1$.
  - **Przypadki brzegowe**:
    - Liczby $\le 1$ ($0$, $1$ oraz ujemne) zwracają `false`.
    - $2$ i $3$ zwracają `true`.
    - Pętla jest chroniona przed przepełnieniem (integer overflow) przy dużych liczbach `long`.
- `MathEx.GetPrimesUpTo(int limit)`
  - Wydajne **Sito Eratostenesa** o złożoności $O(n \log \log n)$.
- `MathEx.GetPrimesInRange(int start, int end)`
  - Zwraca listę liczb pierwszych w zadanym przedziale obustronnie domkniętym.
- `MathEx.PrimeFactors(long n)`
  - Rozkład liczby na czynniki pierwsze (np. dla $360$ zwraca `[2, 2, 2, 3, 3, 5]`).

---

### 2. NWD – Największy Wspólny Dzielnik (`GCD`)
- `MathEx.GCD(int a, int b)` oraz `MathEx.GCD(long a, long b)`
  - Iteracyjny **algorytm Euklidesa** (brak ryzyka przepełnienia stosu rekurencji).
  - **Bezpieczeństwo wartości ujemnych**: wynik NWD jest zawsze nieujemny (`GCD(-24, -36) == 12`).
  - **Obsługa zera**: `GCD(0, 15) == 15`, `GCD(0, 0) == 0`.
  - **Pułapka `int.MinValue`**: Standardowe `Math.Abs(int.MinValue)` w C# wyrzuca `OverflowException` (ponieważ wartość bezwzględna $-2^{31}$ nie mieści się w standardowym dodatnim `int`). W `MathEx` zostało to bezpiecznie obsłużone.
- `MathEx.GCD(params int[] numbers)` oraz `MathEx.GCD(params long[] numbers)`
  - NWD dla dowolnej liczby argumentów, z optymalizacją wczesnego wyjścia (jeśli bieżący NWD osiągnie $1$).

---

### 3. NWW – Najmniejsza Wspólna Wielokrotność (`LCM`)
- `MathEx.LCM(long a, long b)` oraz `MathEx.LCM(params long[] numbers)`
  - Obliczane według wzoru: $\frac{|a|}{\text{NWD}(a, b)} \cdot |b|$.
  - Dzielenie przed mnożeniem zapobiega niepotrzebnemu przepełnieniu arytmetycznemu (`overflow`).

---

## 🛠️ Uruchomienie przykładów

Wystarczy w katalogu projektu wykonać polecenie:

```bash
dotnet run
```

Uruchomi ono program z pliku [Example.cs](Example.cs), który przetestuje wszystkie metody oraz zweryfikuje asercje przypadków brzegowych.