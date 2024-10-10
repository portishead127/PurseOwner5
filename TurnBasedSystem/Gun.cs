using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TurnBasedSystem
{
    internal class Gun: Weapon
    {
        int bullets;

        public Gun(string name, int atk, int atkVarience, int hitChance, int bullets): base(name,atk,atkVarience,hitChance)
        {
            this.bullets = bullets;
        }

        public int Bullets
        {
            get { return bullets; }
            set { bullets = Math.Max(0, value);}
        }

        public void Shoot(Fighter target)
        {
            Bullets--;
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
                Thread.Sleep(50);
            }
            else
            {
                Console.WriteLine("Missed!");
                Thread.Sleep(500);
            }
        }
    }
}
