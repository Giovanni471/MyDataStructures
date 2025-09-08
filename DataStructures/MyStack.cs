using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDataStructures.DataStructures
{
    public class MyStack<T> : IEnumerable<T>, ICollection<T>
    {
        private int _size;
        private MyLinkedNode<T>? _head;

        public int Count => _size;

        public bool IsReadOnly => false;

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

        public void Add(T item)
        {
            Push(item);
        }

        public void Clear()
        {
            _head = null;
            _size = 0;
        }

        public bool Contains(T item)
        {
            var current = _head;
            while (current is not null)
            {
                if(current.Value.Equals(item))
                    return true;

                current = current.Next;
            }

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array is null)
                throw new ArgumentNullException("array");

            if (array.Length < arrayIndex)
                throw new ArgumentOutOfRangeException("index");

            if (array.Length > _size)
                throw new ArgumentException("Insufficient space in the target location to copy the information.");

            if (array.Length - arrayIndex > _size)
                throw new ArgumentException("Insufficient space in the target location to copy the information.");

            if (_head is null)
                return;

            var current = _head;

            int i = arrayIndex;
            while (current is not null && i < array.Length)
            {
                array[i] = current.Value;

                current = current.Next;
                i++;
            }
        }


        public bool Remove(T item)
        {
            var current = _head;
            var currentParent = _head;
            while (current is not null)
            {
                if (current.Value.Equals(item))
                {
                    currentParent.Next = current.Next;
                    _size--;

                    return true;
                }

                currentParent = current;
                current = current.Next;
            }

            return false;
        }
    }
}
