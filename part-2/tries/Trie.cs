using System.Text;

public class Trie
{
    private class Node
    {
        public char Value { get; }
        public bool IsEndOfWord { get; set; }
        public Dictionary<char, Node> Children { get; } = new Dictionary<char, Node>();
        public Node(char value)
        {
            Value = value;
        }

        public bool HasChild(char ch)
        {
            return Children.ContainsKey(ch);
        }

        public void AddChild(char ch)
        {
            Children.Add(ch, new Node(ch));
        }

        public Node? GetChild(char ch)
        {
            return Children.GetValueOrDefault(ch);
        }

        public void RemoveChild(char ch)
        {
            Children.Remove(ch);
        }

        public Node[] GetChildren()
        {
            return Children.Values.ToArray();
        }

        public bool HasChildren()
        {
            return Children.Count > 0;
        }

        public override string ToString()
        {
            return $"Value={Value}";
        }
    }

    private Node _root = new Node(' ');

    public void Insert(string word)
    {
        if (string.IsNullOrEmpty(word))
            return;

        var current = _root;
        foreach (var ch in word.ToLower())
        {
            if (!current!.HasChild(ch))
                current.AddChild(ch);

            current = current.GetChild(ch);
        }
        current.IsEndOfWord = true;
    }

    public bool Contains(string word)
    {
        if (string.IsNullOrEmpty(word))
            return false;

        var current = _root;
        foreach (var ch in word.ToLower())
        {
            if (!current!.HasChild(ch))
                return false;

            current = current.GetChild(ch);
        }

        return current.IsEndOfWord;
    }

    // We add these words to a trie and walk down
    // the trie. If a node has more than one child,
    // that's where these words deviate. Try this
    // with "can", "canada", "care" and "cab". You'll
    // see that these words deviate after "ca".
    //
    // So, we keep walking down the tree as long as
    // the current node has only one child.
    //
    // One edge cases we need to count is when two
    // words are in the same branch and don't deviate.
    // For example "can" and "canada". In this case,
    // we keep walking down to the end because every
    // node (except the last node) has only one child.
    // But the longest common prefix here should be
    // "can", not "canada". So, we should find the
    // shortest word in the list by checking .IsEndOfWord
    public static string LongestCommonPrefix(string[] words)
    {
        var trie = new Trie();
        foreach (var word in words)
            trie.Insert(word);

        var current = trie._root;
        var prefix = new StringBuilder();
        while (current.GetChildren().Length == 1 && !current.IsEndOfWord)
        {
            current = current.GetChildren()[0];
            prefix.Append(current.Value);
        }

        return prefix.ToString();
    }

    public int CountWords()
    {
        return CountWords(_root);
    }

    private int CountWords(Node root)
    {
        var count = 0;

        foreach (var child in root.GetChildren())
            count += CountWords(child);

        if (root.IsEndOfWord) count++;

        return count;
    }

    public bool ContainsRecursive(string word)
    {
        if (string.IsNullOrEmpty(word))
            return false;

        return ContainsRecursive(_root, word, 0);
    }

    private bool ContainsRecursive(Node root, string word, int index)
    {
        if (index == word.Length)
            return root.IsEndOfWord;

        var ch = word[index];
        var child = root.GetChild(ch);

        if (child == null)
            return false;

        return ContainsRecursive(child, word, index + 1);
    }

    public void Remove(string word)
    {
        if (string.IsNullOrEmpty(word))
            return;

        var wordQueue = new Queue<char>(word);
        Remove(_root, wordQueue);
    }

    // Post-Order traversal
    private void Remove(Node root, Queue<char> wordQueue)
    {
        if (wordQueue.Count == 0)
        {
            root.IsEndOfWord = false;
            return;
        }

        var ch = wordQueue.Dequeue();
        var child = root.GetChild(ch);

        if (child == null)
            return;

        Remove(child, wordQueue);

        if (!child.HasChildren() && !child.IsEndOfWord)
            root.RemoveChild(ch);
    }

    public List<string> FindWords(string prefix)
    {
        var words = new List<string>();
        var lastNode = FindLastNode(prefix);
        FindWords(lastNode, prefix, words);

        return words;
    }

    // Pre-Order traversal
    private void FindWords(Node? root, string prefix, List<string> words)
    {
        if (root == null)
            return;

        if (root.IsEndOfWord)
            words.Add(prefix);

        foreach (var child in root.GetChildren())
            FindWords(child, prefix + child.Value, words);
    }

    private Node? FindLastNode(string prefix)
    {
        if (prefix == null)
            return null;

        var current = _root;
        foreach (var ch in prefix)
        {
            var child = current.GetChild(ch);
            if (child == null)
                return null;

            current = child;
        }

        return current;
    }
}