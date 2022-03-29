using System;
namespace ConsoleRPG
{
    public class Character
    {
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
