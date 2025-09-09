using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDataStructures.DataStructures
{
    public class MyHashSet<T> : IEnumerable<T>, ICollection<T>
    {
        private MyLinkedList<T>[] _bucket;
        private int _size;

        public int Count => _size;

        public bool IsReadOnly => false;

        public MyHashSet() 
        {
            _bucket = new MyLinkedList<T>[10]; // Starting length        
            _size = 0;
        }

        public MyHashSet(int size)
        {
            _bucket = new MyLinkedList<T>[size];
            _size = 0;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach(var item in _bucket)
            {
                if (item is null)
                    continue;

                foreach(var item2 in item)
                    yield return item2;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            var hashKey = HashFunction(item);

            while (hashKey > _bucket.Length)
                Resize();

            if (_bucket[hashKey] is null)
            {
                _bucket[hashKey] = new MyLinkedList<T>(item);
                _size++;
                return;
            }

            _bucket[hashKey].Add(item);
            _size++;
            return;
        }

        private void Resize()
        {
            var bucket = new MyLinkedList<T>[_bucket.Length * 2];
            
            _bucket.CopyTo(bucket, 0);
            _bucket = bucket;
        }

        public void Clear()
        {
            _bucket = new MyLinkedList<T>[_bucket.Length];
            _size = 0;
        }

        public bool Contains(T item)
        {
            var hashKey = HashFunction(item);

            if (hashKey > _bucket.Length)
                return false;

            if (_bucket[hashKey] is null)
                return false;
            
            return _bucket[hashKey].Contains(item);
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

            if (_bucket is null)
                return;


            int i = 0;
            foreach (var item in _bucket)
            {
                if (item is null)
                    continue;

                foreach (var item2 in item)
                {
                    array[i] = item2;
                    i++;
                }
            }
        }

        public bool Remove(T item)
        {
            var hashKey = HashFunction(item);

            if (hashKey > _bucket.Length)
                return false;

            if (_bucket[hashKey] is null)
                return false;
            
            return _bucket[hashKey].Remove(item); ;
        }

        private int HashFunction(T key)
        {
            if (key is null)
                throw new ArgumentNullException(nameof(key));

            var hashCode = CalculateHash(key.ToString());
            return Math.Abs(hashCode);
        }

        private int CalculateHash(string val)
        {
            int result = 1;

            if (val is null)
                return result;

            result = val.GetHashCode() % 100; // MaxLength 100 
            return result;
        }
    }
}
