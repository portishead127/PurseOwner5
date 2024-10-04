using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurnBasedSystem
{
    internal abstract class Fighter
    {
        protected int hp;
        protected int sp;
        protected int atk;
        protected int maxHp;

        public void Attack(Fighter target)
        {

        }

        public int HP { get; set; }
        public int MaxHp { get; set; }
        public int Sp { get; set; }
        public int Atk { get; set; }
    }
}
