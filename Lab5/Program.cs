using Lab4;
using System;
using System.IO;
using System.Text.RegularExpressions;

public class TagExtractor
{
    // Метод для извлечения тегов из строки
    public static MyArrayList<string> ExtractTags(string input)
    {
        MyArrayList<string> tags = new MyArrayList<string>();

        // Регулярное выражение для поиска тегов
        Regex tagPattern = new Regex(@"<(/?[A-Za-z][A-Za-z0-9]*)>", RegexOptions.Compiled);
        MatchCollection matches = tagPattern.Matches(input);

        foreach (Match match in matches)
        {
            string tag = match.Value.ToLower(); // Приводим тег к нижнему регистру для корректного сравнения
            string normalizedTag = NormalizeTag(tag); // Нормализуем тег для сравнения

            // Проверяем, содержится ли уже такой тег (игнорируя регистр и наличие "/")
            if (!ContainsTag(tags, normalizedTag))
            {
                tags.Add(tag); // Добавляем только уникальные теги
            }
        }

        return tags;
    }

    // Метод для нормализации тега (приводим к виду без / и в нижний регистр)
    private static string NormalizeTag(string tag)
    {
        // Убираем слеш и делаем строку в нижнем регистре
        return tag.Replace("/", "").ToLower();
    }

    // Метод для проверки наличия тега (с точностью до / и регистра)
    private static bool ContainsTag(MyArrayList<string> tags, string normalizedTag)
    {
        for (int i = 0; i < tags.Size(); i++)
        {
            if (NormalizeTag(tags.Get(i)).Equals(normalizedTag))
            {
                return true;
            }
        }
        return false;
    }

    public static void Main(string[] args)
    {
        // Чтение файла
        string[] lines = File.ReadAllLines("input.txt");

        // Динамический массив для всех тегов
        MyArrayList<string> allTags = new MyArrayList<string>();

        foreach (string line in lines)
        {
            // Извлекаем теги из каждой строки
            MyArrayList<string> tagsFromLine = ExtractTags(line);

            // Добавляем теги в общий массив
            allTags.AddAll(tagsFromLine.ToArray());
        }

        // Выводим результат
        Console.WriteLine("Уникальные теги из файла:");
        for (int i = 0; i < allTags.Size(); i++)
        {
            Console.WriteLine(allTags.Get(i));
        }
    }
}
