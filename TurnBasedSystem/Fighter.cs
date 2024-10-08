using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurnBasedSystem
{
    internal abstract class Fighter
    {
        int hp;
        int sp;
        int atk;
        int maxHp;
        int maxSp;
        int atkVarience;
        bool isDead;

        public Fighter(int hp, int sp, int atk, int atkVarience)
        {
            this.hp = hp;
            this.sp = sp;
            this.atk = atk;
            maxHp = hp;
            maxSp = sp;
            this.atkVarience = atkVarience;
            isDead = false;
        }

        public int HP
        {
            get { return hp; }
            set { hp = value; }
        }
        public int MaxHP
        {
            get { return maxHp; }
            set { maxHp = value; }
        }
        public int Sp
        {
            get { return sp; }
            set { sp = value; }
        }
        public int MaxSP
        {
            get { return maxSp; }
            set { maxSp = value; }
        }
        public int Atk
        {
            get { return atk; }
        }
        public int AtkVarience
        {
            get { return atkVarience; }
        }
        public bool IsDead
        {
            get { return isDead; }
            set { isDead = value; }
        }

        public void Attack(Fighter target)
        {
            int damageToDeal = RollAttack();
            target.HP -= damageToDeal;
            if(target.HP <= 0)
            {
                target.IsDead = true;
            }
        }

        private int RollAttack()
        {
            Random rand = new Random();
            int damageVarience = rand.Next(-AtkVarience, AtkVarience);
            int newAtk = Atk + damageVarience;
            return newAtk;
        }
    }
}
