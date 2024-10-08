using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurnBasedSystem
{
    internal class GameLoop
    {
        public List<PartyMember> activeParty = new List<PartyMember>();
        public List<Shadow> activeShadows = new List<Shadow>();
        public List<Fighter> allFighters = new List<Fighter>();
        public void Start()
        {
            InitialiseFighters();
        }

        void InitialiseFighters()
        {
            PartyMember joker = new PartyMember(999, 999, 10, 2);
            PartyMember skull = new PartyMember(100, 100, 10, 3);
            PartyMember crow = new PartyMember(100, 100, 10, 2);
            PartyMember violet = new PartyMember(100, 100, 10, 1);
            List<PartyMember> partyMembers = new List<PartyMember>() {joker,skull,crow, violet};
            activeParty.AddRange(partyMembers);

            Shadow pixie = new Shadow(50, 10, 10, 3);
            List<Shadow> shadows = new List<Shadow>() {pixie};
            shadows.AddRange(shadows);

            allFighters = activeParty.Cast<Fighter>().Concat(shadows.Cast<Fighter>()).ToList();
        }
    }
}
