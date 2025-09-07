using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDataStructures.DataStructures
{
    /// <summary>
    /// Even if it was possible implementing a queue with a simple array,
    /// I choose to use my Linked Node structure to keep the operations in O(1) time.
    /// I delete from the head and push to the tail.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MyQueue<T> : IEnumerable<T>
    {
        private MyLinkedNode<T>? _head;
        private MyLinkedNode<T>? _tail;

        private int _size;

        public int Length { get { return _size; } }

        public MyQueue()
        {
            _head = null;
            _tail = null;
            _size = 0;
        }

        public MyQueue(T Value)
        {
            _head = new MyLinkedNode<T>(Value);
            _tail = _head;
            _size = 1;
        }

        public void Enqueue(T Value)
        {
            if(_size == 0)
            {
                _head = new MyLinkedNode<T>(Value);
                _tail = _head;
                _size = 1;

                return;
            }

            var temp = new MyLinkedNode<T>(Value);
            _tail.Next = temp;
            _tail = _tail.Next;

            _size++;
        }

        public T? Dequeue()
        {
            if (_size == 0) 
                return default;
            
            if (_head == null)
                return default;

            var temp = _head.Value;
            _head = _head.Next;
            _size--;
            return temp;
        }

        public T? Peek()
        {
            if (_head is null)
                return default;

            return _head.Value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = _head;
            while(current is not null)
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
