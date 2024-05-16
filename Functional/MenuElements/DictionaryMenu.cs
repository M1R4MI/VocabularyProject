using Functional.Data;
using Functional.IO;

namespace Functional.MenuElements;

public class DictionaryMenu
{
    private DataContext dataContext;

    public DictionaryMenu(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    public void DictionaryMenuCall()
    {
        string prompt = $" --- Dictionary Menu for {dataContext.Dictions.First().dictName} ---";
        string[] options = { "Add Word", "Edit Word", "Search Word", "Delete Word", "To Main Menu", "Exit" };
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
                MainMenu main = new MainMenu(dataContext);
                main.MainMenuCall();
                break;
            case 5:
                Exit();
                break;
        }
    }

    private void DeleteWord()//removes selected word from file and collection.
    {
        string keyValue;
        
        Console.WriteLine("Write word you want to delete: ");
        keyValue = Console.ReadLine();
        
        if (dataContext.Dictions.First().keyValues.Keys.Count == 0)
        {
            Console.WriteLine("Word cannot be deleted because it isn't exists.");
        }
        else
        {
            foreach (var i in dataContext.Dictions.First().keyValues.Keys)
            {
                if (i == keyValue)
                {
                    FileUtil.DeleteFromFile(dataContext.Dictions.First().dictName, dataContext.Dictions.
                        First().keyValues, keyValue);
                }
            }
            FileUtil.SaveFile(dataContext.Dictions.First().dictName, dataContext.Dictions.First().keyValues);
            Console.WriteLine("Removal was completed successfully.");
        }
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
        
        if (dataContext.Dictions.First().keyValues.Keys.Contains(keyValue))
        {
            dataContext.Dictions.First().keyValues.Remove(keyValue);
            FileUtil.DeleteFromFile(dataContext.Dictions.First().dictName, dataContext.Dictions.First().keyValues, keyValue);
            
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
            dataContext.Dictions.First().keyValues.Add(newValue,newTranslation);
        }
        else
        {
            Console.WriteLine("The word you choose is not exists in the dictionary or you have written it incorrectly.");
        }
        
        FileUtil.SaveFile(dataContext.Dictions.First().dictName, dataContext.Dictions.First().keyValues);
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
            if(dataContext.Dictions.First().keyValues.TryGetValue(searchKey, out searchValue))
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
        
        if (dataContext.Dictions.First().keyValues.Keys.Contains(keyValue))
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
        
        dataContext.Dictions.First().keyValues.Add(keyValue, translationValue);
        try
        {
            FileUtil.SaveFile(dataContext.Dictions.First().dictName, 
                dataContext.Dictions.First().keyValues);
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
    }
    
    private void Exit()
    {
        if(dataContext.Dictions.Count == 0)
        {
            Environment.Exit(0);
        }
        string filename = dataContext.Dictions.First().dictName;
        Dictionary<string, string> keyValuePairs = dataContext.Dictions.First().keyValues;
        FileUtil.SaveFile(filename, keyValuePairs);
        Environment.Exit(0);
    }
}