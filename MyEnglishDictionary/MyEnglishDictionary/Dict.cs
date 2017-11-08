using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Linq;

namespace MyEnglishDictionary
{
    public class Dict<K, V> : IEnumerable<KeyValuePair<K, V>>
    {
        private const int INITIAL_SIZE = 16;
        LinkedList<KeyValuePair<K, V>>[] values;

        public Dict()
        {
            this.values = new LinkedList<KeyValuePair<K, V>>[INITIAL_SIZE];
        }

        public int Count { get; private set; }
        public int Capacity
        {
            get
            {
                return this.values.Length;
            }
        }

        public void Add(K key, V value)
        {
            this.Count++;

            if (this.Count >= 0.75 * this.Capacity)
            {
                this.ResizeAndReAddValues();
            }
            var hash = this.HashKey(key);

            if (this.values[hash] == null)
            {
                this.values[hash] = new LinkedList<KeyValuePair<K, V>>();
            }

            var keyExistsAlready = this.values[hash].Any(p => p.Key.Equals(key));

            if (keyExistsAlready)
            {
                throw new ArgumentException("Key already exitst. You cannot add 2 elements with the same key!");
            }
            var pair = new KeyValuePair<K, V>(key, value);
            this.values[hash].AddLast(pair);

        }

        public V Find(K key)
        {
            var hash = HashKey(key);

            if (this.values[hash] == null)
            {
                return default(V);
            }
            var collection = this.values[hash];
            //try
            //{
                var result = collection.First(p => p.Key.Equals(key)).Value;

                return result;
            //}
            //catch (InvalidOperationException ex)
            //{
            //    Console.WriteLine("Not found");
            //    return null;
            //}


        }

        public bool ContainsKey(K key)
        {
            var hash = HashKey(key);

            if (this.values[hash] == null)
            {
                return false;
            }
            var collection = this.values[hash];
            return collection.Any(pair => pair.Key.Equals(key));
        }

        private int HashKey(K key)
        {
            var hash = Math.Abs(key.GetHashCode()) % this.Capacity;
            return hash;
        }
        private void ResizeAndReAddValues()
        {
            //cache values
            var cachedValues = this.values;

            //resize
            this.values = new LinkedList<KeyValuePair<K, V>>[2 * this.Capacity];

            //Add values
            this.Count = 0;
            foreach (var collection in cachedValues)
            {
                if (collection != null)
                {
                    foreach (var value in collection)
                    {
                        this.Add(value.Key, value.Value);
                    }
                }
            }
        }

        //public void Remove(K key)
        //{
        //    this.Count--;

        //    var hash = this.HashKey(key);

        //    if (this.values[hash] == null)
        //    {
        //        this.values[hash] = new LinkedList<KeyValuePair<K, V>>();
        //    }

        //    var keyExistsAlready = this.values[hash].Any(p => p.Key.Equals(key));

        //    if (keyExistsAlready)
        //    {
        //        throw new ArgumentException("Key already exitst. You cannot add 2 elements with the same key!");
        //    }
        //    var pair = new KeyValuePair<K, V>(key, value);
        //    this.values[hash].AddLast(pair);

        //}

        public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
        {
            foreach (var collection in this.values)
            {
                if (collection != null)
                {
                    foreach (var value in collection)
                    {
                        yield return value;
                    }
                }
            }
          //  return (IEnumerator<KeyValuePair<K, V>>)this.values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        //public V this[K tKey]
        //{
        //    get
        //    {
        //        int hashCode = tKey.GetHashCode();
        //        int bucketNumber = HashKey(tKey);
        //        int numItem = this.values[bucketNumber];
        //        while (entries[numItem].hashCode != hashCode)
        //        {
        //            numItem = entries[numItem].next;
        //        }
        //        return entries[numItem].value;
        //    }
        //}


    }
}
