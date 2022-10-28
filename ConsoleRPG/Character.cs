using System;

/*  Character class, base class for Enemies and Player
 *  Needs refactoring, a lot of overlap is still happening */

namespace ConsoleRPG
{
    public class Character
    {
        /*  Expanded stats system for player and enemies
         *  Strength manages melee damage
         *  Agility manages to hit and/or dodge
         *  Constitution manages hit points
         *  Intelligence manages magic damage
         *  For now, we're using integer values with 5 being average for a 0 bonus/malus */
        public int strength = 5;
        public int agility = 5;
        public int constitution = 5;
        public int intelligence = 5;
        public int meleeDmgMod;
        public int toHitMod;
        public int dodgeMod;
        public int hpMod;
        public int magicDmgMod;
        public int strLvlMod, agLvlMod, conLvlMod, intLvlMod = 0;

        /*  Status types for characters. 
         *  used to implement different skill effects */
        public bool isInvulnerable = false;
        public int invulDuration;
        public string invulType = "";
        public bool isStunned = false;
        public int stunDuration;

        /*  Some base stats */
        public Class characterClass;
        public Race characterRace;
        public bool turnComplete = false;
        public bool isEnemy = false;

        public int hp;
        public int currentHP;
        public int baseHP;
        public string name;
        public bool isAlive = true;
        public int level = 1;

        public Weapon weapon;

        /*  Initialize derivative stats based on base stats. */
        public void SetStats()
        {
            strength = 5 + characterClass.classStrMod + characterRace.raceStrMod + strLvlMod;
            agility = 5 + characterClass.classAgiMod + characterRace.raceAgiMod + agLvlMod;
            constitution = 5 + characterClass.classConMod + characterRace.raceConMod + conLvlMod;
            intelligence = 5 + characterClass.classIntMod + characterRace.raceIntMod + intLvlMod;
            meleeDmgMod = strength - 5;
            toHitMod = agility - 5;
            dodgeMod = agility - 5;
            hpMod = constitution - 5 + characterClass.classHPPerLevel;
            magicDmgMod = intelligence - 5;
            CalculateHP();
        }

        public void CalculateHP()
        {
            hp = baseHP + hpMod * level;
            currentHP = hp;
        }

        /*  Pretty straightforward if you ask me */
        public void TakeDamage(int damage)
        {
            currentHP -= damage;
            if (currentHP <= 0)
            {
                currentHP = 0;
                Die();
            }    
        }
        /*  Pretty straightforward if you ask me */
        public virtual void Die()
        {
            isAlive = false;
        }

        /*  Handles skill and status effect cooldowns */
        public void TurnManager()
        {
            Program.ut.DebugLine("Turnmanager run on "+name);
            foreach (Skill s in characterClass.skillList)
            {
                if (s.coolDownTimer > 0)
                {
                    s.coolDownTimer--;
                    if (s.coolDownTimer == 0)
                    {
                        Console.WriteLine("Skill " + s.skillName + " is no longer on cooldown");
                    }
                }
            }
            if (invulDuration > 0)
            {
                invulDuration--;
                if (invulDuration <= 0)
                {
                    Console.WriteLine(name + " is no longer invulnerable.");
                    isInvulnerable = false;
                }
            }
            if (stunDuration > 0)
            {
                stunDuration--;
                if (stunDuration <= 0)
                {
                    Console.WriteLine(name + " is no longer stunned.");
                    isStunned = false;
                }
            }
        }

    }
}
