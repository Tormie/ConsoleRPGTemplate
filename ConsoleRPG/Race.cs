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
    }
}
