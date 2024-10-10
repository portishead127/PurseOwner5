using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TurnBasedSystem
{
    internal static class GameLoop
    {
        static List<PartyMember> activeParty = new List<PartyMember>();
        static List<Shadow> activeShadows = new List<Shadow>();
        static List<Fighter> allFighters = new List<Fighter>();

        static int indexOfCurrentTurn = 0;
        static int indexOfCurrentTarget = 0;
        static Fighter fighterToMove;
        static Fighter currentTarget;
        static bool isActionSelected = false;

        static bool battleWon = false;

        static PartyMember joker;
        static PartyMember skull;
        static PartyMember crow;
        static PartyMember violet;

        static Shadow pixie;
        static Shadow bicorn;

        static Melee knife;
        static Melee pipe;
        static Melee laserSword;
        static Melee cutlass;

        static Gun pistol;
        static Gun shotgun;
        static Gun laserGun;
        static Gun musket;

        static Melee pixieMelee;
        static Melee bicornMelee;

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
            knife = new Melee("Nataraja EX", 10, 4, 90);
            pipe = new Melee("Megido Blaster", 10, 4, 90);
            laserSword = new Melee("Ancient Day", 10, 4, 90);
            cutlass = new Melee("Sahasrara", 10, 4, 90);

            pistol = new Gun("Nataraja EX", 3, 1, 80, 8);
            shotgun = new Gun("Megido Blaster", 6, 2, 72, 3);
            laserGun = new Gun("Ancient Day", 4, 0, 75, 9);
            musket = new Gun("Sahasrara", 5, 2, 78, 12);

            pixieMelee = new Melee("Attack", 10, 4, 60);
            bicornMelee = new Melee("Attack", 10, 4, 60);
        }

        static void InitialiseFighters()
        {
            joker = new PartyMember("Joker", 999, 999, knife, pistol);
            skull = new PartyMember("Skull", 100, 100, pipe, shotgun);
            crow = new PartyMember("Crow", 100, 100, laserSword, laserGun);
            violet = new PartyMember("Violet", 100, 100, cutlass, musket);
            List<PartyMember> partyMembers = new List<PartyMember>() {joker, skull, crow, violet};
            activeParty.AddRange(partyMembers);

            pixie = new Shadow("Pixie", 5, 10, pixieMelee);
            bicorn = new Shadow("Bicorn", 1, 5, bicornMelee);
            List<Shadow> shadows = new List<Shadow>() {pixie, bicorn};
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
                    TurnSplashScreen();
                    Thread.Sleep(500);
                    AttackSplashScreen("ATTACKS " + currentTarget.Name);
                    fighterToMove.Melee.Attack(currentTarget);

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
                SetTarget();
                TurnSplashScreen();
                Console.WriteLine($"\nCurrently targeting: {currentTarget.Name}\n\n");
                Console.WriteLine("1. ATTACK");
                Console.WriteLine("2. SHOOT");
                Console.WriteLine("3. GUARD");

                // Check if a key is pressed and if it's different from the last key pressed

                keyPressed = Console.ReadKey(true).Key;

                // Only process the key if it's different from the last key
                    switch (keyPressed)
                    {
                        case ConsoleKey.RightArrow:
                            if (indexOfCurrentTarget == activeShadows.Count - 1)
                            {
                                indexOfCurrentTarget = 0;
                            }
                            else
                            {
                                indexOfCurrentTarget++;
                            }
                            break;
                        case ConsoleKey.LeftArrow:
                            if (indexOfCurrentTarget == 0)
                            {
                                indexOfCurrentTarget = activeShadows.Count - 1;
                            }
                            else
                            {
                                indexOfCurrentTarget--;
                            }
                            break;
                        case ConsoleKey.D1:
                            fighterToMove.Melee.Attack(currentTarget);
                            AttackSplashScreen("ATTACKS " + currentTarget.Name);
                            isActionSelected = true; // Mark action as selected to exit the loop
                            break;
                        case ConsoleKey.D2:
                            Aim();
                            break;
                        case ConsoleKey.D3:
                            fighterToMove.IsGuarding = true;
                            AttackSplashScreen("GUARDS");
                            isActionSelected = true; // Mark action as selected
                            break;
                        default:
                            break;
                    }
                    // Clear the input buffer to prevent the key flood
                    while (Console.KeyAvailable) Console.ReadKey(true);
                // Short delay to prevent excessive CPU usage
                Thread.Sleep(50);

            } while (!isActionSelected);
        }


        static void TurnSplashScreen()
        {
            Console.Clear();
            ShowStatus();
            Console.WriteLine($"{fighterToMove.Name}'s Turn:");
        }

        static void AttackSplashScreen(string action)
        {
            isActionSelected = true;
            Console.WriteLine($"\n{action}");
            Thread.Sleep(1500);
        }

        static void Aim()
        {
            PartyMember partyMemberToMove = (PartyMember)fighterToMove;
            int startingBullets = partyMemberToMove.Gun.Bullets;
            bool outOfAmmoFromStart = false;
            ConsoleKey keyPressed;
            do
            {
                SetTarget();
                TurnSplashScreen();
                Console.WriteLine($"\nCurrently targetting: {currentTarget.Name}\n\n");
                Console.WriteLine($"Bullets: {partyMemberToMove.Gun.Bullets}\n");
                Console.WriteLine("SPACEBAR: SHOOT");
                Console.WriteLine("C: BACK");
                Thread.Sleep(100);
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
                    case ConsoleKey.Spacebar:
                        if (partyMemberToMove.Gun.Bullets == 0 && startingBullets == 0)
                        {
                            outOfAmmoFromStart = true;
                            Console.WriteLine("Out of ammo");
                            isActionSelected = false;
                            Thread.Sleep(300);
                            continue;
                        }
                        partyMemberToMove.Gun.Shoot(currentTarget);
                        outOfAmmoFromStart = false;
                        break;
                    case ConsoleKey.C:
                        return;
                    default: break;
                }
                while (Console.KeyAvailable) Console.ReadKey(true);
                Thread.Sleep(50);
            } while ((keyPressed != ConsoleKey.C && partyMemberToMove.Gun.Bullets > 0)|| outOfAmmoFromStart);

            //Either pressed back or run out of bullets
            if (((PartyMember)fighterToMove).Gun.Bullets < startingBullets)
            {
                isActionSelected = true;
            }
            return;
        }

        static void SetTarget()
        {
            currentTarget = activeShadows[indexOfCurrentTarget];
            if (currentTarget.IsDead)
            {
                for (int i = 0; i < activeShadows.Count; i++)
                {
                    if (!activeShadows[i].IsDead)
                    {
                        indexOfCurrentTarget = i;
                        currentTarget = activeShadows[indexOfCurrentTarget];
                        return;
                    }
                }
                //all shadows dead
                Victory();
            }
        }

        static void Victory()
        {
            Console.Clear();
            Console.WriteLine("VICTORY");
            Console.ReadKey();
            Environment.Exit(0);
        }

        static void ShowStatus()
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
