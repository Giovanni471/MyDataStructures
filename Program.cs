using MyDataStructures.DataStructures;

namespace MyDataStructures
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var queue = new MyQueue<int>();

            queue.Enqueue(1);
            queue.Enqueue(2);

            queue.Enqueue(3);
            queue.Dequeue();

            Console.WriteLine(queue.Peek());
            
            foreach (int i in queue) 
            {
                Console.WriteLine(i);
            }
            Console.WriteLine(queue.Length);
        }
    }
}
