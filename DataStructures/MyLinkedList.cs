using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDataStructures.DataStructures
{
    public class MyLinkedList<T> : IEnumerable<T>, ICollection<T>
    {
        private int _length;
        private MyLinkedNode<T>? _head;
        private MyLinkedNode<T>? _tail;

        public int Length { get { return _length; } }
        public T? Last { get { return GetLast(); } }
        public T? First { get { return GetFirst(); } }

        public int Count => Length;
        public bool IsReadOnly => false;

        public MyLinkedList()
        {
            _head = null;
            _tail = null;
            _length = 0;
        }

        public MyLinkedList(T start)
        {
            _head = new MyLinkedNode<T>(start);
            _tail = new MyLinkedNode<T>(start);
            _length = 1;
        }

        public void AddLast(T value)
        {
            if (_head is null)
            {
                _head = new MyLinkedNode<T>(value);
                _tail = _head;
                _length++;
                return;
            }

            _tail.Next = new MyLinkedNode<T>(value);
            _tail = _tail.Next;
            _length++;
        }

        public void RemoveFirst()
        {
            if (_length == 0)
                return;

            _head = _head.Next;
            _length--;
        }

        public void AddFirst(T value)
        {
            if(_head is null)
            {
                _head = new MyLinkedNode<T>(value);
                _tail = _head;
                _length++;
                return;
            }

            var temp = new MyLinkedNode<T>(value);
            temp.Next = _head;
            _head = temp;

            _length++;
        }

        public void RemoveLast()
        {
            if (_length == 0)
                return;

            var current = _head;
            while (current.Next != _tail)
            {
                current = current.Next;
            }

            _tail = current;
            _length--;
        }

        private T? GetFirst()
        {
            if (_head is null)
                return default(T);

            return _head.Value;
        }

        private T? GetLast()
        {
            if (_tail is null)
                return default(T);

            return _tail.Value;
        }

        #region Interfaces
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
            AddLast(item);
        }

        public void Clear()
        {
            _head = null;
            _tail = null;

            _length = 0;
        }

        public bool Contains(T item)
        {
            if (_head is null)
                return false;

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
            if(array is null)
                throw new ArgumentNullException("array");

            if (array.Length < arrayIndex)
                throw new ArgumentOutOfRangeException("index");

            if (array.Length > _length)
                throw new ArgumentException("Insufficient space in the target location to copy the information.");

            if (array.Length - arrayIndex > _length)
                throw new ArgumentException("Insufficient space in the target location to copy the information.");

            if (_head is null)
                return;

            var current = _head;

            int i = arrayIndex;
            while(current is not null && i < array.Length)
            {
                array[i] = current.Value;
                
                current = current.Next;
                i++;
            }
        }

        public bool Remove(T item)
        {
            if (_head is null)
                return false;

            var current = _head;
            var currentParent = _head;
            while (current is not null)
            {
                if (current.Value.Equals(item))
                {
                    if(current == _head)
                    {
                        _head = null;
                        _tail = _head;
                        _length = 0;
                        return true;
                    }

                    currentParent.Next = current.Next;
                    _length--;
                    return true;
                }

                currentParent = current;
                current = current.Next;
            }
            return false;
        }

        #endregion
    }

    public class MyLinkedNode<T>
    {
        public MyLinkedNode()
        {
            
        }

        public MyLinkedNode(T val)
        {
            Value = val;
        }

        public MyLinkedNode(T val, T next)
        {
            Value = val;
            Next = new MyLinkedNode<T>(next);
        }

        public T? Value { get;  set; }
        public MyLinkedNode<T>? Next {  get; set; }
    }
}
