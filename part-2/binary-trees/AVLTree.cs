public class AVLTree
{
    private class AVLNode
    {
        public int Value { get; }
        public int Height { get; set; }
        public AVLNode? LeftChild { get; set; }
        public AVLNode? RightChild { get; set; }

        public AVLNode(int value)
        {
            Value = value;
        }
    }

    private AVLNode? _root;

    public void Insert(int value)
    {
        _root = Insert(_root, value);
    }

    private AVLNode Insert(AVLNode? root, int value)
    {
        if (root == null)
            return new AVLNode(value);

        if (value < root.Value)
            root.LeftChild = Insert(root.LeftChild, value);
        else
            root.RightChild = Insert(root.RightChild, value);

        SetHeight(root);

        return Balance(root);
    }

    private AVLNode Balance(AVLNode root)
    {
        if (IsLeftHeavy(root))
        {
            if (BalanceFactor(root.LeftChild) < 0)
                root.LeftChild = RotateLeft(root.LeftChild!);
            return RotateRight(root);
        }
        else if (IsRightHeavy(root))
        {
            if (BalanceFactor(root.RightChild) > 0)
                root.RightChild = RotateRight(root.RightChild!);
            return RotateLeft(root);
        }

        return root;
    }

    private AVLNode RotateLeft(AVLNode root)
    {
        var newRoot = root.RightChild!;
        root.RightChild = newRoot.LeftChild;
        newRoot.LeftChild = root;

        SetHeight(root);
        SetHeight(newRoot);

        return newRoot;
    }

    private AVLNode RotateRight(AVLNode root)
    {
        var newRoot = root.LeftChild!;
        root.LeftChild = newRoot.RightChild;
        newRoot.RightChild = root;

        SetHeight(root);
        SetHeight(newRoot);

        return newRoot;
    }

    private void SetHeight(AVLNode root)
    {
        root.Height = Math.Max(Height(root.LeftChild), Height(root.RightChild)) + 1;
    }

    private int Height(AVLNode? root)
    {
        return root == null ? -1 : root.Height;
    }

    private bool IsLeftHeavy(AVLNode? root)
    {
        return BalanceFactor(root) > 1;
    }

    private bool IsRightHeavy(AVLNode? root)
    {
        return BalanceFactor(root) < -1;
    }

    private int BalanceFactor(AVLNode? root)
    {
        return root == null ? 0 : Height(root.LeftChild) - Height(root.RightChild);
    }
}