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
public class Heap
{
    private readonly int _size;
    private readonly int[] _arr;
    private int _curosr;

    public Heap(int size)
    {
        _size = size;
        _arr = new int[_size];
    }

    public bool IsFull()
    {
        return _size == _curosr;
    }

    public bool IsEmpty()
    {
        return _curosr == 0;
    }

    public int Max()
    {
        if (IsEmpty())
            throw new InvalidDataException("Heap is empty.");

        return _arr[0];
    }

    public void Insert(int value)
    {
        if (IsFull())
            throw new OverflowException("Heap is full.");

        _arr[_curosr++] = value;

        BubbleUp();
    }

    public int Remove()
    {
        if (IsEmpty())
            throw new InvalidOperationException("Heap is empty.");

        var root = _arr[0];
        _arr[0] = _arr[--_curosr];
        _arr[_curosr] = 0; // For visal aid

        BubbleDown();

        return root;
    }

    private void BubbleDown()
    {
        int index = 0;
        while (index < _curosr && !IsValidParent(index))
        {
            var largerChildIndex = LargerChildIndex(index);
            Swap(index, largerChildIndex);
            index = largerChildIndex;
        }
    }

    private int LargerChildIndex(int index)
    {
        if (!HasLeftChild(index))
            return index;

        if (!HasRightChild(index))
            return LeftChildIndex(index);

        return _arr[LeftChildIndex(index)] > _arr[RightChildIndex(index)]
            ? LeftChildIndex(index)
            : RightChildIndex(index);
    }

    private bool IsValidParent(int index)
    {
        if (!HasLeftChild(index))
            return true;

        var isValid = _arr[index] >= _arr[LeftChildIndex(index)];
        if (HasRightChild(index))
            isValid &= _arr[index] >= _arr[RightChildIndex(index)];

        return isValid;
    }

    private bool HasRightChild(int index)
    {
        return RightChildIndex(index) < _curosr;
    }

    private bool HasLeftChild(int index)
    {
        return LeftChildIndex(index) < _curosr;
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
        var index = _curosr - 1;
        while (index >= 0 && _arr[index] > _arr[Parent(index)])
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
        return $"[{string.Join(", ", _arr)}]";
    }
}