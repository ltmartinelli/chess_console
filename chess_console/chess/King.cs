using Boardgame;


namespace chess
{

    class King : Piece
    {

        private Match Match;

        public King(Board board, Color color, Match match) : base(board, color)
        {
            Match = match;
        }

        private bool canMove(Position pos)
        {
            Piece p = Board.Piece(pos);
            return p == null || p.Color != Color;

        }

        private bool TestTowerCastling(Position pos)
        {
            Piece p = Board.Piece(pos);
            return p != null && p is Tower && p.Color == Color && p.Movements == 0;
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] mat = new bool[Board.Lines, Board.Rows];

            Position pos = new Position(0, 0);

            //n
            pos.DefineValues(Position.Line - 1, Position.Row);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Row] = true;
            }

            //ne

            pos.DefineValues(Position.Line - 1, Position.Row + 1);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Row] = true;
            }

            //e

            pos.DefineValues(Position.Line, Position.Row + 1);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Row] = true;
            }

            //se

            pos.DefineValues(Position.Line + 1, Position.Row + 1);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Row] = true;
            }

            //s
            pos.DefineValues(Position.Line + 1, Position.Row);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Row] = true;
            }
            //sw
            pos.DefineValues(Position.Line + 1, Position.Row - 1);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Row] = true;
            }
            //w
            pos.DefineValues(Position.Line, Position.Row - 1);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Row] = true;
            }
            //nw
            pos.DefineValues(Position.Line - 1, Position.Row - 1);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Row] = true;
            }

            //#Special Play Castling

            if (Movements == 0 && !Match.Check)
            {
                //Minor
                Position posT1 = new Position(Position.Line, Position.Row + 3);
                if (TestTowerCastling(posT1))
                {
                    Position p1 = new Position(Position.Line, Position.Row + 1);
                    Position p2 = new Position(Position.Line, Position.Row + 2);
                    if (Board.Piece(p1) == null && Board.Piece(p2) == null)
                    {
                        mat[Position.Line, Position.Row + 2] = true;
                    }
                }
                //Major
                Position posT2 = new Position(Position.Line, Position.Row + -4);
                if (TestTowerCastling(posT2))
                {
                    Position p1 = new Position(Position.Line, Position.Row - 1);
                    Position p2 = new Position(Position.Line, Position.Row - 2);
                    Position p3 = new Position(Position.Line, Position.Row - 3);

                    if (Board.Piece(p1) == null && Board.Piece(p2) == null && Board.Piece(p3) == null)
                    {
                        mat[Position.Line, Position.Row -2] = true;
                    }
                }
            }



            return mat;
        }

        public override string ToString()
        {
            return "K";
        }

    }
}