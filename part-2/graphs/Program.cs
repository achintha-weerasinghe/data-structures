var graph = new WeightedGraph();

graph.AddNode("A");
graph.AddNode("B");
graph.AddNode("C");
graph.AddNode("D");

graph.AddEdge("A", "B", 3);
graph.AddEdge("A", "C", 1);
graph.AddEdge("B", "D", 4);
graph.AddEdge("C", "D", 5);
graph.AddEdge("B", "C", 2);

var tree = graph.GetMinimumSpanningTree();
tree.Print();