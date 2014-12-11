using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Markup;
using System.Collections;
using System.Windows.Media.Animation;
using System.Windows;

namespace StandartObjectLibrary
{
    public abstract class IntervalCollectionBase<T> : IList, IList<T>
    {
        private List<T> list = new List<T>();

        #region IList Members

        public int Add(object value)
        {
            list.Add((T)value);
            return list.Count;
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(object value)
        {
            return list.Contains((T)value);
        }

        public int IndexOf(object value)
        {
            return list.IndexOf((T)value);
        }

        public void Insert(int index, object value)
        {
            list.Insert(index, (T)value);
        }

        public bool IsFixedSize
        {
            get { return false; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public void Remove(object value)
        {
            list.Remove((T)value);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        object IList.this[int index]
        {
            get { return (T)list[index]; }
            set { list[index] = (T)value; }
        }

        #endregion

        #region ICollection Members

        public void CopyTo(Array array, int index)
        {
            T[] Intervals = new T[list.Count];
            list.CopyTo(Intervals, index);

            Converter<T, object> converter = delegate(T Interval)
            {
                return (object)Interval;
            };

            array = Array.ConvertAll<T, object>(Intervals, converter);
        }

        public int Count
        {
            get { return list.Count; }
        }

        public bool IsSynchronized
        {
            get { return false; }
        }

        public object SyncRoot
        {
            get { return null; }
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            T[] Intervals = new T[list.Count];
            list.CopyTo(Intervals);

            Converter<T, object> converter = delegate(T Interval)
            {
                return (object)Interval;
            };

            return new IntervalEnumerator(Array.ConvertAll<T, object>(Intervals, converter));
        }

        #endregion

        #region IList<T> Members

        public int IndexOf(T item)
        {
            return list.IndexOf(item);
        }

        public virtual void Insert(int index, T item)
        {
            list.Insert(index, item);
        }

        public virtual T this[int index]
        {
            get { return list[index]; }
            set { list[index] = value; }
        }

        #endregion

        #region ICollection<T> Members

        public virtual void Add(T item)
        {
            list.Add(item);
        }

        public virtual bool Contains(T item)
        {
            return list.Contains(item);
        }

        public virtual void CopyTo(T[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public virtual bool Remove(T item)
        {
            return list.Remove(item);
        }

        #endregion

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return new GenericEnumerator<T>(list.ToArray());
        }

        #endregion
    }

    public class IntervalEnumerator : IEnumerator
    {
        private object[] Intervals;
        int position = -1;

        public IntervalEnumerator(object[] Intervals)
        {
            this.Intervals = Intervals;
        }

        #region IEnumerator Members

        public object Current
        {
            get { return Intervals[position]; }
        }

        public bool MoveNext()
        {
            position++;

            return (position < Intervals.Length);
        }

        public void Reset()
        {
            position = -1;
        }

        #endregion
    }

    public class GenericEnumerator<T> : IEnumerator<T>
    {
        private T[] Intervals;
        int position = -1;

        public GenericEnumerator(T[] Intervals)
        {
            this.Intervals = Intervals;
        }

        #region IEnumerator<T> Members

        public T Current
        {
            get { return Intervals[position]; }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            Intervals = null;
        }

        #endregion

        #region IEnumerator Members

        object IEnumerator.Current
        {
            get { return Intervals[position]; }
        }

        public bool MoveNext()
        {
            position++;
            return (position < Intervals.Length);
        }

        public void Reset()
        {
            position = -1;
        }

        #endregion
    }

}
