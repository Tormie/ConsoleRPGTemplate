using System;
namespace ConsoleRPG
{
    public class Skill
    {
        public string skillName;
        public int skillCooldown;
        public int coolDownTimer = 0;
        public string skillText;
        public string skillUseText;
        public int skillPower;
        public bool targetsAll = false;
        public bool targetsSelf = false;
        public string damageType = "";
        public int classID; //Integer for class identifier -- it was impossible to link both ways Class <> Skill
        // 1 is Warrior, 2 is Mage

        public Skill(string name, string text, string usetext, int cooldown, int power, int cID, bool allEnemies, bool selfTarget, string dType)
        {
            skillName = name;
            skillText = text;
            skillUseText = usetext;
            skillCooldown = cooldown;
            skillPower = power;
            classID = cID;
            targetsAll = allEnemies;
            targetsSelf = selfTarget;
            damageType = dType;
        }

        public Skill Clone()
        {
            Skill s = new Skill(this.skillName,this.skillText,this.skillUseText, this.skillCooldown, this.skillPower, this.classID, this.targetsAll, this.targetsSelf, this.damageType);
            return s;
        }


        public void UseSkill(Character target, Character instigator)
        {
            int damage = 0;
            // If UseSkill called by player.
            if (instigator == Program.player)
            {
                Console.WriteLine(instigator.name + " uses " + skillName + " on " + target.name);
                if (targetsSelf)
                    // If skill targets self
                {
                    switch (damageType)
                    {
                        case "block":
                            Program.ut.TypeLine(instigator.name + " " + skillUseText);
                            target.invulType = damageType;
                            target.invulDuration = skillPower;
                            target.isInvulnerable = true;
                            coolDownTimer = skillCooldown;
                            break;
                        case "hide":
                            Program.ut.TypeLine(instigator.name + " " + skillUseText);
                            target.invulType = damageType;
                            target.invulDuration = skillPower;
                            target.isInvulnerable = true;
                            coolDownTimer = skillCooldown;
                            break;
                        case "healing":
                            int healPower = instigator.magicDmgMod * skillPower;
                            healPower = Math.Clamp(healPower, 5, 100);
                            Program.ut.TypeLine(instigator.name + " " + skillUseText);
                            Program.ut.TypeLine("You are healed for " + healPower + " hit points.");
                            target.TakeDamage(-healPower);
                            if (target.hp > target.baseHP) { target.hp = target.baseHP; }
                            coolDownTimer = skillCooldown;
                            break;
                    }
                }
                else
                {
                    // If skill targets all enemies
                    if (targetsAll)
                    {
                        switch (damageType.ToLower())
                        {
                            case "standard":
                                Program.ut.TypeLine(instigator.name + " " + skillUseText);
                                Random wpnRnd = new Random();
                                int wpdmg = wpnRnd.Next(Program.player.weapon.dmgMin, Program.player.weapon.dmgMax + 1);
                                damage = skillPower * Math.Clamp(wpdmg + Program.player.meleeDmgMod, 1, 20);
                                foreach (ModularEnemy e in Program.currentEncounter.modEnemyList)
                                {
                                    Console.WriteLine(e.name + " takes " + damage + " damage from the " + skillName);
                                    e.TakeDamage(damage);
                                }
                                coolDownTimer = skillCooldown;
                                break;
                            case "magic":
                                Program.ut.TypeLine(instigator.name + " " + skillUseText);
                                damage = skillPower * Math.Clamp(Program.player.magicDmgMod, 1, 10);
                                foreach (ModularEnemy e in Program.currentEncounter.modEnemyList)
                                {
                                    Console.WriteLine(e.name + " takes " + damage + " damage from the " + skillName);
                                    e.TakeDamage(damage);
                                }
                                coolDownTimer = skillCooldown;
                                break;
                            case "stun":
                                Program.ut.TypeLine(instigator.name + " " + skillUseText);
                                foreach (ModularEnemy e in Program.currentEncounter.modEnemyList)
                                {
                                    Console.WriteLine(e.name + " is stunned for "+skillPower+ " turns.");
                                    e.stunDuration =skillPower;
                                    e.isStunned = true;
                                }
                                coolDownTimer = skillCooldown;
                                break;
                        }
                    } else
                    {
                        //If skill targets single enemy
                        switch (damageType.ToLower())
                        {
                            case "standard":
                                Program.ut.TypeLine(instigator.name + " " +skillUseText);
                                Random wpnRnd = new Random();
                                int wpdmg = wpnRnd.Next(Program.player.weapon.dmgMin, Program.player.weapon.dmgMax + 1);
                                damage = skillPower * Math.Clamp(wpdmg + Program.player.meleeDmgMod, 1, 20);
                                Console.WriteLine(target.name + " takes " + damage + " damage from the " + skillName);
                                target.TakeDamage(damage);
                                coolDownTimer = skillCooldown;
                                break;
                            case "magic":
                                Program.ut.TypeLine(instigator.name + " " + skillUseText);
                                damage = skillPower * Math.Clamp(Program.player.magicDmgMod, 1, 10);
                                Console.WriteLine(target.name + " takes " + damage + " damage from the " + skillName);
                                target.TakeDamage(damage);
                                coolDownTimer = skillCooldown;
                                break;
                            case "stun":
                                Program.ut.TypeLine(instigator.name + " " + skillUseText);
                                Console.WriteLine(target.name + " is stunned for " + skillPower + " turns.");
                                target.stunDuration = skillPower;
                                target.isStunned = true;
                                coolDownTimer = skillCooldown;
                                break;
                        }
                    }
                }
            }
            else
            {
                ModularEnemy instEnemy = (ModularEnemy)instigator;
                Console.WriteLine(instEnemy.name + " uses " + skillName + " on " + target.name);
                if (targetsSelf)
                // If skill targets self
                {
                    switch (damageType)
                    {
                        case "block":
                            Program.ut.TypeLine(instigator.name + " " + skillUseText);
                            instEnemy.invulType = damageType;
                            instEnemy.invulDuration = skillPower;
                            instEnemy.isInvulnerable = true;
                            coolDownTimer = skillCooldown;
                            break;
                        case "hide":
                            Program.ut.TypeLine(instigator.name + " " + skillUseText);
                            instEnemy.invulType = damageType;
                            instEnemy.invulDuration = skillPower;
                            instEnemy.isInvulnerable = true;
                            coolDownTimer = skillCooldown;
                            break;
                        case "healing":
                            int healPower = instEnemy.magicDmgMod * skillPower;
                            healPower = Math.Clamp(healPower, 5, 100);
                            Program.ut.TypeLine(instigator.name + " " + skillUseText);
                            Program.ut.TypeLine("You are healed for " + healPower + " hit points.");
                            instEnemy.TakeDamage(-healPower);
                            if (instEnemy.hp > instEnemy.baseHP) { instEnemy.hp = instEnemy.baseHP; }
                            coolDownTimer = skillCooldown;
                            break;
                    }
                }
                else
                {
                    // If skill targets enemy
                    switch (damageType.ToLower())
                    {
                        case "standard":
                            Program.ut.TypeLine(instigator.name + " " + skillUseText);
                            Random wpnRnd = new Random();
                            int wpdmg = wpnRnd.Next(instEnemy.wieldedWeapon.dmgMin, instEnemy.wieldedWeapon.dmgMax + 1);
                            damage = skillPower * Math.Clamp(wpdmg + instEnemy.meleeDmgMod, 1, 20);
                            Console.WriteLine(target.name + " takes " + damage + " damage from the " + skillName);
                            target.TakeDamage(damage);
                            coolDownTimer = skillCooldown;
                            break;
                        case "magic":
                            Program.ut.TypeLine(instigator.name + " " + skillUseText);
                            damage = skillPower * Math.Clamp(instEnemy.magicDmgMod, 1, 10);
                            Console.WriteLine(target.name + " takes " + damage + " damage from the " + skillName);
                            target.TakeDamage(damage);
                            coolDownTimer = skillCooldown;
                            break;
                        case "stun":
                            Program.ut.TypeLine(instigator.name + " " + skillUseText);
                            Console.WriteLine(target.name + " is stunned for " + skillPower + " turns.");
                            target.stunDuration = skillPower;
                            target.isStunned = true;
                            coolDownTimer = skillCooldown;
                            break;
                    } 
                }
            } 
        }
    }
}
