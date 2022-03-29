using System;
using System.Collections.Generic;
namespace ConsoleRPG
{
    public class Class
    {
        public string className;
        public int classBaseHP;
        public int classHPPerLevel;
        public int classStrMod;
        public int classAgiMod;
        public int classConMod;
        public int classIntMod;

        public List<Skill> skillList;

        public Class(string name, int bHP, int hpLvl, int cSMod, int cAMod, int cCMod, int cIMod,List<Skill> cSkills)
        {
            className = name;
            classBaseHP = bHP;
            classHPPerLevel = hpLvl;
            classStrMod = cSMod;
            classAgiMod = cAMod;
            classConMod = cCMod;
            classIntMod = cIMod;
            skillList = cSkills;
        }
    }
}
