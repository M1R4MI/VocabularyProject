using Functional.Data;
using Functional.IO;

namespace Functional.MenuElements;

public class DictionaryMenu
{
    private DataContext _dataContext;
    private MainMenu main;
    public DictionaryMenu(DataContext dataContext)
    {
        _dataContext = dataContext;
        main = new MainMenu(_dataContext);
    } 
    
    public void DictionaryMenuCall()
    {
        string prompt = $" --- Dictionary Menu for {_dataContext.Dictions.First().dictName} ---";
        string[] options = { "Add Word", "Edit Word", "Search Word", "Delete Word","Show all words", "To Main Menu", "Exit" };
        Menu dictionaryMenu = new Menu(prompt, options);
        int selectedIndex = dictionaryMenu.Run();

        switch (selectedIndex)
        {
            case 0:
                AddWord();
                break;
            case 1:
                EditWord();
                break;
            case 2:
                SearchTranslation();
                break;
            case 3:
                DeleteWord();
                break;
            case 4:
                ShowAll();
                break;
            case 5:
                main.MainMenuCall();
                break;
            case 6:
                Exit();
                break;
        }
    }

    private void DeleteWord()//removes selected word from file and collection.
    {
        string keyValue;
        
        Console.WriteLine("Write word you want to delete: ");
        keyValue = Console.ReadLine();
        
        if (_dataContext.Dictions.First().keyValues.Keys.Count == 0)
        {
            Console.WriteLine("Word cannot be deleted because it isn't exists.");
        }
        else
        {
            foreach (var i in _dataContext.Dictions.First().keyValues.Keys)
            {
                if (i == keyValue)
                {
                    FileUtil.DeleteFromFile(_dataContext.Dictions.First().dictName, _dataContext.Dictions.
                        First().keyValues, keyValue);
                }
            }
            FileUtil.SaveFile(_dataContext.Dictions.First().dictName, _dataContext.Dictions.First().keyValues);
            Console.WriteLine("Removal was completed successfully.");
        }
        
        Console.WriteLine("If you want to delete some other words press \"y\"");
        string stopKey = Console.ReadLine();
        if (stopKey == "y" || stopKey == "Y" || stopKey == "yes" || stopKey == "у")
            AddWord();

        DictionaryMenuCall();
    }

    private void EditWord()
    {
        string keyValue, newValue, newTranslation;
        Console.WriteLine("Write word you want to change");
        keyValue = Console.ReadLine();
        
        while (keyValue == null)
        {
            keyValue = Console.ReadLine();
        }
        
        if (_dataContext.Dictions.First().keyValues.Keys.Contains(keyValue))
        {
            _dataContext.Dictions.First().keyValues.Remove(keyValue);
            FileUtil.DeleteFromFile(_dataContext.Dictions.First().dictName, _dataContext.Dictions.First().keyValues, keyValue);
            
            Console.WriteLine("Write value to which you want to change word that you choose: ");
            newValue = Console.ReadLine();
            while (newValue == null)
            {
                newValue = Console.ReadLine();
            }
            
            Console.WriteLine("Write also translation for this word");
            newTranslation = Console.ReadLine();
            while (newTranslation == null)
            {
                newTranslation = Console.ReadLine();
            }
            _dataContext.Dictions.First().keyValues.Add(newValue,newTranslation);
        }
        else
        {
            Console.WriteLine("The word you choose is not exists in the dictionary or you have written it incorrectly.");
        }
        
        FileUtil.SaveFile(_dataContext.Dictions.First().dictName, _dataContext.Dictions.First().keyValues);
        DictionaryMenuCall();
    }

    public void SearchTranslation()
    {
        Dictionary<string, string> translation = new Dictionary<string, string>();
        string searchKey = "", searchValue = "";
        bool stopPoint = true;

        while(stopPoint)
        {
            Console.Write("Write a word to find translation: ");
            searchKey = Console.ReadLine();
            if(_dataContext.Dictions.First().keyValues.TryGetValue(searchKey, out searchValue))
            {
                Console.WriteLine($"Translation of word {searchKey} is: {searchValue}");
                translation.Add(searchKey, searchValue);
                stopPoint = false;
                
            }
            else
            {
                string errorMessage = "This word doesn't exists.";
            }
        }
        Console.WriteLine("If you want to proceed working with this dictionary press \"y\"");
        string stopKey = Console.ReadLine();
        if (stopKey == "y" || stopKey == "Y" || stopKey == "yes" || stopKey == "у")
            DictionaryMenuCall();
        main.MainMenuCall();
    }

    private void AddWord()
    {
        string keyValue = "", translationValue;
        
        Console.Clear();
        Console.WriteLine("Write value of the word you want to add to the dictionary: ");
        keyValue = Console.ReadLine();
        
        while (keyValue == null)
        {
            keyValue = Console.ReadLine();
        }
        
        if (_dataContext.Dictions.First().keyValues.Keys.Contains(keyValue))
        {
            Console.Write("The word you want to add is already exists.\n");
            return;
        }

        Console.Write("Write translation for the word: ");
        translationValue = Console.ReadLine();
        while (translationValue == null)
        {
            translationValue = Console.ReadLine();
        }
        
        _dataContext.Dictions.First().keyValues.Add(keyValue, translationValue);
        try
        {
            FileUtil.SaveFile(_dataContext.Dictions.First().dictName, 
                _dataContext.Dictions.First().keyValues);
            Console.WriteLine("Word successfully added to the dictionary.\n");
        }
        catch (IOException ex)
        {
            Console.WriteLine("File operation exception: " + ex);
            return;
        }
        
        Console.WriteLine("If you want to proceed to the new word press \"y\"");
        string stopKey = Console.ReadLine();
        if (stopKey == "y" || stopKey == "Y" || stopKey == "yes" || stopKey == "у")
            AddWord();
        DictionaryMenuCall();
    }

    private void ShowAll()
    {
        Console.WriteLine("Word     Translation");
        foreach (var item in _dataContext.Dictions.First().keyValues)
        {
            Console.WriteLine($"{item.Key}   {item.Value}");
        }
        
        Console.WriteLine("If you want to proceed working with dictionary press \"y\"");
        string stopKey = Console.ReadLine();
        if (stopKey == "y" || stopKey == "Y" || stopKey == "yes" || stopKey == "у")
            DictionaryMenuCall();
    }
    
    private void Exit()
    {
        if(_dataContext.Dictions.Count == 0)
        {
            Environment.Exit(0);
        }
        string filename = _dataContext.Dictions.First().dictName;
        Dictionary<string, string> keyValuePairs = _dataContext.Dictions.First().keyValues;
        FileUtil.SaveFile(filename, keyValuePairs);
        Environment.Exit(0);
    }
}