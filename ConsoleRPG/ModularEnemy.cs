using System;
using System.Collections.Generic;

namespace ConsoleRPG
{
    public class ModularEnemy : Character
    {
        public Weapon wieldedWeapon;
        public int hpPerLvl = 1;
        public int dmgMod = 0;
        public int dmgModPerLevel = 1;
        public int hitChancePerLevel = 1;
        public int hitChance = 50;
        public int enemyHPmod = -50;

        public ModularEnemy(int ihp, string sname, int iDmgMod, int iHitC)
        {
            baseHP = ihp;
            hp = ihp;
            name = sname;
            dmgMod = iDmgMod;
            hitChance = iHitC;
            InitWeapon();
        }

        public ModularEnemy Clone()
        {
            ModularEnemy e = new ModularEnemy(this.hp, this.name, this.dmgMod, this.hitChance);
            e.InitWeapon();
            return e;
        }

        public void setLevel()
        {
            baseHP = hp + hpPerLvl * (level - 1);
            hp = baseHP;
            dmgMod = dmgMod + dmgModPerLevel * (level - 1);
            hitChance = hitChance + hitChancePerLevel * (level - 1);
        }

        void InitWeapon()
        {
            Random rnd = new Random();
            wieldedWeapon = Program.dl.playerWeaponList[rnd.Next(0, Program.dl.playerWeaponList.Count)];
        }

        public override void Die()
        {
            Program.ut.TypeLine(name + " cries out in agony as it dies");
            Program.player.gainXP(1000 * level);
            //Program.currentEncounter.enemyList.Remove(this);
            Program.monstersDefeated++;
            isAlive = false;
        }

        public void EnemyAction()
        {
            Random hitPct = new Random();
            Program.ut.TypeLine("The " + name + "(" + level + ") takes a swing at you with its " + wieldedWeapon.name + "!");
            if (hitPct.Next(0, 101) <= Math.Clamp(hitChance + (hitChancePerLevel * (level - 1)), 0, 100))
            {
                Random wpDamage = new Random();
                int dmg = wpDamage.Next(wieldedWeapon.dmgMin, wieldedWeapon.dmgMax + 1) + dmgMod;
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
