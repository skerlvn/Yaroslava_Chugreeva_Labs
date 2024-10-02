using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace Lab3
{
    public partial class Form1 : Form
    {
        // Поля для хранения массивов
        private List<int[]> testArrays;
        private volatile Dictionary<string, List<double>> sortResults;
        public Form1()
        {
            InitializeComponent();
        }

        private async void buttonRunTests_Click(object sender, EventArgs e)
        {
            if (testArrays == null || testArrays.Count == 0)
            {
                MessageBox.Show("Сначала сгенерируйте массивы.");
                return;
            }
            sortResults = new Dictionary<string, List<double>>();

            int selectedGroupIndex = comboBoxSortGroup.SelectedIndex;
     

            switch (selectedGroupIndex)
            {
                case 0:
                    Parallel.Invoke(
                        () => RunSortingTestsWithResult(SortingAlgorithms.BubbleSort, "Bubble Sort"),
                        () => RunSortingTestsWithResult(SortingAlgorithms.ShakerSort, "Shaker Sort"),
                        () => RunSortingTestsWithResult(SortingAlgorithms.GnomeSort, "Gnome Sort"),
                        () => RunSortingTestsWithResult(SortingAlgorithms.SelectionSort, "Selection Sort"),
                        () => RunSortingTestsWithResult(SortingAlgorithms.InsertionSort, "Insertion Sort")
                    );
                    break;
                case 1:
                    Parallel.Invoke(
                        () => RunSortingTestsWithResult(SortingAlgorithms.ShellSort, "Shell Sort"),
                        () => RunSortingTestsWithResult(SortingAlgorithms.TreeSort, "Tree Sort"),
                        () => RunSortingTestsWithResult(SortingAlgorithms.BitonicSort, "Bitonic Sort")
                    );
                    break;
                case 2:
                    Parallel.Invoke(
                        () => RunSortingTestsWithResult(SortingAlgorithms.CombSort, "Comb Sort"),
                        () => RunSortingTestsWithResult(SortingAlgorithms.HeapSort, "Heap Sort"),
                        () => RunSortingTestsWithResult(SortingAlgorithms.QuickSort, "Quick Sort"),
                        () => RunSortingTestsWithResult(SortingAlgorithms.MergeSort, "Merge Sort"),
                        () => RunSortingTestsWithResult(SortingAlgorithms.CountingSort, "Counting Sort"),
                        () => RunSortingTestsWithResult(SortingAlgorithms.RadixSort, "Radix Sort")
                    );
                    break;
                default:
                    MessageBox.Show("Выберите группу алгоритмов.");
                    break;
            }

            DisplayGraph(sortResults);
        }

        private async void RunSortingTestsWithResult(Action<int[]> sortMethod, string sortName)
        {
            // Список для хранения времени выполнения
            var times = new List<double>();

            // Параметры для многократного запуска
            int numRuns = 20; // 20 запусков для каждого теста
            int[] sizes = testArrays.Select(arr => arr.Length).ToArray();

            foreach (int[] array in testArrays)
            {
                double totalTime = 0;

            
                Parallel.For(0, numRuns, run=>
                {
                    // Клонируем массив перед каждым запуском
                    int[] arrayCopy = (int[])array.Clone();

                    // Измерение времени
                    Stopwatch stopwatch = Stopwatch.StartNew();
                    sortMethod(arrayCopy);
                    stopwatch.Stop();

                    totalTime += stopwatch.ElapsedMilliseconds;
                });
                

                // Среднее время
                double avgTime = totalTime / numRuns;
                times.Add(avgTime);
            }
            //lock(sortResults)
            sortResults[sortName] = times;

        }

        // График
        private void DisplayGraph(Dictionary<string, List<double>> sortResults)
        {
            var pane = zedGraphControl.GraphPane;
            pane.CurveList.Clear();
            pane.Title.Text = "Сравнение эффективности алгоритмов сортировки";
            pane.XAxis.Title.Text = "Размер массива";
            pane.YAxis.Title.Text = "Среднее время (мс)";

            // Размеры массивов для тестирования
            var sizes = testArrays.Select(arr => (double)arr.Length).ToArray();

            // Цвета для графиков
            var colors = new Color[] 
            { 
                Color.Red, 
                Color.Blue, 
                Color.Green, 
                Color.Purple, 
                Color.Orange,
                Color.Black,
            };

            int colorIndex = 0;

            foreach (var sortResult in sortResults)
            {
                var curve = pane.AddCurve(sortResult.Key, sizes, sortResult.Value.ToArray(), colors[colorIndex++ % colors.Length]);
                curve.Symbol.Type = SymbolType.Circle;
                curve.Line.Width = 2;
            }

            zedGraphControl.AxisChange();
            zedGraphControl.Invalidate();
        }

        // Кнопка для генерации массивов
        private void buttonGenerateArrays_Click(object sender, EventArgs e)
        {
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
                    MessageBox.Show("Выберите группу тестовых данных.");
                    return;
            }

            MessageBox.Show("Массивы сгенерированы.");
        }
        private void buttonSaveResults_Click(object sender, EventArgs e)
        {
            if (testArrays == null || testArrays.Count == 0)
            {
                MessageBox.Show("Сначала сгенерируйте массивы.");
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            saveFileDialog.Title = "Сохранить результаты";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                {
                    writer.WriteLine("Результаты сортировки:");

                    // Сохраняем сгенерированные массивы
                    writer.WriteLine("Сгенерированные массивы:");
                    for (int i = 0; i < testArrays.Count; i++)
                    {
                        writer.WriteLine($"Массив {i + 1}: {string.Join(", ", testArrays[i])}");
                    }

                    writer.WriteLine();

                    // Выполняем сортировки и сохраняем результаты для каждого алгоритма
                    int selectedGroupIndex = comboBoxSortGroup.SelectedIndex;

                    switch (selectedGroupIndex)
                    {
                        case 0:
                            SaveSortedArray(SortingAlgorithms.BubbleSort, "Bubble Sort", writer);
                            SaveSortedArray(SortingAlgorithms.ShakerSort, "Shaker Sort", writer);
                            SaveSortedArray(SortingAlgorithms.GnomeSort, "Gnome Sort", writer);
                            SaveSortedArray(SortingAlgorithms.SelectionSort, "Selection Sort", writer);
                            SaveSortedArray(SortingAlgorithms.InsertionSort, "Insertion Sort", writer);
                            break;
                        case 1:
                            SaveSortedArray(SortingAlgorithms.ShellSort, "Shell Sort", writer);
                            SaveSortedArray(SortingAlgorithms.TreeSort, "Tree Sort", writer);
                            SaveSortedArray(SortingAlgorithms.BitonicSort, "Bitonic Sort", writer);
                            break;
                        case 2:
                            SaveSortedArray(SortingAlgorithms.CombSort, "Comb Sort", writer);
                            SaveSortedArray(SortingAlgorithms.HeapSort, "Heap Sort", writer);
                            SaveSortedArray(SortingAlgorithms.QuickSort, "Quick Sort", writer);
                            SaveSortedArray(SortingAlgorithms.MergeSort, "Merge Sort", writer);
                            SaveSortedArray(SortingAlgorithms.CountingSort, "Counting Sort", writer);
                            SaveSortedArray(SortingAlgorithms.RadixSort, "Radix Sort", writer);
                            break;
                        default:
                            MessageBox.Show("Выберите группу алгоритмов для сортировки.");
                            break;
                    }

                    MessageBox.Show("Результаты успешно сохранены.");
                }
            }
        }

        private void SaveSortedArray(Action<int[]> sortMethod, string sortName, StreamWriter writer)
        {
            writer.WriteLine($"Результаты для {sortName}:");

            for (int i = 0; i < testArrays.Count; i++)
            {
                int[] arrayCopy = (int[])testArrays[i].Clone();
                sortMethod(arrayCopy);  // Сортировка массива

                writer.WriteLine($"Отсортированный массив {i + 1}: {string.Join(", ", arrayCopy)}");
            }

            writer.WriteLine();
        }
        // Генерация случайных массивов
        private int[] GetArraySizesToAlgorithm()
        {
            int selectedGroupIndex = comboBoxSortGroup.SelectedIndex;

            var sizes = new List<int>();
            for (int i = 1; i <= 4 + selectedGroupIndex; i++)
                sizes.Add((int)Math.Pow(10.0, i));

            return sizes.ToArray();
            
        }
        private void GenerateRandomArrays()
        {
            int[] sizes = GetArraySizesToAlgorithm();
            Random rand = new Random();

            foreach (int size in sizes)
            {
                int[] array = new int[size];
                for (int i = 0; i < size; i++)
                {
                    array[i] = rand.Next(0, 1000);
                }
                testArrays.Add(array);
            }
        }

        // Генерация частично отсортированных массивов
        private void GeneratePartiallySortedArrays()
        {
            int[] sizes = GetArraySizesToAlgorithm();
            Random rand = new Random();

            foreach (int size in sizes)
            {
                int[] array = new int[size];
                for (int i = 0; i < size; i++)
                {
                    array[i] = rand.Next(0, 1000);
                }

                // Разбиваем на подмассивы и сортируем их
                int subArrayCount = rand.Next(1, 6);
                int subArraySize = size / subArrayCount;

                for (int i = 0; i < subArrayCount; i++)
                {
                    int startIndex = i * subArraySize;
                    Array.Sort(array, startIndex, subArraySize);
                }

                testArrays.Add(array);
            }
        }

        // Генерация почти отсортированных массивов с перестановками
        private void GenerateNearlySortedArrays()
        {
            int[] sizes = GetArraySizesToAlgorithm();
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

        // Генерация отсортированных массивов с изменениями
        private void GenerateModifiedSortedArrays()
        {
            int[] sizes = GetArraySizesToAlgorithm();
            Random rand = new Random();

            foreach (int size in sizes)
            {
                int[] array = Enumerable.Range(0, size).ToArray();
                int modifications = rand.Next(1, size / 5);

                for (int i = 0; i < modifications; i++)
                {
                    int index = rand.Next(0, size);
                    array[index] = rand.Next(0, 1000);
                }

                testArrays.Add(array);
            }
        }
    }
}
