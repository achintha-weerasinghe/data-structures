public class HashTable
{
    private class KeyValuePair
    {
        public int Key { get; set; }
        public string Value { get; set; }

        public KeyValuePair(int key, string value)
        {
            Key = key;
            Value = value;
        }
    }

    private int _size;
    private LinkedList<KeyValuePair>[] _arr;

    public HashTable(int size)
    {
        _size = size;
        _arr = new LinkedList<KeyValuePair>[_size];
    }

    public void Put(int key, string value)
    {
        var pair = FindPairByKey(key);

        if (pair != null)
            pair.Value = value;
        else
            FindOrCreateList(key).AddLast(new KeyValuePair(key, value));

    }

    public string? Get(int key)
    {
        var pair = FindPairByKey(key);

        return (pair != null) ? pair.Value : null;
    }

    public void Remove(int key)
    {
        var pair = FindPairByKey(key);

        if (pair == null)
            throw new KeyNotFoundException();

        FindList(key)!.Remove(pair);
    }

    private LinkedList<KeyValuePair>? FindList(int key)
    {
        return _arr[Hash(key)];
    }

    private LinkedList<KeyValuePair> FindOrCreateList(int key)
    {
        var index = Hash(key);
        var list = _arr[index];
        if (list == null)
            list = _arr[index] = new LinkedList<KeyValuePair>();

        return list;
    }

    private KeyValuePair? FindPairByKey(int key)
    {
        var list = FindList(key);

        if (list != null)
            foreach (var p in list)
                if (p.Key == key)
                    return p;

        return null;
    }

    private int Hash(int key)
    {
        return Math.Abs(key) % _size;
    }
}