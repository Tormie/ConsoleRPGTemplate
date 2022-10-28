using System;
using System.Collections.Generic;

/*  Utils class - houses some utilitary functions
 *  Apparently this class turned into housing game related functions too */

namespace ConsoleRPG
{
    public class Utils
    {
        public void DebugLine(string line)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(line);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void WarningLine(string line)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(line);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void ErrorLine(string line)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(line);
            Console.ForegroundColor = ConsoleColor.White;
        }


        public void TypeLine(string line)
        {
            for (int i = 0; i < line.Length; i++)
            {
                Console.Write(line[i]);
                System.Threading.Thread.Sleep(25);
            }
            Console.Write("\n");
        }

        public void TypeLine(string line, int delay)
        {
            for (int i = 0; i < line.Length; i++)
            {
                Console.Write(line[i]);
                System.Threading.Thread.Sleep(delay);
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

        public string expandString(string input, int desiredLength)
        {
            string target = input;
            if (input.Length < desiredLength)
            {
                for (int i = 0; i < desiredLength-input.Length; i++)
                {
                    target += " ";
                }
            }
            return target;
        }

        public void PrintCharacterSheet(Player target)
        {
            PrintHorizontalLine();
            Console.WriteLine("Name:         " + target.name + "   Level: " + target.level);
            Console.WriteLine("Race:         " + target.characterRace.raceName);
            Console.WriteLine("Class:        " + target.characterClass.className); 
            Console.WriteLine("Hit Points:   " + target.currentHP + "/" + target.hp);
            Console.WriteLine("Experience:   " + target.playerXP);
            PrintHorizontalLine();
            Console.WriteLine("Strength:     " + target.strength + "   Melee Damage modifier: " + target.meleeDmgMod);
            Console.WriteLine("Agility:      " + target.agility + "   To Hit modifier: " + target.toHitMod);
            Console.WriteLine("                  Dodge modifier: " + target.dodgeMod);
            Console.WriteLine("Constitution: " + target.constitution + "   Hit Points Per Level: " + target.hpMod);
            Console.WriteLine("Intelligence: " + target.intelligence + "   Magic Damage modifier: " + target.magicDmgMod);
            PrintHorizontalLine();
            Console.WriteLine("Weapon:       " + target.weapon.name + "   Damage: " + target.weapon.dmgMin + "-" + target.weapon.dmgMax + " (" + target.weapon.critChance + "%) for x" + target.weapon.critMult + " damage");
            PrintHorizontalLine();
            Console.WriteLine("Skills(damage type)");
            Console.WriteLine();
            foreach(Skill s in target.characterClass.skillList)
            {
                Console.WriteLine(s.skillName + "(" + s.damageType+")");
                Console.WriteLine("Cooldown: " + s.skillCooldown + " Power: " + s.skillPower);
                if (s.targetsSelf) { Console.Write("Targets self. | " ); }
                else { Console.Write("Targets other. | " ); }
                if (s.targetsAll) { Console.WriteLine("All targets."); }
                else { Console.WriteLine("Single target."); }
            }
            Console.WriteLine();
        }

        public void PrintCharacterSheet(ModularEnemy target)
        {
            PrintHorizontalLine();
            Console.WriteLine("Name:         " + target.name + "   Level: " + target.level);
            Console.WriteLine("Hit Points:   " + target.currentHP + "/" + target.hp);
            Console.WriteLine("XP value:     " + target.xpValue);
            PrintHorizontalLine();
            Console.WriteLine("Strength:     " + target.strength + "   Melee Damage modifier: " + target.meleeDmgMod);
            Console.WriteLine("Agility:      " + target.agility + "   To Hit modifier: " + target.toHitMod + "    To Hit chance: "+target.hitChance+"%");
            Console.WriteLine("                  Dodge modifier: " + target.dodgeMod);
            Console.WriteLine("Constitution: " + target.constitution + "   Hit Points Per Level: " + target.hpMod);
            Console.WriteLine("Intelligence: " + target.intelligence + "   Magic Damage modifier: " + target.magicDmgMod);
            PrintHorizontalLine();
            Console.WriteLine("Weapon:       " + target.weapon.name + "   Damage: " + (target.weapon.dmgMin) + "-"
                            + (target.weapon.dmgMax) + " (" + target.weapon.critChance + "%) for x"+target.weapon.critMult+" damage");
            PrintHorizontalLine();
            Console.WriteLine("Skills(damage type)");
            Console.WriteLine();
            foreach (Skill s in target.characterClass.skillList)
            {
                Console.WriteLine(s.skillName + "(" + s.damageType + ")");
            }
            Console.WriteLine();
        }

        public void PrintHorizontalLine()
        {
            int cWidth = Console.WindowWidth;
            for (int i = 0; i < cWidth; i++)
            {
                Console.Write("-");
            }
        }

        public string ReturnHorizontalLine()
        {
            int cWidth = Console.WindowWidth;
            string result = "";
            for (int i = 0; i < cWidth; i++)
            {
                result += "-";
            }
            return result;
        }

        public string GetResponse(string question, string[] options)
        {
            TypeLine(question);
            string response = Console.ReadLine();
            
            foreach (string s in options)
            {
                if (response.ToLower() == s.ToLower() )
                {
                    return response;
                }
            }
            Console.WriteLine("Incorrect choice, please try again.");
            
            return GetResponse(question, options);
        }
    }
}
