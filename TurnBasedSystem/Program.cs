using System;
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
            PartyMember joker = new PartyMember(999,999,50);
            PartyMember skull = new PartyMember(100,100,10);
            PartyMember crow = new PartyMember(100,100,10);
            PartyMember violet= new PartyMember(100,100,10);

            Shadow enemy = new Shadow(100,100,10);

            joker.Attack(enemy);
            skull.Attack(enemy);
            crow.Attack(enemy);
            violet.Attack(enemy);
        }
    }
}
