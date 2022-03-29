using System;
using System.Collections.Generic;
namespace ConsoleRPG
{
    public class Weapon
    {
        public int damage = 0;
        public int dmgMin, dmgMax = 0;
        int attackSpeed = 1;
        public int critChance;
        public int critMult;
        public string name;
        //string[] weaponNames = new string[] { "Axe", "Sword", "Dagger", "Spear" };
        //public Weapon()
        //{
        //    Random rnd = new Random();
        //    name = weaponNames[rnd.Next(0, weaponNames.Length)];
        //    damage = rnd.Next(5, 9);
        //    attackSpeed = rnd.Next(1, 3);
        //}
        public Weapon(string wName, int minDmg, int maxDmg, int critC, int critD)
        {
            name = wName;
            dmgMin = minDmg;
            dmgMax = maxDmg;
            critChance = critC;
            critMult = critD;
        }
    }
}
