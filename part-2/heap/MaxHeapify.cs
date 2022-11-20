public class MaxHeapify
{
    public static void Heapify(int[] numbers)
    {
        /*  Half the nodes of the heap are in the leaf level (last level)
            Therefore, we do not need to heapify the last level.
            We can find the last parent node using the following equation.

            lastParentIndex = TotalNodes / 2 - 1

            Most of the nodes in the top are in the right position.
            So by stating heapify from bottom to top can optimize the algorithm.
        */
        var lastParentIndex = numbers.Length / 2 - 1;
        for (var i = lastParentIndex; i >= 0; i--)
            Heapify(numbers, i);

    }

    private static void Heapify(int[] numbers, int index)
    {
        var largerIndex = index;

        var leftIndex = index * 2 + 1;
        if (leftIndex < numbers.Length && numbers[largerIndex] < numbers[leftIndex])
            largerIndex = leftIndex;

        var rightIndex = index * 2 + 2;
        if (rightIndex < numbers.Length && numbers[largerIndex] < numbers[rightIndex])
            largerIndex = rightIndex;

        if (largerIndex == index)
            return;

        Swap(numbers, index, largerIndex);
        Heapify(numbers, largerIndex);
    }

    private static void Swap(int[] numbers, int first, int second)
    {
        var temp = numbers[first];
        numbers[first] = numbers[second];
        numbers[second] = temp;
    }

    public static int GetKthLargest(int[] numbers, int k)
    {
        if (k < 1 || k > numbers.Length)
            throw new InvalidDataException();

        var heap = new Heap(numbers.Length);

        foreach (var number in numbers)
            heap.Insert(number);

        for (var i = 0; i < k - 1; i++)
            heap.Remove();

        return heap.Max();
    }

    public static bool IsMaxHeap(int[] arr)
    {
        return IsMaxHeap(arr, 0);
    }

    private static bool IsMaxHeap(int[] arr, int index)
    {
        var lastParentIndex = arr.Length / 2 - 1;
        if (index > lastParentIndex)
            return true;

        var leftChildIndex = index * 2 + 1;
        if (leftChildIndex < arr.Length && arr[leftChildIndex] > arr[index])
            return false;

        var rightChildIndex = index * 2 + 2;
        if (rightChildIndex < arr.Length && arr[rightChildIndex] > arr[index])
            return false;

        return IsMaxHeap(arr, leftChildIndex) && IsMaxHeap(arr, rightChildIndex);
    }
}