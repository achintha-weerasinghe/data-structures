public class StackQueue
{
    private int _size;
    private Stack<int> _stack1, _stack2;

    public StackQueue(int size)
    {
        _size = size;
        _stack1 = new Stack<int>(_size);
        _stack2 = new Stack<int>(_size);
    }

    public bool IsEmpty()
    {
        return IsStack1Empty() && IsStack2Empty();
    }

    public bool IsFull()
    {
        return _stack1.Count == _size;
    }

    public void Enqueue(int item)
    {
        if (IsFull())
            throw new OverflowException();

        _stack1.Push(item);
    }

    public int Dequeue()
    {
        if (IsEmpty())
            throw new InvalidOperationException();

        if (IsStack2Empty()) FillStack2();

        return _stack2.Pop();
    }

    public int Peek()
    {
        if (IsEmpty())
            throw new InvalidOperationException();

        if (IsStack2Empty()) FillStack2();

        return _stack2.Peek();
    }

    public override string ToString()
    {
        return $"[{string.Join(", ", _stack1)}] [{string.Join(", ", _stack2)}]";
    }

    private void FillStack2()
    {
        while (_stack1.Count != 0)
            _stack2.Push(_stack1.Pop());
    }

    private bool IsStack1Empty()
    {
        return _stack1.Count == 0;
    }

    private bool IsStack2Empty()
    {
        return _stack2.Count == 0;
    }
}