using System;
using System.Collections.Generic;

/*  Encounter class - contains all data and functionality for the games' encounters.
 *  Currently it only handles combat encounters. */

namespace ConsoleRPG
{
    public class Encounter
    {
        public string encounterName = "";
        public string encounterIntro = "";
        public int encounterLevel;
        public bool isCombatEncounter = false;
        public List<Enemy> enemyList;
        int enemyHP = 0;
        public bool enemiesDefeated = false;
        public int encounterXP;
        public string encounterOuttro;
        public string result;
        public int resultStrength;

        public Encounter(string eName, string eIntro, string eOuttro, bool isCombat)
        {
            encounterName = eName;
            encounterIntro = eIntro;
            encounterOuttro = eOuttro;
            isCombatEncounter = isCombat;
        }

        public Encounter(string eName, string eIntro, string eOuttro, string eResult, int eResStr)
        {
            encounterName = eName;
            encounterIntro = eIntro;
            encounterOuttro = eOuttro;
            result = eResult;
            resultStrength = eResStr;
        }

        public void Initialize()
        {
            Random encRnd = new Random();
            encounterLevel = Program.player.level + encRnd.Next(0, 3) - 1;
            if (isCombatEncounter)
            {
                enemyList = new List<Enemy>();
                Random rnd = new Random();
                for (int i = 1; i <= encounterLevel/3+1; i++)
                {
                    Enemy e = Program.dl.enemyList[rnd.Next(0, Program.dl.enemyList.Count)].Clone();
                    e.level = Program.player.level;
                    e.setLevel();
                    enemyList.Add(e);
                }
                foreach (Enemy e in enemyList)
                {
                    e.isAlive = true;
                    e.hp = e.baseHP;
                }
            }
        }

        public void RunCombatEncounter()
        {
            while (enemiesDefeated == false)
            {
                Console.WriteLine("Player: " + Program.player.hp + "/" + (Program.player.baseHP + Program.player.playerClass.classHPPerLevel * (Program.player.level-1)));
                Console.WriteLine("Level: " + Program.player.level);
                Console.WriteLine("Weapon: " + Program.player.playerWeapon.name + "("+Program.player.playerWeapon.dmgMin+"-"+Program.player.playerWeapon.dmgMax+
                    "). Damage Modifier: " + Program.player.playerDamageMod);
                Console.WriteLine("---------------------");
                foreach (Enemy e in enemyList)
                {
                    if (e.hp > 0)
                    {
                        Console.WriteLine(e.name + "("+e.level+") ("+e.wieldedWeapons[0].name+"(" + (e.wieldedWeapons[0].dmgMin + e.dmgMod)+"-"
                            + (e.wieldedWeapons[0].dmgMax + e.dmgMod)+"("+e.hitChance+"%)): " + e.hp + " /" + e.baseHP);
                        Console.WriteLine("---------------------");
                    }
                }
                enemyHP = 0;
                foreach (Enemy e in enemyList)
                {
                    enemyHP += e.hp;
                }
                if (enemyHP <= 0)
                {
                    enemiesDefeated = true;
                }
                if (enemiesDefeated == true)
                {
                    break;
                }
                Program.player.PlayerAttack();
                enemyHP = 0;
                foreach (Enemy e in enemyList)
                {
                    enemyHP += e.hp;
                }
                if (enemyHP <= 0)
                {
                    enemiesDefeated = true;
                }
                if (enemiesDefeated == true)
                {
                    break;
                }
                else
                {
                    Random rnd2 = new Random();
                    enemyList[rnd2.Next(0, enemyList.Count)].EnemyAction();
                }

            }
        }


        public void Run()
        {
            Initialize();
            Program.ut.TypeLine(encounterIntro);
            if (isCombatEncounter == true)
            {
                RunCombatEncounter();
                Program.ut.TypeLine(encounterOuttro);
            }
            else
            {
                Program.ut.TypeLine(encounterOuttro);
            }
            Program.encountersWon++;
        }
    }
}
