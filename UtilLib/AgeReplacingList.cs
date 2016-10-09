using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilLib
{
    /// <summary>
    /// A list with a certain capacity. When this capacity is reached and more items is added, 
    /// the last updated items will be replaced by the new items added
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AgeReplacingList<T> : IList<T>
    {
        private List<T> _data;
        private List<int> _updateNumbers;
        private int _updateNumberCounter = int.MinValue;

        public int Count => _data.Count;
        public bool IsReadOnly => false;
        public int Capacity { get; }

        public T this[int index]
        {
            get { return _data[index]; }
            set
            {
                _data[index] = value;
                _updateNumbers[index] = ++_updateNumberCounter;
            }
        }

        public AgeReplacingList(int capacity)
        {
            _data = new List<T>(capacity);
            _updateNumbers = new List<int>(capacity);
            Capacity = capacity;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        public int IndexOf(T item) => _data.IndexOf(item);

        public void Insert(int index, T item)
        {
            _data.Insert(index, item);
            _updateNumbers[index] = ++_updateNumberCounter;
        }

        public void RemoveAt(int index)
        {
            _data.RemoveAt(index);
            _updateNumbers.RemoveAt(index);
        }

        public void Add(T item)
        {
            if (Capacity != Count)
            {
                _data.Add(item);
                _updateNumbers.Add(++_updateNumberCounter);
            }
            else
            {
                var index = _updateNumbers.IndexOf(_updateNumbers.Min());
                _data[index] = item;
                _updateNumbers[index] = ++_updateNumberCounter;
            }
        }

        public void Clear()
        {
            _data.Clear();
            _updateNumbers.Clear();
        }

        public bool Contains(T item) => _data.Contains(item);

        public void CopyTo(T[] array, int arrayIndex)
        {
            _data.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            try
            {
                var index = _data.IndexOf(item);
                _data.RemoveAt(index);
                _updateNumbers.RemoveAt(index);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
