﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurnBasedSystem
{
    internal class Shadow : Fighter
    {
        public Shadow(string name, int hp, int sp, Melee melee) : base(name, hp, sp, melee) { }
    }
}
