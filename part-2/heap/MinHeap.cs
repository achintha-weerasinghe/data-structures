public class MinHeap
{
    private class Node
    {
        public int Key { get; }
        public string Value { get; }

        public Node(int key, string value)
        {
            Key = key;
            Value = value;
        }

        public override string ToString()
        {
            return $"{Key}={Value}";
        }
    }

    private readonly int _size;
    private readonly Node[] _arr;
    private int _cursor;

    public MinHeap(int size)
    {
        _size = size;
        _arr = new Node[_size];
    }

    public bool IsFull()
    {
        return _size == _cursor;
    }

    public bool IsEmpty()
    {
        return _cursor == 0;
    }

    public void Insert(int key, string value)
    {
        if (IsFull())
            throw new OverflowException("heap is full.");

        _arr[_cursor++] = new Node(key, value);

        BubbleUp();
    }

    public int Remove()
    {
        if (IsEmpty())
            throw new InvalidOperationException("heap is empty.");

        var root = _arr[0];
        _arr[0] = _arr[--_cursor];
        _arr[_cursor] = null; // For visual aid

        BubbleDown();

        return root.Key;
    }

    private void BubbleDown()
    {
        var index = 0;
        while (index <= _cursor && !IsValidParent(index))
        {
            var smallerChildIndex = SmallerChildIndex(index);
            Swap(smallerChildIndex, index);
            index = smallerChildIndex;
        }
    }

    private int SmallerChildIndex(int index)
    {
        if (!HasLeftChild(index))
            return index;

        if (!HasRightChild(index))
            return LeftChildIndex(index);

        return _arr[LeftChildIndex(index)].Key < _arr[RightChildIndex(index)].Key
            ? LeftChildIndex(index)
            : RightChildIndex(index);
    }

    private bool IsValidParent(int index)
    {
        if (!HasLeftChild(index))
            return true;

        var isValid = _arr[index].Key <= _arr[LeftChildIndex(index)].Key;
        if (HasRightChild(index))
            isValid &= _arr[index].Key <= _arr[RightChildIndex(index)].Key;

        return isValid;
    }

    private bool HasLeftChild(int index)
    {
        return LeftChildIndex(index) < _cursor;
    }

    private bool HasRightChild(int index)
    {
        return RightChildIndex(index) < _cursor;
    }

    private int LeftChildIndex(int index)
    {
        return index * 2 + 1;
    }

    private int RightChildIndex(int index)
    {
        return index * 2 + 2;
    }


    private void BubbleUp()
    {
        var index = _cursor - 1;
        while (index >= 0 && _arr[index].Key < _arr[Parent(index)].Key)
        {
            Swap(index, Parent(index));
            index = Parent(index);
        }
    }

    private void Swap(int first, int second)
    {
        var temp = _arr[first];
        _arr[first] = _arr[second];
        _arr[second] = temp;
    }

    private int Parent(int index)
    {
        return (index - 1) / 2;
    }

    public override string ToString()
    {
        return $"[{string.Join<Node>(", ", _arr)}]";
    }
}