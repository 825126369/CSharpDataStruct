using System;
using System.Diagnostics;

namespace xkDic
{
    public class xkDic<TKey, TValue>
    {
        private int[] _buckets;
        private Entry[] _entries;

        private int _count;
        private int _freeList;
        private int _freeCount;
        private int _version;

        private const int StartOfFreeList = -3;

        private struct Entry
        {
            public uint hashCode;
            /// <summary>
            /// 0-based index of next entry in chain: -1 means end of chain
            /// also encodes whether this entry _itself_ is part of the free list by changing sign and subtracting 3,
            /// so -2 means end of free list, -3 means index 0 but on free list, -4 means index 1 but on free list, etc.
            /// </summary>
            public int next;
            public TKey key;     // Key of entry
            public TValue value; // Value of entry
        }

        private int Initialize(int capacity)
        {
            int size = HashHelpers.GetPrime(capacity);
            int[] buckets = new int[size];
            Entry[] entries = new Entry[size];

            // Assign member variables after both arrays allocated to guard against corruption from OOM if second fails
            _freeList = -1;
            _buckets = buckets;
            _entries = entries;

            return size;
        }

        public bool Add(TKey key, TValue value)
        {
            if (key == null)
            {
                throw new Exception();
            }

            if (_buckets == null)
            {
                Initialize(0);
            }

            Debug.Assert(_buckets != null);

            Entry[] entries = _entries;
            Debug.Assert(entries != null, "expected entries to be non-null");

            uint hashCode = (uint)key.GetHashCode();
            
            uint collisionCount = 0;
            ref int bucket = ref GetBucket(hashCode);
            int i = bucket - 1; // Value in _buckets is 1-based

            while (true)
            {
                if ((uint)i >= (uint)entries.Length)
                {
                    break;
                }

                if (entries[i].hashCode == hashCode && entries[i].key.Equals(key))
                {
                    return false;
                }

                i = entries[i].next;
                
                collisionCount++;
                if (collisionCount > (uint)entries.Length)
                {
                    // The chain of entries forms a loop; which means a concurrent update has happened.
                    // Break out of the loop and throw, rather than looping forever.
                    throw new Exception();
                }
            }

            int index;
            if (_freeCount > 0)
            {
                index = _freeList;
                Debug.Assert((StartOfFreeList - entries[_freeList].next) >= -1, "shouldn't overflow because `next` cannot underflow");
                _freeList = StartOfFreeList - entries[_freeList].next;
                _freeCount--;
            }
            else
            {
                int count = _count;
                if (count == entries.Length)
                {
                    Resize();
                    bucket = ref GetBucket(hashCode);
                }
                index = count;
                _count = count + 1;
                entries = _entries;
            }

            ref Entry entry = ref entries[index];
            entry.hashCode = hashCode;
            entry.next = bucket - 1; // Value in _buckets is 1-based
            entry.key = key;
            entry.value = value;
            bucket = index + 1; // Value in _buckets is 1-based
            _version++;

            return true;
        }

        public bool FindValue(TKey key, out TValue value)
        {
            value = default;
            Debug.Assert(_entries != null, "expected entries to be != null");

            uint hashCode = (uint)key.GetHashCode();
            int i = GetBucket(hashCode);
            Entry[] entries = _entries;
            uint collisionCount = 0;

            i--;
            do
            {
                if ((uint)i >= (uint)entries.Length)
                {
                    return false;
                }

                Entry entry = entries[i];
                if (entry.hashCode == hashCode && entry.key.Equals(key))
                {
                    value = entry.value;
                    return true;
                }

                i = entry.next;

                collisionCount++;
            } while (collisionCount <= (uint)entries.Length);

            return false;

        }

        public bool Remove(TKey key)
        {
            if (key == null)
            {
                throw new Exception();
            }
            
            uint collisionCount = 0;
            uint hashCode = (uint)key.GetHashCode();
            ref int bucket = ref GetBucket(hashCode);
            Entry[] entries = _entries;
            int last = -1;
            int i = bucket - 1; // Value in buckets is 1-based
            while (i >= 0)
            {
                ref Entry entry = ref entries[i];
                if (entry.hashCode == hashCode && entry.key.Equals(key))
                {
                    if (last < 0)
                    {
                        bucket = entry.next + 1; // Value in buckets is 1-based
                    }
                    else
                    {
                        entries[last].next = entry.next;
                    }

                    Debug.Assert((StartOfFreeList - _freeList) < 0, "shouldn't underflow because max hashtable length is MaxPrimeArrayLength = 0x7FEFFFFD(2146435069) _freelist underflow threshold 2147483646");
                    entry.next = StartOfFreeList - _freeList;

                    entry.key = default;
                    entry.value = default;
                    
                    _freeList = i;
                    _freeCount++;
                    return true;
                }

                last = i;
                i = entry.next;

                collisionCount++;
                if (collisionCount > (uint)entries.Length)
                {
                    throw new Exception();
                }
            }

            return false;
        }

        public void Clear()
        {
            int count = _count;
            if (count > 0)
            {
                Array.Clear(_buckets, 0, _buckets.Length);

                _count = 0;
                _freeList = -1;
                _freeCount = 0;
                Array.Clear(_entries, 0, count);
            }
        }

        public void Resize()
        {
            int newSize = HashHelpers.ExpandPrime(_count);

            // Value types never rehash
            Debug.Assert(_entries != null, "_entries should be non-null");
            Debug.Assert(newSize >= _entries.Length);

            Entry[] entries = new Entry[newSize];

            int count = _count;
            Array.Copy(_entries, entries, count);

            _buckets = new int[newSize];
            for (int i = 0; i < count; i++)
            {
                if (entries[i].next >= -1)
                {
                    ref int bucket = ref GetBucket(entries[i].hashCode);
                    entries[i].next = bucket - 1; // Value in _buckets is 1-based
                    bucket = i + 1;
                }
            }

            _entries = entries;
        }

        private ref int GetBucket(uint hashCode)
        {
            int[] buckets = _buckets;
            return ref buckets[hashCode % (uint)buckets.Length];
        }

    }
}
