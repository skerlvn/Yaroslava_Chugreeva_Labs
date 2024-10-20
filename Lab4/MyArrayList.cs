using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    public class MyArrayList<T>
    {
        private T[] elementData; 
        private int size;        

        public MyArrayList()
        {
            elementData = new T[10];
            size = 0;
        }

        public MyArrayList(T[] a)
        {
            elementData = new T[a.Length];
            Array.Copy(a, elementData, a.Length);
            size = a.Length;
        }

        public MyArrayList(int capacity)
        {
            elementData = new T[capacity];
            size = 0;
        }

        public void Add(T e)
        {
            EnsureCapacity(size + 1);
            elementData[size++] = e;
        }

        public void AddAll(T[] a)
        {
            EnsureCapacity(size + a.Length);
            Array.Copy(a, 0, elementData, size, a.Length);
            size += a.Length;
        }

        public void Clear()
        {
            Array.Clear(elementData, 0, size);
            size = 0;
        }

        public bool Contains(object o)
        {
            return IndexOf(o) >= 0;
        }

        public bool ContainsAll(T[] a)
        {
            foreach (var item in a)
            {
                if (!Contains(item))
                    return false;
            }
            return true;
        }

        public bool IsEmpty()
        {
            return size == 0;
        }

        public bool Remove(object o)
        {
            int index = IndexOf(o);
            if (index >= 0)
            {
                RemoveAt(index);
                return true;
            }
            return false;
        }

        public void RemoveAll(T[] a)
        {
            foreach (var item in a)
            {
                Remove(item);
            }
        }
        public void RetainAll(T[] a)
        {
            List<T> retainList = new List<T>(a);
            for (int i = size - 1; i >= 0; i--)
            {
                if (!retainList.Contains(elementData[i]))
                {
                    RemoveAt(i);
                }
            }
        }

        public int Size()
        {
            return size;
        }

        public T[] ToArray()
        {
            T[] result = new T[size];
            Array.Copy(elementData, result, size);
            return result;
        }

        public T[] ToArray(T[] a)
        {
            if (a == null || a.Length < size)
            {
                return ToArray(); 
            }
            Array.Copy(elementData, a, size);
            if (a.Length > size)
            {
                a[size] = default(T);
            }
            return a;
        }

        public void Add(int index, T e)
        {
            CheckRangeForAdd(index);
            EnsureCapacity(size + 1);
            Array.Copy(elementData, index, elementData, index + 1, size - index);
            elementData[index] = e;
            size++;
        }
        public void AddAll(int index, T[] a)
        {
            CheckRangeForAdd(index);
            EnsureCapacity(size + a.Length);
            Array.Copy(elementData, index, elementData, index + a.Length, size - index);
            Array.Copy(a, 0, elementData, index, a.Length);
            size += a.Length;
        }
        public T Get(int index)
        {
            CheckRange(index);
            return elementData[index];
        }
        public int IndexOf(object o)
        {
            for (int i = 0; i < size; i++)
            {
                if (Equals(elementData[i], o))
                    return i;
            }
            return -1;
        }

        public int LastIndexOf(object o)
        {
            for (int i = size - 1; i >= 0; i--)
            {
                if (Equals(elementData[i], o))
                    return i;
            }
            return -1;
        }

        public T RemoveAt(int index)
        {
            CheckRange(index);
            T oldValue = elementData[index];
            int numMoved = size - index - 1;
            if (numMoved > 0)
                Array.Copy(elementData, index + 1, elementData, index, numMoved);
            elementData[--size] = default(T);
            return oldValue;
        }
        public T Set(int index, T e)
        {
            CheckRange(index);
            T oldValue = elementData[index];
            elementData[index] = e;
            return oldValue;
        }
        public MyArrayList<T> SubList(int fromIndex, int toIndex)
        {
            CheckRange(fromIndex);
            CheckRange(toIndex);
            if (fromIndex > toIndex) throw new ArgumentException("fromIndex больше toIndex");

            int subListSize = toIndex - fromIndex;
            T[] subArray = new T[subListSize];
            Array.Copy(elementData, fromIndex, subArray, 0, subListSize);
            return new MyArrayList<T>(subArray);
        }
        private void EnsureCapacity(int minCapacity)
        {
            if (minCapacity > elementData.Length)
            {
                int newCapacity = elementData.Length * 3 / 2 + 1;
                if (newCapacity < minCapacity)
                    newCapacity = minCapacity;
                T[] newArray = new T[newCapacity];
                Array.Copy(elementData, newArray, size);
                elementData = newArray;
            }
        }
        private void CheckRange(int index)
        {
            if (index < 0 || index >= size)
                throw new IndexOutOfRangeException($"Индекс {index} вне границ массива.");
        }
        private void CheckRangeForAdd(int index)
        {
            if (index < 0 || index > size)
                throw new IndexOutOfRangeException($"Индекс {index} вне допустимого диапазона.");
        }
    }

}
