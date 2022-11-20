public class MyAVLTree
{
    private class MyAVLNode
    {
        public int Value { get; }
        public int Height { get; set; } = 0;
        public MyAVLNode? LeftChild { get; set; }
        public MyAVLNode? RightChild { get; set; }
        public MyAVLNode(int value)
        {
            Value = value;
        }
    }

    private MyAVLNode? _root;

    public void Insert(int value)
    {
        var child = new MyAVLNode(value);

        if (_root == null) _root = child;
        else Insert(_root, child);
    }

    private void Insert(MyAVLNode parent, MyAVLNode child)
    {
        if (child.Value < parent.Value)
        {
            if (parent.LeftChild == null)
                parent.LeftChild = child;
            else
                Insert(parent.LeftChild, child);
            parent.Height = Math.Max(parent.LeftChild.Height, (parent.RightChild != null ? parent.RightChild.Height : 0)) + 1;
        }
        else if (child.Value > parent.Value)
        {
            if (parent.RightChild == null)
                parent.RightChild = child;
            else
                Insert(parent.RightChild, child);
            parent.Height = Math.Max(parent.RightChild.Height, (parent.LeftChild != null ? parent.LeftChild.Height : 0)) + 1;
        }
        else throw new InvalidDataException("Node already exists!");
    }
}