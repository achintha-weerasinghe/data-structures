public class LinkedListQueue
{
    private Node? _head, _tail;
    private int _count = 0;

    public bool IsEmpty()
    {
        return _count == 0;
    }

    // O(1)
    public int Size()
    {
        return _count;
    }

    // O(1)
    public void Enqueue(int item)
    {
        var node = new Node(item);
        if (IsEmpty())
            _head = _tail = node;
        else
        {
            _tail!.Next = node;
            _tail = node;
        }

        _count++;
    }

    // O(1)
    public int Dequeue()
    {
        if (IsEmpty())
            throw new InvalidOperationException();

        var first = _head;
        if (_head == _tail) // _count == 1
            _head = _tail = null;
        else
        {
            _head = _head!.Next;
            first!.Next = null;
        }

        _count--;
        return first!.Value;
    }

    // O(1)
    public int Peek()
    {
        if (IsEmpty())
            throw new InvalidOperationException();

        return _head!.Value;
    }
}

public class Node
{
    public int Value;
    public Node? Next;

    public Node(int value)
    {
        Value = value;
    }
}