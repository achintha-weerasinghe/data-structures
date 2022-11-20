public class Path
{
    private List<string> _nodes = new List<string>();

    public void Add(string node)
    {
        _nodes.Add(node);
    }

    public override string ToString()
    {
        return $"[{string.Join(", ", _nodes)}]";
    }
}

public class WeightedGraph
{
    private class Node
    {
        public string Label { get; }

        private List<Edge> _edges = new List<Edge>();
        public Node(string label)
        {
            Label = label;
        }

        public void AddEdge(Node to, int weight)
        {
            this._edges.Add(new Edge(this, to, weight));
        }

        public List<Edge> GetEdges()
        {
            return _edges;
        }

        public bool HasEdges()
        {
            return _edges.Count > 0;
        }

        public override string ToString()
        {
            return Label;
        }
    }
    private class Edge
    {
        public Node From { get; }
        public Node To { get; }
        public int Weight { get; }

        public Edge(Node from, Node to, int weight)
        {
            From = from;
            To = to;
            Weight = weight;
        }

        public override string ToString()
        {
            return $"{From}->{To}";
        }
    }

    private Dictionary<string, Node> _nodes = new Dictionary<string, Node>();

    public void AddNode(string label)
    {
        _nodes.Add(label, new Node(label));
    }

    public void AddEdge(string from, string to, int weight)
    {
        var fromNode = _nodes.GetValueOrDefault(from);
        var toNode = _nodes.GetValueOrDefault(to);

        if (fromNode == null || toNode == null)
            throw new NullReferenceException("Nodes are null");

        var hasEdge = false;
        foreach (var edge in fromNode.GetEdges())
            if (edge.To == toNode)
            {
                hasEdge = true;
                break;
            }

        if (!hasEdge)
            fromNode.AddEdge(toNode, weight);

        hasEdge = false;
        foreach (var edge in toNode.GetEdges())
            if (edge.To == fromNode)
            {
                hasEdge = true;
                break;
            }

        if (!hasEdge)
            toNode.AddEdge(fromNode, weight);
    }

    // Breadth first traversal is used.
    // However, when choosing children nodes,
    // the node with the shortest distance
    // will be chosen next using a priority queue.
    public Path GetShortestPath(string from, string to)
    {
        var fromNode = _nodes.GetValueOrDefault(from);
        var toNode = _nodes.GetValueOrDefault(to);

        if (fromNode == null || toNode == null)
            throw new NullReferenceException("node are null");

        var previousNodes = new Dictionary<Node, Node>();
        var distances = new Dictionary<Node, int>();
        foreach (var node in _nodes.Values)
            distances.Add(node, int.MaxValue);
        distances[fromNode] = 0;

        var visited = new HashSet<Node>();

        var queue = new PriorityQueue<Node, int>();
        queue.Enqueue(fromNode, 0);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if (visited.Contains(current))
                continue;

            visited.Add(current);
            foreach (var edge in current.GetEdges())
            {
                if (visited.Contains(edge.To))
                    continue;

                var newDistance = distances[current] + edge.Weight;
                if (distances[edge.To] > newDistance)
                {
                    distances[edge.To] = newDistance;
                    previousNodes[edge.To] = current;
                    queue.Enqueue(edge.To, newDistance);
                }
            }
        }

        return BuildPath(toNode, previousNodes);
    }

    private Path BuildPath(Node toNode, Dictionary<Node, Node> previousNodes)
    {
        var stack = new Stack<Node>();
        stack.Push(toNode);
        var previous = previousNodes.GetValueOrDefault(toNode);

        while (previous != null)
        {
            stack.Push(previous);
            previous = previousNodes.GetValueOrDefault(previous);
        }

        var path = new Path();
        while (stack.Count > 0)
            path.Add(stack.Pop().Label);

        return path;
    }

    public bool HasCycle()
    {
        var visited = new HashSet<Node>();

        foreach (var node in _nodes.Values)
            if (!visited.Contains(node) && HasCycle(node, null, visited))
                return true;

        return false;
    }

    private bool HasCycle(Node current, Node? parent, HashSet<Node> visited)
    {
        visited.Add(current);

        foreach (var edge in current.GetEdges())
        {
            if (edge.To == parent) continue;

            if (visited.Contains(edge.To) || HasCycle(edge.To, current, visited))
                return true;
        }

        return false;
    }

    public WeightedGraph GetMinimumSpanningTree()
    {
        var tree = new WeightedGraph();

        if (_nodes.Count == 0)
            return tree;

        var edges = new PriorityQueue<Edge, int>();

        var node = _nodes.Values.First();
        tree.AddNode(node.Label);
        foreach (var edge in node.GetEdges())
            edges.Enqueue(edge, edge.Weight);

        if (edges.Count == 0)
            return tree;

        while (_nodes.Count > tree._nodes.Count)
        {
            var minEdge = edges.Dequeue();
            var nextNode = minEdge.To;

            if (tree.ContainsNode(nextNode.Label))
                continue;

            tree.AddNode(nextNode.Label);
            tree.AddEdge(minEdge.From.Label, minEdge.To.Label, minEdge.Weight);

            foreach (var edge in nextNode.GetEdges())
                if (!tree.ContainsNode(edge.To.Label))
                    edges.Enqueue(edge, edge.Weight);
        }

        return tree;
    }

    public bool ContainsNode(string node)
    {
        return _nodes.ContainsKey(node);
    }

    public void Print()
    {
        foreach (var pair in _nodes)
        {
            if (pair.Value.GetEdges().Count > 0)
                Console.WriteLine($"{pair.Key} is connected with [{string.Join(", ", pair.Value.GetEdges())}]");
        }
    }
}
