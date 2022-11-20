public class BinaryTree
{
    private class Node
    {
        public int Value { get; set; }
        public Node? LeftChild { get; set; }
        public Node? RightChild { get; set; }

        public Node(int value)
        {
            Value = value;
        }
    }

    private Node? _root;

    public void Insert(int value)
    {
        var node = new Node(value);
        var toBeParent = FindToBeParent(value);

        if (toBeParent == null) _root = node;
        else if (toBeParent.Value > value) toBeParent.LeftChild = node;
        else toBeParent.RightChild = node;
    }

    public bool Find(int value)
    {
        var node = FindNode(value);
        if (node == null) return false;

        return true;
    }

    public List<int> GetAncestors(int value)
    {
        var list = new List<int>();
        GetAncestors(_root, value, list);
        return list;
    }

    private bool GetAncestors(Node? node, int value, List<int> list)
    {
        // We should traverse the tree until we find the target value. If
        // find the target value, we return true without adding the current node
        // to the list; otherwise, if we ask for ancestors of 5, 5 will be also
        // added to the list.
        if (node == null)
            return false;

        if (node.Value == value)
            return true;

        // If we find the target value in the left or right sub-trees, that means
        // the current node is one of the ancestors. So we add it to the list.
        if (GetAncestors(node.LeftChild, value, list) ||
            GetAncestors(node.RightChild, value, list))
        {
            list.Add(node.Value);
            return true;
        }

        return false;
    }

    public bool AreSiblings(int value1, int value2)
    {
        return AreSiblings(_root, value1, value2);
    }

    private bool AreSiblings(Node? parent, int value1, int value2)
    {
        if (parent == null)
            return false;

        var leftChild = parent.LeftChild;
        var rightChild = parent.RightChild;

        if (leftChild != null && rightChild != null)
        {
            if
            (leftChild.Value == value1 && rightChild.Value == value2 ||
            leftChild.Value == value2 && rightChild.Value == value1)
                return true;
        }

        return AreSiblings(leftChild, value1, value2)
            || AreSiblings(rightChild, value1, value2);
    }

    public bool Contains(int value)
    {
        return BSTContains(_root, value);
    }

    /*
        This is for any binary tree
        not for Binary Search Tree (BST)
        This should be O(n), because we have to check every node
        in the worst case.
    */
    private bool Contains(Node? node, int value)
    {
        if (node == null) return false;

        if (node.Value == value) return true;

        return Contains(node.LeftChild, value) || Contains(node.RightChild, value);
    }

    /*
        This is for Binary Search Trees.
        This runs in O(log n). Every step we go deep,
        we reduce the nodes by half.
    */
    private bool BSTContains(Node? node, int value)
    {
        if (node == null)
            return false;

        if (node.Value == value)
            return true;

        if (value < node.Value)
            return Contains(node.LeftChild, value);
        else
            return Contains(node.RightChild, value);
    }

    // Works only for binary search trees
    public int Max()
    {
        if (_root == null)
            throw new EntryPointNotFoundException();

        return FindRightMostNodeRecursive(_root).Value;
    }

    public int CountLeaves()
    {
        if (_root == null)
            return 0;
        return CountLeaves(_root);
    }

    private int CountLeaves(Node? node)
    {
        if (node == null)
            return 0;

        if (IsLeaf(node))
            return 1;

        return CountLeaves(node.LeftChild) + CountLeaves(node.RightChild);
    }

    public int Size()
    {
        return Size(_root);
    }

    private int Size(Node? node)
    {
        if (node == null)
            return 0;

        return 1 + Size(node.LeftChild) + Size(node.RightChild);
    }

    public void TraverseLevelOrder()
    {
        for (var h = 0; h <= Height(); h++)
            foreach (var i in GetNodesAtDistance(h))
                Console.WriteLine(i);
    }

    public List<int> GetNodesAtDistance(int distance)
    {
        var list = new List<int>();
        AddNodesToListAtDistance(_root, distance, list);
        return list;
    }

    private void AddNodesToListAtDistance(Node? node, int distance, List<int> list)
    {

        if (node == null) return;

        if (distance == 0)
        {
            list.Add(node.Value);
            return;
        }

        AddNodesToListAtDistance(node.LeftChild, distance - 1, list);
        AddNodesToListAtDistance(node.RightChild, distance - 1, list);
    }

    public bool Validate()
    {
        return Validate(_root, int.MaxValue, int.MinValue);
    }

    /*
        This is the solution by Mosh.
        This is pre-order traversal. Order of traversing the tree is,
        root, left, right
    */
    private bool Validate(Node? node, int max, int min)
    {
        if (node == null)
            return true;

        if (min < node.Value && node.Value < max)
            return Validate(node.LeftChild, node.Value, min)
            && Validate(node.RightChild, max, node.Value);

        return false;
    }


    /*
        This is my own solution without having max and min parameters.
        This is pre-order traversal. Order of traversing the tree is,
        root, left, right
    */
    private bool Validate(Node? node)
    {
        if (node == null)
            return true;

        var validLeft = true;
        var validRight = true;

        if (node.LeftChild != null)
            if (node.LeftChild.Value < node.Value)
                validLeft = Validate(node.LeftChild);
            else
                return false;

        if (node.RightChild != null)
            if (node.Value < node.RightChild.Value)
                validRight = Validate(node.RightChild);
            else
                return false;

        return validLeft && validRight;
    }

    public bool Equals(BinaryTree tree)
    {
        if (tree == null)
            return false;
        return Equals(_root, tree._root);
    }

    /*
        Here we have used pre-order traversal to comapre the two trees.
        The order of compairing is root, left, right
    */
    private bool Equals(Node? root1, Node? root2)
    {
        if (root1 == null && root2 == null)
            return true;

        if (root1 == null || root2 == null)
            return false;

        return root1.Value == root2.Value
            && Equals(root1.LeftChild, root2.LeftChild)
            && Equals(root1.RightChild, root2.RightChild);
    }

    /*
        O(log n)
        This is the code to find the min value of
        a Binary Search Tree. You just have to find the left most node.
    */
    public int MinBST()
    {
        if (_root == null)
            throw new InvalidDataException();

        var current = _root;
        var last = current;
        while (current != null)
        {
            last = current;
            current = current.LeftChild;
        }

        return last.Value;
    }

    public int Min()
    {
        if (_root == null)
            throw new InvalidDataException();

        return Min(_root);
    }

    /*
        O(n)
        This method is for all the binary trees,
        If you have a Binary Search Tree,
        You can find the left most node to get the min value
    */
    private int Min(Node? node)
    {
        if (node == null)
            return int.MaxValue;

        if (IsLeaf(node))
            return node.Value;

        var left = Min(node.LeftChild);
        var right = Min(node.RightChild);

        return Math.Min(Math.Min(left, right), node.Value);
    }

    public int Height()
    {
        return Height(_root);
    }

    /* Breaking down the problem
       Find the height of the left and right sub trees
       then add 1 for the max height and return.
    */
    private int Height(Node? node)
    {
        if (node == null)
            return -1;

        if (IsLeaf(node)) return 0;

        return Math.Max(Height(node.LeftChild), Height(node.RightChild)) + 1;
    }

    public void TraversePreOrder()
    {
        TraversePreOrder(_root);
    }
    public void TraverseInOrder()
    {
        TraverseInOrder(_root);
    }
    public void TraversePostOrder()
    {
        TraversePostOrder(_root);
    }

    private void TraversePreOrder(Node? node)
    {
        if (node == null)
            return;

        Console.Write($"{node.Value}, ");
        TraversePreOrder(node.LeftChild);
        TraversePreOrder(node.RightChild);
    }

    private void TraverseInOrder(Node? node)
    {
        if (node == null)
            return;

        TraverseInOrder(node.LeftChild);
        Console.Write($"{node.Value}, ");
        TraverseInOrder(node.RightChild);
    }

    private void TraversePostOrder(Node? node)
    {
        if (node == null)
            return;

        TraversePostOrder(node.LeftChild);
        TraversePostOrder(node.RightChild);
        Console.Write($"{node.Value}, ");
    }

    // This is fairly complex
    // Mosh: They normally don't come in interviews
    public void Remove(int value)
    {
        if (_root == null)
            throw new KeyNotFoundException();

        var node = FindNode(value);
        if (node == null)
            throw new KeyNotFoundException();

        DeleteNode(node);
    }

    /*  To delete a node,
        Node to be deleted is the leaf: Simply remove from the tree.
        
              50                            50
           /     \         delete(20)      /   \
          30      70       --------->    30     70 
         /  \    /  \                     \    /  \ 
       20   40  60   80                   40  60   80

        Node to be deleted has only one child: Copy the child to the node and delete the child

              50                            50
           /     \         delete(30)      /   \
          30      70       --------->    40     70 
            \    /  \                          /  \ 
            40  60   80                       60   80

        Node to be deleted has two children: Find inorder successor of the node.
        Copy contents of the inorder successor to the node and delete the inorder successor.
        Note that inorder predecessor can also be used.

              50                            60
           /     \         delete(50)      /   \
          40      70       --------->    40    70 
                 /  \                            \ 
                60   80                           80
    */
    private void DeleteNode(Node node)
    {
        if (node.LeftChild != null && node.RightChild != null)
        {
            var inOrderSuccessor = FindLeftMostNode(node.RightChild);
            node.Value = inOrderSuccessor.Value;
            DeleteNode(inOrderSuccessor);
        }
        else if (node.LeftChild != null)
        {
            node.Value = node.LeftChild.Value;
            node.LeftChild = null;
        }
        else if (node.RightChild != null)
        {
            node.Value = node.RightChild.Value;
            node.RightChild = null;
        }
        else
        {
            var parent = FindParentNode(node);
            if (parent!.RightChild == node) parent.RightChild = null;
            else parent.LeftChild = null;
        }
    }

    private Node? FindToBeParent(int value)
    {
        Node? toBeParent = _root;
        if (toBeParent == null) return null;

        while (true)
        {
            if (toBeParent.Value == value)
                throw new ArgumentException("Node already exists");

            if (toBeParent.Value > value && toBeParent.LeftChild != null)
                toBeParent = toBeParent.LeftChild;
            else if (toBeParent.Value < value && toBeParent.RightChild != null)
                toBeParent = toBeParent.RightChild;
            else
                return toBeParent;
        }
    }

    private Node? FindNode(int value)
    {
        var node = _root;
        while (node != null)
        {
            if (node.Value == value) return node;
            else node = (node.Value > value) ? node.LeftChild : node.RightChild;
        }

        return null;
    }

    private Node? FindParentNode(Node node)
    {
        if (node == _root) return null;

        var currentNode = _root;
        while (currentNode != null)
        {
            if (currentNode.LeftChild == node || currentNode.RightChild == node)
                return currentNode;
            else
                currentNode = (currentNode.Value > node.Value) ? currentNode.LeftChild : currentNode.RightChild;
        }

        throw new KeyNotFoundException();
    }

    private Node FindLeftMostNode(Node node)
    {
        var leftMostNode = node;
        while (leftMostNode.LeftChild != null)
            leftMostNode = leftMostNode.LeftChild;

        return leftMostNode;
    }

    private Node FindRightMostNode(Node node)
    {
        var rightMostNode = node;
        while (rightMostNode.RightChild != null)
            rightMostNode = rightMostNode.RightChild;

        return rightMostNode;
    }

    private Node FindRightMostNodeRecursive(Node node)
    {
        if (node.RightChild == null)
            return node;

        return FindRightMostNodeRecursive(node.RightChild);
    }

    private bool IsLeaf(Node node)
    {
        return node.LeftChild == null && node.RightChild == null;
    }

    public bool IsBalanced()
    {
        return IsBalanced(_root);
    }

    private bool IsBalanced(Node? root)
    {
        if (root == null)
            return true;

        var balanceFactor = Height(root.LeftChild) - Height(root.RightChild);

        return Math.Abs(balanceFactor) <= 1
            && IsBalanced(root.LeftChild)
            && IsBalanced(root.RightChild);
    }

    public bool IsPerfect()
    {
        return Size() == (Math.Pow(2, Height() + 1) - 1);
    }
}