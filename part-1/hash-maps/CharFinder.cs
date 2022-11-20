public class CharFinder
{
    public static char FindFirstNonRepeatedChar(string str)
    {
        var dic = new Dictionary<char, int>();

        foreach (var ch in str)
            if (dic.ContainsKey(ch)) dic[ch]++;
            else dic.Add(ch, 1);

        foreach (var ch in str)
            if (dic[ch] == 1)
                return ch;

        return char.MinValue;
    }

    public static char FindFirstRepeatedChar(string str)
    {
        var set = new HashSet<char>();

        foreach (var ch in str)
        {
            if (set.Contains(ch))
                return ch;
            set.Add(ch);
        }

        return char.MinValue;
    }
}