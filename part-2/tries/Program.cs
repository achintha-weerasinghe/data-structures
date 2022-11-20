var trie = new Trie();

trie.Insert("car");
trie.Insert("card");
trie.Insert("care");
trie.Insert("careful");
trie.Insert("cater");
trie.Insert("egg");

Console.WriteLine(trie.ContainsRecursive("cargo"));