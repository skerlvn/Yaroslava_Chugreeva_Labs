using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            // Логика запуска тестов будет здесь
        }

        private void buttonSaveResults_Click(object sender, EventArgs e)
        {
            // Логика сохранения результатов будет здесь
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
                    break;
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
                int subArraySize = rand.Next(10, size / 10);

                for (int i = 0; i < size; i++)
                {
                    array[i] = rand.Next(0, 1000);
                }

                // Сортируем подмассивы случайного размера
                for (int i = 0; i < size; i += subArraySize)
                {
                    int end = Math.Min(i + subArraySize, size);
                    Array.Sort(array, i, end - i);
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
    }
}
