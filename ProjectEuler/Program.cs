List<long> fibs = new();

long N = (long)1e18;
BuildFibs(N);

long result = CountTotalZeckendorfOnes(N);
Console.WriteLine(result);

void BuildFibs(long N)
{
    fibs.Add(1);
    fibs.Add(2);

    while (true)
    {
        long next = fibs[^1] + fibs[^2];
        if (next > N) break;
        fibs.Add(next);
    }
}

long CountTotalZeckendorfOnes(long N)
{
    int k = fibs.Count;
    long[,] dpCount = new long[k + 1, 2];
    long[,] dpOnes  = new long[k + 1, 2];

    dpCount[0, 0] = 1;

    for (int i = 0; i < k; i++)
    {
        for (int prev = 0; prev <= 1; prev++)
        {
            long count = dpCount[i, prev];
            long ones  = dpOnes[i, prev];
            if (count == 0) continue;

            // place 0
            dpCount[i + 1, 0] += count;
            dpOnes[i + 1, 0]  += ones;

            // place 1 (only if previous was 0)
            if (prev == 0)
            {
                dpCount[i + 1, 1] += count;
                dpOnes[i + 1, 1]  += ones + count;
            }
        }
    }

    // Fibonacci representation of N
    long totalOnes = 0;
    int prevBit = 0;

    for (int i = k - 1; i >= 0; i--)
    {
        if (fibs[i] <= N)
        {
            // count all numbers with 0 at this position
            totalOnes += dpOnes[i, 0] + dpOnes[i, 1];

            if (prevBit == 1) break;

            N -= fibs[i];
            totalOnes += 1;
            prevBit = 1;
        }
        else
        {
            prevBit = 0;
        }
    }

    return totalOnes;
}