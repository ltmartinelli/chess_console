using Boardgame;

namespace chess
{

    class Pawn : Piece
    {
        private Match _match;


        public Pawn(Board board, Color color, Match match) : base(board, color)
        {
            _match = match;
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

                //En Passant
                if (Position.Line == 3)
                {
                    Position left = new Position(Position.Line, Position.Row - 1);
                    if (Board.validPosition(left) && HasEnemy(left) && Board.Piece(left) == _match.VulnerableEnPassant)
                    {
                        mat[left.Line -1 , left.Row] = true;
                    }
                    Position right = new Position(Position.Line, Position.Row + 1);
                    if (Board.validPosition(right) && HasEnemy(right) && Board.Piece(right) == _match.VulnerableEnPassant)
                    {
                        mat[right.Line -1 , right.Row] = true;
                    }
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

                //En Passant
                if (Position.Line == 4)
                {
                    Position left = new Position(Position.Line, Position.Row - 1);
                    if (Board.validPosition(left) && HasEnemy(left) && Board.Piece(left) == _match.VulnerableEnPassant)
                    {
                        mat[left.Line +1, left.Row] = true;
                    }
                    Position right = new Position(Position.Line, Position.Row + 1);
                    if (Board.validPosition(right) && HasEnemy(right) && Board.Piece(right) == _match.VulnerableEnPassant)
                    {
                        mat[right.Line +1, right.Row] = true;
                    }
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