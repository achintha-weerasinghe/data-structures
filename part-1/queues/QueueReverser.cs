public class QueueReverser
{
    public static void reverse(Queue<int> queue)
    {
        var stack = new Stack<int>();

        while (queue.Count != 0)
            stack.Push(queue.Dequeue());
        while (stack.Count != 0)
            queue.Enqueue(stack.Pop());
    }

    public static void reverse(Queue<int> queue, int k)
    {
        var stack = new Stack<int>();
        var remain = queue.Count - k;

        if (k < 0 || remain < 0)
            throw new OverflowException();

        for (int i = 0; i < k; i++)
            stack.Push(queue.Dequeue());

        while (stack.Count > 0)
            queue.Enqueue(stack.Pop());

        for (int i = 0; i < remain; i++)
            queue.Enqueue(queue.Dequeue());
    }
}