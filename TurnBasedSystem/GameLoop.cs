using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurnBasedSystem
{
    internal static class GameLoop
    {
        public static List<PartyMember> activeParty = new List<PartyMember>();
        public static List<Shadow> activeShadows = new List<Shadow>();
        public static List<Fighter> allFighters = new List<Fighter>();

        public static int indexOfCurrentTurn = 0;
        public static int indexOfCurrentTarget = 0;
        public static Fighter fighterToMove;
        public static Fighter currentTarget;
        public static bool isActionSelected = false;

        static PartyMember joker;
        static PartyMember skull;
        static PartyMember crow;
        static PartyMember violet;

        static Shadow pixie;

        static Melee melee;
        static Melee knife;
        static Melee pipe;
        static Melee laserSword;
        static Melee cutlass;

        static Gun pistol;
        static Gun shotgun;
        static Gun laserGun;
        static Gun musket;

        public static void Start()
        {
            Initialise();
            RunGameLoop();
        }

        static void Initialise()
        {
            InitialiseWeapons();
            InitialiseFighters();
        }

        static void RunGameLoop()
        {
            StartTurn();
        }

        static void InitialiseWeapons()
        {
            melee = new Melee("Melee", 10, 4, 60);
            knife = new Melee("Nataraja EX", 10, 4, 90);
            pipe = new Melee("Megido Blaster", 10, 4, 90);
            laserSword = new Melee("Ancient Day", 10, 4, 90);
            cutlass = new Melee("Sahasrara", 10, 4, 90);

            pistol = new Gun("Nataraja EX", 3, 1, 80, 8);
            shotgun = new Gun("Megido Blaster", 6, 2, 72, 3);
            laserGun = new Gun("Ancient Day", 4, 0, 75, 9);
            musket = new Gun("Sahasrara", 5, 2, 78, 12);
        }

        static void InitialiseFighters()
        {
            joker = new PartyMember("Joker", 999, 999, knife, pistol);
            skull = new PartyMember("Skull", 100, 100, pipe, shotgun);
            crow = new PartyMember("Crow", 100, 100, laserSword, laserGun);
            violet = new PartyMember("Violet", 100, 100, cutlass, musket);
            List<PartyMember> partyMembers = new List<PartyMember>() {joker, skull, crow, violet};
            activeParty.AddRange(partyMembers);

            pixie = new Shadow("Pixie", 50, 10, melee);
            List<Shadow> shadows = new List<Shadow>() {pixie};
            activeShadows.AddRange(shadows);

            allFighters = activeParty.Cast<Fighter>().Concat(shadows.Cast<Fighter>()).ToList();
        }

        static void StartTurn()
        {
            while (!joker.IsDead)// or all shadows are not dead
            {
                //Put logic that checks if a player should move 
                fighterToMove = allFighters[indexOfCurrentTurn];
                if (fighterToMove.IsDead)
                {
                    indexOfCurrentTurn++;
                    if (indexOfCurrentTurn >= allFighters.Count)
                    {
                        indexOfCurrentTurn = 0;
                    }
                    continue;
                }

                if (fighterToMove.GetType() != typeof(Shadow))
                {
                    fighterToMove.IsGuarding = false;
                    ChooseAction();
                }
                else
                {
                    //Shadow ai logic
                    currentTarget = joker;
                    fighterToMove.Melee.Attack();
                }

                indexOfCurrentTurn++;
                if (indexOfCurrentTurn >= allFighters.Count)
                {
                    indexOfCurrentTurn = 0;
                }
            }
        }

        static void ChooseAction()
        {
            isActionSelected = false;
            ConsoleKey keyPressed;

            do
            {
                currentTarget = activeShadows[indexOfCurrentTarget];

                Console.Clear();
                ShowStatus();
                Console.WriteLine($"{fighterToMove.Name}'s Turn:");
                Console.WriteLine($"\nCurrently targetting: {currentTarget.Name}\n\n");
                Console.WriteLine("1. ATTACK");
                Console.WriteLine("2. SHOOT");
                Console.WriteLine("3. GUARD");
                keyPressed = Console.ReadKey(true).Key;
                switch (keyPressed)
                {
                    case ConsoleKey.RightArrow:
                        if (indexOfCurrentTarget == activeShadows.Count - 1)
                        {
                            indexOfCurrentTarget = 0;
                            break;
                        }
                        indexOfCurrentTarget++;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (indexOfCurrentTarget == 0)
                        {
                            indexOfCurrentTarget = activeShadows.Count - 1;
                            break;
                        }
                        indexOfCurrentTarget--;
                        break;
                    case ConsoleKey.D1:
                        fighterToMove.Melee.Attack();
                        break;
                    case ConsoleKey.D2:
                        ((PartyMember)fighterToMove).Gun.Aim();
                        break;
                    case ConsoleKey.D3:
                        fighterToMove.IsGuarding = true;
                        isActionSelected = true;
                        break;
                    default: break;
                }
            } while (!isActionSelected);        
        }

        public static void ShowStatus()
        {
            for(int i = 0; i < allFighters.Count; i++)
            {
                Fighter fighterToOutput = allFighters[i];
                Console.WriteLine($"{fighterToOutput.Name}: HP: {fighterToOutput.HP}/{fighterToOutput.MaxHP}\tSP: {fighterToOutput.SP}/{fighterToOutput.MaxSP}\n");
            }
            Console.WriteLine();
        }
    }
}
