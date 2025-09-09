using MyDataStructures.DataStructures;

namespace MyDataStructures
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var MyHashTable = new MyHashTable<string, string>(10);

            MyHashTable.Add("test", "test");
            MyHashTable.Add("test", "testProva");
            MyHashTable.Add("tets", "test2");

            Console.WriteLine(MyHashTable["test"]);
        }
    }
}
