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

        public void Attack()
        {
            if (RollToHit())
            {
                int damageToDeal = RollAttack();
                if (GameLoop.currentTarget.IsGuarding)
                {
                    GameLoop.currentTarget.HP -= (int)Math.Ceiling(damageToDeal * 0.4);
                    GameLoop.currentTarget.IsGuarding = false;
                    return;
                }
                GameLoop.currentTarget.HP -= damageToDeal;
            }
            else
            {
                Console.WriteLine("Missed!");
                Thread.Sleep(1000);
            }
            GameLoop.isActionSelected = true;
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
