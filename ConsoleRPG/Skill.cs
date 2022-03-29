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
        public int classID; //Integer for class identifier -- it was impossible to link both ways Class <> Skill
        // 1 is Warrior, 2 is Mage

        public Skill(string name, string text, string usetext, int cooldown, int power, int cID, bool allEnemies)
        {
            skillName = name;
            skillText = text;
            skillUseText = usetext;
            skillCooldown = cooldown;
            skillPower = power;
            classID = cID;
            targetsAll = allEnemies;
        }

        public void UseSkill(Enemy target)
        {
            if (coolDownTimer >= skillCooldown)
            {
                int damage = skillPower * Program.player.playerDamageMod;
                Program.ut.TypeLine(skillUseText + target.name + ". Doing " + damage + " to it.");
                target.TakeDamage(damage);
                coolDownTimer = 0;
            }
            else
            {
                Program.ut.TypeLine("Cooldown for " + skillName + " has not yet run out. Please wait " + (skillCooldown - coolDownTimer) + " more turns.");
            }
        }
    }
}
