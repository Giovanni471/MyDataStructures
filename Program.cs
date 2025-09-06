using MyDataStructures.DataStructures;

namespace MyDataStructures
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var list = new MyLinkedList<int>();

            list.Add(1);
            list.Add(3);
            list.Add(4);

            var arr = new int[3];
            list.CopyTo(arr, 0);


            foreach (int i in arr)
            {
                Console.WriteLine(i);
            }

            //Console.WriteLine(list.First);

            var lista = new LinkedList<int>();
            lista.AddLast(1);
            lista.AddLast(2);
            lista.AddLast(3);

            arr = new int[3];
            lista.CopyTo(arr, 0);

            foreach (int i in arr) 
            {
                Console.WriteLine(i);
            }
        }
    }
}
