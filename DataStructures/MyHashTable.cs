using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyDataStructures.DataStructures
{
    public class MyHashTable<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private MyLinkedList<(TKey k, TValue v)>[] _bucket;
        private int _capacity;
        public MyHashTable()
        {
            _bucket = new MyLinkedList<(TKey k, TValue v)>[10];
            _capacity = 0;
        }

        public MyHashTable(int size)
        {
            _bucket = new MyLinkedList<(TKey k, TValue v)>[size];
            _capacity = 0;
        }

        public TValue this[TKey key] { get => GetValue(key); set => Add(key, value); }

        private TValue GetValue(TKey? key)
        {
            var keyHashed = HashFunction(key);

            if (_bucket[keyHashed] is null) 
            {
                throw new ArgumentNullException();
            }

            return _bucket[keyHashed].FirstOrDefault(x => x.k.Equals(key)).v;
        }

        public ICollection<TKey> Keys => _bucket.Where(x => x is not null).SelectMany(x => x.Select(y => y.k)).ToList();

        public ICollection<TValue> Values => _bucket.Where(x => x is not null).SelectMany(x => x.Select( y => y.v)).ToList();

        public int Count => _capacity;

        public bool IsReadOnly => false;

        public void Add(TKey key, TValue value)
        {
            var keyHashed = HashFunction(key);

            while (keyHashed > _bucket.Length)
                Resize();

            if(_bucket[keyHashed] is null)
            {
                _bucket[keyHashed] = new MyLinkedList<(TKey k, TValue v)>((key, value));
                _capacity++;
                return;
            }

            if(_bucket[keyHashed].Any(x => x.k.Equals(key)))
            {
                var element = _bucket[keyHashed].FirstOrDefault(x => x.k.Equals(key));
                _bucket[keyHashed].Remove(element);
                _capacity--;
            }

            _bucket[keyHashed].AddFirst((key, value));
            _capacity++;
        }

        private void Resize()
        {
            var arr = new MyLinkedList<(TKey k, TValue v)>[_bucket.Length * 2]; // Resize logic without losing elements 

            _bucket.CopyTo(arr, 0);
            _bucket = arr;
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            _bucket = new MyLinkedList<(TKey k, TValue v)>[_bucket.Length];
            _capacity = 0;
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            var keyHashed = HashFunction(item.Key);

            if (_bucket[keyHashed] is null)
                return false;
            
            var current = _bucket[keyHashed];
            foreach (var v in current)
            {
                if(v.Equals(item.Value))
                    return true;
            }

            return false;
        }

        public bool ContainsKey(TKey key)
        {
            var keyHashed = HashFunction(key);

            return _bucket[keyHashed] is not null;
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (array is null)
                throw new ArgumentNullException("array");

            if (array.Length < arrayIndex)
                throw new ArgumentOutOfRangeException("index");

            if (array.Length > _capacity)
                throw new ArgumentException("Insufficient space in the target location to copy the information.");

            if (array.Length - arrayIndex > _capacity)
                throw new ArgumentException("Insufficient space in the target location to copy the information.");

            if (_bucket is null)
                return;

            int i = 0;
            foreach (var arr in _bucket)
            {
                if (arr is null)
                    continue;

                foreach (var element in arr)
                {
                    array[i] = new KeyValuePair<TKey, TValue>(element.k, element.v);
                    i++;
                }
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (var arr in _bucket)
            {
                if (arr is null)
                    continue;

                foreach (var element in arr)
                {
                    yield return new KeyValuePair<TKey, TValue>(element.k, element.v);
                }
            }
        }

        public bool Remove(TKey key)
        {
            var keyHashed = HashFunction(key);

            if (_bucket[keyHashed] is not null)
            {
                foreach(var arr in _bucket[keyHashed])
                {
                    if (arr.k.Equals(key))
                    {
                        _bucket[keyHashed].Remove(arr);
                        _capacity--;
                        return true;
                    }
                }                
            }

            return false;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key);
        }

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            var keyHashed = HashFunction(key);

            if (_bucket[keyHashed] is not null)
            {
                value = _bucket[keyHashed].First(x => x.k.Equals(key)).v;
                return true;
            }

            value = default;
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private int HashFunction(TKey key)
        {
            if (key is null) 
                throw new ArgumentNullException(nameof(key));

            var hashCode = CalculateHash(key.ToString());
            return hashCode & 0x7FFFFFFF;
        }

        private int CalculateHash(string val)
        {
            int result = 1;

            if (val == null)
                return result;

            result = val.GetHashCode() % 100; // MaxLength 100 
            return result;
        }
    }
}
