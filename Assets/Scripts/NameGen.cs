using System;
using System.Collections.Generic;
using System.IO;

public class NameGen
{
    List<string> prefixes = new List<string>();
    List<string> suffixes = new List<string>();

    private static NameGen _instance;
    public static NameGen Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new NameGen();
            }
            return _instance;
        }
    }
    public NameGen()
    {
        StreamReader sr = new StreamReader("Assets/Data Files/Prefixes.txt");
        string? line = sr.ReadLine();
        while (line != null)
        {
            prefixes.Add(line);
            line = sr.ReadLine();
        }
        sr.Close();

        sr = new StreamReader("Assets/Data Files/Suffixes.txt");
        line = sr.ReadLine();
        while (line != null)
        {
            suffixes.Add(line);
            line = sr.ReadLine();
        }
        sr.Close();
    }
    public string GenerateRandomCompanyName()
    {
        Random random = MyRandom.Instance;
        string prefix = prefixes[random.Next(prefixes.Count)];
        string suffix = suffixes[random.Next(suffixes.Count)];
        return prefix + " " + suffix;
    }
}