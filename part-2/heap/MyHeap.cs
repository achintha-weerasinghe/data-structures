/*
    Index arrangement [0, 1, 2, 3, 4, 5, 6]
          0
        /   \
       1     2
      / \   / \
     3   4  5  6

    leftChild = parent * 2 + 1
    rightChild = parent * 2 + 2

    parent = (child - 1) / 2
*/
public class MyHeap
{
    private int[] _arr;
    private readonly int _size;
    private int _cursor;

    public MyHeap(int size)
    {
        _size = size;
        _arr = new int[_size];
    }

    public bool IsFull()
    {
        return _size == _cursor;
    }

    public bool IsEmpty()
    {
        return _cursor == 0;
    }

    public void Insert(int value)
    {
        if (IsFull())
            throw new OverflowException("Heap is full");

        _arr[_cursor++] = value;

        BubbleUp();
    }

    public int Remove()
    {
        if (IsEmpty())
            throw new InvalidOperationException("Heap is empty");

        var root = _arr[0];
        _arr[0] = _arr[--_cursor];
        _arr[_cursor] = 0; // For visual aid

        BubbleDown();

        return root;
    }

    private void BubbleDown()
    {
        var index = 0;
        while (index <= _cursor && !isValidParent(index))
        {
            var bigChildIndex = BigChildIndex(index);
            Swap(bigChildIndex, index);
            index = bigChildIndex;
        }
    }

    private int BigChildIndex(int index)
    {
        if (LeftChildIndex(index) == -1)
            return -1;

        if (RightChildIndex(index) == -1)
            return LeftChildIndex(index);

        return RightChild(index) > LeftChild(index)
            ? RightChildIndex(index)
            : LeftChildIndex(index);
    }

    private bool isValidParent(int index)
    {
        if (LeftChildIndex(index) == -1)
            return true;

        var isValid = _arr[index] >= LeftChild(index);
        if (RightChildIndex(index) >= 0)
            isValid &= _arr[index] >= RightChild(index);

        return isValid;
    }

    private int LeftChild(int index)
    {
        return _arr[LeftChildIndex(index)];
    }

    private int RightChild(int index)
    {
        return _arr[RightChildIndex(index)];
    }

    private int LeftChildIndex(int index)
    {
        var child = index * 2 + 1;
        return child < _cursor ? child : -1;
    }

    private int RightChildIndex(int index)
    {
        var rightChild = LeftChildIndex(index) + 1;
        return rightChild < _cursor ? rightChild : -1;
    }


    private void BubbleUp()
    {
        var index = _cursor - 1;
        while (index > 0 && _arr[index] > _arr[Parent(index)])
        {
            Swap(index, Parent(index));
            index = Parent(index);
        }
    }

    private int Parent(int index)
    {
        return (index - 1) / 2;
    }

    private void Swap(int first, int second)
    {
        var temp = _arr[first];
        _arr[first] = _arr[second];
        _arr[second] = temp;
    }

    public int Max()
    {
        if (IsEmpty())
            throw new InvalidDataException();

        return _arr[0];
    }

    public override string ToString()
    {
        return $"[{string.Join(", ", _arr)}]";
    }
}