using System;
using System.Collections.Generic;
namespace ConsoleRPG
{
    public class Menu
    {
        public List<string> menuOptions = new List<string>();
        public int selectedOption = 0;

        public Menu(List<string> options)
        {
            menuOptions = options;
        }

        public void PrintInit()
        {
            for (int i = 0; i < menuOptions.Count; i++)
            {
                Console.ResetColor();
                if (i == selectedOption)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                Console.WriteLine(menuOptions[i]);
            }
        }

        public int Run()
        {
            PrintInit();
            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    selectedOption++;
                    if (selectedOption > menuOptions.Count - 1) { selectedOption = 0; }
                }
                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    selectedOption--;
                    if (selectedOption < 0) { selectedOption = menuOptions.Count - 1; }
                }
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    return selectedOption;
                }
                Console.Clear();
                for (int i = 0; i < menuOptions.Count; i++)
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
