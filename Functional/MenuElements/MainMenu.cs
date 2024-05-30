using Functional.Data;
using Functional.Entities;
using Functional.IO;

namespace Functional.MenuElements;

public class MainMenu(DataContext dataContext)
{
    private readonly DictionaryMenu _dictionary = new(dataContext);

    public void MainMenuCall()
    {
        string prompt = " --- Main Menu ---";
        string[] options = ["Choose dictionary", "Create new dictionary", "Exit"];
        Menu mainMenu = new Menu(prompt, options);
        int selectedIndex = mainMenu.Run();

        switch (selectedIndex)
        {
            case 0:
                DictionaryChoose();
                break;
            case 1:
                DictionaryCreate();
                break;
            case 2:
                Exit();
                break;
        }
    }
    
    private void DictionaryChoose()
    {
        int number = 0;
        FileInfo[] files = FileUtil.GetDirectoryFiles();
        foreach(var file in files)
        {
            Console.WriteLine($"{++number} {file.Name}");
        }
        Console.WriteLine("Choose dictionary");
        number = int.Parse(Console.ReadLine() ?? string.Empty) - 1;
        string filename = files[number].Name;

        Diction diction = new Diction
        {
            dictName = filename,
            keyValues = FileUtil.OpenFile(filename)
        };

        dataContext.Dictions.Add(diction);
        Console.Clear();
        _dictionary.DictionaryMenuCall();
    }

    private void DictionaryCreate()
    {
        FileInfo[] fileInfos = FileUtil.GetDirectoryFiles();
        bool point = true;

        while (point)
        {
            Console.WriteLine("Write dictionary name");
            string? fileName = Console.ReadLine();
            fileName += ".txt";
            if (FileUtil.GetDirectoryFiles().Length == 0)
            {
                point = false;
            }
            else
            {
                foreach(var file in fileInfos)
                {
                    if (fileName == file.Name)
                    {
                        Console.WriteLine("The dictionary already exists.");
                        point = true;
                        break;
                    }
                    point = false;
                }
            }   

            if( point == false) 
            {
                Diction diction = new Diction
                {
                    dictName = fileName,
                    keyValues = new Dictionary<string, string>()
                };
                dataContext.Dictions.Add(diction);
            }
            
        } 
        Console.Clear();
        _dictionary.DictionaryMenuCall();
    }

    private void Exit()
    {
        if(dataContext.Dictions.Count == 0)
        {
            Environment.Exit(0);
        }
        string? filename = dataContext.Dictions.First().dictName;
        Dictionary<string, string> keyValuePairs = dataContext.Dictions.First().keyValues;
        FileUtil.SaveFile(filename, keyValuePairs);
        Environment.Exit(0);
    }
}