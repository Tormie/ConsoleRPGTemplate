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
         *  Intelligence manages magic damage
         *  For now, we're using values 1-9 with 5 being average for a 0 bonus/malus */
        public int strength = 5;
        public int agility = 5;
        public int constitution = 5;
        public int intelligence = 5;
        public int meleeDmgMod;
        public int toHitMod;
        public int dodgeMod;
        public int hpMod;
        public int magicDmgMod;

        public Class characterClass;
        public Race characterRace;

        public int hp;
        public int baseHP;
        public string name;
        public bool isAlive = true;
        public int level = 1;

        public void SetStats()
        {
            strength += characterClass.classStrMod + characterRace.raceStrMod;
            agility += characterClass.classAgiMod + characterRace.raceAgiMod;
            constitution += characterClass.classConMod + characterRace.raceConMod;
            intelligence += characterClass.classIntMod + characterRace.raceIntMod;

            meleeDmgMod = strength - 5;
            toHitMod = agility - 5;
            dodgeMod = agility - 5;
            hpMod = constitution - 5 + characterClass.classHPPerLevel;
            magicDmgMod = intelligence - 5;
        }

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
