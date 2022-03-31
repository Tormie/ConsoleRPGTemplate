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
            if (instigator == Program.player)
            {
                Console.WriteLine(instigator.name + " uses " + skillName + " on " + target.name);
                if (targetsSelf)
                {
                    switch (damageType)
                    {
                        case "block":
                            Program.ut.TypeLine(skillUseText);
                            target.invulType = damageType;
                            target.invulDuration = skillPower;
                            target.isInvulnerable = true;
                            coolDownTimer = skillCooldown;
                            break;
                        case "hide":
                            Program.ut.TypeLine(skillUseText);
                            target.invulType = damageType;
                            target.invulDuration = skillPower;
                            target.isInvulnerable = true;
                            coolDownTimer = skillCooldown;
                            break;
                        case "healing":
                            int healPower = instigator.magicDmgMod * 5;
                            Program.ut.TypeLine(skillUseText);
                            Program.ut.TypeLine("You are healed for " + healPower + " hit points.");
                            target.TakeDamage(-healPower);
                            if (target.hp > target.baseHP) { target.hp = target.baseHP; }
                            coolDownTimer = skillCooldown;
                            break;
                    }
                }
                
            }
            else
            {
                Console.WriteLine(instigator.name + " uses " + skillName + " on " + target.name);
                coolDownTimer = skillCooldown;
            } 
        }
    }
}
