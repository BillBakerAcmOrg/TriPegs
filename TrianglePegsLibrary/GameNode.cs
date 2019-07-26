using System;
using System.Collections.Generic;
using System.Text;

namespace trianglePegs
{
    public class GameNode
    {
        private List<GameNode> _children;
        private trianglePegs.game _game;
        private int _bestScore = 15;

        private GameNode()
        {
        }

        public GameNode( trianglePegs.game aGame)
        {
            _game = aGame;
            _children = new List<GameNode>();
        }

        public List<GameNode> Children
        {
            get { return _children; }
            set { _children = value; }
        }

        public trianglePegs.game Game
        {
            get { return _game; }
            set { }
        }

        public int BestScore
        {
            get
            {
                if (_bestScore == 15)
                {
                    if (_children.Count == 0)
                    {
                        _bestScore = _game.PegsLeft;
                    }
                    else
                    {
                        _children.ForEach(delegate(GameNode gn)
                        {
                            _bestScore = Math.Min(_bestScore, gn.BestScore);
                        });
                    }
                }
                return _bestScore;
            }
        }

        public override string ToString()
       {
           return string.Format("There are {0} pegs remaining with {1} available moves", _game.PegsLeft, _game.AvailableMoves.Count);
       }
    }
}
