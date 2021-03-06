namespace Boardgame

{
    class Position
    {
        public int Line { get; set; }
        public int Row { get; set; }


        public Position(int line, int column)
        {
            Line = line;
            Row = column;
        }

        public override string ToString()
        {
            return Line + ", " + Row;
        }

        public void DefineValues(int line, int row)
        {
            Line = line;
            Row = row;
        }
    }
}
