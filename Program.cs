using MyDataStructures.DataStructures;

namespace MyDataStructures
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var stack = new MyStack<int>(5);

            stack.Push(20);
            stack.Push(30);
            stack.Push(40);


            Console.WriteLine(stack.Pop());
            Console.WriteLine(stack.Pop());
            Console.WriteLine(stack.Pop());

            Console.WriteLine(stack.Count);
        }
    }
}
