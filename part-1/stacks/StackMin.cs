public class StackMin
{
    private int _size;
    // Instead StackDouble, you can use
    // two seperate Stack objects
    private StackDouble _doubleStack;

    public StackMin(int size)
    {
        _size = size;
        _doubleStack = new StackDouble(_size);
    }

    public void Push(int item)
    {
        _doubleStack.Push1(item);

        if (_doubleStack.IsEmpty2() || _doubleStack.Peek2() >= item)
            _doubleStack.Push2(item);

    }

    public int Pop()
    {
        var top = _doubleStack.Pop1();

        if (_doubleStack.Peek2() == top)
            _doubleStack.Pop2();

        return top;
    }

    public int Min()
    {
        return _doubleStack.Peek2();
    }

    public override string ToString()
    {
        return _doubleStack.ToString();
    }
}