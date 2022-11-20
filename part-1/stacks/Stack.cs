public class Stack
{
    private int _size;
    private int[] _arr;
    private int _count = 0;
    public Stack(int size)
    {
        if (size <= 0)
            throw new ArgumentOutOfRangeException("value should be 1 or greater.");

        _size = size;
        _arr = new int[_size];
    }

    public bool IsEmpty()
    {
        return _count == 0;
    }

    public void Push(int item)
    {
        if (_count == _size)
            throw new StackOverflowException();

        _arr[_count++] = item;
    }

    public int Pop()
    {
        if (IsEmpty())
            throw new InvalidOperationException();

        return _arr[--_count];
    }

    public int Peek()
    {
        if (IsEmpty())
            throw new InvalidOperationException();

        return _arr[_count - 1];
    }

    public override string ToString()
    {
        return $"[{string.Join(", ", _arr.Take(_count))}]";
    }
}