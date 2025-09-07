using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDataStructures.DataStructures
{
    public class MyDoubleLinkedList<T> : IEnumerable<T>, ICollection<T>
    {
        private MyDoubleLinkedListNode<T>? _head;
        private MyDoubleLinkedListNode<T>? _tail;
        private int _length;

        public int Count => _length;

        public bool IsReadOnly => false;

        public MyDoubleLinkedList()
        {
            _head = null;
            _tail = null;
            _length = 0;
        }

        public MyDoubleLinkedList(T Value)
        {
            _head = new MyDoubleLinkedListNode<T>(Value);
            _tail = _head;
            _length = 1;
        }

        public void AddLast(T Value)
        {
            if(_length == 0)
            {
                _head = new MyDoubleLinkedListNode<T>(Value);
                _tail = _head;
                _length = 1;
                return;
            }

            _tail.Right = new MyDoubleLinkedListNode<T>(Value);
            
            var temp = _tail;
            _tail = _tail.Right;
            _tail.Left = temp;

            _length++;
        }

        public void AddFirst(T Value)
        {
            if (_length == 0)
            {
                _head = new MyDoubleLinkedListNode<T>(Value);
                _tail = _head;
                _length = 1;
                return;
            }

            var temp = new MyDoubleLinkedListNode<T>(Value);
            temp.Right = _head;
            _head.Left = temp;

            _head = temp;
            _length++;
        }

        public void Add(T Value)
        {
            AddLast(Value);
        }

        public bool RemoveLast()
        {
            if(_length == 0)
                return false;

            if(_tail is null)
                return false;

            _tail = _tail.Left;
            _tail.Right = null;

            _length--;
            return true;
        }

        public bool RemoveFirst()
        {
            if (_length == 0)
                return false;

            _head = _head.Right;
            _head.Left = null;

            _length--;
            return true;
        }

        public bool Remove()
        {
            return RemoveLast();
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = _head;
            while (current is not null)
            {
                yield return current.Val;
                current = current.Right;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Clear()
        {
            _head = null;
            _tail = null;
            _length = 0;
        }

        public bool Contains(T item)
        {
            if(_length == 0)
                return false;

            var current = _head;
            while (current is not null)
            {
                if(current.Val.Equals(item))
                    return true;

                current = current.Right;
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array is null)
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
            while (current is not null && i < array.Length)
            {
                array[i] = current.Val;

                current = current.Right;
                i++;
            }
        }

        public bool Remove(T item)
        {
            if (_length == 0)
                return false;

            
            var current = _head;
            while (current is not null)
            {

                if (current.Val.Equals(item))
                {
                    var currentLeft = current.Left;
                    var currentRight = current.Right;

                    if(currentRight is not null)
                        currentRight.Left = currentLeft;
                    
                    if(currentLeft is not null)
                        currentLeft.Right = currentRight;

                    if(current == _head)
                    {
                        _head = currentRight;
                    }

                    _length--;

                    return true;
                }

                current = current.Right;
            }
            return false;
        }
    }

    public class MyDoubleLinkedListNode<T> 
    { 
        public T? Val;

        public MyDoubleLinkedListNode<T>? Left {  get; set; }
        public MyDoubleLinkedListNode<T>? Right {  get; set; }

        public MyDoubleLinkedListNode()
        {
            Left = null;
            Right = null;
        }

        public MyDoubleLinkedListNode(T Value) 
        {
            Val = Value;
            Left = null;
            Right = null;
        }

        public MyDoubleLinkedListNode(T Value, T left)
        {
            Val = Value;
            Left = new MyDoubleLinkedListNode<T>(left);
            Right = null;
        }

        public MyDoubleLinkedListNode(T Value, T left, T right)
        {
            Val = Value;
            Left = new MyDoubleLinkedListNode<T>(left);
            Right = new MyDoubleLinkedListNode<T>(right);
        }
    }
}
