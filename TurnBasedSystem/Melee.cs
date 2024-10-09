using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurnBasedSystem
{
    internal class Melee : Weapon
    { 
        public Melee(string name, int atk, int atkVarience, int hitChance): base(name,atk,atkVarience,hitChance){}
    }
}
