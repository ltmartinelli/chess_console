using chess;
using Boardgame;

namespace chess
{
    class PositionChess
    {
        public char Row { get; set; }
        public int Line { get; set; }

        public PositionChess(char row, int line)
        {
            Row = row;
            Line = line;
        }

        public Position toPosition()
        {
            return new Position(8 - Line, Row - 'a');
        }

        public override string ToString()
        {
            return "" + Row + Line;
        }
    }
}
