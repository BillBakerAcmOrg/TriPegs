using System;
using System.Collections.Generic;
using System.Text;
using trianglePegs;

namespace TriRunner
{
    class Program
    {
        static GameNode gameTreeRoot;
        static System.Collections.Hashtable uniqueGameStates = new System.Collections.Hashtable(5000);

        static void BuildTree(GameNode root)
        {
            //build children
            foreach (MoveTuple mv in root.Game.AvailableMoves)
            {
                trianglePegs.game child = root.Game.Clone();
                child.Move(mv.original, mv.destination);
                Int64 childSig = child.Signature;
                if (!uniqueGameStates.Contains(childSig))
                {
                    GameNode gnChild = new GameNode(child);
                    root.Children.Add(gnChild);
                    uniqueGameStates.Add(childSig, gnChild);
                    BuildTree(gnChild);
                }
            }
        
        }

        static void DumpTree(GameNode rootNode)
        {
            Console.WriteLine(rootNode.ToString());
            rootNode.Children.ForEach(delegate(GameNode gn) { DumpTree(gn);});

            //foreach (GameNode gn in rootNode.Children)
            //{
            //    DumpTree(gn);
            //}
        }

        static int[] GameCounter = new int[15];

        static void CountFinishedGames(GameNode rootNode)
        {
            rootNode.Children.ForEach(delegate(GameNode gn)
            {
                if (gn.Game.AvailableMoves.Count == 0)
                {
                    GameCounter[gn.Game.PegsLeft]++;
                }
                CountFinishedGames(gn);
            });
        }

        static void Main(string[] args)
        {
            for (int idx = 0; idx < 15; idx++)
            {
                gameTreeRoot = new GameNode(new game());
                gameTreeRoot.Game.SetupBoard(idx);

                //build children
                BuildTree(gameTreeRoot);

                //DumpTree(gameTreeRoot);

                CountFinishedGames(gameTreeRoot);
            }

            int totalGames = 0;
            for (int idx = 0; idx < 15; idx++)
            {
                Console.WriteLine(string.Format("{0} Games have {1} Pegs remaining.", GameCounter[idx], idx));
                System.Diagnostics.Debug.WriteLine(string.Format("{0} Games have {1} Pegs remaining.", GameCounter[idx], idx));
                totalGames += GameCounter[idx];
            }
        
            Console.WriteLine(string.Format("{0} Games.", totalGames));
            System.Diagnostics.Debug.WriteLine(string.Format("{0} Games.", totalGames));

            //foreach (MoveTuple mv in gameTreeRoot.Game.AvailableMoves)
            //{
            //    trianglePegs.game child = gameTreeRoot.Game.Clone();
            //    child.Move(mv.original, mv.destination);
            //    gameTreeRoot.Children.Add(new GameNode(child));
            //}

            //game aGame;
            //int moves = 15;
            //while (moves >=15) //(aGame.PegsLeft > 1 || moves > 12)
            //{
            //    aGame = new game();
            //    moves = PlayAGame(aGame);

            //    Console.WriteLine("After {1} moves you have {0} Pegs left.", aGame.PegsLeft, moves);
            //    Console.WriteLine(aGame.ToString());
            //}

            Console.ReadKey();
        }

        static int PlayAGame(game aGame)
        {
            int moves = 0;
            Random rand = new Random(DateTime.Now.Millisecond);
            System.Collections.Generic.List<MoveTuple> availableMoves = aGame.AvailableMoves;
            Console.WriteLine(aGame.ToString());
            while (availableMoves.Count > 0)
            {
                moves++;
                int choice;
                if (availableMoves.Count > 1)
                    choice = rand.Next(0, availableMoves.Count - 1);
                else
                    choice = 0;

                MoveTuple mt = aGame.AvailableMoves[choice];
                Console.WriteLine("{2} available Moves, Moving peg {0} to {1}", mt.original, mt.destination, availableMoves.Count);
                aGame.Move(mt.original, mt.destination);

                availableMoves = aGame.AvailableMoves;
            }
            return moves;
        }
    }
}
