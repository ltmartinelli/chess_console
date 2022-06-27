using System;
using board;
using chess;

namespace chess
{
    class Match
    {

        public Board Board { get; private set; }
        private int Turn;
        private Color CurrentPlayer;
        public bool HasFinished { get; private set; }

        public Match()
        {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            PlacePieces();
            HasFinished = false;
        }

        public void ExecuteMovement(Position origin, Position target)
        {
            Piece p = Board.RemovePiece(origin);
            p.IncrementMovements();
            Piece capturedPiece = Board.RemovePiece(target);
            Board.PlacePiece(p, target);

        }

        private void PlacePieces()
        {
            Board.PlacePiece(new Tower(Board, Color.Black), new PositionChess('c', 1).toPosition());

        }
    }
}
