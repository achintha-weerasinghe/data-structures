public class Expression
{
    private char[] _leftBrackets = new char[] { '(', '{', '[', '<' };
    private char[] _rightBrackets = new char[] { ')', '}', ']', '>' };
    private string _exp;
    public Expression(string exp)
    {
        _exp = exp;
    }

    public bool IsBalanced()
    {
        var stack = new Stack<char>();

        foreach (var ch in _exp)
        {
            if (IsLeftBracket(ch))
                stack.Push(ch);

            if (IsRightBracket(ch))
            {
                if (stack.Count == 0) return false;

                var top = stack.Pop();
                if (!BracketsMatch(top, ch)) return false;
            }
        }

        return stack.Count == 0;
    }

    private bool IsLeftBracket(char ch)
    {
        return _leftBrackets.Contains(ch);
    }

    private bool IsRightBracket(char ch)
    {
        return _rightBrackets.Contains(ch);
    }

    private bool BracketsMatch(char left, char right)
    {
        return Array.IndexOf(_leftBrackets, left) == Array.IndexOf(_rightBrackets, right);
    }
}