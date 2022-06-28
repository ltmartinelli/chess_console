namespace Boardgame

{
    class Board
    {
        public int Lines { get; set; }
        public int Rows { get; set; }
        private Piece[,] _pieces;


        public Board(int lines, int rows)
        {
            Lines = lines;
            Rows = rows;
            _pieces = new Piece[lines, rows];
        }

        public Piece Piece(int line, int row)
        {
            return _pieces[line, row];   
        } 

        public Piece Piece(Position pos)
        {
            return _pieces[pos.Line, pos.Row];
        }

        public void PlacePiece(Piece p, Position pos)
        {
            if (hasPiece(pos)) { throw new BoardException("There is already a piece in this position"); }
            _pieces[pos.Line, pos.Row] = p;
            p.Position = pos;
        }

        public Piece RemovePiece(Position pos)
        {
            if (!hasPiece(pos)) { return null; }
            Piece aux = Piece(pos);
            aux.Position = null;
            _pieces[pos.Line, pos.Row] = null;
            return aux;
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
