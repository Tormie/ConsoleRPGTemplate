using System;
using System.Collections.Generic;

/*  Weapon class - blueprint for a weapon.
 *  Actuall weapons are created by the Datalib class */

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
