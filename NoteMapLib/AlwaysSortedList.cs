using System;
using System.Collections;
using System.Collections.Generic;

namespace NoteMapLib
{
    /// <summary>
    /// A list that sorts it self when it is manipulated.
    /// <remarks>
    /// Just like keys in a dictionary, the data in this list should not change once added to the list.
    /// </remarks>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AlwaysSortedList<T> : ICollection<T> where T : IComparable<T>
    {
        private readonly List<T> _data;

        public AlwaysSortedList()
        {
            _data = new List<T>();
        }

        public AlwaysSortedList(int capacity)
        {
            _data = new List<T>(capacity);
        }

        public AlwaysSortedList(IEnumerable<T> collection)
        {
            _data = new List<T>(collection);
            _data.Sort();
        }

        public T this[int index] => _data[index];
        public int Count => _data.Count;
        public int Capacity => _data.Capacity;
        public bool IsReadOnly => false;

        public void Clear() => _data.Clear();
        public bool Contains(T item) => _data.Contains(item);
        public void CopyTo(T[] array, int arrayIndex) => _data.CopyTo(array, arrayIndex);
        public int IndexOf(T item) => _data.IndexOf(item);
        public bool Remove(T item) => _data.Remove(item);
        public void RemoveAt(int index) => _data.RemoveAt(index);
        public IEnumerator<T> GetEnumerator() => _data.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _data.GetEnumerator();
        
        public void Add(T item)
        {
            var start = 0;
            var end = Count;

            while (start != end)
            {
                var mid = start + (end - start) / 2;
                var compTo = item.CompareTo(_data[mid]);

                if (compTo < 0)
                {
                    end = mid;
                }
                else if (compTo > 0)
                {
                    start = mid + 1;
                }
                else
                {
                    start = mid;
                    end = mid;
                }
            }

            _data.Insert(start, item);
        }

    }
}
