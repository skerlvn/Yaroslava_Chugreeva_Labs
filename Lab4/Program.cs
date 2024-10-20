using Lab4;
using System;
using System.Collections.Generic;

class Program
{
    public static void Main(string[] args)
    {
        MyArrayList<int> myList = new MyArrayList<int>();
        myList.Add(1);
        myList.Add(2);
        myList.Add(3);
        myList.Add(4);
        myList.Add(5);
        myList.Add(6);
        myList.Add(7);

        int[] subList = myList.SubList(2, 5).ToArray();
        Console.WriteLine("Прямой диапазон (2 до 5): " + string.Join(", ", subList));

        myList = new MyArrayList<int>();
        myList.Add(1);
        myList.Add(2);
        myList.Add(3);
        Console.WriteLine("Текущий список: " + string.Join(", ", myList.ToArray()));

        int[] elementsToAdd = { 4, 5, 6 };
        myList.AddAll(elementsToAdd);
        Console.WriteLine("Добавлены {4, 5, 6} в конец: " + string.Join(", ", myList.ToArray()));

        myList.Remove(o: 2);
        Console.WriteLine("Удален объект '2': " + string.Join(", ", myList.ToArray()));

        bool containsFive = myList.Contains(5);
        Console.WriteLine("Проверка существования объекта '5': " + containsFive);

        int[] elementsToRemove = { 1, 4 };
        myList.RemoveAll(elementsToRemove);
        Console.WriteLine("Удалены элементы из массива (1, 4): " + string.Join(", ", myList.ToArray()));

        myList.Clear();
        Console.WriteLine("Очищенный список: " + (myList.IsEmpty() ? "Empty" : string.Join(", ", myList.ToArray())));
        

    }
}