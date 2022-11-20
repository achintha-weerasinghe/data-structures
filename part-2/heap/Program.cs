var queue = new MinPriorityQueue(4);

queue.Add("Sehansha", 3);
queue.Add("Achintha", 1);
queue.Add("Punsara", 2);
queue.Add("Dimashi", 4);

while (!queue.IsEmpty())
    Console.WriteLine(queue.Remove());