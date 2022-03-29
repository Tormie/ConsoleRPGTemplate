using System;
using System.Collections.Generic;

/*  PlayerClass class - blueprint for a player character class.
 *  Actuall classes are created by the Datalib class */

namespace ConsoleRPG
{
    public class PlayerClass
    {
        public string className;
        public int classBaseHP;
        public int classDmgMod;
        public int classHPPerLevel;
        public int classDmgPerLevel;
        public PlayerSkill[] playerSkills;
        public List<PlayerSkill> playerSkillList;

        public PlayerClass(string name, int bHP, int dmgMod, int hpLvl, int dmgLvl, List<PlayerSkill> pSkills)
        {
            className = name;
            classBaseHP = bHP;
            classDmgMod = dmgMod;
            classHPPerLevel = hpLvl;
            classDmgPerLevel = dmgLvl;
            playerSkillList = pSkills;
        }
    }
}
