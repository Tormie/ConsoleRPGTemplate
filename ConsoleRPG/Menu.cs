using System;
namespace ConsoleRPG
{
    public class Menu
    {
        public string[] menuOptions;
        public int selectedOption = 0;

        public Menu(string[] options)
        {
            menuOptions = options;
        }

        public int Run()
        {
            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    selectedOption++;
                    if (selectedOption > menuOptions.Length - 1) { selectedOption = 0; }
                }
                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    selectedOption--;
                    if (selectedOption < 0) { selectedOption = menuOptions.Length - 1; }
                }
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    return selectedOption;
                }
                Console.Clear();
                for (int i = 0; i < menuOptions.Length; i++)
                {
                    Console.ResetColor();
                    if (i == selectedOption)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    Console.WriteLine(menuOptions[i]);
                }
            } while (keyInfo.Key != ConsoleKey.X);
            return 0;
        }
    }
}
