

namespace board
{
    class Board
    {
        public int Lines { get; set; }
        public int Rows { get; set; }
        private Piece[,] Pieces;

        public Board(int lines, int rows)
        {
            Lines = lines;
            Rows = rows;
            Pieces = new Piece[lines, rows];

        }

        public Piece Piece(int line, int row)
        {
            return Pieces[line, row];   
        } 

        public Piece Piece(Position pos)
        {
            return Pieces[pos.Line, pos.Row];
        }

        public void PlacePiece(Piece p, Position pos)
        {
            Pieces[pos.Line, pos.Row] = p;
            p.Position = pos;
        }


        public bool hasPiece(Position pos)
        {
            validatePosition(pos);
            return Piece(pos) != null;
        }

        public bool validPosition(Position pos)
        {
            if (pos.Line <0 || pos.Line >= Lines || pos.Row<0 || pos.Row >= Rows)
            {
                return false;
            }
            return true;
        }

        public void validatePosition(Position pos)
        {
            if (!validPosition(pos))
            {
                throw new BoardException("Invalid Position!");
            }
        }

    }
}
