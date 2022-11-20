public class StackWithTwoQueues
{
    private Queue<int> _queue1 = new Queue<int>();
    private Queue<int> _queue2 = new Queue<int>();
    private int _top;

    // O(1)
    public bool IsEmpty()
    {
        return _queue1.Count == 0;
    }

    // O(1)
    public int Size()
    {
        return _queue1.Count;
    }

    // O(1)
    public void Push(int item)
    {
        _queue1.Enqueue(item);
        _top = item;
    }

    // O(n)
    public int Pop()
    {
        if (IsEmpty())
            throw new InvalidOperationException();

        while (_queue1.Count > 1)
        {
            _top = _queue1.Dequeue();
            _queue2.Enqueue(_top);
        }

        SwapQueues();

        return _queue2.Dequeue();
    }

    // O(1)
    public int Peek()
    {
        if (IsEmpty())
            throw new InvalidOperationException();

        return _top;
    }

    // O(1)
    private void SwapQueues()
    {
        var temp = _queue2;
        _queue2 = _queue1;
        _queue1 = temp;
    }
}