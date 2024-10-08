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
        public PartyMember(int hp, int sp, int atk, int atkVarience): base(hp,sp,atk,atkVarience){}
    }
}
