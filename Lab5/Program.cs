using Lab4;
using System;
using System.IO;
using System.Text.RegularExpressions;

public class Lab5
{
    public static MyArrayList<string> ExtractTags(string input)
    {
        MyArrayList<string> tags = new MyArrayList<string>();

        Regex tagPattern = new Regex(@"<(/?[A-Za-z][A-Za-z0-9]*)>", RegexOptions.Compiled);
        MatchCollection matches = tagPattern.Matches(input);

        foreach (Match match in matches)
        {
            string tag = match.Value.ToLower(); 
            string normalizedTag = NormalizeTag(tag); 

            if (!ContainsTag(tags, normalizedTag))
            {
                tags.Add(tag); 
            }
        }

        return tags;
    }

    private static string NormalizeTag(string tag)
    {
        return tag.Replace("/", "").ToLower();
    }

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
        MyArrayList<string> lines = new MyArrayList<string>(File.ReadAllLines("input.txt"));

        MyArrayList<string> allTags = new MyArrayList<string>();

        for (int i = 0; i < lines.Size(); i++)
        {
            MyArrayList<string> tagsFromLine = ExtractTags(lines.Get(i));

            allTags.Add($"Строка {i}");
            allTags.AddAll(tagsFromLine.ToArray());
        }

        Console.WriteLine("Уникальные теги из файла:");
        for (int i = 0; i < allTags.Size(); i++)
        {
            Console.WriteLine(allTags.Get(i));
        }
    }
}
