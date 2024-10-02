using System;
using System.IO;

class Program
{
    private static void Main(string[] args)
    {
        try
        {
            string[] lines = File.ReadAllLines("input.txt");

            int N = int.Parse(lines[0]);

            double[] vector = new double[N];
            string[] vectorData = lines[1].Split(' ');

            for (int i = 0; i < N; i++)
            {
                vector[i] = double.Parse(vectorData[i]);
            }

            double[,] matrix = new double[N, N];
            for (int i = 0; i < N; i++)
            {
                string[] row = lines[i + 2].Split(' ');
                for (int j = 0; j < N; j++)
                {
                    matrix[i, j] = double.Parse(row[j]);
                }
            }

            if (!IsSymmetric(matrix, N))
            {
                Console.WriteLine("Матрица G не является симметричной.");
            }
            else if (matrix.GetLength(0) != N || matrix.GetLength(1) != N)
            {
                Console.WriteLine("Матрица не является совместимой для вектора X");
            }
            else
            {
                double length = CalculateVectorLength(vector, matrix, N);
                Console.WriteLine($"Длина вектора: {length}");
            }

        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    private static bool IsSymmetric(double[,] matrix, int N)
    {
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                if (matrix[i, j] != matrix[j, i])
                    return false;
            }
        }
        return true;
    }

    private static double CalculateVectorLength(double[] vector, double[,] G, int N)
    {
        double[] temp = new double[N];

        for (int i = 0; i < N; i++)
        {
            temp[i] = 0;
            for (int j = 0; j < N; j++)
            {
                temp[i] += G[i, j] * vector[j];
            }
        }

        double result = 0;
        for (int i = 0; i < N; i++)
        {
            result += vector[i] * temp[i];
        }

        return Math.Sqrt(result);
    }

}
