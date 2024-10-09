using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurnBasedSystem
{
    internal abstract class Fighter
    {
        string name;
        int hp;
        int sp;
        int maxHp;
        int maxSp;
        Melee melee;

        bool isDead;
        bool isGuarding;

        static Random rand = new Random();


        public Fighter(string name, int hp, int sp, Melee melee)
        {
            this.name = name;
            this.hp = hp;
            this.sp = sp;
            maxHp = hp;
            maxSp = sp;
            this.melee = melee;

            isDead = false;
            isGuarding = false;
        }

        public string Name
        {
            get { return name; }
        }
        public int HP
        {
            get { return hp; }
            set
            {
                hp = Math.Max(0, value);
                if(hp == 0)
                {
                    isDead = true;
                }
            }
        }
        public int MaxHP
        {
            get { return maxHp; }
            set { maxHp = value; }
        }
        public int SP
        {
            get { return sp; }
            set { sp = value; }
        }
        public int MaxSP
        {
            get { return maxSp; }
            set { maxSp = value; }
        }
        public Melee Melee
        {
            get { return melee; }
            set { melee = value; }
        }

        public bool IsDead
        {
            get { return isDead; }
            set { isDead = value; }
        }
        public bool IsGuarding
        {
            get { return isGuarding; }
            set { isGuarding = value;} 
        }
    }
}
