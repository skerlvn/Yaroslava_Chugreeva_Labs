using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    public class MyArrayList<T>
    {
        private T[] elementData; // Внутренний массив
        private int size;        // Количество элементов в динамическом массиве

        // 1) Конструктор MyArrayList() для создания пустого динамического массива.
        public MyArrayList()
        {
            elementData = new T[10]; // Начальный размер массива - 10 элементов
            size = 0;
        }

        // 2) Конструктор MyArrayList(T[] a) для создания динамического массива и заполнения его элементами из передаваемого массива a.
        public MyArrayList(T[] a)
        {
            elementData = new T[a.Length];
            Array.Copy(a, elementData, a.Length);
            size = a.Length;
        }

        // 3) Конструктор MyArrayList(int capacity) для создания пустого динамического массива с внутренним массивом указанной емкости.
        public MyArrayList(int capacity)
        {
            elementData = new T[capacity];
            size = 0;
        }

        // 4) Метод add(T e) для добавления элемента в конец динамического массива.
        // Если размер динамического массива больше размера внутреннего массива, создаётся новый массив большего размера.
        public void Add(T e)
        {
            EnsureCapacity(size + 1);
            elementData[size++] = e;
        }

        // 5) Метод addAll(T[] a) для добавления элементов из массива.
        public void AddAll(T[] a)
        {
            EnsureCapacity(size + a.Length);
            Array.Copy(a, 0, elementData, size, a.Length);
            size += a.Length;
        }

        // 6) Метод clear() для удаления всех элементов из динамического массива.
        public void Clear()
        {
            Array.Clear(elementData, 0, size);
            size = 0;
        }

        // 7) Метод contains(object o) для проверки, находится ли указанный объект в динамическом массиве.
        public bool Contains(object o)
        {
            return IndexOf(o) >= 0;
        }

        // 8) Метод containsAll(T[] a) для проверки, содержатся ли указанные объекты в динамическом массиве.
        public bool ContainsAll(T[] a)
        {
            foreach (var item in a)
            {
                if (!Contains(item))
                    return false;
            }
            return true;
        }

        // 9) Метод isEmpty() для проверки, является ли динамический массив пустым.
        public bool IsEmpty()
        {
            return size == 0;
        }

        // 10) Метод remove(object o) для удаления указанного объекта из динамического массива, если он есть.
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

        // 11) Метод removeAll(T[] a) для удаления указанных объектов из динамического массива.
        public void RemoveAll(T[] a)
        {
            foreach (var item in a)
            {
                Remove(item);
            }
        }

        // 12) Метод retainAll(T[] a) для оставления в динамическом массиве только указанных объектов.
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

        // 13) Метод size() для получения размера динамического массива в элементах.
        public int Size()
        {
            return size;
        }

        // 14) Метод toArray() для возвращения массива объектов, содержащего все элементы динамического массива.
        public T[] ToArray()
        {
            T[] result = new T[size];
            Array.Copy(elementData, result, size);
            return result;
        }

        // 15) Метод toArray(T[] a) для возвращения массива объектов, содержащего все элементы динамического массива.
        // Если аргумент a равен null, создаётся новый массив.
        public T[] ToArray(T[] a)
        {
            if (a == null || a.Length < size)
            {
                return ToArray(); // Создать новый массив, если переданный массив меньше или null.
            }
            Array.Copy(elementData, a, size);
            if (a.Length > size)
            {
                a[size] = default(T); // Устанавливаем последний элемент в default, если массив больше.
            }
            return a;
        }

        // 16) Метод add(int index, T e) для добавления элемента в указанную позицию.
        public void Add(int index, T e)
        {
            CheckRangeForAdd(index);
            EnsureCapacity(size + 1);
            Array.Copy(elementData, index, elementData, index + 1, size - index);
            elementData[index] = e;
            size++;
        }

        // 17) Метод addAll(int index, T[] a) для добавления элементов в указанную позицию.
        public void AddAll(int index, T[] a)
        {
            CheckRangeForAdd(index);
            EnsureCapacity(size + a.Length);
            Array.Copy(elementData, index, elementData, index + a.Length, size - index);
            Array.Copy(a, 0, elementData, index, a.Length);
            size += a.Length;
        }

        // 18) Метод get(int index) для возвращения элемента в указанной позиции.
        public T Get(int index)
        {
            CheckRange(index);
            return elementData[index];
        }

        // 19) Метод indexOf(object o) для возвращения индекса указанного объекта, или -1, если его нет.
        public int IndexOf(object o)
        {
            for (int i = 0; i < size; i++)
            {
                if (Equals(elementData[i], o))
                    return i;
            }
            return -1;
        }

        // 20) Метод lastIndexOf(object o) для нахождения последнего вхождения указанного объекта, или -1, если его нет.
        public int LastIndexOf(object o)
        {
            for (int i = size - 1; i >= 0; i--)
            {
                if (Equals(elementData[i], o))
                    return i;
            }
            return -1;
        }

        // 21) Метод remove(int index) для удаления и возвращения элемента в указанной позиции.
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

        // 22) Метод set(int index, T e) для замены элемента в указанной позиции новым элементом.
        public T Set(int index, T e)
        {
            CheckRange(index);
            T oldValue = elementData[index];
            elementData[index] = e;
            return oldValue;
        }

        // 23) Метод subList(int fromIndex, int toIndex) для возвращения части динамического массива.
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

        // Метод для обеспечения увеличения емкости массива
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

        // Проверка на допустимость индекса
        private void CheckRange(int index)
        {
            if (index < 0 || index >= size)
                throw new IndexOutOfRangeException($"Индекс {index} вне границ массива.");
        }

        // Проверка на допустимость индекса для добавления
        private void CheckRangeForAdd(int index)
        {
            if (index < 0 || index > size)
                throw new IndexOutOfRangeException($"Индекс {index} вне допустимого диапазона.");
        }
    }

}
