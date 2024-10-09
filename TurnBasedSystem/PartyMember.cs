using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurnBasedSystem
{
    internal class PartyMember : Fighter 
    {
        Gun gun;

        public PartyMember(string name, int hp, int sp, Melee melee, Gun gun): base(name,hp,sp, melee)
        {
            this.gun = gun;
        }

        public Gun Gun
        {
            get { return gun; }
            set { gun = value; }
        }
    }
}
