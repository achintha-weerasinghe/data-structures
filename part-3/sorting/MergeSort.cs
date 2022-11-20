public class MergeSort
{
    public static void Sort(int[] arr)
    {
        // Base condition
        if (arr.Length == 1) return;

        // Dividing the array
        int middle = arr.Length / 2;

        int[] left = new int[middle];
        for (var i = 0; i < middle; i++)
            left[i] = arr[i];

        int[] right = new int[arr.Length - middle];
        for (var i = middle; i < arr.Length; i++)
            right[i - middle] = arr[i];

        // Sort left and right
        Sort(left);
        Sort(right);

        // Merge the sorted left and right arrays
        Merge(left, right, arr);
    }

    private static void Merge(int[] left, int[] right, int[] arr)
    {
        int l = 0, r = 0, k = 0;

        while (l < left.Length && r < right.Length)
        {
            if (left[l] <= right[r])
                arr[k++] = left[l++];
            else
                arr[k++] = right[r++];
        }

        while (l < left.Length)
            arr[k++] = left[l++];

        while (r < right.Length)
            arr[k++] = right[r++];
    }
}