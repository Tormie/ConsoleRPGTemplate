using System;
using System.Collections.Generic;
namespace ConsoleRPG
{
    public class Utils
    {
        public void TypeLine(string line)
        {
            for (int i = 0; i < line.Length; i++)
            {
                Console.Write(line[i]);
                System.Threading.Thread.Sleep(25);
            }
            Console.Write("\n");
        }

        public void EnterToCont()
        {
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
        }

        public void GetPath()
        {
            Console.WriteLine(System.AppContext.BaseDirectory);
        }
    }
}
