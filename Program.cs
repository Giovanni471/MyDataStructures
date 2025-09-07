using MyDataStructures.DataStructures;

namespace MyDataStructures
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var lista = new MyDoubleLinkedList<int>(4);

            lista.AddFirst(5);
            lista.AddLast(15);
            lista.AddLast(55);

            lista.Remove(5);
            lista.Remove(4);

            foreach (int i in lista)
            {
                Console.WriteLine(i);
            }
        }
    }
}
