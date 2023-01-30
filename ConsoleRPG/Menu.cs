using System;
using System.Collections.Generic;
namespace ConsoleRPG
{
    public class Menu
    {
        public List<string> menuOptions = new List<string>();
        public int selectedOption = 0;
        string displayText;

        public Menu(List<string> options, string topText)
        {
            menuOptions = options;
            displayText = topText;
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
            Console.ResetColor();
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
                Console.Write(displayText);
                for (int i = 0; i < menuOptions.Count; i++)
                {
                    Console.ResetColor();
                    if (i == selectedOption)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else
                    {
                        Console.ResetColor();
                    } 
                        
                    Console.WriteLine(menuOptions[i]);
                    Console.ForegroundColor = ConsoleColor.White;
                }
            } while (keyInfo.Key != ConsoleKey.X);
            return 0;
        }
    }
}
