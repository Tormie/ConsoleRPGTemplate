using System;
using System.Collections.Generic;

namespace ConsoleRPG
{
    public class ModularEnemy : Character
    {
        //public Weapon wieldedWeapon;
        public int hpPerLvl = 1;
        public int dmgMod = 0;
        public int dmgModPerLevel = 1;
        public int hitChancePerLevel = 1;
        public int baseHitChance = 50;
        public int hitChance = 50;
        public int enemyHPmod = -50;
        public int xpMod = 0;
        public int xpValue = 1000;

        public ModularEnemy()
        {
            InitModularEnemy();
            baseHP -= enemyHPmod;
            SetStats();
            hpMod = hpMod / 2;
            hitChance = Math.Clamp((baseHitChance + hitChancePerLevel * (level - 1) + toHitMod), 0, 100);
            InitWeapon();
        }

        public void InitModularEnemy()
        {
            Random rnd = new Random();
            characterClass = Program.dl.classList[rnd.Next(0, Program.dl.classList.Count)].Clone();
            rnd = new Random();
            characterRace = Program.dl.raceList[rnd.Next(0, Program.dl.raceList.Count)].Clone();
            name = characterRace.raceName + " " + characterClass.className;
        }

        void SetXP()
        {
            if (hp > 50)
            {
                xpMod += 10;
            }
            if (dmgMod > 3)
            {
                xpMod += 10;
            }
            if (hitChance > 60)
            {
                xpMod += 10;
            }
            xpValue = xpValue + xpMod + (50 * level);
        }

        public void setLevel()
        {
            hp = hp + hpMod * (level - 1);
            currentHP = hp;
            dmgMod = meleeDmgMod + dmgModPerLevel * (level - 1);
            hitChance = Math.Clamp((baseHitChance + hitChancePerLevel * (level - 1) + toHitMod),0,100);
            SetXP();
        }

        void InitWeapon()
        {
            Random rnd = new Random();
            weapon = Program.dl.playerWeaponList[rnd.Next(0, Program.dl.playerWeaponList.Count)];
        }

        public override void Die()
        {
            Program.ut.TypeLine(name + " cries out in agony as it dies");
            Program.ut.TypeLine("You gain " + xpValue + " experience points.");
            Program.ut.EnterToCont();
            Program.player.gainXP(xpValue);
            Program.currentEncounter.modEnemyList.Remove(this);
            Program.monstersDefeated++;
            isAlive = false;
        }

        public void EnemyAction()
        {
            if (!isStunned)
            {
                Random useSkill = new Random();
                if (useSkill.Next(0, 101) > 25)
                {
                    EnemyAttack();
                }
                else
                {
                    EnemyUseSkill();
                }
            } else
            {
                Console.WriteLine(name + " is stunned and cannot take action this turn.");
            }
            if (turnComplete)
            {
                TurnManager();
            }
        }
        void EnemyAttack()
        {
            if (Program.player.isInvulnerable)
            {
                if (Program.player.invulType == "block")
                {
                    Program.ut.TypeLine("The enemy attack bounces off harmlessly. You take no damage.");
                }
                if (Program.player.invulType == "stealth")
                {
                    Program.ut.TypeLine("The enemy cannot seem to locate you, it spends its turn looking for you.");
                }
                turnComplete = true;
            }
            else
            {
                Random hitPct = new Random();
                Program.ut.TypeLine("The " + name + "(" + level + ") takes a swing at you with its " + weapon.name + "!");
                if (hitPct.Next(0, 101) <= Math.Clamp(hitChance + (hitChancePerLevel * (level - 1)), 0, 100))
                {
                    Random wpDamage = new Random();

                    int dmg = wpDamage.Next(weapon.dmgMin, weapon.dmgMax + 1) + dmgMod;
                    Program.ut.TypeLine("It hits you for " + dmg + " damage!");
                    Program.player.TakeDamage(dmg);
                }
                else
                {
                    Program.ut.TypeLine("It misses, leaving it wide open for you to retaliate!");
                }
                turnComplete = true;
            }
        }
        void EnemyUseSkill()
        {
            Random rndSkill = new Random();
            Skill usedSkill = characterClass.skillList[rndSkill.Next(0, characterClass.skillList.Count)];
            if (usedSkill.coolDownTimer <= 0)
            {
                if (usedSkill.targetsSelf)
                {
                    usedSkill.UseSkill(this, this);
                } else
                {
                    usedSkill.UseSkill(Program.player, this);
                }
                turnComplete = true;
            } else
            {
                Program.ut.TypeLine("The " + name + " tries to use " + usedSkill.skillName);
                Program.ut.TypeLine("It fails horribly, leaving it wide open to attack.");
                turnComplete = true;
            }
        }

    }
}
