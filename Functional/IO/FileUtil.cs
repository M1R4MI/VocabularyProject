﻿using System.Text;
using System.Text.RegularExpressions;

namespace Functional.IO;

public class FileUtil
{
    private static string _filePath = Path.Combine(Environment.CurrentDirectory,  @"Dictionaries\");

    //Reading file in a specific path while checking if it is the right file extension
    public static Dictionary<string, string> OpenFile(string fileName)
    {
        string fullPath = Path.Combine(_filePath, fileName);
        Dictionary<string, string> readFile = File.ReadAllLines(fullPath, Encoding.Default)
            .Select(x => Regex.Match(x, @"(\w*)\s*(\w*.*)")).ToDictionary(x => x.Groups[1].Value,
                x => x.Groups[2].Value);
        return readFile;
    }
    
    //function saves information to the dictionary file
    public static void SaveFile(string fileName, Dictionary<string, string> dictionaryFile)
    {
        if (dictionaryFile.Count == 0)
            return;
        string fullPath = Path.Combine(_filePath, fileName);
        using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            using (StreamWriter writer = new StreamWriter(stream, Encoding.Unicode))
                foreach (var item in dictionaryFile)
                    writer.WriteLine($"{item.Key} {item.Value}");
    }

    // finds specific word in file and deletes it from file(overwrites it)
    public static void DeleteFromFile(string fileName, Dictionary<string, string> dictionaryFile, string key)
    {
        if (dictionaryFile.Count == 0)
        {
            Console.WriteLine("File is empty.");   
        }
        else
        {
            string fullPath = Path.Combine(_filePath, fileName);
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            using (StreamWriter writer = new StreamWriter(stream, Encoding.Unicode))
                foreach (var item in dictionaryFile)
                {
                    if (key.Equals(item.Key))
                        dictionaryFile.Remove(item.Key);
                    writer.WriteLine($"{item.Key} {item.Value}");
                }
        }
    }
    
    //Function gets information about files in specified path
    public static FileInfo[] GetDirectoryFiles() 
    { 
        DirectoryInfo directoryInfo = new DirectoryInfo(_filePath);
        FileInfo[] fileInfos = directoryInfo.GetFiles();
        Console.WriteLine(_filePath);
        return fileInfos;
    }
}