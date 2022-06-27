using Boardgame;


namespace chess
{

    class King : Piece
    {
        public King(Board board, Color color) : base(board, color)
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

            Position pos = new Position(0,0);

            //n
            pos.DefineValues(Position.Line - 1, Position.Row);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Row] = true;
            }

            //ne

            pos.DefineValues(Position.Line - 1, Position.Row+1);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Row] = true;
            }

            //e

            pos.DefineValues(Position.Line, Position.Row+1);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Row] = true;
            }

            //se

            pos.DefineValues(Position.Line + 1, Position.Row+1);
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
            pos.DefineValues(Position.Line + 1, Position.Row-1);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Row] = true;
            }
            //w
            pos.DefineValues(Position.Line, Position.Row-1);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Row] = true;
            }
            //nw
            pos.DefineValues(Position.Line - 1, Position.Row-1);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Row] = true;
            }
            return mat;
        }

        public override string ToString()
        {
            return "K";
        }

    }
}