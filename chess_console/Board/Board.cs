

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

    }
}
