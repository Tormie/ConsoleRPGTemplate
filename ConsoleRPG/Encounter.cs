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
        
        public List<ModularEnemy> modEnemyList;
        int enemyHP = 0;
        public bool enemiesDefeated = false;
        public int encounterXP;
        public string encounterOuttro;
        public string result;
        public int resultStrength;


        //For a chance encounter with max 3 options.
        public bool isChanceEncounter = false;
        public string chanceResult1, chanceResult2, chanceResult3;
        public string chanceEffect1, chanceEffect2, chanceEffect3;
        public int percentageChance1, percentageChance2, percentageChance3;

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

        public Encounter(string eName, string eIntro, string eOuttro, string result1, string effect1, int chance1, string result2, string effect2, int chance2, string result3, string effect3, int chance3, bool isChance)
        {
            encounterName = eName;
            encounterIntro = eIntro;
            encounterOuttro = eOuttro;
            chanceResult1 = result1;
            chanceResult2 = result2;
            chanceResult3 = result3;
            chanceEffect1 = effect1;
            chanceEffect2 = effect2;
            chanceEffect3 = effect3;
            percentageChance1 = chance1;
            percentageChance2 = chance2;
            percentageChance3 = chance3;
            isChanceEncounter = isChance;
        }

        public Encounter Clone()
        {
            if (isCombatEncounter == true)
            {
                Encounter e = new Encounter(this.encounterName, this.encounterIntro, this.encounterOuttro, this.isCombatEncounter);
                return e;
            }
            else if (isChanceEncounter == true)
            {
                Encounter e = new Encounter(this.encounterName, this.encounterIntro, this.encounterOuttro, this.chanceResult1, this.chanceEffect1, this.percentageChance1, this.chanceResult2, this.chanceEffect2, this.percentageChance2, this.chanceResult3, this.chanceEffect3, this.percentageChance3, this.isChanceEncounter);
                return e;
            }
            else
            {
                Encounter e = new Encounter(this.encounterName, this.encounterIntro, this.encounterOuttro, this.result, this.resultStrength);
                return e;
            }
            
        }

        public void Initialize()
        {
            Random encRnd = new Random();
            encounterLevel = Program.player.level + encRnd.Next(0, 3) - 1;
            if (isCombatEncounter)
            {
                PopulateEncounter();
            }
        }

        void PopulateEncounter()
        {
            modEnemyList = new List<ModularEnemy>();
            Random rnd = new Random();
            for (int i = 1; i <= encounterLevel / 3 + 1; i++)
            {
                ModularEnemy m = new ModularEnemy();
                m.level = Program.player.level;
                m.setLevel();
                modEnemyList.Add(m);
            }
            foreach (ModularEnemy m in modEnemyList)
            {
                m.isAlive = true;
                m.currentHP = m.hp;
            }
        }

        public void RunModularCombatEncounter()
        {
            while (enemiesDefeated == false)
            {
                Console.WriteLine("Player: " + Program.player.currentHP + "/" + Program.player.hp);
                Console.WriteLine("Level: " + Program.player.level);
                Console.WriteLine("Weapon: " + Program.player.weapon.name + "(" + Program.player.weapon.dmgMin + "-" + Program.player.weapon.dmgMax +
                    "). Damage Modifier: " + Program.player.playerDamageMod);
                Console.WriteLine("-----------");
                Console.WriteLine("| Enemies |");
                Console.WriteLine("-----------");
                foreach (ModularEnemy e in modEnemyList)
                {
                    if (e.hp > 0)
                    {
                        Console.WriteLine(e.name + "(" + e.level + ") (" + e.weapon.name + "(" + (e.weapon.dmgMin + e.dmgMod) + "-"
                            + (e.weapon.dmgMax + e.dmgMod) + "(" + e.hitChance + "%)): " + e.currentHP + " /" + e.hp);
                        Console.WriteLine("---------------------");
                    }
                }
                enemyHP = 0;
                foreach (ModularEnemy e in modEnemyList)
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
                Program.player.turnComplete = false;
                Program.player.PlayerPreAction();
                enemyHP = 0;
                foreach (ModularEnemy e in modEnemyList)
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
                    ModularEnemy enm = modEnemyList[rnd2.Next(0, modEnemyList.Count)];
                    enm.turnComplete = false;
                    enm.EnemyAction();
                }

            }
        }

        public void RunChanceEncounter()
        {
            Random rnd = new Random();
            int roll = rnd.Next(1, 101);
            if (!String.IsNullOrEmpty(chanceEffect3))
            {
                if (roll <= percentageChance1)
                {
                    // Print string for option 1
                    // Activate event for option 1
                }
                else if (roll > percentageChance1 && roll <= percentageChance1+percentageChance2)
                {
                    // Print string for option 2
                    // Activate event for option 2
                }
                else if (roll > percentageChance1 + percentageChance2)
                {
                    // Print string for option 3
                    // Activate event for option 3
                }
            } else
            {
                if (roll <= percentageChance1)
                {
                    // Print string for option 1
                    // Activate event for option 1
                }
                else if (roll > percentageChance1)
                {
                    // Print string for option 2
                    // Activate event for option 2
                }
            }
        }

        public void RunEncounter()
        {
            if (result == "healing")
            {
                Program.player.TakeDamage(-resultStrength);
            }
            if (result == "upgrade")
            {
                List<string> choices = new List<string>();
                choices.Add("Damage +1");
                choices.Add("Critical Chance + 5");
                choices.Add("Critical Damage + 1");
                Console.WriteLine("Please select a stat to increase:\n");
                Program.menu = new Menu(choices, "Please select a stat to increase:\n");
                int menuOption = Program.menu.Run();
                switch (menuOption)
                {
                    case 0:
                        Program.player.weapon.dmgMin++;
                        Program.player.weapon.dmgMax++;
                        Program.player.weapon.level++;
                        Program.player.weapon.name = Program.player.weapon.baseName + " +" + Program.player.weapon.level;
                        break;
                    case 1:
                        Program.player.weapon.critChance += 5;
                        Program.player.weapon.level++;
                        Program.player.weapon.name = Program.player.weapon.baseName + " +" + Program.player.weapon.level;
                        break;
                    case 2:
                        Program.player.weapon.critMult++;
                        Program.player.weapon.level++;
                        Program.player.weapon.name = Program.player.weapon.baseName + " +" + Program.player.weapon.level;
                        break;
                }
            }
        }

        public void Run()
        {
            Initialize();
            Program.ut.TypeLine(encounterIntro);
            if (isCombatEncounter == true)
            {
                RunModularCombatEncounter();
                Program.ut.TypeLine(encounterOuttro);
                Program.ut.EnterToCont();
            }
            else if (isChanceEncounter == true)
            {
                RunChanceEncounter();
                Program.ut.TypeLine(encounterOuttro);
                Program.ut.EnterToCont();
            }
            else
            {
                RunEncounter();
                Program.ut.TypeLine(encounterOuttro);
                Program.ut.EnterToCont();
            }
            Program.encountersWon++;
        }
    }
}
