using System;
namespace ConsoleRPG
{
    public class Race
    {
        public string raceName;
        public int raceStrMod;
        public int raceAgiMod;
        public int raceConMod;
        public int raceIntMod;
        public string raceShort;


        public Race(string name, int rSmod, int rAmod, int rCmod, int rImod)
        {
            raceName = name;
            raceStrMod = rSmod;
            raceAgiMod = rAmod;
            raceConMod = rCmod;
            raceIntMod = rImod;
            raceShort = name + " (str: " + rSmod + " agi: " + rAmod + " con: " + rCmod + " int: " + rImod;
        }

        public Race Clone()
        {
            Race r = new Race(this.raceName, this.raceStrMod, this.raceAgiMod, this.raceConMod, this.raceIntMod);
            return r;
        }
    }
}
