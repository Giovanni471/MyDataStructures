using MyDataStructures.DataStructures;

namespace MyDataStructures
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var mySet = new MyHashSet<int>(3);
            
            mySet.Add(81); 
            mySet.Add(10);
            mySet.Add(30);

            var result1 = mySet.Contains(10);
        }
    }
}
