﻿using System;
using System.Collections.Generic;

/*  Player class - extends Character class
 *  Contains all data and functionality for the player.
 *  Contains chosen class, weapon and leveling
 *  Probably needs some refactoring, some overlap with Character and Enemy might occur */

namespace ConsoleRPG
{
    public class Player : Character
    {
        //public Weapon playerWeapon = null;
        public int playerDamageMod = 1;
        public int playerXP = 0;
        int statBoostRate = 3;

        public Player(string pName)
        {
            name = pName;
            isEnemy = false;
        }

        public void gainXP(int gainedXP)
        {
            playerXP += gainedXP;
            if (playerXP >= 1000 + 1000 * (level - 1))
            {
                LevelUp();
            }
        }

        public void LevelUp()
        {
            Console.Clear();
            level++;
            Random msg = new Random();
            string bigMessage = "";
            bigMessage += (Program.dl.levelUpMessages[msg.Next(0, Program.dl.levelUpMessages.Count)]);
            Program.ut.TypeLine(Program.dl.levelUpMessages[msg.Next(0, Program.dl.levelUpMessages.Count)]);
            if (level % statBoostRate == 0)
            {
                IncreaseStats(bigMessage);
            }
            SetStats();
            Program.ut.TypeLine("You have reached level " + level + ". Your HP has increased to " + hp + ".");
            Program.ut.EnterToCont();
        }

        public void IncreaseStats(string message)
        {
            List<string> choices = new List<string>();
            choices.Add("Strength");
            choices.Add("Agility");
            choices.Add("Constitution");
            choices.Add("Intelligence");
            message += "Please select a stat to increase:\n";
            Console.WriteLine("Please select a stat to increase:\n");
            Program.menu = new Menu(choices, message);
            int menuOption = Program.menu.Run();
            switch (menuOption)
            {
                case 0:
                    strLvlMod++;
                    break;
                case 1:
                    agLvlMod++;
                    break;
                case 2:
                    conLvlMod++;
                    break;
                case 3:
                    intLvlMod++;
                    break;
            }
            SetStats();
        }

        public void InitPlayer()
        {
            ChooseRace();
            ChooseClass();
            ChooseWeapon();
            SetStats();
        }

        public void ChooseRace()
        {
            System.Threading.Thread.Sleep(250);
            Console.Clear();
            List<string> choices = new List<string>();
            List<Race> tempRaceList = Program.dl.raceList;
            string hugeString = "";
            hugeString += name + "! Please choose your race.\n";
            int tempLength = tempRaceList.Count;
            for (int i = 0; i <= tempLength -1; i+=3)
            {
                if ((tempLength - i) / 3 >= 1)
                {
                    hugeString += Program.ut.ReturnHorizontalLine();
                    hugeString += (Program.ut.expandString(tempRaceList[i].raceName, 24) + "|" + Program.ut.expandString(tempRaceList[i + 1].raceName, 24) + "|" + Program.ut.expandString(tempRaceList[i + 2].raceName, 24) + "\n");
                    hugeString += (Program.ut.expandString("str: "+tempRaceList[i].raceStrMod + " agi: " + tempRaceList[i].raceAgiMod, 24) + "|" + Program.ut.expandString("str: " + tempRaceList[i + 1].raceStrMod + " agi: " + tempRaceList[i + 1].raceAgiMod, 24) + "|" + Program.ut.expandString("str: " + tempRaceList[i + 2].raceStrMod + " agi: " + tempRaceList[i + 2].raceAgiMod, 24) + "\n");
                    hugeString += (Program.ut.expandString("con: " + tempRaceList[i].raceConMod + " int: " + tempRaceList[i].raceIntMod, 24) + "|" + Program.ut.expandString("con: " + tempRaceList[i + 1].raceConMod + " int: " + tempRaceList[i + 1].raceIntMod, 24) + "|" + Program.ut.expandString("con: " + tempRaceList[i + 2].raceConMod + " int: " + tempRaceList[i + 2].raceIntMod, 24) + "\n");

                }
                else if ((tempLength - i) % 3 == 2)
                {
                    hugeString += Program.ut.ReturnHorizontalLine();
                    hugeString += (Program.ut.expandString(tempRaceList[i].raceName, 24) + "|" + Program.ut.expandString(tempRaceList[i + 1].raceName, 24) + "|" + "\n");
                    hugeString += (Program.ut.expandString("str: " + tempRaceList[i].raceStrMod + " agi: " + tempRaceList[i].raceAgiMod, 24) + "|" + Program.ut.expandString("str: " + tempRaceList[i + 1].raceStrMod + " agi: " + tempRaceList[i + 1].raceAgiMod, 24) + "|" + "\n");
                    hugeString += (Program.ut.expandString("con: " + tempRaceList[i].raceConMod + " int: " + tempRaceList[i].raceIntMod, 24) + "|" + Program.ut.expandString("con: " + tempRaceList[i + 1].raceConMod + " int: " + tempRaceList[i + 1].raceIntMod, 24) + "|" + "\n");

                }
                else if ((tempLength - i) % 3 == 1)
                {
                    hugeString += Program.ut.ReturnHorizontalLine();
                    hugeString += (Program.ut.expandString(tempRaceList[i].raceName, 24) + "|" + "\n");
                    hugeString += (Program.ut.expandString("str: " + tempRaceList[i].raceStrMod + " agi: " + tempRaceList[i].raceAgiMod, 24) + "|" + "\n");
                    hugeString += (Program.ut.expandString("con: " + tempRaceList[i].raceConMod + " int: " + tempRaceList[i].raceIntMod, 24) + "|" + "\n");
                }
            }
            hugeString += Program.ut.ReturnHorizontalLine();
            hugeString += "\n";
            Console.Write(hugeString);
            foreach (Race p in Program.dl.raceList)
            {
                choices.Add(p.raceName);
            }
            Program.menu = new Menu(choices, hugeString);
            int menuOption = Program.menu.Run();
            characterRace = Program.dl.raceList[menuOption].Clone();
            Program.ut.TypeLine("You chose to be a " + characterRace.raceName);
            Program.ut.EnterToCont();
        }

        public void ChooseClass()
        {
            System.Threading.Thread.Sleep(250);
            Console.Clear();
            List<Class> tempClassList = Program.dl.classList;
            string hugeString = "";
            hugeString += name + "! Please choose your class.\n";
            List<string> choices = new List<string>();
            int tempLength = tempClassList.Count;

            for (int i = 0; i <= tempLength - 1; i += 3)
            {
                if ((tempLength - i) / 3 >= 1)
                {
                    hugeString += Program.ut.ReturnHorizontalLine();
                    hugeString += (Program.ut.expandString(tempClassList[i].className, 24) + "|" + Program.ut.expandString(tempClassList[i+1].className, 24) + "|" + Program.ut.expandString(tempClassList[i+2].className, 24) + "\n");
                    hugeString += (Program.ut.expandString("HP: "+ tempClassList[i].classBaseHP + "("+ tempClassList[i].classHPPerLevel + "/lvl)", 24) + "|" + Program.ut.expandString("HP: " + tempClassList[i+1].classBaseHP + "(" + tempClassList[i+1].classHPPerLevel + "/lvl)", 24) + "|" + Program.ut.expandString("HP: " + tempClassList[i+2].classBaseHP + "(" + tempClassList[i+2].classHPPerLevel + "/lvl)", 24) + "\n");
                    hugeString += (Program.ut.expandString("str: " + tempClassList[i].classStrMod + " agi: " + tempClassList[i].classAgiMod, 24) + "|" + Program.ut.expandString("str: " + tempClassList[i + 1].classStrMod + " agi: " + tempClassList[i + 1].classAgiMod, 24) + "|" + Program.ut.expandString("str: " + tempClassList[i + 2].classStrMod + " agi: " + tempClassList[i + 2].classAgiMod, 24) + "\n");
                    hugeString += (Program.ut.expandString("con: " + tempClassList[i].classConMod + " int: " + tempClassList[i].classIntMod, 24) + "|" + Program.ut.expandString("con: " + tempClassList[i + 1].classConMod + " int: " + tempClassList[i + 1].classIntMod, 24) + "|" + Program.ut.expandString("con: " + tempClassList[i + 2].classConMod + " int: " + tempClassList[i + 2].classIntMod, 24) + "\n");
                    hugeString += (Program.ut.expandString("Skills: " + tempClassList[i].skillList[0].skillName, 24) + "|" + Program.ut.expandString("Skills: " + tempClassList[i + 1].skillList[0].skillName, 24) + "|" + Program.ut.expandString("Skills: " + tempClassList[i + 2].skillList[0].skillName, 24) + "\n");
                    hugeString += (Program.ut.expandString("        " + tempClassList[i].skillList[1].skillName, 24) + "|" + Program.ut.expandString("        " + tempClassList[i + 1].skillList[1].skillName, 24) + "|" + Program.ut.expandString("        " + tempClassList[i + 2].skillList[1].skillName, 24) + "\n");

                }
                else if ((tempLength - i) % 3 == 2)
                {
                    hugeString += Program.ut.ReturnHorizontalLine();
                    hugeString += (Program.ut.expandString(tempClassList[i].className, 24) + "|" + Program.ut.expandString(tempClassList[i+1].className, 24) + "|" + "\n");
                    hugeString += (Program.ut.expandString("HP: " + tempClassList[i].classBaseHP + "(" + tempClassList[i].classHPPerLevel + "/lvl)", 24) + "|" + Program.ut.expandString("HP: " + tempClassList[i + 1].classBaseHP + "(" + tempClassList[i + 1].classHPPerLevel + "/lvl)", 24) + "|" + "\n");
                    hugeString += (Program.ut.expandString("str: " + tempClassList[i].classStrMod + " agi: " + tempClassList[i].classAgiMod, 24) + "|" + Program.ut.expandString("str: " + tempClassList[i + 1].classStrMod + " agi: " + tempClassList[i + 1].classAgiMod, 24) + "|" + "\n");
                    hugeString += (Program.ut.expandString("con: " + tempClassList[i].classConMod + " int: " + tempClassList[i].classIntMod, 24) + "|" + Program.ut.expandString("con: " + tempClassList[i + 1].classConMod + " int: " + tempClassList[i + 1].classIntMod, 24) + "|" + "\n");
                    hugeString += (Program.ut.expandString("Skills: " + tempClassList[i].skillList[0].skillName, 24) + "|" + Program.ut.expandString("Skills: " + tempClassList[i + 1].skillList[0].skillName, 24) + "|" + "\n");
                    hugeString += (Program.ut.expandString("        " + tempClassList[i].skillList[1].skillName, 24) + "|" + Program.ut.expandString("        " + tempClassList[i + 1].skillList[1].skillName, 24) + "|" + "\n");
                }
                else if ((tempLength - i) % 3 == 1)
                {
                    hugeString += Program.ut.ReturnHorizontalLine();
                    hugeString += (Program.ut.expandString(tempClassList[i].className, 24) + "|" + "\n");
                    hugeString += (Program.ut.expandString("HP: " + tempClassList[i].classBaseHP + "(" + tempClassList[i].classHPPerLevel + "/lvl)", 24) + "|" + "\n");
                    hugeString += (Program.ut.expandString("str: " + tempClassList[i].classStrMod + " agi: " + tempClassList[i].classAgiMod, 24) + "|" + "\n");
                    hugeString += (Program.ut.expandString("con: " + tempClassList[i].classConMod + " int: " + tempClassList[i].classIntMod, 24) + "|" + "\n");
                    hugeString += (Program.ut.expandString("Skills: " + tempClassList[i].skillList[0].skillName, 24) + "|" + "\n");
                    hugeString += (Program.ut.expandString("        " + tempClassList[i].skillList[1].skillName, 24) + "|" + "\n");

                }
            }
            hugeString += Program.ut.ReturnHorizontalLine();
            hugeString += "\n";
            Console.Write(hugeString);

            foreach (Class p in Program.dl.classList)
            {
                choices.Add(p.className);
            }
            Program.menu = new Menu(choices, hugeString);
            int menuOption = Program.menu.Run();
            characterClass = Program.dl.classList[menuOption].Clone();
            Program.ut.TypeLine("Congratulations! You are now a mighty " + characterClass.className);
            baseHP = characterClass.classBaseHP;
            SetStats();
            Program.ut.EnterToCont();
        }

        public void ChooseWeapon()
        {
            System.Threading.Thread.Sleep(250);
            Console.Clear();
            List<Weapon> tempWeaponList = Program.dl.playerWeaponList;
            string hugeString = "";
            hugeString += "Mighty " + characterClass.className + " " + name + "! Please select your weapon of choice.\n";
            List<string> choices = new List<string>();
            int tempLength = tempWeaponList.Count;

            for (int i = 0; i <= tempLength - 1; i += 3)
            {
                if ((tempLength - i) / 3 >= 1)
                {
                    hugeString += Program.ut.ReturnHorizontalLine();
                    hugeString += (Program.ut.expandString(tempWeaponList[i].name, 24) + "|" + Program.ut.expandString(tempWeaponList[i + 1].name, 24) + "|" + Program.ut.expandString(tempWeaponList[i + 2].name, 24) + "\n");
                    hugeString += (Program.ut.expandString("Damage: " + tempWeaponList[i].dmgMin + "-" + tempWeaponList[i].dmgMax, 24) + "|" + Program.ut.expandString("Damage: " + tempWeaponList[i+1].dmgMin + "-" + tempWeaponList[i+1].dmgMax, 24) + "|" + Program.ut.expandString("Damage: " + tempWeaponList[i+2].dmgMin + "-" + tempWeaponList[i+2].dmgMax, 24) + "\n");
                    hugeString += (Program.ut.expandString("Critical: " + tempWeaponList[i].critChance + "% dmg x" + tempWeaponList[i].critMult, 24) + "|" + Program.ut.expandString("Critical: " + tempWeaponList[i+1].critChance + "% dmg x" + tempWeaponList[i+1].critMult, 24) + "|" + Program.ut.expandString("Critical: " + tempWeaponList[i+2].critChance + "% dmg x" + tempWeaponList[i+2].critMult, 24) + "\n");

                }
                else if ((tempLength - i) % 3 == 2)
                {
                    hugeString += Program.ut.ReturnHorizontalLine();
                    hugeString += (Program.ut.expandString(tempWeaponList[i].name, 24) + "|" + Program.ut.expandString(tempWeaponList[i + 1].name, 24) + "|" + "\n");
                    hugeString += (Program.ut.expandString("Damage: " + tempWeaponList[i].dmgMin + "-" + tempWeaponList[i].dmgMax, 24) + "|" + Program.ut.expandString("Damage: " + tempWeaponList[i + 1].dmgMin + "-" + tempWeaponList[i + 1].dmgMax, 24) + "|" + "\n");
                    hugeString += (Program.ut.expandString("Critical: " + tempWeaponList[i].critChance + "% dmg x" + tempWeaponList[i].critMult, 24) + "|" + Program.ut.expandString("Critical: " + tempWeaponList[i + 1].critChance + "% dmg x" + tempWeaponList[i + 1].critMult, 24) + "|" + "\n");

                }
                else if ((tempLength - i) % 3 == 1)
                {
                    hugeString += Program.ut.ReturnHorizontalLine();
                    hugeString += (Program.ut.expandString(tempWeaponList[i].name, 24) + "|" + "\n");
                    hugeString += (Program.ut.expandString("Damage: " + tempWeaponList[i].dmgMin + "-" + tempWeaponList[i].dmgMax, 24) + "|" + "\n");
                    hugeString += (Program.ut.expandString("Critical: " + tempWeaponList[i].critChance + "% dmg x" + tempWeaponList[i].critMult, 24) + "|" + "\n");
                }
            }
            hugeString += Program.ut.ReturnHorizontalLine();
            hugeString += "\n";
            Console.Write(hugeString);
            foreach (Weapon w in Program.dl.playerWeaponList)
            {
                choices.Add(w.name);
            }
            Program.menu = new Menu(choices, hugeString);
            int menuOption = Program.menu.Run();
            weapon = Program.dl.playerWeaponList[menuOption].Clone();
            Program.ut.TypeLine("You have chosen to wield a " + weapon.name + ". It does " + weapon.dmgMin + " to " + weapon.dmgMax + " damage.");
            Program.ut.EnterToCont();
            Console.Clear();
        }

        public void PlayerPreAction()
        {
            PlayerAction();
            if (turnComplete)
            {
                TurnManager();
            }
        }

        public void PlayerAction()
        {
            if (!isStunned)
            {
                //Console.WriteLine("What do you do?");
                List<string> choices = new List<string>();
                choices.Add("Attack with your weapon(default action)");
                choices.Add("Use a skill");
                choices.Add("View character sheet");
                choices.Add("View enemy character sheet");
                Program.menu = new Menu(choices, "What do you do?\n");
                int menuOption = Program.menu.Run();
                //Console.WriteLine("(A)ttack with your weapon(default action), use a (S)kill, view (C)haracter sheet or view (E)nemy character sheet.");
                //string playerInput = Console.ReadLine();
                //if (playerInput.ToLower() == "c")
                if (menuOption == 2)
                {
                    ViewPlayerCharacterSheet();
                }
                //if (playerInput.ToLower() == "e")
                if (menuOption == 3)
                {
                    ViewEnemyCharacterSheet();
                }
                //if (playerInput.ToLower() == "s")
                if (menuOption == 1)
                {
                    PlayerUseSkill();
                }
                //if (playerInput.ToLower() == "a" || playerInput == "")
                if (menuOption == 0)
                {
                    PlayerAttack();
                }
            } else
            {
                Program.ut.TypeLine("You are stunned and cannot take action this turn. You can choose to view (C)haracter sheet or view (E)nemy character sheet.");
                Program.ut.TypeLine("Press enter to end your turn.");
                string playerInput = Console.ReadLine();
                if (playerInput.ToLower() == "c")
                {
                    ViewPlayerCharacterSheet();
                }
                if (playerInput.ToLower() == "e")
                {
                    ViewEnemyCharacterSheet();
                }
                turnComplete = true;
            }
        }

        public void PlayerUseSkill()
        {
            ModularEnemy target = null;
            Console.WriteLine("Which skill do you want to use?(Type Back to return)");
            List<string> choices = new List<string>();
            foreach (Skill s in characterClass.skillList)
            {
                // List possible skill choices
                choices.Add(s.skillName);
                Console.WriteLine(s.skillName + "(" + s.damageType + ")");
                Console.WriteLine("Cooldown: " + s.skillCooldown + " Power: " + s.skillPower);
                if (s.targetsSelf) { Console.Write("Targets self. | "); }
                else { Console.Write("Targets other. | "); }
                if (s.targetsAll) { Console.WriteLine("All targets."); }
                else { Console.WriteLine("Single target."); }
            }
            string playerChoice = Program.ut.GetResponse("Which skill do you want to use?(Type Back to return).", choices.ToArray()).ToLower();
            if (playerChoice.ToLower() == "back")
            {
                // Revert to player action choice
                PlayerAction();
            }
            else
            {
                foreach (Skill sk in characterClass.skillList)
                    // Select skill that has been chosen
                {
                    Console.WriteLine(sk.skillName);
                    Console.WriteLine(sk.targetsSelf);
                    if (sk.skillName.ToLower() == playerChoice.ToLower())
                    {
                        if (sk.coolDownTimer > 0)
                        {
                            // Do not use skill if cooldown has not expired
                            Program.ut.TypeLine("Cooldown for " + sk.skillName + " has not expired, please wait " + sk.coolDownTimer + " more turns.");
                            PlayerAction();
                            break;
                        }
                        else
                        {
                            if (sk.targetsSelf)
                            {
                                // Use skill on self
                                sk.UseSkill(this, this);
                                turnComplete = true;
                            }
                            if (!sk.targetsSelf)
                            {
                                if (sk.targetsAll)
                                {
                                    // Use skill on all enemies
                                    sk.UseSkill(Program.currentEncounter.modEnemyList[0], this);
                                    turnComplete = true;
                                }
                                else
                                {
                                    if (Program.currentEncounter.modEnemyList.Count == 1)
                                    {
                                        // If a single enemy, set target to that one
                                        target = Program.currentEncounter.modEnemyList[0];
                                    }
                                    else
                                    {
                                        // If multiple enemies, allow player to select target
                                        Console.WriteLine("On which enemy?(Choose the number)");
                                        foreach (ModularEnemy e in Program.currentEncounter.modEnemyList)
                                        {
                                            if (e.hp > 0)
                                            {
                                                Console.WriteLine(Program.currentEncounter.modEnemyList.IndexOf(e) + " " + e.name + "(" + e.level + ") (" + e.weapon.name + ")");
                                            }
                                        }
                                        string playerInput = Console.ReadLine();
                                        int iTarget = Int32.Parse(playerInput);
                                        target = Program.currentEncounter.modEnemyList[iTarget];
                                    }
                                    // Use skill on set target
                                    sk.UseSkill(target, this);
                                    turnComplete = true;
                                }
                            }
                            break;
                        }
                    }

                }
            }
        }

        public void PlayerAttack()
        {
            //string playerInput;
            ModularEnemy target = null;
            bool bCrit = false;
            Random crit = new Random();
            if (crit.Next(1, 101) <= weapon.critChance)
            {
                bCrit = true;
            }
            if (Program.currentEncounter.modEnemyList.Count > 1)
            {
                List<string> choices = new List<string>();
                foreach (ModularEnemy e in Program.currentEncounter.modEnemyList)
                {
                    if (e.hp > 0)
                    {
                        choices.Add(Program.currentEncounter.modEnemyList.IndexOf(e) + " " + e.name + "(" + e.level + ") (" + e.weapon.name + ")");
                    }
                }
                Program.menu = new Menu(choices, "Attack which enemy?\n");
                int menuOption = Program.menu.Run();
                for (int i = 0; i < choices.Count; i++)
                {
                    if (i == menuOption)
                    {
                        target = Program.currentEncounter.modEnemyList[i];
                    }
                }


                //Console.WriteLine("On which enemy?(Choose the number)");
                //foreach (ModularEnemy e in Program.currentEncounter.modEnemyList)
                //{
                //    if (e.hp > 0)
                //    {
                //        Console.WriteLine(Program.currentEncounter.modEnemyList.IndexOf(e) + " " + e.name + "(" + e.level + ") (" + e.weapon.name + ")");
                //    }
                //}
                //playerInput = Console.ReadLine();
                //int iTarget = Int32.Parse(playerInput);
                //target = Program.currentEncounter.modEnemyList[iTarget];
            }
            else
            {
                target = Program.currentEncounter.modEnemyList[0];
            }
            Console.Clear();

            if (target.isInvulnerable)
            {
                if (target.invulType == "block")
                {
                    Program.ut.TypeLine("Your attack bounces off harmlessly. You deal no damage to the enemy.");
                }
                if (target.invulType == "hide")
                {
                    Program.ut.TypeLine("You cannot seem to locate the enemy. You spend your turn looking for it.");
                }
            }
            else
            {
                Program.ut.TypeLine("You attack " + target.name + " with your " + weapon.name);
                Random rnd = new Random();
                int damage;
                if (bCrit == true)
                {
                    damage = (rnd.Next(weapon.dmgMin, weapon.dmgMax + 1) + meleeDmgMod) * weapon.critMult;
                    Program.ut.TypeLine("You score a critical hit, dealing " + damage + " points of damage to the " + target.name);
                }
                else
                {
                    damage = rnd.Next(weapon.dmgMin, weapon.dmgMax + 1) + meleeDmgMod;
                    Program.ut.TypeLine("You hit " + target.name + " for " + damage + " points of damage");
                }
                target.TakeDamage(damage);
            }
            turnComplete = true;
        }

        public void ViewPlayerCharacterSheet()
        {
            Console.Clear();
            Program.ut.PrintCharacterSheet(Program.player);
            Program.ut.EnterToCont();
            Console.Clear();
            PlayerAction();
        }

        public void ViewEnemyCharacterSheet()
        {
            string playerInput;
            ModularEnemy target;
            if (Program.currentEncounter.modEnemyList.Count == 1)
            {
                Program.ut.PrintCharacterSheet(Program.currentEncounter.modEnemyList[0]);
                Program.ut.EnterToCont();
                Console.Clear();
                PlayerAction();
            }
            else
            {
                Console.WriteLine("Character sheet for which enemy?(Choose the number)");
                foreach (ModularEnemy e in Program.currentEncounter.modEnemyList)
                {
                    if (e.hp > 0)
                    {
                        Console.WriteLine(Program.currentEncounter.modEnemyList.IndexOf(e) + " " + e.name + "(" + e.level + ") (" + e.weapon.name + ")");
                    }
                }
                playerInput = Console.ReadLine();
                int iTarget = Int32.Parse(playerInput);
                target = Program.currentEncounter.modEnemyList[iTarget];
                Program.ut.PrintCharacterSheet(target);
                Program.ut.EnterToCont();
                Console.Clear();
                PlayerAction();
            }
        }

        public override void Die()
        {
            Program.ut.TypeLine("Suffering the ultimate defeat, your final breath escapes you.");
            isAlive = false;
            Program.GameOver();
        }
    }
}
