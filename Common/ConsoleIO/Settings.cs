using System.Text;

namespace Common.ConsoleIO;

public static class Settings
{
    public static void SetConsoleParam(string title)
    {
        Console.Title = title;
        Console.Clear();
        Console.OutputEncoding = Encoding.Unicode;
        Console.InputEncoding = Encoding.Unicode;
    }
}