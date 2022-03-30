using System;
using System.Collections.Generic;

/*  Enemy class - extends Character class
 *  Contains all data and functionality for the enemies in the game.
 *  Probably needs some refactoring, some overlap with Character and Player might occur */

namespace ConsoleRPG
{
    public class Enemy : Character
    {
        int hands = 1;
        public Weapon[] wieldedWeapons;
        public int hpPerLvl = 1;
        public int dmgMod = 0;
        public int dmgModPerLevel = 1;
        public int hitChancePerLevel = 1;
        public int hitChance = 50;

        public Enemy(int ihp, string sname, int ihands, int iDmgMod, int iHitC)
        {
            baseHP = ihp;
            hp = ihp;
            name = sname;
            hands = ihands;
            dmgMod = iDmgMod;
            hitChance = iHitC;
            InitWeapons();
        }

        public Enemy Clone()
        {
            Enemy e = new Enemy(this.hp, this.name, this.hands, this.dmgMod, this.hitChance);
            e.InitWeapons();
            return e;
        }

        public void setLevel()
        {
            baseHP = hp + hpPerLvl * (level-1);
            hp = baseHP;
            dmgMod = dmgMod + dmgModPerLevel * (level - 1);
            hitChance = hitChance + hitChancePerLevel * (level - 1); 
        }

        void InitWeapons()
        {
            List<Weapon> enemyWeaponList = new List<Weapon>();
            enemyWeaponList.Add(new Weapon("Sword", 2, 5,5,2));
            enemyWeaponList.Add(new Weapon("Axe", 1, 8,5,2));
            enemyWeaponList.Add(new Weapon("Dagger", 3, 4,5,2));
            wieldedWeapons = new Weapon[hands];
            Random rnd = new Random();
            for (int i = 0; i < hands; i++)
            {
                wieldedWeapons[i] = enemyWeaponList[rnd.Next(0, enemyWeaponList.Count)];
            }
        }

        public override void Die()
        {
            Program.ut.TypeLine(name + " cries out in agony as it dies");
            Program.player.gainXP(250 * level);
            //Program.currentEncounter.enemyList.Remove(this);
            Program.monstersDefeated++;
            isAlive = false;
        }

        public void EnemyAction()
        {
            foreach (Weapon w in wieldedWeapons)
            {
                Random hitPct = new Random();
                Program.ut.TypeLine("The " + name + "(" + level + ") takes a swing at you with its " + w.name + "!");
                if (hitPct.Next(0, 101) <= Math.Clamp(hitChance + (hitChancePerLevel * (level - 1)), 0, 100))
                {
                    Random wpDamage = new Random();
                    int dmg = wpDamage.Next(w.dmgMin, w.dmgMax + 1) + dmgMod;
                    Program.ut.TypeLine("It hits you for " + dmg + " damage!");
                    Program.player.TakeDamage(dmg);
                }
                else
                {
                    Program.ut.TypeLine("It misses, leaving it wide open for you to retaliate!");
                }
            }
        }
    }
}
