public class BucketSort
{
    public static void Sort(int[] arr, int numberOfBuckets = 3)
    {
        var i = 0;
        foreach (var bucket in CreateBuckets(arr, numberOfBuckets))
        {
            bucket.Sort();
            foreach (var item in bucket)
                arr[i++] = item;
        }
    }

    private static List<List<int>> CreateBuckets(int[] arr, int numberOfBuckets)
    {
        var buckets = new List<List<int>>();
        for (var i = 0; i < numberOfBuckets; i++)
            buckets.Add(new List<int>());

        foreach (var item in arr)
            buckets.ElementAt(item / numberOfBuckets < numberOfBuckets
                ? item / numberOfBuckets
                : numberOfBuckets - 1).Add(item);

        return buckets;
    }
}