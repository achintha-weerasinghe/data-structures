var stack = new StackWithTwoQueues();

stack.Push(10);
stack.Push(20);
stack.Push(30);
stack.Push(40);

while (!stack.IsEmpty())
    Console.Write($"{stack.Pop()}, ");