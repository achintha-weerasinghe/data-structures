using System.Text;
using System.Text.RegularExpressions;

public class StringManipulation
{
    private static readonly HashSet<char> _vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u' };

    public static int CountVowels(string str)
    {
        var count = 0;
        foreach (var ch in str.ToLower())
            if (_vowels.Contains(ch))
                count++;

        return count;
    }

    public static string Reverse(string str)
    {
        // Creating a stack and use that to reverse works
        // But we need extra space to store the stack
        var sb = new StringBuilder();
        for (var i = str.Length - 1; i >= 0; i--)
            sb.Append(str.ElementAt(i));

        return sb.ToString();
    }

    public static string ReverseWords(string sentence)
    {
        var words = sentence.Trim().Split(" ");
        return string.Join(" ", words.Reverse());
    }

    public static bool AreRotationsIteration(string str1, string str2)
    {
        var length = str1.Length;

        if (length != str2.Length)
            return false;

        var offset = str2.IndexOf(str1[0]);

        while (offset >= 0)
        {
            for (var i = 0; i < length; i++)
                if (str1[i] != str2[(offset + i) % length])
                    return false;

            offset = str2.IndexOf(str1[0], offset + 1);
        }

        return true;
    }

    /*
        If the strings are too long, then we have to
        allocate too many space for the concatenated string.
        In that case, the above iteration method is better.
        Otherwise, this substring finder is simpler.
    */
    public static bool AreRotations(string str1, string str2)
    {
        return (str1.Length == str2.Length
            && (str1 + str1).Contains(str2));
    }

    // Using only a set may not preserve the order we add
    public static string RemoveDuplicates(string str)
    {
        var sb = new StringBuilder();
        var seen = new HashSet<char>();

        foreach (var ch in str)
        {
            if (!seen.Contains(ch))
            {
                seen.Add(ch);
                sb.Append(ch);
            }
        }

        return sb.ToString();
    }

    // Using hash table
    public static char MostRepeatedChar(string str)
    {
        var counts = new Dictionary<char, int>();

        foreach (var ch in str)
        {
            if (!counts.ContainsKey(ch))
                counts.Add(ch, 0);

            counts[ch]++;
        }

        var max = 0;
        var result = ' ';
        foreach (var pair in counts)
            if (pair.Value > max)
            {
                max = pair.Value;
                result = pair.Key;
            }

        return result;
    }

    // If we don't have access to hash table
    public static char MostRepeatedCharArray(string str)
    {
        var ASCII_SIZE = 256;
        var frequencies = new int[ASCII_SIZE];

        foreach (var ch in str)
            frequencies[ch]++;

        var max = 0;
        var result = ' ';
        for (var i = 0; i < ASCII_SIZE; i++)
            if (frequencies[i] > max)
            {
                max = frequencies[i];
                result = (char)i;
            }

        return result;
    }

    public static string TitleCase(string str)
    {
        if (str.Trim().Length == 0)
            return "";

        var words = Regex.Replace(str.Trim(), " +", " ").Split(" ");

        for (var i = 0; i < words.Length; i++)
            words[i] = words[i].Substring(0, 1).ToUpper()
                + words[i].Substring(1).ToLower();

        return string.Join(" ", words);
    }

    // O(n)
    public static bool AreAnagram(string first, string second)
    {
        const int ENGLISH_ALPHABET = 26;
        var frequencies = new int[ENGLISH_ALPHABET];

        foreach (var ch in first)
            frequencies[ch - 'a']++;

        foreach (var ch in second)
            frequencies[ch - 'a']--;

        foreach (var count in frequencies)
            if (count != 0)
                return false;

        return true;
    }

    public static bool IsPalindrome(string str)
    {
        int start = 0, end = str.Length - 1;

        while (start < end)
            if (str[start++] != str[end--])
                return false;

        return true;
    }
}