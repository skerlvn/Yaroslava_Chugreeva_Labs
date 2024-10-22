namespace Lab6
{
    public class MyVector<T>
    {
        private T[] elementData;     // Массив для хранения элементов вектора
        private int elementCount;    // Количество элементов в векторе
        private int capacityIncrement; // Величина приращения ёмкости при необходимости

        public MyVector(int initialCapacity, int capacityIncrement)
        {
            if (initialCapacity < 0) throw new ArgumentException("Capacity must be non-negative.");
            elementData = new T[initialCapacity];
            this.capacityIncrement = capacityIncrement;
            elementCount = 0;
        }

        public MyVector(int initialCapacity) : this(initialCapacity, 0) { }

        public MyVector() : this(10, 0) { }

        public MyVector(T[] a)
        {
            elementData = new T[a.Length];
            Array.Copy(a, elementData, a.Length);
            elementCount = a.Length;
            capacityIncrement = 0;
        }

        public void Add(T e)
        {
            EnsureCapacity(elementCount + 1);
            elementData[elementCount++] = e;
        }

        public void AddAll(T[] a)
        {
            EnsureCapacity(elementCount + a.Length);
            Array.Copy(a, 0, elementData, elementCount, a.Length);
            elementCount += a.Length;
        }

        public void Clear()
        {
            Array.Clear(elementData, 0, elementCount);
            elementCount = 0;
        }

        public bool Contains(object o)
        {
            return IndexOf(o) >= 0;
        }

        public bool ContainsAll(T[] a)
        {
            foreach (T item in a)
            {
                if (!Contains(item)) return false;
            }
            return true;
        }

        public bool IsEmpty()
        {
            return elementCount == 0;
        }

        public bool Remove(object o)
        {
            int index = IndexOf(o);
            if (index >= 0)
            {
                Remove(index);
                return true;
            }
            return false;
        }

        public void RemoveAll(T[] a)
        {
            foreach (T item in a)
            {
                Remove(item);
            }
        }

        public void RetainAll(T[] a)
        {
            for (int i = elementCount - 1; i >= 0; i--)
            {
                if (Array.IndexOf(a, elementData[i]) < 0)
                {
                    Remove(i);
                }
            }
        }

        public int Size()
        {
            return elementCount;
        }

        public T[] ToArray()
        {
            T[] result = new T[elementCount];
            Array.Copy(elementData, result, elementCount);
            return result;
        }

        public T[] ToArray(T[] a)
        {
            if (a == null || a.Length < elementCount)
            {
                return ToArray();
            }
            Array.Copy(elementData, a, elementCount);
            if (a.Length > elementCount)
            {
                a[elementCount] = default(T);
            }
            return a;
        }

        public void Add(int index, T e)
        {
            if (index < 0 || index > elementCount) throw new ArgumentOutOfRangeException();
            EnsureCapacity(elementCount + 1);
            Array.Copy(elementData, index, elementData, index + 1, elementCount - index);
            elementData[index] = e;
            elementCount++;
        }

        public void AddAll(int index, T[] a)
        {
            if (index < 0 || index > elementCount) throw new ArgumentOutOfRangeException();
            EnsureCapacity(elementCount + a.Length);
            Array.Copy(elementData, index, elementData, index + a.Length, elementCount - index);
            Array.Copy(a, 0, elementData, index, a.Length);
            elementCount += a.Length;
        }

        public T Get(int index)
        {
            if (index < 0 || index >= elementCount) throw new ArgumentOutOfRangeException();
            return elementData[index];
        }

        public int IndexOf(object o)
        {
            if (o == null)
            {
                for (int i = 0; i < elementCount; i++)
                {
                    if (elementData[i] == null) return i;
                }
            }
            else
            {
                for (int i = 0; i < elementCount; i++)
                {
                    if (o.Equals(elementData[i])) return i;
                }
            }
            return -1;
        }

        public int LastIndexOf(object o)
        {
            if (o == null)
            {
                for (int i = elementCount - 1; i >= 0; i--)
                {
                    if (elementData[i] == null) return i;
                }
            }
            else
            {
                for (int i = elementCount - 1; i >= 0; i--)
                {
                    if (o.Equals(elementData[i])) return i;
                }
            }
            return -1;
        }

        public T Remove(int index)
        {
            if (index < 0 || index >= elementCount) throw new ArgumentOutOfRangeException();
            T removedElement = elementData[index];
            int numMoved = elementCount - index - 1;
            if (numMoved > 0)
            {
                Array.Copy(elementData, index + 1, elementData, index, numMoved);
            }
            elementData[--elementCount] = default(T);
            return removedElement;
        }

        public T Set(int index, T e)
        {
            if (index < 0 || index >= elementCount) throw new ArgumentOutOfRangeException();
            T oldValue = elementData[index];
            elementData[index] = e;
            return oldValue;
        }

        public MyVector<T> SubList(int fromIndex, int toIndex)
        {
            if (fromIndex < 0 || toIndex > elementCount || fromIndex > toIndex)
                throw new ArgumentOutOfRangeException();

            MyVector<T> sublist = new MyVector<T>(toIndex - fromIndex);
            for (int i = fromIndex; i < toIndex; i++)
            {
                sublist.Add(elementData[i]);
            }
            return sublist;
        }

        public T FirstElement()
        {
            if (elementCount == 0) throw new InvalidOperationException("Vector is empty.");
            return elementData[0];
        }

        public T LastElement()
        {
            if (elementCount == 0) throw new InvalidOperationException("Vector is empty.");
            return elementData[elementCount - 1];
        }

        public void RemoveElementAt(int pos)
        {
            Remove(pos);
        }
        public void RemoveRange(int begin, int end)
        {
            if (begin < 0 || end > elementCount || begin > end) throw new ArgumentOutOfRangeException();
            int numRemoved = end - begin;
            Array.Copy(elementData, end, elementData, begin, elementCount - end);
            Array.Clear(elementData, elementCount - numRemoved, numRemoved); // Обнуляем удалённые элементы
            elementCount -= numRemoved;
        }

        private void EnsureCapacity(int minCapacity)
        {
            if (minCapacity > elementData.Length)
            {
                int newCapacity = (capacityIncrement > 0)
                    ? elementData.Length + capacityIncrement
                    : elementData.Length * 2;

                if (newCapacity < minCapacity) newCapacity = minCapacity;
                T[] newArray = new T[newCapacity];
                Array.Copy(elementData, newArray, elementCount);
                elementData = newArray;
            }
        }
    }

}
