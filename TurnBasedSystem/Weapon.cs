using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TurnBasedSystem
{
    internal abstract class Weapon
    {
        protected Random rand = new Random();

        string name;
        int atk;
        int atkVarience;
        int hitChance;

        public Weapon(string name, int atk, int atkVarience, int hitChance)
        {
            this.name = name;
            this.atk = atk;
            this.atkVarience = atkVarience;
            this.hitChance = hitChance;
        }

        public string Name
        {
            get { return name; }
        }
        public int Atk
        {
            get { return atk; }
        }
        public int AtkVarience
        {
            get { return atkVarience; }
        }
        public int HitChance
        {
            get { return hitChance; }
        }

        public void Attack(Fighter target)
        {
            if (RollToHit())
            {
                int damageToDeal = RollAttack();
                if (target.IsGuarding)
                {
                    target.HP -= (int)Math.Ceiling(damageToDeal * 0.4);
                    target.IsGuarding = false;
                    return;
                }
                target.HP -= damageToDeal;
            }
            else
            {
                Console.WriteLine("Missed!");
                Thread.Sleep(2000);
            }
        }

        protected bool RollToHit()
        {
            if(hitChance >= rand.Next(1, 101)){
                return true;
            }
            return false;
        }

        protected int RollAttack()
        {
            int damageVarience = rand.Next(-atkVarience, atkVarience);
            int newAtk = atk + damageVarience;
            return newAtk;
        }
    }
}
