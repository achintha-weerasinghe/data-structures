public class MinPriorityQueue
{
    private MinHeap _heap;

    public MinPriorityQueue(int size)
    {
        _heap = new MinHeap(size);
    }

    public void Add(string value, int priority)
    {
        _heap.Insert(priority, value);
    }

    public int Remove()
    {
        return _heap.Remove();
    }

    public bool IsEmpty()
    {
        return _heap.IsEmpty();
    }

    public override string ToString()
    {
        return _heap.ToString();
    }
}