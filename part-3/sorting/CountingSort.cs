public class CountingSort
{
    public static void Sort(int[] arr, int max)
    {
        var counts = new int[max + 1];

        foreach (var item in arr)
            counts[item]++;
        
        var j = 0;
        for (var i = 0; i <= max; i++)
            while (counts[i]-- > 0)
                arr[j++] = i;
    }
}