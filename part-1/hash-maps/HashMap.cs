public class HashMap
{
    private class KeyValuePair
    {
        public int Key { get; }
        public string Value { get; set; }
        public KeyValuePair(int key, string value)
        {
            Key = key;
            Value = value;
        }
    }

    private readonly int _size;
    private int _count;
    private KeyValuePair[] _arr;

    public HashMap(int size)
    {
        _size = size;
        _arr = new KeyValuePair[_size];
    }

    public void Put(int key, string value)
    {
        var index = Probe(key);

        if (index == -1)
            throw new OverflowException();

        if (_arr[index] == null)
            _arr[index] = new KeyValuePair(key, value);
        else
            _arr[index].Value = value;

        _count++;
    }

    public string? Get(int key)
    {
        int index = Probe(key);

        if (index >= 0 && _arr[index] != null)
            return _arr[index].Value;

        return null;
    }

    public void Remove(int key)
    {
        int index = Probe(key);

        if (index == -1 || _arr[index] == null)
            throw new KeyNotFoundException();

        _arr[index] = null;
        _count--;
    }

    public int Size()
    {
        return _count;
    }

    private int Probe(int key)
    {
        int hash = Hash(key);
        var index = hash;
        var i = 1;
        while (_arr[index] != null && _arr[index].Key != key)
        {
            index = (hash + i++) % _size;

            if (index == hash)
                return -1; // _arr is full
        }

        return index;
    }

    private int Hash(int key)
    {
        return Math.Abs(key) % _size;
    }
}