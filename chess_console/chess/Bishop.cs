using Boardgame;

namespace chess
{

    class Bishop : Piece
    {
        public Bishop(Board board, Color color) : base(board, color)
        {
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

            //nw
            pos.DefineValues(Position.Line - 1, Position.Row-1);
            while (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Row] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                {
                    break;
                }
                pos.DefineValues(pos.Line-1,pos.Row-1);
            }

            //ne
            pos.DefineValues(Position.Line-1, Position.Row + 1);
            while (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Row] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                {
                    break;
                }
                pos.DefineValues(pos.Line - 1, pos.Row + 1);
            }

            //se
            pos.DefineValues(Position.Line + 1, Position.Row+1);
            while (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Row] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                {
                    break;
                }
                pos.DefineValues(pos.Line + 1, pos.Row + 1);
            }

            //sw
            pos.DefineValues(Position.Line+1, Position.Row - 1);
            while (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Row] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                {
                    break;
                }
                pos.DefineValues(pos.Line + 1, pos.Row - 1);
            }

            return mat;

        }

        public override string ToString()
        {
            return "B";
        }
    }
}