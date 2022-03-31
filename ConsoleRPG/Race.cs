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


        public Race(string name, int rSmod, int rAmod, int rCmod, int rImod)
        {
            raceName = name;
            raceStrMod = rSmod;
            raceAgiMod = rAmod;
            raceConMod = rCmod;
            raceIntMod = rImod;
        }

        public Race Clone()
        {
            Race r = new Race(this.raceName, this.raceStrMod, this.raceAgiMod, this.raceConMod, this.raceIntMod);
            return r;
        }
    }
}
