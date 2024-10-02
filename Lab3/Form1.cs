using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace Lab3
{
    public partial class Form1 : Form
    {
        // Поля для хранения массивов
        private List<int[]> testArrays;
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonRunTests_Click(object sender, EventArgs e)
        {
            if (testArrays == null || testArrays.Count == 0)
            {
                MessageBox.Show("Сначала сгенерируйте массивы.");
                return;
            }
            var sortResults = new Dictionary<string, List<long>>();

            int selectedGroupIndex = comboBoxSortGroup.SelectedIndex;

            switch (selectedGroupIndex)
            {
                case 0:
                    Parallel.Invoke(
                        () => RunSortingTestsWithResult(SortingAlgorithms.BubbleSort, "Bubble Sort", sortResults),
                        () => RunSortingTestsWithResult(SortingAlgorithms.ShakerSort, "Shaker Sort", sortResults),
                        () => RunSortingTestsWithResult(SortingAlgorithms.GnomeSort, "Gnome Sort", sortResults),
                        () => RunSortingTestsWithResult(SortingAlgorithms.SelectionSort, "Selection Sort", sortResults),
                        () => RunSortingTestsWithResult(SortingAlgorithms.InsertionSort, "Insertion Sort", sortResults)
                    );
                    break;
                case 1:
                    Parallel.Invoke(
                        () => RunSortingTestsWithResult(SortingAlgorithms.ShellSort, "Shell Sort", sortResults),
                        () => RunSortingTestsWithResult(SortingAlgorithms.TreeSort, "Tree Sort", sortResults),
                        () => RunSortingTestsWithResult(SortingAlgorithms.BitonicSort, "Bitonic Sort", sortResults)
                    );
                    break;
                case 2:
                    Parallel.Invoke(
                        () => RunSortingTestsWithResult(SortingAlgorithms.CombSort, "Comb Sort", sortResults),
                        () => RunSortingTestsWithResult(SortingAlgorithms.HeapSort, "Heap Sort", sortResults),
                        () => RunSortingTestsWithResult(SortingAlgorithms.QuickSort, "Quick Sort", sortResults),
                        () => RunSortingTestsWithResult(SortingAlgorithms.MergeSort, "Merge Sort", sortResults),
                        () => RunSortingTestsWithResult(SortingAlgorithms.CountingSort, "Counting Sort", sortResults),
                        () => RunSortingTestsWithResult(SortingAlgorithms.RadixSort, "Radix Sort", sortResults)
                    );
                    break;
                default:
                    MessageBox.Show("Выберите группу алгоритмов.");
                    break;
            }

            DisplayGraph(sortResults);

        }

        private void DisplayGraph(Dictionary<string, List<long>> sortResults)
        {
            var pane = zedGraphControl.GraphPane;
            pane.CurveList.Clear();
            pane.Title.Text = "Сравнение эффективности алгоритмов сортировки";
            pane.XAxis.Title.Text = "Размер массива";
            pane.YAxis.Title.Text = "Время (мс)";

            // Размеры массивов для тестирования
            var sizes = new double[] { 10, 100, 1000, 10000, 100000, 1000000 };

            // Массив цветов для линий графика
            var colors = new Color[]
            {
                Color.Red,
                Color.Blue,
                Color.Green,
                Color.Purple,
                Color.Orange,
                Color.Brown,
                Color.Pink,
                Color.Yellow
            };

            int colorIndex = 0;

            foreach (var sortResult in sortResults)
            {
                var times = sortResult.Value.Select(v => (double)v).ToArray();

                // Добавление кривой с уникальным цветом
                var curve = pane.AddCurve(sortResult.Key, sizes, times, colors[colorIndex % colors.Length]);

                // Настройка кривой
                curve.Symbol.Type = SymbolType.Circle; // Знак на кривой
                curve.Line.Width = 2; // Толщина линии

                colorIndex++;
            }

            // Добавление легенды
            pane.Legend.IsVisible = true;
            pane.Legend.Position = LegendPos.InsideTopRight; // Позиция легенды

            zedGraphControl.AxisChange();
            zedGraphControl.Invalidate();
        }

        private void buttonSaveResults_Click(object sender, EventArgs e)
        {
            // Открываем диалог для сохранения файла
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files|*.txt";
            saveFileDialog.Title = "Сохранить результаты";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Вызываем метод для сохранения массивов в файл
                SaveResultsToFile(saveFileDialog.FileName);
            }
        }
        private void SaveResultsToFile(string fileName)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    // Проходим по сгенерированным массивам
                    for (int i = 0; i < testArrays.Count; i++)
                    {
                        writer.WriteLine($"Массив {i + 1} (Размер: {testArrays[i].Length}):");
                        writer.WriteLine(string.Join(", ", testArrays[i]));
                        writer.WriteLine(); // Пустая строка для разделения массивов

                        // Если массив был отсортирован, можно добавить результаты сортировки
                        writer.WriteLine($"Отсортированный массив {i + 1}:");
                        int[] sortedArray = (int[])testArrays[i].Clone(); // Клонируем массив перед сортировкой
                        SortingAlgorithms.QuickSort(sortedArray); // Пример сортировки, замените на нужную
                        writer.WriteLine(string.Join(", ", sortedArray));
                        writer.WriteLine(); // Пустая строка для разделения результатов
                    }
                }
                MessageBox.Show("Результаты успешно сохранены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonGenerateArrays_Click(object sender, EventArgs e)
        {
            // Генерация массивов в зависимости от выбранной группы
            int selectedGroupIndex = comboBoxDataGroup.SelectedIndex;
            testArrays = new List<int[]>();

            switch (selectedGroupIndex)
            {
                case 0:
                    GenerateRandomArrays();
                    break;
                case 1:
                    GeneratePartiallySortedArrays();
                    break;
                case 2:
                    GenerateNearlySortedArrays();
                    break;
                case 3:
                    GenerateModifiedSortedArrays();
                    break;
                default:
                    MessageBox.Show("Пожалуйста, выберите группу тестовых данных.");
                    return;
            }

            MessageBox.Show("Массивы сгенерированы.");
        }

        // Метод для генерации случайных массивов
        private void GenerateRandomArrays()
        {   
            int[] sizes = { 10, 100, 1000, 10000, 100000 };
            Random rand = new Random();

            foreach (int size in sizes)
            {
                int[] array = new int[size];
                for (int i = 0; i < size; i++)
                {
                    array[i] = rand.Next(0, 1000); // числа от 0 до 1000
                }
                testArrays.Add(array);
            }
        }

        // Метод для генерации массивов, разбитых на подмассивы
        private void GeneratePartiallySortedArrays()
        {
            int[] sizes = { 10, 100, 1000, 10000, 100000 };
            Random rand = new Random();

            foreach (int size in sizes)
            {
                int[] array = new int[size];

                // Заполнение массива случайными числами
                for (int i = 0; i < size; i++)
                {
                    array[i] = rand.Next(0, 1000);
                }

                // Разбиение массива на несколько отсортированных подмассивов
                int subArrayCount = rand.Next(1, 6); // Количество подмассивов (от 1 до 5)
                int elementsPerSubArray = size / subArrayCount; // Среднее количество элементов в подмассиве
                int lastSubArraySize = size - elementsPerSubArray * (subArrayCount - 1); // Размер последнего подмассива

                // Сортировка подмассивов случайного размера
                for (int i = 0; i < subArrayCount; i++)
                {
                    int startIndex = i * elementsPerSubArray;
                    int currentSubArraySize = (i == subArrayCount - 1) ? lastSubArraySize : elementsPerSubArray; // Обработка последнего подмассива

                    Array.Sort(array, startIndex, currentSubArraySize);
                }

                testArrays.Add(array);
            }
        }

        // Метод для генерации отсортированных массивов с перестановками
        private void GenerateNearlySortedArrays()
        {
            int[] sizes = { 10, 100, 1000, 10000, 100000 };
            Random rand = new Random();

            foreach (int size in sizes)
            {
                int[] array = Enumerable.Range(0, size).ToArray();
                int swaps = rand.Next(1, size / 10);

                for (int i = 0; i < swaps; i++)
                {
                    int index1 = rand.Next(0, size);
                    int index2 = rand.Next(0, size);
                    (array[index1], array[index2]) = (array[index2], array[index1]);
                }

                testArrays.Add(array);
            }
        }

        // Метод для генерации отсортированных массивов с заменёнными элементами
        private void GenerateModifiedSortedArrays()
        {
            int[] sizes = { 10, 100, 1000, 10000, 100000 };
            Random rand = new Random();

            foreach (int size in sizes)
            {
                int[] array = Enumerable.Range(0, size).ToArray();

                // Заменяем несколько случайных элементов
                int replacements = rand.Next(size / 10, size / 2);

                for (int i = 0; i < replacements; i++)
                {
                    int index = rand.Next(0, size);
                    array[index] = rand.Next(0, 1000);
                }

                testArrays.Add(array);
            }
        }

        private void RunSortingTests(Action<int[]> sortMethod, string sortName)
        {
            Parallel.For(0, testArrays.Count, i =>
            {
                int[] arrayCopy = (int[])testArrays[i].Clone(); // Клонируем массив для каждой сортировки

                Stopwatch stopwatch = Stopwatch.StartNew();
                sortMethod(arrayCopy);
                stopwatch.Stop();

                // Используем синхронизацию для добавления результатов в UI (thread-safe)
                Invoke((MethodInvoker)delegate
                {
                    Console.WriteLine($"Сортировка {sortName} заняла {stopwatch.ElapsedMilliseconds} мс для массива длиной {arrayCopy.Length}.");
                });
            });
        }
        private void RunSortingTestsWithResult(Action<int[]> sortMethod, string sortName, Dictionary<string, List<long>> sortResults)
        {
            // Инициализация списка для хранения результатов времени выполнения
            var times = new List<long>();

            // Параллельное выполнение тестов
            Parallel.For(0, testArrays.Count, i =>
            {
                // Создание копии массива для сортировки
                int[] arrayCopy = (int[])testArrays[i].Clone();

                // Измерение времени выполнения алгоритма
                Stopwatch stopwatch = Stopwatch.StartNew();
                sortMethod(arrayCopy); // Вызов метода сортировки
                stopwatch.Stop();

                // Добавление времени выполнения в список
                lock (times) // Синхронизация для избежания проблем с конкурентным доступом
                {
                    times.Add(stopwatch.ElapsedMilliseconds);
                }
            });

            // Сохранение собранных результатов в общий словарь
            lock (sortResults) // Синхронизация при записи в словарь
            {
                sortResults[sortName] = times; // Запись времени выполнения для текущего алгоритма
            }
        }
    }
}
