namespace Functional.MenuElements;

public class Menu(string prompt, string[] options)
{
    private int _selectedIndex = 0;

    private void MenuArea(int x, int y, int width, int height)
    {
        for(; height > 0;)
        {
            Console.SetCursorPosition(x, y + --height);
            Console.Write(new string(' ', width));
        }
        Console.SetCursorPosition(x, y);
    }
    
    private void DisplayOptions()
    {
        
        MenuArea(0,1,34,7);
        Console.SetCursorPosition(0,0);
        Console.WriteLine(prompt);
        Console.SetCursorPosition(0,1);
        Console.WriteLine();
        for (int i = 0; i < options.Length; i++)
        {
            if (i == _selectedIndex)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
                Console.WriteLine($"{i + 1} {options[i]} <-");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else
            {
                Console.WriteLine($"{i + 1} {options[i]}");
            }
        }
    }

    public int Run()
    {
        ConsoleKey keyPressed;
        do
        {
            Console.Clear();
            DisplayOptions();
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            keyPressed = keyInfo.Key;
            
            //update _selectedIndex on arrow keys.
            Console.WriteLine("\n\n");
            if (keyPressed == ConsoleKey.UpArrow)
            {
                _selectedIndex--;
                if (_selectedIndex == -1) _selectedIndex = options.Length - 1;
            }
        
            else if (keyPressed == ConsoleKey.DownArrow)
            {
                _selectedIndex++;
                if (_selectedIndex == options.Length) _selectedIndex = 0;
            }
        } while (keyPressed!= ConsoleKey.Enter);
        
        return _selectedIndex;
    }
}