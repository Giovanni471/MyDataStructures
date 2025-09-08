using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDataStructures.DataStructures
{
    public class MyStack<T> : IEnumerable<T>
    {
        private int _size;
        private MyLinkedNode<T>? _head;

        public int Length {  get { return _size; } }

        public MyStack() 
        {
            _head = null;
            _size = 0;
        }

        public MyStack(T Value) 
        {
            _head = new MyLinkedNode<T>(Value);
            _size = 1;
        }

        public void Push(T value)
        {
            if(_head == null)
            {
                _head = new MyLinkedNode<T>(value);
                _size = 1;
                return;
            }

            var temp = new MyLinkedNode<T>(value);
            temp.Next = _head;
            _head = temp;
            _size++;
        }

        public T? Pop() 
        {
            if (_head == null)
                throw new ArgumentNullException("this");

            var temp = _head.Value;

            _head = _head.Next;
            _size--;
            return temp;
        }

        public T? Peek()
        {
            if (_head == null)
                throw new ArgumentNullException("this");

            return _head.Value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = _head;
            while (current is not null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
