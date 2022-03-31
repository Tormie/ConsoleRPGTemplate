using System;
using System.Collections.Generic;

/*  Main program - activates utils and datalib
 *  runs the game
 *  Game logic might have to be moved to a separate class */

namespace ConsoleRPG
{
    class Program
    {
        /*  Init classes and several base vars */
        public static Utils ut = new Utils();
        public static DataLib dl = new DataLib();
        public static Player player;
        public static List<Encounter> gameEncounters;
        public static Encounter currentEncounter;
        public static bool gameOver = false;
        public static int encountersWon = 0;
        public static int monstersDefeated = 0;

        /*  Nice and clean main method */
        static void Main(string[] args)
        {
            PlayGame();
        }

        /*  Runs the basic game logic */
        static void PlayGame()
        {
            InitiatePlayer();
            InitiateEncounters();
            while (!gameOver)
            {
                currentEncounter = gameEncounters[0];
                currentEncounter.enemiesDefeated = false;
                currentEncounter.Run();
                gameEncounters.Remove(currentEncounter);
                if (gameEncounters.Count == 0)
                {
                    EndGame();
                }
            }
            Console.ReadLine();
        }

        /*  Ends the game, might need some editing to encompass both win and game over */
        static void EndGame()
        {
            ut.TypeLine("Congratulations! You have conquered all this game has to offer!");
            ut.TypeLine("You managed to survive " + encountersWon + " encounters.");
            ut.TypeLine("You defeated " + monstersDefeated + " monsters!");
            ut.TypeLine("You have reached level " + player.level);
            ut.TypeLine("Would you like to try again? (Y/N)");
            string answer = Console.ReadLine().ToLower();
            if (answer == "y")
            {
                PlayGame();
            }
            else if (answer == "n")
            {
                return;
            }
        }

        /*  Ends the game, might want to add this to the EndGame() function */
        static void GameOver()
        {
            ut.TypeLine("You lie on the floor, beaten to a bloody pulp by the monsters.");
            ut.TypeLine("You managed to survive " + encountersWon + " encounters.");
            ut.TypeLine("You defeated " + monstersDefeated + " monsters!");
            ut.TypeLine("You have reached level " + player.level);
            ut.TypeLine("Would you like to try again? (Y/N)");
            string answer = Console.ReadLine().ToLower();
            if (answer == "y")
            {
                PlayGame();
            }
            else if (answer == "n")
            {
                return;
            }
        }

        /*  Pretty straightforward, just in its own function for clarity */
        static void PrintIntro()
        {
            ut.TypeLine("You walk into a dimly lit cave, cautious as ever you proceed slowly. \nSuddenly you see movement, monsters appear!");
            ut.TypeLine("Suddenly you see movement, monsters appear!");
        }

        /*  Sets up player character based on player input */
        static void InitiatePlayer()
        {
            ut.TypeLine("Welcome, player. Please enter your name.");
            string pname = Console.ReadLine();
            player = new Player(pname);
            player.InitPlayer();
            player.invulType = "stealth";
        }

        /*  Fills encounter list based on encounter types */
        static void InitiateEncounters()
        {
            gameEncounters = new List<Encounter>();
            for (int i = 0; i < 10; i++)
            {
                gameEncounters.Add(dl.encounterList[0].Clone());
                gameEncounters.Add(dl.encounterList[1].Clone());
                gameEncounters.Add(dl.encounterList[2].Clone());
                gameEncounters.Add(dl.encounterList[3].Clone());
            }
        }

    }
}
