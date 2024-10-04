using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurnBasedSystem
{
    internal class Shadow : Fighter
    {
        public Shadow(int hp,int sp, int atk)
        {
            this.hp = hp;
            maxHp = hp;
            this.sp = sp;
            this.atk = atk;
        }
    }
}
