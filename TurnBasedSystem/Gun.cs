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

        void Shoot()
        {
            Bullets--;
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
                Thread.Sleep(250);
            }
        }

        public void Aim()
        {
            int startingBullets = Bullets;
            List<Shadow> activeShadows = GameLoop.activeShadows;
            ConsoleKey keyPressed;
            do
            {
                GameLoop.currentTarget = activeShadows[GameLoop.indexOfCurrentTarget];

                Console.Clear();
                GameLoop.ShowStatus();
                Console.WriteLine($"{GameLoop.fighterToMove.Name}'s Turn:");
                Console.WriteLine($"\nCurrently targetting: {GameLoop.currentTarget.Name}\n\n");
                Console.WriteLine("SPACEBAR: SHOOT");
                Console.WriteLine("C: BACK");
                keyPressed = Console.ReadKey(true).Key;
                switch (keyPressed)
                {
                    case ConsoleKey.RightArrow:
                        if (GameLoop.indexOfCurrentTarget == activeShadows.Count - 1)
                        {
                            GameLoop.indexOfCurrentTarget = 0;
                            break;
                        }
                        GameLoop.indexOfCurrentTarget++;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (GameLoop.indexOfCurrentTarget == 0)
                        {
                            GameLoop.indexOfCurrentTarget = activeShadows.Count - 1;
                            break;
                        }
                        GameLoop.indexOfCurrentTarget--;
                        break;
                    case ConsoleKey.Spacebar:
                        ((PartyMember)GameLoop.fighterToMove).Gun.Shoot();
                        break;
                    case ConsoleKey.C:
                        break;
                    default: break;
                }
            } while (keyPressed != ConsoleKey.C && Bullets > 0);

            //Either pressed back or run out of bullets
            if(Bullets < startingBullets)
            {
                GameLoop.isActionSelected = true;
            }

            return;
        }
    }
}
