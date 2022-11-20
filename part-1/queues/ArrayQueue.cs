public class ArrayQueue
{
    private int _front, _rear, _count = 0;
    private int _size;
    private int[] _arr;

    public ArrayQueue(int size)
    {
        _size = size;
        _arr = new int[_size];
    }

    public bool IsEmpty()
    {
        return _count == 0;
    }

    public bool IsFull()
    {
        return _size == _count;
    }

    public void Enqueue(int item)
    {
        if (IsFull())
            throw new OverflowException();

        _arr[_rear] = item;
        _rear = (_rear + 1) % _size;
        _count++;
    }

    public int Dequeue()
    {
        if (IsEmpty())
            throw new InvalidOperationException();

        var item = _arr[_front];

        _front = (_front + 1) % _size;
        _count--;

        return item;
    }

    public int Peek()
    {
        if (IsEmpty())
            throw new InvalidOperationException();

        return _arr[_front];
    }

    public override string ToString()
    {
        return $"[{string.Join(", ", _arr)}]";
    }
}