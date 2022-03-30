using System;
using System.Collections.Generic;

/*  Utils class - houses some utilitary functions */

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

        public void PrintCharacterSheet(Player target)
        {
            PrintHorizontalLine();
            Console.WriteLine("Name:         " + target.name + "   Level: " + target.level);
            Console.WriteLine("Race:         " + target.characterRace.raceName);
            Console.WriteLine("Class:        " + target.characterClass.className); 
            Console.WriteLine("Hit Points:   " + target.hp + "/" + target.baseHP);
            Console.WriteLine("Experience:   " + target.playerXP);
            PrintHorizontalLine();
            Console.WriteLine("Strength:     " + target.strength + "   Melee Damage modifier: " + target.meleeDmgMod);
            Console.WriteLine("Agility:      " + target.agility + "   To Hit modifier: " + target.toHitMod);
            Console.WriteLine("                  Dodge modifier: " + target.dodgeMod);
            Console.WriteLine("Constitution: " + target.constitution + "   Hit Points Per Level: " + target.hpMod);
            Console.WriteLine("Intelligence: " + target.intelligence + "   Magic Damage modifier: " + target.magicDmgMod);
            PrintHorizontalLine();
            Console.WriteLine("Weapon:       " + target.playerWeapon.name + "   Damage: " + target.playerWeapon.dmgMin + "-" + target.playerWeapon.dmgMax);
            PrintHorizontalLine();
            Console.WriteLine("Skills");
            foreach(Skill s in target.characterClass.skillList)
            {
                Console.WriteLine(s.skillName + " , " + s.damageType);
            }
        }

        public void PrintCharacterSheet(Enemy target)
        {
            PrintHorizontalLine();
            Console.WriteLine("Name:         " + target.name + "   Level: " + target.level);
            Console.WriteLine("Hit Points:   " + target.hp + "/" + target.baseHP);
            PrintHorizontalLine();
            Console.WriteLine("Strength:     " + target.strength + "   Melee Damage modifier: " + target.meleeDmgMod);
            Console.WriteLine("Agility:      " + target.agility + "   To Hit modifier: " + target.toHitMod);
            Console.WriteLine("                      Dodge modifier: " + target.dodgeMod);
            Console.WriteLine("Constitution: " + target.constitution + "   Hit Points Modifier: " + target.hpMod);
            Console.WriteLine("Intelligence: " + target.intelligence + "   Magic Damage modifier: " + target.magicDmgMod);
            PrintHorizontalLine();
            foreach (Weapon w in target.wieldedWeapons)
            {
                Console.WriteLine("Weapon:       " + w.name + "   Damage: " + w.dmgMin + "-" + w.dmgMax);
            }
        }

        public void PrintCharacterSheet(ModularEnemy target)
        {
            PrintHorizontalLine();
            Console.WriteLine("Name:         " + target.name + "   Level: " + target.level);
            Console.WriteLine("Hit Points:   " + target.hp + "/" + target.baseHP);
            PrintHorizontalLine();
            Console.WriteLine("Strength:     " + target.strength + "   Melee Damage modifier: " + target.meleeDmgMod);
            Console.WriteLine("Agility:      " + target.agility + "   To Hit modifier: " + target.toHitMod + "    To Hit chance: "+target.hitChance+"%");
            Console.WriteLine("                  Dodge modifier: " + target.dodgeMod);
            Console.WriteLine("Constitution: " + target.constitution + "   Hit Points Per Level: " + target.hpMod);
            Console.WriteLine("Intelligence: " + target.intelligence + "   Magic Damage modifier: " + target.magicDmgMod);
            PrintHorizontalLine();
            Console.WriteLine("Weapon:       " + target.wieldedWeapon.name + "   Damage: " + (target.wieldedWeapon.dmgMin) + "-"
                            + (target.wieldedWeapon.dmgMax) + " (" + target.wieldedWeapon.critChance + "%) for x"+target.wieldedWeapon.critMult+" damage");
        }

        public void PrintHorizontalLine()
        {
            int cWidth = Console.WindowWidth;
            for (int i = 0; i < cWidth; i++)
            {
                Console.Write("-");
            }
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
