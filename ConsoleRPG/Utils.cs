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
            Console.WriteLine("Agility:      " + target.agility + "   To Hit modifier: " + target.toHitMod);
            Console.WriteLine("                      Dodge modifier: " + target.dodgeMod);
            Console.WriteLine("Constitution: " + target.constitution + "   Hit Points Modifier: " + target.hpMod);
            Console.WriteLine("Intelligence: " + target.intelligence + "   Magic Damage modifier: " + target.magicDmgMod);
            PrintHorizontalLine();
            Console.WriteLine("Weapon:       " + target.wieldedWeapon.name + "   Damage: " + target.wieldedWeapon.dmgMin + "-" + target.wieldedWeapon.dmgMax);
        }

        public void PrintHorizontalLine()
        {
            int cWidth = Console.WindowWidth;
            for (int i = 0; i < cWidth; i++)
            {
                Console.Write("-");
            }
        }
    }
}
