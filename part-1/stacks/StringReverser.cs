using System.Text;

public class StringReverser
{
    public string reverse(string str)
    {
        var stack = new Stack<char>(str);
        var sb = new StringBuilder();

        while (stack.Count != 0)
            sb.Append(stack.Pop());

        return sb.ToString();
    }
}