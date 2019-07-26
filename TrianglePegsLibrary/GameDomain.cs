using System;
using System.Collections.Generic;
using System.Text;

namespace trianglePegs
{
    public class GameDomain
    {
        Dictionary<Int64, GameNode> uniqueGameStates = new Dictionary<long, GameNode>(5000);
        //System.Collections.Hashtable uniqueGameStates = new System.Collections.Hashtable(5000);

        void BuildTree(GameNode root)
        {
            //build children
            foreach (MoveTuple mv in root.Game.AvailableMoves)
            {
                trianglePegs.game child = root.Game.Clone();
                child.Move(mv.original, mv.destination);
                Int64 childSig = child.Signature;
                if (!uniqueGameStates.ContainsKey(childSig))
                {
                    GameNode gnChild = new GameNode(child);
                    root.Children.Add(gnChild);
                    uniqueGameStates.Add(childSig, gnChild);
                    BuildTree(gnChild);
                }
                else
                { 
                    root.Children.Add(uniqueGameStates[childSig]);
                }

            }
        }

        public GameDomain()
        {
            // For each possible starting game configuration
            // build out all game tree.
            GameNode gameTreeRoot;
            for (int idx = 0; idx < 15; idx++)
            {
                gameTreeRoot = new GameNode(new game());
                gameTreeRoot.Game.SetupBoard(idx);
                BuildTree(gameTreeRoot);
                BestPossibleScore(gameTreeRoot.Game.Signature);
            }
        }

        public int BestPossibleScore(Int64 Sig)
        {
            int ret = 0;
            if (uniqueGameStates.ContainsKey(Sig))
            {
                ret = uniqueGameStates[Sig].BestScore;
            }
            return ret;
        }

        public MoveTuple SuggestNextMove(Int64 Sig)
        {
            MoveTuple mv = null;
            if (uniqueGameStates.ContainsKey(Sig))
            {
                GameNode gameNode = uniqueGameStates[Sig];
                GameNode gameNodeChild = gameNode.Children.Find(delegate(GameNode gn) { if (gn.BestScore == gameNode.BestScore) return true; else return false; });
                if (gameNodeChild != null)
                {
                    //mv = gameNodeChild.Game.GetLastMove;
                    mv = game.Diff(gameNode.Game, gameNodeChild.Game);
                    //if (Sig != gameNodeChild.Game.Signature)
                    //{
                    //    System.Diagnostics.Debugger.Break();
                    //}
                }
            }
            return mv;            
        }
    }
}
