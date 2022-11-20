var stack = new StackMin(10);

stack.Push(5);
stack.Push(2);
stack.Push(10);
stack.Push(2);
stack.Push(1);

Console.WriteLine(stack.ToString());
Console.WriteLine(stack.Min());

stack.Pop();

Console.WriteLine(stack.ToString());
Console.WriteLine(stack.Min());

stack.Pop();

Console.WriteLine(stack.ToString());
Console.WriteLine(stack.Min());