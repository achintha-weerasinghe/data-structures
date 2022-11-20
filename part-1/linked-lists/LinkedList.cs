public class LinkedList<T>
{
    private Node<T>? _first;
    private Node<T>? _last;
    private int _size = 0;

    private bool IsEmpty()
    {
        return _first == null;
    }

    private Node<T>? GetPrevious(Node<T> node)
    {
        var cur = _first;

        while (cur != null)
        {
            if (cur.Next == node)
                return cur;

            cur = cur.Next;
        }

        return null;
    }

    // O(1)
    public void AddFirst(T value)
    {
        var temp = new Node<T>(value);

        if (_first == null)
            _first = _last = temp;
        else
        {
            temp.Next = _first;
            _first = temp;
        }

        _size++;
    }

    // O(1)
    public void AddLast(T value)
    {
        var temp = new Node<T>(value);

        if (_last == null)
            _first = _last = temp;
        else
        {
            _last.Next = temp;
            _last = temp;
        }

        _size++;
    }

    // O(1)
    public void DeleteFirst()
    {
        if (_first == null)
            throw new Exception("no nodes found!");

        if (_first == _last)
            _first = _last = null;
        else
        {
            var second = _first.Next;
            _first.Next = null;
            _first = second;
        }

        _size--;
    }

    // O(n)
    public void DeleteLast()
    {
        if (_last == null)
            throw new Exception("no nodes found!");

        var prev = GetPrevious(_last);

        if (prev == null)
            _first = _last = null;
        else
        {
            _last = prev;
            _last.Next = null;
        }

        _size--;
    }

    // O(n)
    public int IndexOf(T value)
    {
        var index = 0;
        var cur = _first;

        while (cur != null)
        {
            if (EqualityComparer<T>.Default.Equals(cur.Value, value))
                return index;

            cur = cur.Next;
            index++;

        }

        return -1;
    }

    // O(n)
    public bool Contains(T value)
    {
        return this.IndexOf(value) >= 0;
    }

    // O(1)
    public int Size()
    {
        return _size;
    }

    // O(n)
    public void Reverse()
    {
        if (_first == null)
            return;

        Node<T>? prev = null;
        var cur = _first;
        while (cur != null)
        {
            var next = cur.Next;
            cur.Next = prev;
            prev = cur;
            cur = next;
        }

        _last = _first;
        _first = prev;
    }

    // O(n)
    public T? GetKthFromTheEnd(int k)
    {
        if (_first == null)
            throw new Exception("list is empty");

        var distance = k - 1;
        var kth = _first;
        var cur = _first;

        var index = 0;
        while (cur.Next != null)
        {
            if (index >= distance)
                kth = kth!.Next;

            cur = cur.Next;
            index++;
        }

        if (index < distance)
            throw new Exception("k is too large");

        return kth!.Value;
    }

    // O(n)
    public void PrintMiddle()
    {
        if (_first == null)
            throw new Exception("list is empty");

        var cur = _first;
        var mid = _first;

        while (cur != _last && cur!.Next != _last)
        {
            cur = cur.Next!.Next;
            mid = mid!.Next;
        }

        if (cur == _last)
            Console.WriteLine(mid!.Value);
        else
            Console.WriteLine($"{mid!.Value}, {mid.Next!.Value}");
    }

    // O(n)
    public bool HasLoop()
    {
        var fast = _first;
        var slow = _first;

        while (fast != null && fast.Next != null)
        {
            fast = fast.Next.Next;
            slow = slow!.Next;

            if (fast == slow)
                return true;
        }

        return false;
    }

    public static LinkedList<int> CreateWithLoop()
    {
        var list = new LinkedList<int>();
        list.AddLast(10);
        list.AddLast(20);
        list.AddLast(30);

        // Get a reference to 30
        var node = list._last;

        list.AddLast(40);
        list.AddLast(50);

        // Create the loop
        list._last!.Next = node;

        return list;
    }

    public T[] ToArray()
    {
        T[] arr = new T[_size];

        var cur = _first;
        var index = 0;
        while (cur != null)
        {
            arr[index++] = cur.Value;
            cur = cur.Next;
        }

        return arr;
    }

    public void Print()
    {
        var cur = _first;

        while (cur != null)
        {
            Console.WriteLine(cur.Value);
            cur = cur.Next;
        }
    }
}

public class Node<T>
{
    public T Value;
    public Node<T>? Next;

    public Node(T value)
    {
        this.Value = value;
    }
}