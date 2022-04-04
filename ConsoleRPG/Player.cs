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
        int statBoostRate = 3;

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
            Random msg = new Random();
            Program.ut.TypeLine(Program.dl.levelUpMessages[msg.Next(0, Program.dl.levelUpMessages.Count)]);
            Program.ut.TypeLine("You have reached level " + level + ". Your HP has increased to " + hp + ".");
            if (level % statBoostRate == 0)
            {
                IncreaseStats();
            }
            SetStats();
            Program.ut.EnterToCont();
        }

        public void IncreaseStats()
        {
            List<string> choices = new List<string>();
            choices.Add("Strength");
            choices.Add("Agility");
            choices.Add("Constitution");
            choices.Add("Intelligence");
            string playerChoice = Program.ut.GetResponse("Please select a stat to increase:", choices.ToArray()).ToLower();
            switch (playerChoice.ToLower())
            {
                case "strength":
                    strLvlMod++;
                    break;
                case "agility":
                    agLvlMod++;
                    break;
                case "constitution":
                    conLvlMod++;
                    break;
                case "intelligence":
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
            Console.WriteLine(name + "! Please choose your race.");
            List<string> choices = new List<string>();
            foreach (Race p in Program.dl.raceList)
            {
                choices.Add(p.raceShort);
                Console.WriteLine("Name " + p.raceName);
                Console.Write("Str: " + p.raceStrMod + " Agi: " + p.raceAgiMod + "\n");
                Console.Write("Con: " + p.raceConMod + " Int: " + p.raceIntMod + "\n");
                Console.WriteLine("---------------------");
            }
            Program.menu = new Menu(choices);
            int menuOption = Program.menu.Run();
            characterRace = Program.dl.raceList[menuOption].Clone();
            //string playerChoice = Program.ut.GetResponse("Please enter the name of the race you choose to be:", choices.ToArray()).ToLower();
            //foreach (Race p in Program.dl.raceList)
            //{
            //    if (p.raceName.ToLower() == playerChoice)
            //    {
            //        characterRace = p.Clone();
            //    }
            //}
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
                choices.Add(p.classShort);
                Console.WriteLine("Name " + p.className);
                Console.WriteLine();
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
            Program.menu = new Menu(choices);
            int menuOption = Program.menu.Run();
            characterClass = Program.dl.classList[menuOption].Clone();
            //string playerChoice = Program.ut.GetResponse("Please enter the name of the class you choose to be:", choices.ToArray()).ToLower();
            //foreach (Class p in Program.dl.classList)
            //{
            //    if (p.className.ToLower() == playerChoice)
            //    {
            //        characterClass = p.Clone();
            //    }
            //}
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
                Console.WriteLine("What do you do?");
                Console.WriteLine("(A)ttack with your weapon(default action), use a (S)kill, view (C)haracter sheet or view (E)nemy character sheet.");
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
                if (playerInput.ToLower() == "a" || playerInput == "")
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
            choices.Add("Back");
            choices.Add("Kill");
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
                                                Console.WriteLine(Program.currentEncounter.modEnemyList.IndexOf(e) + " " + e.name + "(" + e.level + ") (" + e.wieldedWeapon.name + ")");
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
            Program.GameOver();
        }
    }
}
