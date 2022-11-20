var map = new HashMap(5);

map.Put(1, "Punsara");
map.Put(3, "Achintha");
map.Put(6, "Hirushi");
map.Put(8, "Dimashi");
map.Put(13, "Malith");
map.Put(13, "Rasika");
map.Put(1, "Nimal");

Console.WriteLine($"Before {map.Get(13)}");
map.Remove(5);
Console.WriteLine($"After {map.Get(13)}");