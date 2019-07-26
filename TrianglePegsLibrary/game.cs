using System;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using System.IO;
using System.Runtime.InteropServices;

namespace trianglePegs
{
	/// <summary>
	/// This class keeps track of a game in progress.
	/// The game is the classic triangle peg game. The board is laid out as follows:
    ///	     0
    ///	    1 2
    ///	   3 4 5
    ///   6 7 8 9
    ///	 0 1 2 3 4 
    ///	 
	/// </summary>
	[Serializable]
	public class game: ICloneable
	{
        //[StructLayout(LayoutKind.Sequential)]
        //public struct Control 
        //{
        //    [MarshalAs(UnmanagedType.ByValArray, SizeConst=3)] public byte[] code;
        //    public int  length;        // 0 <= length <= 50
        //    [MarshalAs(UnmanagedType.ByValArray, SizeConst=50)] public byte[] info;
        //} 

		private bool[] board = new bool[15];
        private System.Collections.Generic.Stack<MoveTuple> pastMoves = new System.Collections.Generic.Stack<MoveTuple>(50);   

        // legalMove[oldposition]= array of valid position
        [NonSerialized]
        static int[,] legalMoves = {{ 3,  5, -1, -1},           //0
                                    { 6,  8, -1, -1},           //1
                                    { 7,  9, -1, -1},           //2
                                    { 0, 10, 12,  5},           //3
                                    {11, 13, -1, -1},           //4
                                    { 0, 14, 12,  3},           //5
                                    { 1,  8, -1, -1},           //6
                                    { 9,  2, -1, -1},           //7
                                    { 1,  6, -1, -1},           //8
                                    { 2,  7, -1, -1},           //9
                                    { 3, 12, -1, -1},          //10
                                    {13,  4, -1, -1},          //11
                                    { 3,  5, 14, 10},          //12
                                    {11,  4, -1, -1},          //13
                                    {12,  5, -1, -1}           //14
                                    };


        System.Collections.Specialized.ListDictionary jumpedSpace = new System.Collections.Specialized.ListDictionary();

        public MoveTuple GetLastMove
        {
            get
            {
                if (pastMoves.Count == 0)
                {
                    throw new Exception("The are no moves to undo.");
                }
                else
                {
                    return pastMoves.Peek() as MoveTuple;
                }
            }
        }

        /// <summary>
        /// Checks the board for standing pegs 
        /// </summary>
        /// <returns>the number of pegs still standing</returns>
        public int PegsLeft
        {
            get 
            {
                int count = 0;
                int idx;
                for (idx=0; idx<board.Length; idx++)
                    if (board[idx])
                        count++;
                return count;
            }
        }

        public game()
		{
			SetupBoard(0);
            jumpedSpace.Add("0,3", 1);
            jumpedSpace.Add("0,5", 2);
            
            jumpedSpace.Add("1,6",3);
            jumpedSpace.Add("1,8",4);

            jumpedSpace.Add("2,7",4);
            jumpedSpace.Add("2,9",5);

            jumpedSpace.Add("3,5",4);
            jumpedSpace.Add("3,0",1);
            jumpedSpace.Add("3,12",7);
            jumpedSpace.Add("3,10",6);

            jumpedSpace.Add("4,13",8);
            jumpedSpace.Add("4,11",7);

            jumpedSpace.Add("5,0",2);
            jumpedSpace.Add("5,12",8);
            jumpedSpace.Add("5,14",9);
            jumpedSpace.Add("5,3",4);

            jumpedSpace.Add("6,1",3);
            jumpedSpace.Add("6,8",7);

            jumpedSpace.Add("7,9",8);
            jumpedSpace.Add("7,2",4);

            jumpedSpace.Add("8,1",4);
            jumpedSpace.Add("8,6",7);

            jumpedSpace.Add("9,2",5);
            jumpedSpace.Add("9,7",8);

            jumpedSpace.Add("10,3",6);
            jumpedSpace.Add("10,12",11);

            jumpedSpace.Add("11,4",7);
            jumpedSpace.Add("11,13",12);

            jumpedSpace.Add("12,3",7);
            jumpedSpace.Add("12,5",8);
            jumpedSpace.Add("12,14",13);
            jumpedSpace.Add("12,10",11);

            jumpedSpace.Add("13,11",12);
            jumpedSpace.Add("13,4",8);

            jumpedSpace.Add("14,13",12);
            jumpedSpace.Add("14,9",5);
		}

        public Int64 Signature
        {
            get 
            {
                Int64 sig = 0;
                for (int idx = 0; idx <= board.GetUpperBound(0); idx++)
                {
                    if (board[idx])
                    {
                        sig += Convert.ToInt64(Math.Pow(2, idx));
                    }
                }

                return sig; 
            }
            set { }
        }
	
        /// <summary>
		/// This function initializes the board placing a peg in all the spaces leaving one space 
		/// blank. The space to leave blank is passed in.
		/// </summary>
		/// <param name="emptySpace"></param>
		public void SetupBoard(int emptySpace)
		{
            if (emptySpace < board.GetLowerBound(0) || emptySpace > board.GetUpperBound(0))
                throw new ArgumentOutOfRangeException("emptySpace", emptySpace, 
                    string.Format("must be between [0] and [1]", board.GetLowerBound(0), board.GetUpperBound(0)));

            for (int idx = 0; idx <= board.GetUpperBound(0); idx++)
            {
                if (idx == emptySpace)
                    board[idx] = false;
                else
                    board[idx] = true;
            }
		}

        public bool IsSpaceOpen(int space)
        {
            if (space < 0 || space > board.Length)
                throw new ArgumentOutOfRangeException("space", space, 
                    string.Format("must be between 0 and {0}",  board.Length));

            return !board[space];
        }

        /// <summary>
        /// This function is used to move a peg from one position to a new position
        /// </summary>
        /// <param name="peg"></param>
        /// <param name="newPosition"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void Move(int oldPosition, int newPosition)
        {
            if (oldPosition < 0 || oldPosition > board.Length)
                throw new ArgumentOutOfRangeException("oldPosition", oldPosition, 
                    string.Format("must be between 0 and {0}",  board.Length));

            if (newPosition < 0 || newPosition > board.Length)
                throw new ArgumentOutOfRangeException("oldPosition", oldPosition, 
                    string.Format("must be between 0 and {0}", board.Length));

            if (!board[oldPosition])
                throw new ArgumentException(string.Format("There is no peg at this position {0}", oldPosition), "oldPosition");

            if (board[newPosition])
                throw new ArgumentException(string.Format("There is already a peg at the new position {0}", newPosition), "newPosition");
            
            if (!IsLegalMove(oldPosition, newPosition))
                throw new ArgumentException(string.Format("Not a valid new position {0}", newPosition), newPosition.ToString());

            int spaceJumped = JumpedSpace(oldPosition, newPosition);
            if (spaceJumped == -1 || !board[spaceJumped] )
                throw new Exception(string.Format("There is must be a peg at {0} to jump", spaceJumped));

            board[oldPosition] = false;
            board[spaceJumped] = false;
            board[newPosition] = true;

            pastMoves.Push(new MoveTuple(oldPosition, spaceJumped, newPosition));
        }

        public System.Collections.Generic.List<MoveTuple> AvailableMoves
        {
            get
            {
                System.Collections.Generic.List<MoveTuple> moves = new System.Collections.Generic.List<MoveTuple>();
                if (!GameOver)
                {
                    int move;
                    int oldPos = 0;
                    int newPos = 0;
                    int idx;

                    for (move = 0; move <= legalMoves.GetUpperBound(0); move++)
                    {
                        oldPos = move;

                        if (!board[oldPos])
                        {
                            continue;
                        }

                        for (idx=0; idx <= legalMoves.GetUpperBound(1) && legalMoves[oldPos,idx] != -1; idx++)
                        {
                            newPos = legalMoves[oldPos,idx];
                            if (IsValidMove(oldPos, newPos))
                            {
                                moves.Add(new MoveTuple(oldPos, JumpedSpace(oldPos, newPos), newPos));
                            }
                        }
                    }
                }
                return moves;
            }
        }

        public bool SuggestedMove(ref int oldPosition, ref int newPosition)
        {
            bool found = false;
            if (!GameOver)
            {
                Random rand = new Random(System.DateTime.Now.Millisecond);

                System.Collections.Generic.List<MoveTuple> moves = AvailableMoves;
                if (moves.Count > 0)
                {
                    int idx = rand.Next(moves.Count-1);
                    MoveTuple suggestedMove = moves[idx] as MoveTuple;
                    oldPosition = suggestedMove.original;
                    newPosition = suggestedMove.destination;
                    found = true;
                }
            }
            return found;
        }

        public bool GameOver
        {
            get
            {
                int idx;
                int oldPosition;
                int newPosition;
                bool found = false;
                int move;
                for (move = 0; move <= legalMoves.GetUpperBound(0) && !found; move++)
                {
                    oldPosition = move;

                    //System.Diagnostics.Debug.WriteLine(string.Format("Checking oldPosition {0}", oldPosition));

                    if (!board[oldPosition])
                    {
                        //System.Diagnostics.Debug.WriteLine(string.Format("No Peg at oldPosition {0}", oldPosition));
                        continue;
                    }

                    for (idx=0; idx <= legalMoves.GetUpperBound(1) && legalMoves[oldPosition,idx] != -1; idx++)
                    {
                        newPosition = legalMoves[oldPosition,idx];
                        //System.Diagnostics.Debug.WriteLine(string.Format("Checking for a move to newPosition {0}", newPosition));
                        if (IsValidMove(oldPosition, newPosition))
                        {
                            found = true;
                            break;
                        }
                    }
                }
                return !found;
            }
        }

        private bool IsValidMove(int oldPosition, int newPosition)
        {
            if (!board[oldPosition])
            {
                //System.Diagnostics.Debug.WriteLine(string.Format("No Peg at oldPosition {0}", oldPosition));
                return false;
            }

            if (!IsLegalMove(oldPosition, newPosition))
            {
                return false;
            }
            
            //int jumpedSpace = JumpedSpace(oldPosition, newPosition);
            //System.Diagnostics.Debug.WriteLine(string.Format("testing Move from {0} to {1} over {2}", oldPosition, newPosition, jumpedSpace));
            if (!board[newPosition] && board[JumpedSpace(oldPosition, newPosition)])
            {
                //System.Diagnostics.Debug.WriteLine(string.Format("Found Legal Move from {0} to {1}", oldPosition, newPosition));
                return true;
            }

            return false;
        }

        private bool IsLegalMove(int oldPosition, int newPosition)
        {
            int idx;
            bool found = false;
            for (idx=0; idx <= legalMoves.GetUpperBound(1) && legalMoves[oldPosition,idx] != -1; idx++)
            {
                if (legalMoves[oldPosition,idx] == newPosition)
                {
                    found = true;
                    break;
                }
            }
            return found;
        }

        private int JumpedSpace(int oldPosition, int newPosition)
        {
            int spaceJumped = -1;
            string key = string.Format("{0},{1}", oldPosition, newPosition);
            if (jumpedSpace.Contains(key))
            {
                spaceJumped = (int)jumpedSpace[key];
            }
            else
            {
                key = string.Format("{0},{1}", newPosition, oldPosition);
                if (jumpedSpace.Contains(key))
                {
                    spaceJumped = (int)jumpedSpace[key];
                }
            }
            return spaceJumped;
        }

        public void Undo()
        {
            if (pastMoves.Count == 0)
            {
                throw new Exception("The are no moves to undo.");
            }
            else
            {
                MoveTuple lastMove = pastMoves.Pop();
                board[lastMove.original] = true;
                board[lastMove.jumped] = true;
                board[lastMove.destination] = false;
            }
        }

        public void SaveToFile(string fileName)
        {
            try
            {
                Stream write = File.Create(fileName);
                SoapFormatter soapWrite = new SoapFormatter();
                soapWrite.Serialize(write, this);
                write.Close();
            }
            catch (Exception exc)
            {
                System.Diagnostics.Trace.WriteLine(exc.Message);
                throw exc;
            }
        }

        static public game LoadGameFromFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new System.ArgumentException(string.Format("Cannot Open file {0} for deserialization.", fileName));
            }
            try
            {
                Stream read = File.OpenRead(fileName);
                SoapFormatter soapRead = new SoapFormatter();

                game aGame = soapRead.Deserialize(read) as game;
                if (aGame == null)
                    throw new System.Exception(string.Format("This file doesn't contain a Peg Game, {0}", fileName));

                return aGame;
            }
            catch (Exception exc)
            {
                System.Diagnostics.Trace.WriteLine(exc.Message);
                throw exc;
            }
        }

        public void LoadFromFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new System.ArgumentException(string.Format("Cannot Open file {0} for deserialization.", fileName));
            }

            try
            {
                Stream read = File.OpenRead(fileName);
                SoapFormatter soapRead = new SoapFormatter();

                game aGame = soapRead.Deserialize(read) as game;
                if (aGame == null)
                    throw new System.Exception(string.Format("This file doesn't contain a Peg Game, {0}", fileName));

                aGame.board.CopyTo(this.board,0);
                this.pastMoves = null; // not sure if necessary
                this.pastMoves = new System.Collections.Generic.Stack<MoveTuple>(aGame.pastMoves);
            }
            catch (Exception exc)
            {
                System.Diagnostics.Trace.WriteLine(exc.Message);
                throw exc;
            }
        }

        public override string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("Sig:" + this.Signature.ToString() + Environment.NewLine);
            int idx;
            for (idx=0; idx<board.Length; idx++)
            {
                if (idx == 1 || idx == 3 || idx == 6 || idx == 10)
                    sb.Append(Environment.NewLine);

                if (board[idx])
                    sb.Append("1");
                else
                    sb.Append("0");
            }
            sb.Append(Environment.NewLine);
            if (this.pastMoves.Count > 0)
            {
                sb.Append("Moves:" + Environment.NewLine);
                foreach (MoveTuple mv in pastMoves)
                {
                    sb.Append("\t" + mv.ToString() + Environment.NewLine);
                }
            }
            else
            {
                sb.Append("No Moves yet" + Environment.NewLine);
            }
            return sb.ToString();
        }

        #region ICloneable Implementation
        // Explicit interface method impl -- available for 
        // clients of ICloneable, but invisible to casual 
        // clients of MyCloneableClass
        object ICloneable.Clone()
        {
            // simply delegate to our type-safe cousin
            return this.Clone();
        }

        // Friendly, type-safe clone method
        public virtual game Clone()
        {
            // Start with a flat, memberwise copy
            game x = this.MemberwiseClone() as game;

            // Then deep-copy everything that needs the 
            // special attention
            //...
            x.board = new bool[15];
            x.pastMoves = new System.Collections.Generic.Stack<MoveTuple>(50);   

            for (int idx = this.board.GetLowerBound(0); idx<= this.board.GetUpperBound(0); idx++)
            {
                x.board[idx] = this.board[idx];
            }

            if (this.pastMoves.Count > 0)
            {
                Array arr = this.pastMoves.ToArray();
                for (int idx = arr.GetUpperBound(0); idx >= arr.GetLowerBound(0); idx--)
                {
                    if (arr.GetValue(idx) != null)
                    {
                        x.pastMoves.Push((MoveTuple)arr.GetValue(idx));
                    }
                }
            }
            return x;
        }
        #endregion

        public static MoveTuple Diff(game BeforeMoveBoard, game AfterMoveBoard)
        {
            if (BeforeMoveBoard.PegsLeft-1 != AfterMoveBoard.PegsLeft)
                throw new ArgumentException("The BeforeMoveBoard must have only one more peg on board than the AfterMoveBoard", "BeforeMoveBoard");

            Int64 b4Sig = BeforeMoveBoard.Signature;
            Int64 aftSig = AfterMoveBoard.Signature;

            MoveTuple mt = null;

            for (int originalPosition = 0; originalPosition <= BeforeMoveBoard.board.GetUpperBound(0); originalPosition++)
            {
                if (BeforeMoveBoard.board[originalPosition] != AfterMoveBoard.board[originalPosition])
                {
                    if (!AfterMoveBoard.board[originalPosition])
                    { // Since originalPosition is empty, it could be the original position
                        for (int destinationPosition = 0; destinationPosition <= BeforeMoveBoard.board.GetUpperBound(0); destinationPosition++)
                        {   // if once a blank is found now look for possible leagl moves
                            if (BeforeMoveBoard.IsValidMove(originalPosition, destinationPosition))
                            {
                                int jumpedSpace = BeforeMoveBoard.JumpedSpace(destinationPosition, originalPosition);
                                if (aftSig == b4Sig + Convert.ToInt64(Math.Pow(2, destinationPosition) - Math.Pow(2, jumpedSpace) - Math.Pow(2, originalPosition)))
                                {
                                    mt = new MoveTuple(originalPosition, jumpedSpace, destinationPosition);
                                    break;
                                }
                            }
                        }
                    }
                    if (mt != null)
                        break;
                }
            }
            return mt;
        }
    }
}
