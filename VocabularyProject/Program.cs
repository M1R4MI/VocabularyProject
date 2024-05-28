using Common.ConsoleIO;
using Functional.Data;
using Functional.MenuElements;

namespace VocabularyProject
{
    static class Program
    {
        static void Main()
        {
            Settings.SetConsoleParam("English - Ukrainian Dictionary");

            DataContext dataContext = new DataContext();
            MainMenu mainMenu = new MainMenu(dataContext);
            mainMenu.MainMenuCall();
        }
    }
}