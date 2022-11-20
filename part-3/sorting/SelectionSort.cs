public class SelectionSort
{
    public static void Sort(int[] arr)
    {
        for (var i = 0; i < arr.Length; i++)
        {
            var minIndex = i;
            for (var j = i; j < arr.Length; j++)
            {
                if (arr[minIndex] > arr[j])
                    minIndex = j;
            }

            Swap(arr, i, minIndex);
        }
    }

    private static void Swap(int[] arr, int index1, int index2)
    {
        var temp = arr[index1];
        arr[index1] = arr[index2];
        arr[index2] = temp;
    }
}