public class BubbleSort
{
    public static void Sort(int[] arr)
    {
        bool sorted;
        for (int i = 0; i < arr.Length; i++)
        {
            sorted = true;
            for (int j = 1; j < arr.Length - i; j++)
                if (arr[j] < arr[j - 1])
                {
                    Swap(arr, j, j - 1);
                    sorted = false;
                }

            if (sorted)
                break;
        }
    }

    private static void Swap(int[] arr, int index1, int index2)
    {
        var temp = arr[index1];
        arr[index1] = arr[index2];
        arr[index2] = temp;
    }
}