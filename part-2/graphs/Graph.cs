public class Graph
{
    private class Node
    {
        public string Label { get; }
        public Node(string label)
        {
            Label = label;
        }

        public override string ToString()
        {
            return Label;
        }
    }

    private Dictionary<Node, List<Node>> _adjacencyList = new Dictionary<Node, List<Node>>();
    private Dictionary<string, Node> _nodes = new Dictionary<string, Node>();

    public void AddNode(string label)
    {
        var node = new Node(label);
        _nodes.Add(label, node);
        _adjacencyList.Add(node, new List<Node>()); // So we don't need to perform null check later
    }

    public void AddEdge(string from, string to)
    {
        var fromNode = _nodes.GetValueOrDefault(from);
        var toNode = _nodes.GetValueOrDefault(to);

        if (fromNode == null || toNode == null)
            throw new ArgumentNullException("Nodes are null");

        var edgesList = _adjacencyList[fromNode];
        foreach (var edge in edgesList)
            if (edge == toNode)
                return;

        edgesList.Add(toNode);
    }

    public void RemoveNode(string label)
    {
        var node = _nodes.GetValueOrDefault(label);
        if (node == null)
            return;

        foreach (var list in _adjacencyList.Values)
            list.Remove(node);

        _adjacencyList.Remove(node);
        _nodes.Remove(label);
    }

    public void RemoveEdge(string from, string to)
    {
        var fromNode = _nodes.GetValueOrDefault(from);
        var toNode = _nodes.GetValueOrDefault(to);

        if (fromNode == null || toNode == null)
            return;

        var list = _adjacencyList[fromNode];
        list.Remove(toNode);
    }

    public void TraverseDepthFirst(string from)
    {
        var fromNode = _nodes.GetValueOrDefault(from);
        if (fromNode == null)
            throw new NullReferenceException("Node not found!");

        TraverseDepthFirst(fromNode, new HashSet<Node>());
    }

    private void TraverseDepthFirst(Node node, HashSet<Node> visited)
    {
        Console.WriteLine(node);
        visited.Add(node);

        foreach (var n in _adjacencyList[node])
            if (!visited.Contains(n))
                TraverseDepthFirst(n, visited);
    }

    public void TraverseDepthFirstIterative(string from)
    {
        var fromNode = _nodes.GetValueOrDefault(from);
        if (fromNode == null)
            throw new NullReferenceException("Node not found!");

        var visited = new HashSet<Node>();

        var stack = new Stack<Node>();
        stack.Push(fromNode);

        while (stack.Count > 0)
        {
            var current = stack.Pop();

            if (visited.Contains(current))
                continue;

            Console.WriteLine(current);
            visited.Add(current);

            foreach (var n in _adjacencyList[current])
                if (!visited.Contains(n))
                    stack.Push(n);
        }
    }

    public void TraverseBreadthFirstIterative(string from)
    {
        var fromNode = _nodes.GetValueOrDefault(from);
        if (fromNode == null)
            throw new NullReferenceException("Node not found!");

        var visited = new HashSet<Node>();

        var queue = new Queue<Node>();
        queue.Enqueue(fromNode);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if (visited.Contains(current))
                continue;

            Console.WriteLine(current);
            visited.Add(current);

            foreach (var n in _adjacencyList[current])
                if (!visited.Contains(n))
                    queue.Enqueue(n);
        }
    }

    // Since we don't have a root node to start,
    // we have to try start from every node in the graph with a loop.
    public List<string> TopologicalSort()
    {
        var stack = new Stack<Node>();
        var visited = new HashSet<Node>();

        foreach (var node in _nodes.Values)
            TopologicalSort(node, visited, stack);

        var sorted = new List<string>();
        while (stack.Count > 0)
            sorted.Add(stack.Pop().Label);

        return sorted;
    }

    // Depth-First traversal
    private void TopologicalSort(Node node, HashSet<Node> visited, Stack<Node> stack)
    {
        if (visited.Contains(node))
            return;

        visited.Add(node);

        foreach (var neighbor in _adjacencyList[node])
            TopologicalSort(neighbor, visited, stack);

        stack.Push(node);
    }

    public bool HasCycle()
    {
        var all = new HashSet<Node>();
        var visiting = new HashSet<Node>();
        var visited = new HashSet<Node>();

        foreach (var node in _nodes.Values)
            all.Add(node);

        while (all.Count > 0)
            if (HasCycle(all.First(), all, visiting, visited))
                return true;

        return false;
    }

    private bool HasCycle(Node current, HashSet<Node> all, HashSet<Node> visiting, HashSet<Node> visited)
    {
        all.Remove(current);
        visiting.Add(current);

        foreach (var neighbor in _adjacencyList[current])
        {
            if (visited.Contains(neighbor))
                continue;

            if (visiting.Contains(neighbor))
                return true;

            if (HasCycle(neighbor, all, visiting, visited))
                return true;
        }

        visiting.Remove(current);
        visited.Add(current);
        return false;
    }

    public void Print()
    {
        foreach (var pair in _adjacencyList)
        {
            if (pair.Value.Count > 0)
                Console.WriteLine($"{pair.Key.Label} is connected with [{string.Join(", ", pair.Value)}]");
        }
    }
}