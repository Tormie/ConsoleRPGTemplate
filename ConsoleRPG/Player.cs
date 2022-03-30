using System;
using System.Collections.Generic;

/*  Player class - extends Character class
 *  Contains all data and functionality for the player.
 *  Contains chosen class, weapon and leveling
 *  Probably needs some refactoring, some overlap with Character and Enemy might occur */

namespace ConsoleRPG
{
    public class Player : Character
    {
        public Weapon playerWeapon = null;
        public int playerDamageMod = 1;
        public int playerXP = 0;

        public Player(string pName)
        {
            name = pName;
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
            hp = characterClass.classBaseHP + hpMod * (level - 1);
            Random msg = new Random();
            Program.ut.TypeLine(Program.dl.levelUpMessages[msg.Next(0, Program.dl.levelUpMessages.Count)]);
            Program.ut.TypeLine("You have reached level " + level + ". Your HP has increased to " + hp + ".");
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
            Console.WriteLine(name + "! Please choose your race.");
            List<string> choices = new List<string>();
            foreach (Race p in Program.dl.raceList)
            {
                choices.Add(p.raceName);
                Console.WriteLine("Name " + p.raceName);
                Console.Write("Str: " + p.raceStrMod + " Agi: " + p.raceAgiMod + "\n");
                Console.Write("Con: " + p.raceConMod + " Int: " + p.raceIntMod + "\n");
                Console.WriteLine("---------------------");
            }
            string playerChoice = Program.ut.GetResponse("Please enter the name of the race you choose to be:", choices.ToArray()).ToLower();
            foreach (Race p in Program.dl.raceList)
            {
                if (p.raceName.ToLower() == playerChoice)
                {
                    characterRace = p;
                }
            }
            Program.ut.TypeLine("You chose to be a " + characterRace.raceName);
            Program.ut.EnterToCont();
        }

        public void ChooseClass()
        {
            System.Threading.Thread.Sleep(250);
            Console.Clear();
            Console.WriteLine(name + "! Please choose your class.");
            List<string> choices = new List<string>();
            foreach (Class p in Program.dl.classList)
            {
                choices.Add(p.className);
                Console.WriteLine("Name " + p.className);
                Console.WriteLine("HP " + p.classBaseHP);
                Console.Write("Str: " + p.classStrMod + " Agi: " + p.classAgiMod + "\n");
                Console.Write("Con: " + p.classConMod + " Int: " + p.classIntMod + "\n");
                Console.Write("Skills: ");
                foreach (Skill ps in p.skillList)
                {
                    Console.Write(ps.skillName+ "  ");
                }
                Console.WriteLine();
                Console.WriteLine("---------------------");
            }
            string playerChoice = Program.ut.GetResponse("Please enter the name of the class you choose to be:", choices.ToArray()).ToLower();
            foreach (Class p in Program.dl.classList)
            {
                if (p.className.ToLower() == playerChoice)
                {
                    characterClass = p;
                }
            }
            Program.ut.TypeLine("Congratulations! You are now a mighty " + characterClass.className);
            baseHP = characterClass.classBaseHP;
            hp = characterClass.classBaseHP;
            Program.ut.EnterToCont();
        }

        public void ChooseWeapon()
        {
            System.Threading.Thread.Sleep(250);
            Console.Clear();
            Console.WriteLine("Mighty "+characterClass.className + " "+ name + "! Please select your weapon of choice.");
            List<string> choices = new List<string>();
            foreach (Weapon w in Program.dl.playerWeaponList)
            {
                choices.Add(w.name);
                Console.WriteLine(w.name + " doing " + w.dmgMin + " to " + w.dmgMax + " damage. Crit Chance: "+w.critChance+" for "+w.critMult+" times damage.");
                Console.WriteLine("---------------------");
            }
            string playerChoice = Program.ut.GetResponse("Please enter the name of your weapon of choice:", choices.ToArray()).ToLower();
            foreach (Weapon w in Program.dl.playerWeaponList)
            {
                if (w.name.ToLower() == playerChoice)
                {
                    playerWeapon = w.Clone();
                }
            }
            Program.ut.TypeLine("You have chosen to wield a " + playerWeapon.name + ". It does " + playerWeapon.dmgMin + " to " + playerWeapon.dmgMax + " damage.");
            Program.ut.EnterToCont();
            Console.Clear();
        }

        public void PlayerAction()
        {
            Console.WriteLine("What do you do?");
            Console.WriteLine("(A)ttack with your weapon, use a (S)kill, view (C)haracter sheet or view (E)nemy character sheet.");
            string playerInput = Console.ReadLine();
            if (playerInput.ToLower() == "c")
            {
                ViewPlayerCharacterSheet();
            }
            if (playerInput.ToLower() == "e")
            {
                ViewEnemyCharacterSheet();
            }

            if (playerInput.ToLower() == "s")
            {
                PlayerUseSkill();
            }
            if (playerInput.ToLower() == "a")
            {
                PlayerAttack();
            }
        }

        public void PlayerUseSkill()
        {
            Enemy target = null;
            string playerInput;
            Console.WriteLine("Which skill do you want to use?(Type Back to return)");
        }

        public void PlayerAttack()
        {
            string playerInput;
            ModularEnemy target = null;
            bool bCrit = false;
            Random crit = new Random();
            if (crit.Next(1, 101) <= playerWeapon.critChance)
            {
                bCrit = true;
            }
            if (Program.currentEncounter.modEnemyList.Count > 1)
            {
                Console.WriteLine("On which enemy?(Choose the number)");
                foreach (ModularEnemy e in Program.currentEncounter.modEnemyList)
                {
                    if (e.hp > 0)
                    {
                        Console.WriteLine(Program.currentEncounter.modEnemyList.IndexOf(e) + " " + e.name + "(" + e.level + ") (" + e.wieldedWeapon.name + ")");
                    }
                }
                playerInput = Console.ReadLine();
                int iTarget = Int32.Parse(playerInput);
                target = Program.currentEncounter.modEnemyList[iTarget];
            }
            else
            {
                target = Program.currentEncounter.modEnemyList[0];
            }
            Console.Clear();
            Program.ut.TypeLine("You attack " + target.name + " with your " + playerWeapon.name);
            Random rnd = new Random();
            int damage;
            if (bCrit == true)
            {
                damage = (rnd.Next(playerWeapon.dmgMin, playerWeapon.dmgMax + 1) + meleeDmgMod) * playerWeapon.critMult;
                Program.ut.TypeLine("You score a critical hit, dealing " + damage + " points of damage to the " + target.name);
            }
            else
            {
                damage = rnd.Next(playerWeapon.dmgMin, playerWeapon.dmgMax + 1) + meleeDmgMod;
                Program.ut.TypeLine("You hit " + target.name + " for " + damage + " points of damage");
            }
            target.TakeDamage(damage);
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
                        Console.WriteLine(Program.currentEncounter.modEnemyList.IndexOf(e) + " " + e.name + "(" + e.level + ") (" + e.wieldedWeapon.name + ")");
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
        }
    }
}
