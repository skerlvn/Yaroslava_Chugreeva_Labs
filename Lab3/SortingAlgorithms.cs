using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public static class SortingAlgorithms
    {
        // 1. Сортировка пузырьком (Bubble sort)
        public static void BubbleSort(int[] array)
        {
            int n = array.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        Swap(ref array[j], ref array[j + 1]);
                    }
                }
            }
        }

        // 2. Шейкерная сортировка (Shaker sort)
        public static void ShakerSort(int[] array)
        {
            int left = 0;
            int right = array.Length - 1;
            bool swapped = true;
            while (left < right && swapped)
            {
                swapped = false;
                for (int i = left; i < right; i++)
                {
                    if (array[i] > array[i + 1])
                    {
                        Swap(ref array[i], ref array[i + 1]);
                        swapped = true;
                    }
                }
                right--;

                for (int i = right; i > left; i--)
                {
                    if (array[i] < array[i - 1])
                    {
                        Swap(ref array[i], ref array[i - 1]);
                        swapped = true;
                    }
                }
                left++;
            }
        }

        // 3. Сортировка расчёской (Comb sort)
        public static void CombSort(int[] array)
        {
            int gap = array.Length;
            const double shrinkFactor = 1.3;
            bool sorted = false;

            while (gap > 1 || !sorted)
            {
                if (gap > 1)
                {
                    gap = (int)(gap / shrinkFactor);
                }

                sorted = true;
                for (int i = 0; i + gap < array.Length; i++)
                {
                    if (array[i] > array[i + gap])
                    {
                        Swap(ref array[i], ref array[i + gap]);
                        sorted = false;
                    }
                }
            }
        }

        // 4. Сортировка вставками (Insertion sort)
        public static void InsertionSort(int[] array)
        {
            for (int i = 1; i < array.Length; i++)
            {
                int key = array[i];
                int j = i - 1;

                while (j >= 0 && array[j] > key)
                {
                    array[j + 1] = array[j];
                    j--;
                }
                array[j + 1] = key;
            }
        }

        // 5. Сортировка Шелла (Shellsort)
        public static void ShellSort(int[] array)
        {
            int n = array.Length;
            int gap = n / 2;

            while (gap > 0)
            {
                for (int i = gap; i < n; i++)
                {
                    int temp = array[i];
                    int j = i;
                    while (j >= gap && array[j - gap] > temp)
                    {
                        array[j] = array[j - gap];
                        j -= gap;
                    }
                    array[j] = temp;
                }
                gap /= 2;
            }
        }
        // Реализация Tree Sort
        public static void TreeSort(int[] array)
        {
            if (array.Length == 0) return;

            // Строим бинарное дерево
            BinaryTree tree = new BinaryTree();
            foreach (var value in array)
            {
                tree.Insert(value);
            }

            // Извлекаем элементы через итеративный in-order обход
            var sortedList = tree.IterativeInOrderTraversal();

            // Копируем отсортированные элементы обратно в массив
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = sortedList[i];
            }
        }

        // Класс бинарного дерева поиска
        private class BinaryTree
        {
            private Node root;

            private class Node
            {
                public int Value;
                public Node Left;
                public Node Right;

                public Node(int value)
                {
                    Value = value;
                    Left = null;
                    Right = null;
                }
            }

            // Итеративная вставка элемента
            public void Insert(int value)
            {
                Node newNode = new Node(value);
                if (root == null)
                {
                    root = newNode;
                    return;
                }

                Node current = root;
                Node parent = null;

                while (current != null)
                {
                    parent = current;
                    if (value < current.Value)
                    {
                        current = current.Left;
                    }
                    else
                    {
                        current = current.Right;
                    }
                }

                if (value < parent.Value)
                {
                    parent.Left = newNode;
                }
                else
                {
                    parent.Right = newNode;
                }
            }

            // Итеративный in-order обход дерева
            public List<int> IterativeInOrderTraversal()
            {
                List<int> result = new List<int>();
                Stack<Node> stack = new Stack<Node>();
                Node current = root;

                while (current != null || stack.Count > 0)
                {
                    // Движемся по левым узлам
                    while (current != null)
                    {
                        stack.Push(current);
                        current = current.Left;
                    }

                    // Достаем элемент из стека
                    current = stack.Pop();
                    result.Add(current.Value); // Обрабатываем узел

                    // Переходим к правым узлам
                    current = current.Right;
                }

                return result;
            }
        }


        // 7. Гномья сортировка (Gnome sort)
        public static void GnomeSort(int[] array)
        {
            int i = 1;
            int j = 2;
            while (i < array.Length)
            {
                if (array[i - 1] <= array[i])
                {
                    i = j;
                    j++;
                }
                else
                {
                    Swap(ref array[i - 1], ref array[i]);
                    i--;
                    if (i == 0)
                    {
                        i = j;
                        j++;
                    }
                }
            }
        }

        // 8. Сортировка выбором (Selection sort)
        public static void SelectionSort(int[] array)
        {
            int n = array.Length;
            for (int i = 0; i < n - 1; i++)
            {
                int minIdx = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (array[j] < array[minIdx])
                    {
                        minIdx = j;
                    }
                }
                Swap(ref array[minIdx], ref array[i]);
            }
        }

        // Вспомогательный метод для обмена элементов
        private static void Swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }

        public static void HeapSort(int[] array)
        {
            int n = array.Length;

            // Построение кучи (max heap)
            for (int i = n / 2 - 1; i >= 0; i--)
                Heapify(array, n, i);

            // Один за другим извлекаем элементы из кучи
            for (int i = n - 1; i >= 0; i--)
            {
                // Перемещаем текущий корень в конец
                int temp = array[0];
                array[0] = array[i];
                array[i] = temp;

                // Вызываем процедуру для уменьшенной кучи
                Heapify(array, i, 0);
            }
        }

        private static void Heapify(int[] array, int n, int i)
        {
            int largest = i; // Инициализируем корень как наибольший
            int left = 2 * i + 1; // левый = 2*i + 1
            int right = 2 * i + 2; // правый = 2*i + 2

            // Если левый дочерний элемент больше корня
            if (left < n && array[left] > array[largest])
                largest = left;

            // Если правый дочерний элемент больше, чем наибольший элемент на данный момент
            if (right < n && array[right] > array[largest])
                largest = right;

            // Если наибольший элемент не корень
            if (largest != i)
            {
                int swap = array[i];
                array[i] = array[largest];
                array[largest] = swap;

                // Рекурсивно преобразуем затронутое поддерево
                Heapify(array, n, largest);
            }
        }
        // Итеративная версия QuickSort
        public static void QuickSort(int[] array)
        {
            if (array.Length == 0) return;

            Stack<(int, int)> stack = new Stack<(int, int)>();
            stack.Push((0, array.Length - 1));

            while (stack.Count > 0)
            {
                var (low, high) = stack.Pop();

                if (low < high)
                {
                    // Разделяем массив и получаем индекс опорного элемента
                    int pivotIndex = Partition(array, low, high);

                    // Кладём в стек подмассивы для дальнейшей сортировки
                    stack.Push((low, pivotIndex - 1)); // Левая часть
                    stack.Push((pivotIndex + 1, high)); // Правая часть
                }
            }
        }

        private static int Partition(int[] array, int low, int high)
        {
            int pivot = array[high];
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (array[j] <= pivot)
                {
                    i++;
                    Swap(ref array[i], ref array[j]);
                }
            }

            Swap(ref array[i + 1], ref array[high]);
            return i + 1;
        }

        public static void MergeSort(int[] array)
        {
            if (array.Length <= 1) return;

            int mid = array.Length / 2;
            int[] left = new int[mid];
            int[] right = new int[array.Length - mid];

            Array.Copy(array, left, mid);
            Array.Copy(array, mid, right, 0, array.Length - mid);

            MergeSort(left);
            MergeSort(right);
            Merge(array, left, right);
        }

        private static void Merge(int[] array, int[] left, int[] right)
        {
            int i = 0, j = 0, k = 0;

            while (i < left.Length && j < right.Length)
            {
                if (left[i] <= right[j])
                {
                    array[k++] = left[i++];
                }
                else
                {
                    array[k++] = right[j++];
                }
            }

            while (i < left.Length)
            {
                array[k++] = left[i++];
            }

            while (j < right.Length)
            {
                array[k++] = right[j++];
            }
        }
        public static void CountingSort(int[] array)
        {
            int max = array.Max();
            int min = array.Min();
            int range = max - min + 1;

            int[] count = new int[range];
            int[] output = new int[array.Length];

            // Подсчёт вхождений элементов
            for (int i = 0; i < array.Length; i++)
                count[array[i] - min]++;

            // Изменение count[i] так, чтобы count[i] содержал
            // фактическую позицию элемента в выходном массиве
            for (int i = 1; i < count.Length; i++)
                count[i] += count[i - 1];

            // Построение выходного массива
            for (int i = array.Length - 1; i >= 0; i--)
            {
                output[count[array[i] - min] - 1] = array[i];
                count[array[i] - min]--;
            }

            // Копируем отсортированные элементы в исходный массив
            Array.Copy(output, array, array.Length);
        }
        public static void RadixSort(int[] array)
        {
            int max = array.Max();

            for (int exp = 1; max / exp > 0; exp *= 10)
                CountingSortByDigit(array, exp);
        }

        private static void CountingSortByDigit(int[] array, int exp)
        {
            int n = array.Length;
            int[] output = new int[n];
            int[] count = new int[10]; // Десятичные цифры

            // Подсчёт вхождений элементов
            for (int i = 0; i < n; i++)
                count[(array[i] / exp) % 10]++;

            // Изменение count[i] так, чтобы count[i] содержал
            // фактическую позицию элемента в выходном массиве
            for (int i = 1; i < 10; i++)
                count[i] += count[i - 1];

            // Построение выходного массива
            for (int i = n - 1; i >= 0; i--)
            {
                output[count[(array[i] / exp) % 10] - 1] = array[i];
                count[(array[i] / exp) % 10]--;
            }

            // Копируем отсортированные элементы в исходный массив
            Array.Copy(output, array, n);
        }
        public static void BitonicSort(int[] array)
        {
            BitonicSort(array, 0, array.Length, true);
        }

        private static void BitonicSort(int[] array, int low, int cnt, bool dir)
        {
            if (cnt > 1)
            {
                int k = cnt / 2;

                // Сортируем в возрастающем порядке
                BitonicSort(array, low, k, true);
                // Сортируем в порядке убывания
                BitonicSort(array, low + k, cnt - k, false);

                // Объединяем битонные последовательности
                BitonicMerge(array, low, cnt, dir);
            }
        }

        private static void BitonicMerge(int[] array, int low, int cnt, bool dir)
        {
            if (cnt > 1)
            {
                int k = cnt / 2;

                for (int i = low; i < low + k; i++)
                    if ((dir && array[i] > array[i + k]) || (!dir && array[i] < array[i + k]))
                        Swap(ref array[i], ref array[i + k]);

                BitonicMerge(array, low, k, dir);
                BitonicMerge(array, low + k, cnt - k, dir);
            }
        }

    }

}
