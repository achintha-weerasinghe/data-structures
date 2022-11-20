public class QuickSort
{
    public static void Sort(int[] arr)
    {
        Sort(arr, 0, arr.Length - 1);
    }

    private static void Sort(int[] arr, int start, int end)
    {
        if (start >= end)
            return;

        // Partitiona and get the pivot boundary
        var boundary = Partition(arr, start, end);

        // Sort the left and right sides of the pivot
        Sort(arr, start, boundary - 1);
        Sort(arr, boundary + 1, end);
    }

    private static int Partition(int[] arr, int start, int end)
    {
        var pivot = arr[end]; // last element
        var boundary = start - 1; // initially we have no elements in the left partition

        for (var i = start; i <= end; i++)
            if (arr[i] <= pivot)
                Swap(arr, ++boundary, i); // Move from right to left partition

        return boundary;
    }

    private static void Swap(int[] arr, int index1, int index2)
    {
        var temp = arr[index1];
        arr[index1] = arr[index2];
        arr[index2] = temp;
    }
}