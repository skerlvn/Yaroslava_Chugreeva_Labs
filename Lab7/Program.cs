using Lab6;
using System;
using System.IO;

public class Lab7
{
    public static bool IsValidIP(string ip)
    {
        string[] parts = ip.Split('.');
        if (parts.Length != 4) return false; 

        foreach (string part in parts)
        {
            if (!IsNumeric(part) || part.Length > 3) return false;
            
            int num = int.Parse(part);
            
            if (num < 0 || num > 255) return false;
            
            if (part.Length > 1 && part[0] == '0') return false;
        }

        return true;
    }

    private static bool IsNumeric(string str)
    {
        foreach (char c in str)
        {
            if (!char.IsDigit(c)) return false;
        }
        return true;
    }

    public static void ExtractIPs(string line, MyVector<string> ipVector)
    {
        string[] tokens = line.Split(' ');  

        foreach (string token in tokens)
        {
            if (IsValidIP(token) && !ipVector.Contains(token))
            {
                ipVector.Add(token);  
            }
        }
    }

    public static void Main(string[] args)
    {
        MyVector<string> lines = new MyVector<string>(); 
        MyVector<string> ipAddresses = new MyVector<string>();  

        using (StreamReader reader = new StreamReader("input.txt"))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                lines.Add(line);  
            }
        }

        for (int i = 0; i < lines.Size(); i++)
        {
            string line = lines.Get(i);
            ExtractIPs(line, ipAddresses);  
        }

        using (StreamWriter writer = new StreamWriter("output.txt"))
        {
            for (int i = 0; i < ipAddresses.Size(); i++)
            {
                writer.WriteLine(ipAddresses.Get(i));  
            }
        }

        Console.WriteLine("IP-адреса успешно извлечены и записаны в output.txt.");
    }
}
