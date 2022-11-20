public class Array
{
    private int _length;
    private int[] _arr;
    private int _cursor = 0;

    public Array(int length)
    {
        this._length = length;
        this._arr = new int[length];
    }

    // O(n)
    private void increseSize()
    {
        if (_cursor == _length)
        {
            _length = _length * 2;
            int[] tempArr = new int[_length];
            for (int i = 0; i < _cursor; i++)
            {
                tempArr[i] = _arr[i];
            }

            _arr = tempArr;
        }
    }

    // O(n)
    public void insert(int number)
    {
        this.increseSize();

        _arr[_cursor++] = number;
    }

    // O(n)
    public void removeAt(int index)
    {
        if (index >= _cursor || index < 0)
            throw new Exception("Index is not accessible");

        for (int i = index; i < _cursor - 1; i++)
            _arr[i] = _arr[i + 1];

        _cursor--;

    }

    // O(n)
    public int indexOf(int number)
    {
        for (int i = 0; i < _length; i++)
            if (_arr[i] == number)
                return i;

        return -1;
    }

    // O(n)
    public int max()
    {
        if (_cursor == 0)
            throw new Exception("Please insert values");

        int max = _arr[0];
        for (int i = 1; i < _cursor; i++)
            if (max < _arr[i])
                max = _arr[i];

        return max;
    }

    // O(n^2)
    public Array intersect(int[] numbers)
    {
        Array common = new Array(numbers.Length);


        for (int i = 0; i < numbers.Length; i++)
            if (this.indexOf(numbers[i]) >= 0)
                common.insert(numbers[i]);

        return common;
    }

    // O(n)
    public void reverse()
    {
        int[] arr = new int[_length];

        for (int i = _cursor - 1; i >= 0; i--)
            arr[_cursor - 1 - i] = _arr[i];

        _arr = arr;
    }

    // O(n)
    public void insertAt(int item, int index)
    {
        if (index < 0 || index >= _cursor)
            throw new Exception("Index access failed");

        this.increseSize();

        int temp = _arr[index];
        _arr[index] = item;

        for (int i = index + 1; i < _cursor; i++)
        {
            int temp2 = _arr[i];
            _arr[i] = temp;
            temp = temp2;
        }

        this.insert(temp);
    }

    public int[] toArray()
    {
        if (_length == _cursor)
            return _arr;

        int[] arr = new int[_cursor];

        for (int i = 0; i < _cursor; i++)
        {
            arr[i] = _arr[i];
        }

        return arr;
    }

    public void print()
    {
        for (int i = 0; i < _cursor; i++)
        {
            Console.WriteLine(_arr[i]);
        }
    }
}
