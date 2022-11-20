public class StackDouble
{
    private int _size;
    private int[] _arr;
    private int _count1, _count2 = 0;
    public StackDouble(int size)
    {
        if (size <= 0)
            throw new ArgumentOutOfRangeException("value should be 1 or greater.");

        _size = size;
        _arr = new int[_size];
    }

    public bool IsEmpty1()
    {
        return _count1 == 0;
    }

    public bool IsEmpty2()
    {
        return _count2 == 0;
    }

    public bool IsFull()
    {
        return _count1 == _size - _count2;
    }

    public void Push1(int item)
    {
        if (IsFull())
            throw new StackOverflowException();

        _arr[_count1++] = item;
    }

    public void Push2(int item)
    {
        if (IsFull())
            throw new StackOverflowException();

        _arr[_size - ++_count2] = item;
    }

    public int Pop1()
    {
        if (IsEmpty1())
            throw new InvalidOperationException();

        return _arr[--_count1];
    }

    public int Pop2()
    {
        if (IsEmpty2())
            throw new InvalidOperationException();

        return _arr[_size - _count2--];
    }

    public int Peek1()
    {
        if (IsEmpty1())
            throw new InvalidOperationException();

        return _arr[_count1 - 1];
    }

    public int Peek2()
    {
        if (IsEmpty2())
            throw new InvalidOperationException();

        return _arr[_size - _count2];
    }

    public override string ToString()
    {
        return $"[{string.Join(", ", _arr)}]";
    }
}