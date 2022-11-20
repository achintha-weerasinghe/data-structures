class MyClass
{
    // O(1 + 1) => O(2) => O(1) - Constant
    public void Log0(int[] numbers)
    {
        Console.WriteLine(numbers[0]); // O(1)
        Console.WriteLine(numbers[0]); // O(1)
    }

    // O(n + m) => O(n) - Linear
    public void Log1(int[] numbers, string[] strings)
    {
        foreach (int n in numbers) // O(n)
            Console.WriteLine(n);

        foreach (string s in strings) // O(m)
            Console.WriteLine(s);
    }

    // O(n * n) => O(n^2)
    public void Log2(int[] numbers)
    {
        foreach (int n in numbers) // O(n)
            foreach (int m in numbers) // O(n)
                Console.WriteLine(n + m);
    }

    // O(n * n * n) => O(n^3)
    public void Log3(int[] numbers)
    {
        foreach (int n in numbers) // O(n)
            foreach (int m in numbers) // O(n)
                foreach (int o in numbers) // O(n)
                    Console.WriteLine(n + m + o);
    }

    // Space complexity
    // O(n)
    public void Log4(string[] strings)
    {
        var copy = new string[strings.Length]; // O(n)

        foreach (string s in strings) // O(1)
            Console.WriteLine(s);
    }
}
