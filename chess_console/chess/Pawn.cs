using Boardgame;


namespace chess
{

    class Pawn : Piece
    {
        public Pawn(Board board, Color color) : base(board, color)
        {
        }

        public bool HasEnemy(Position pos)
        {
            Piece p = Board.Piece(pos);
            return p != null && p.Color != Color;
        }

        private bool IsFree(Position pos)
        {
            return Board.Piece(pos) == null;
        }


        /*
                private bool canMove(Position pos)
                {
                    Piece p = Board.Piece(pos);
                    return p == null || p.Color != Color;

                }
        */

        public override bool[,] PossibleMovements()
        {
            bool[,] mat = new bool[Board.Lines, Board.Rows];

            Position pos = new Position(0, 0);


            //WhitePawn
            if (Color == Color.White)
            {
                pos.DefineValues(Position.Line - 1, Position.Row);
                if (Board.validPosition(pos) && IsFree(pos))
                {
                    mat[pos.Line, pos.Row] = true;
                }
                pos.DefineValues(Position.Line - 2, Position.Row);
                if (Board.validPosition(pos) && IsFree(pos) && Movements == 0)
                {
                    mat[pos.Line, pos.Row] = true;
                }
                pos.DefineValues(Position.Line - 1, Position.Row - 1);
                if (Board.validPosition(pos) && HasEnemy(pos))
                {
                    mat[pos.Line, pos.Row] = true;
                }
                pos.DefineValues(Position.Line - 1, Position.Row + 1);
                if (Board.validPosition(pos) && HasEnemy(pos))
                {
                    mat[pos.Line, pos.Row] = true;
                }

            }
            //BlackPawn
            else
            {
                pos.DefineValues(Position.Line + 1, Position.Row);
                if (Board.validPosition(pos) && IsFree(pos))
                {
                    mat[pos.Line, pos.Row] = true;
                }
                pos.DefineValues(Position.Line + 2, Position.Row);
                if (Board.validPosition(pos) && IsFree(pos) && Movements == 0)
                {
                    mat[pos.Line, pos.Row] = true;
                }
                pos.DefineValues(Position.Line + 1, Position.Row - 1);
                if (Board.validPosition(pos) && HasEnemy(pos))
                {
                    mat[pos.Line, pos.Row] = true;
                }
                pos.DefineValues(Position.Line + 1, Position.Row + 1);
                if (Board.validPosition(pos) && HasEnemy(pos))
                {
                    mat[pos.Line, pos.Row] = true;
                }
            }

            return mat;
        }

        public override string ToString()
        {
            return "P";
        }

    }
}