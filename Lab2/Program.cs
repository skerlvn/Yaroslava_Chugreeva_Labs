using System;

struct Complex
{
    public double Real { get; set; }
    public double Imaginary { get; set; }

    public Complex()
    {
        Real = 0.0;
        Imaginary = 0.0;
    }
    public Complex(double real, double imaginary)
    {
        Real = real;
        Imaginary = imaginary;
    }

    public void Add(Complex other)
    {
        Real += other.Real;
        Imaginary += other.Imaginary;
    }

    public void Subtract(Complex other)
    {
        Real -= other.Real;
        Imaginary -= other.Imaginary;
    }

    public void Multiply(Complex other)
    {
        double newReal = Real * other.Real - Imaginary * other.Imaginary;
        double newImaginary = Real * other.Imaginary + Imaginary * other.Real;
        Real = newReal;
        Imaginary = newImaginary;
    }

    public void Divide(Complex other)
    {
        double denominator = other.Real * other.Real + other.Imaginary * other.Imaginary;
        if (denominator == 0)
        {
            Console.WriteLine("Деление на ноль!");
            return;
        }
        double newReal = (Real * other.Real + Imaginary * other.Imaginary) / denominator;
        double newImaginary = (Imaginary * other.Real - Real * other.Imaginary) / denominator;
        Real = newReal;
        Imaginary = newImaginary;
    }

    public double Modulus()
    {
        return Math.Sqrt(Real * Real + Imaginary * Imaginary);
    }

    public double Argument()
    {
        return Math.Atan2(Imaginary, Real);
    }

    public void Print()
    {
        Console.WriteLine($"Комплексное число: {Real} + {Imaginary}i");
    }
}

class Program
{
    public static void Main(string[] args)
    {
        Complex number = new Complex(); 
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\nМеню:");
            Console.WriteLine("1. Ввести новое комплексное число");
            Console.WriteLine("2. Сложить с другим числом");
            Console.WriteLine("3. Вычесть другое число");
            Console.WriteLine("4. Умножить на другое число");
            Console.WriteLine("5. Разделить на другое число");
            Console.WriteLine("6. Найти модуль");
            Console.WriteLine("7. Найти аргумент");
            Console.WriteLine("8. Показать текущее число");
            Console.WriteLine("Q. Выйти");

            Console.Write("Выберите действие: ");
            string choice = Console.ReadLine().ToUpper();

            switch (choice)
            {
                case "1":
                    number = ReadComplex();
                    break;
                case "2":
                    Complex toAdd = ReadComplex();
                    number.Add(toAdd);
                    break;
                case "3":
                    Complex toSubtract = ReadComplex();
                    number.Subtract(toSubtract);
                    break;
                case "4":
                    Complex toMultiply = ReadComplex();
                    number.Multiply(toMultiply);
                    break;
                case "5":
                    Complex toDivide = ReadComplex();
                    number.Divide(toDivide);
                    break;
                case "6":
                    Console.WriteLine($"Модуль числа: {number.Modulus()}");
                    break;
                case "7":
                    Console.WriteLine($"Аргумент числа: {number.Argument()}");
                    break;
                case "8":
                    number.Print();
                    break;
                case "Q":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Неизвестная команда.");
                    break;
            }
        }
    }

    private static Complex ReadComplex()
    {
        Console.Write("Введите вещественную часть: ");
        double real = double.Parse(Console.ReadLine());

        Console.Write("Введите мнимую часть: ");
        double imaginary = double.Parse(Console.ReadLine());

        return new Complex(real, imaginary);
    }
}
