using Boardgame;

namespace chess
{
    class Knight : Piece
    {
        public Knight(Board board, Color color) : base(board, color)
        {
        }

        public override string ToString()
        {
            /*
            *  since the king is K, Knight will be defined as H, as in Horse, 
            *  which is its name in several languages           
            */
            return "H";
        }

        private bool canMove(Position pos)
        {
            Piece p = Board.Piece(pos);
            return p == null || p.Color != Color;
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] mat = new bool[Board.Lines, Board.Rows];

            Position pos = new Position(0, 0);

            
            pos.DefineValues(Position.Line - 1, Position.Row-2);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Row] = true;
            }
            
            pos.DefineValues(Position.Line - 2, Position.Row - 1);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Row] = true;
            }
            
            pos.DefineValues(Position.Line - 2, Position.Row +1);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Row] = true;
            }
            
            pos.DefineValues(Position.Line -1, Position.Row + 2);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Row] = true;
            }

            pos.DefineValues(Position.Line +1, Position.Row + 2);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Row] = true;
            }

            pos.DefineValues(Position.Line + 2, Position.Row +1);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Row] = true;
            }

            pos.DefineValues(Position.Line + 2, Position.Row - 1);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Row] = true;
            }

            pos.DefineValues(Position.Line + 1, Position.Row - 2);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Row] = true;
            }

            return mat;

        }
    }
}
