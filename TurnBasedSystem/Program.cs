﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TurnBasedSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            GameLoop.Start();
        }
    }
}
