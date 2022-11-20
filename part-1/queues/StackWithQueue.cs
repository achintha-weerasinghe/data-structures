public class StackWithQueue
{
    private Queue<int> _queue = new Queue<int>();

    // O(1)
    public bool IsEmpty()
    {
        return _queue.Count == 0;
    }

    // O(n)
    public void Push(int item)
    {
        _queue.Enqueue(item);
        BringNewItemTop();
    }

    // O(1)
    public int Pop()
    {
        return _queue.Dequeue();
    }

    // O(1)
    public int Peek()
    {
        return _queue.Peek();
    }

    // O(1)
    private void BringNewItemTop()
    {
        for (var i = 1; i < _queue.Count; i++)
            _queue.Enqueue(_queue.Dequeue());
    }
}