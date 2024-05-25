using System.Text;
using Common.ConsoleIO;
using Functional.Data;
using Functional.MenuElements;

namespace VocabularyProject
{
    class Program
    {
        private static MainMenu _mainMenu;
        private static DataContext dataContext;
        
        static void Main(string[] args)
        {
            Settings.SetConsoleParam("English - Ukrainian Dictionary");

            dataContext = new DataContext();
            _mainMenu = new MainMenu(dataContext);
            _mainMenu.MainMenuCall();
        }
    }
}