//             Array    Heap
// Insertion - O(n) --> O(log n)
// Deletion  - O(1) --> O(log n)
public class PriorityQueue
{
    private Heap heap = new Heap(10);

    public void Enqueue(int item)
    {
        heap.Insert(item);
    }

    public int Dequeue()
    {
        return heap.Remove();
    }

    public bool IsEmpty()
    {
        return heap.IsEmpty();
    }
}