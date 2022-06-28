namespace Boardgame
{
    abstract class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int Movements { get; protected set; }
        public Board Board { get; protected set; }

        public Piece(Board board, Color color)
        {
            Position = null;
            Color = color;
            Board = board;
            Movements = 0;
        }

        public void IncrementMovements()
        {
            Movements++;
        }

        public void DecrementMovements()
        {
            Movements--;
        }

        public bool HasPossibleMovements()
        {
            bool[,] mat = PossibleMovements();
            for(int i = 0; i < Board.Lines; i++)
            {
                for (int j = 0; j < Board.Rows; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CanMoveTo(Position pos)
        {
            return PossibleMovements()[pos.Line, pos.Row];
        }

        public abstract bool [,] PossibleMovements();
    }
}
