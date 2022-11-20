public class HashTableExercises
{
    // O(n)
    public static int MostFrequent(int[] numbers)
    {
        var dic = new Dictionary<int, int>();

        foreach (var n in numbers)
            if (dic.ContainsKey(n))
                dic[n]++;
            else
                dic.Add(n, 1);

        var max = -1;
        var key = -1;
        foreach (var p in dic)
            if (p.Value > max)
            {
                max = p.Value;
                key = p.Key;
            }

        return key;
    }

    // O(n)
    public static int CountPairsWithDiff(int[] numbers, int k)
    {
        // For a given number (a) and difference (diff), number (b) can be:
        //
        // b = a + diff
        // b = a - diff
        //
        // We can iterate over our array of numbers, and for each number,
        // check to see if we have (current + diff) or (current - diff).
        // But looking up items in an array is an O(n) operation. With this
        // algorithm, we need two nested loops (one to pick a,
        // and the other to find b). This will be an O(n^2) operation.
        //
        // We can optimize this by using a set. Sets are like hash tables
        // but they only store keys. We can look up a number in constant time.
        // No need to iterate the array to find it.

        // So, we start by adding all the numbers to a set for quick look up.
        var set = new HashSet<int>();

        foreach (var n in numbers)
            set.Add(n);

        // Now, we iterate over the array of numbers one more time,
        // and for each number check to see if we have (a + diff) or
        // (a - diff) in our set.
        //
        // Once we're done, we should remove this number from our set
        // so we don't double count it.
        int count = 0;
        foreach (var n in numbers)
        {
            if (set.Contains(n - k))
                count++;
            if (set.Contains(n + k))
                count++;
            set.Remove(n);
        }

        return count;
    }

    // On(n) - This is more efficient,
    // because we don't need to iterate the whole array
    public static int[]? twoSum(int[] numbers, int target)
    {
        var dic = new Dictionary<int, int>();

        for (int i = 0; i < numbers.Length; i++)
        {
            var ans = target - numbers[i];
            if (dic.ContainsKey(ans))
                return new int[] { dic[ans], i };

            dic.Add(numbers[i], i);
        }

        return null;
    }

    // O(n) - The above one is more efficient
    public static int[] twoSum2(int[] numbers, int target)
    {
        var set = new HashSet<int>();
        foreach (var n in numbers)
            set.Add(target - n);

        int[] result = new int[2];
        var index = 1;
        for (var i = 0; i < numbers.Length; i++)
        {
            if (set.Contains(numbers[i]))
                result[index--] = i;

            if (index < 0)
                break;
        }

        return result;
    }
}