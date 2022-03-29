using System;

/*  Character class, base class for Enemies and Player
 *  Needs refactoring, a lot of overlap is still happening */

namespace ConsoleRPG
{
    public class Character
    {
        /*  Groundwork for expanded stats system for player and enemies
         *  Strength manages melee damage
         *  Agility manages to hit and/or dodge
         *  Constitution manages hit points
         *  Intelligence manages magic damage   */
        public int strength;
        public int agility;
        public int constitution;
        public int intelligence;
        public int meleeDmgMod;
        public int toHitMod;
        public int dodgeMod;
        public int hpMod;
        public int magicDmgMod;


        public int hp;
        public int baseHP;
        public string name;
        public bool isAlive = true;
        public int level = 1;

        public void TakeDamage(int damage)
        {
            hp -= damage;
            if (hp <= 0)
            {
                hp = 0;
                Die();
            }    
        }

        public virtual void Die()
        {
            isAlive = false;
        }

    }
}
