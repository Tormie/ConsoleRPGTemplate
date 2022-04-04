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
        public int critChance;
        public int critMult;
        public string name;
        public string wpShort;

        public Weapon(string wName, int minDmg, int maxDmg, int critC, int critD)
        {
            name = wName;
            dmgMin = minDmg;
            dmgMax = maxDmg;
            critChance = critC;
            critMult = critD;
            wpShort = wName + " dmg: " + dmgMin + "-" + dmgMax + " (crit: " + critChance + "% - x" + critMult + ")";
        }

        public Weapon Clone()
        {
            Weapon w = new Weapon(this.name, this.dmgMin, this.dmgMax, this.critChance, this.critMult);
            return w;
        }
    }
}
