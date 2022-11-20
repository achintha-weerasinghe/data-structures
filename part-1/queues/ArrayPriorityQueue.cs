public class ArrayPriorityQueue
{
    private int _size;
    private int[] _arr;
    private int _count = 0;

    public ArrayPriorityQueue(int size)
    {
        if (size <= 0)
            throw new ArgumentOutOfRangeException();

        _size = size;
        _arr = new int[_size];
    }

    public bool IsFull()
    {
        return _size == _count;
    }

    public bool IsEmpty()
    {
        return _count == 0;
    }

    public void Enqueue(int item)
    {
        if (IsFull())
            throw new OverflowException();

        var index = ShiftItemsToInsert(item);

        _arr[index] = item;
        _count++;
    }

    public int Dequeue()
    {
        if (IsEmpty())
            throw new InvalidOperationException();

        return _arr[--_count];
    }

    public override string ToString()
    {
        return $"[{string.Join(", ", _arr)}]";
    }

    private int ShiftItemsToInsert(int item)
    {   
        var index = _count;
        while (index > 0)
        {
            if (_arr[index - 1] >= item)
                break;

            _arr[index] = _arr[index - 1];
            index--;
        }

        return index;
    }
}