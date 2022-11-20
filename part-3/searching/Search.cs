public class Search
{
    public static int LinearSearch(int[] arr, int target)
    {
        for (var i = 0; i < arr.Length; i++)
            if (arr[i] == target)
                return i;

        return -1;
    }

    public static int BinarySearch(int[] arr, int target)
    {
        int left = 0, right = arr.Length - 1;

        while (left <= right)
        {
            var middle = (left + right) / 2;

            if (arr[middle] == target)
                return middle;

            if (arr[middle] > target)
                right = middle - 1;
            else
                left = middle + 1;
        }

        return -1;
    }

    public static int BinarySearchRecursive(int[] arr, int target)
    {
        return BinarySearchRecursive(arr, target, 0, arr.Length - 1);
    }

    private static int BinarySearchRecursive(int[] arr, int target, int left, int right)
    {
        if (left > right)
            return -1;

        var middle = (left + right) / 2;

        if (arr[middle] == target)
            return middle;

        if (arr[middle] > target)
            return BinarySearchRecursive(arr, target, left, middle - 1);

        return BinarySearchRecursive(arr, target, middle + 1, right);
    }

    public static int TernarySearch(int[] arr, int target)
    {
        return TernarySearch(arr, target, 0, arr.Length - 1);
    }

    private static int TernarySearch(int[] arr, int target, int left, int right)
    {
        if (left > right)
            return -1;

        var partitionSize = (right - left) / 3;
        var mid1 = left + partitionSize;
        var mid2 = right - partitionSize;

        if (arr[mid1] == target)
            return mid1;

        if (arr[mid2] == target)
            return mid2;

        if (target < arr[mid1])
            return TernarySearch(arr, target, left, mid1 - 1);

        if (target > arr[mid2])
            return TernarySearch(arr, target, mid2 + 1, right);

        return TernarySearch(arr, target, mid1 + 1, mid2 - 1);
    }

    public static int JumpSearch(int[] arr, int target)
    {
        var length = arr.Length;
        var blockSize = (int)Math.Sqrt(length);

        var start = 0;
        var next = blockSize;

        while (start < length && target <= arr[next - 1])
        {
            start = next;
            next += blockSize;

            if (next > length) next = length;
        }

        while (start < next)
        {
            if (arr[start] == target)
                return start;

            start++;
        }

        return -1;
    }

    public static int ExponentialSearch(int[] arr, int target)
    {
        var bound = 1;

        while (bound < arr.Length && target > arr[bound])
            bound *= 2;

        var lowerBound = bound / 2;
        var upperBound = Math.Min(bound, arr.Length - 1);

        return BinarySearchRecursive(arr, target, lowerBound, upperBound);
    }
}