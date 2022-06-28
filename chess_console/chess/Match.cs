using System;
using Boardgame;
using chess;

namespace chess
{
    class Match
    {

        public Board Board { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
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

        public void ExecutePlay(Position origin, Position target)
        {
            ExecuteMovement(origin, target);
            Turn++;
            ChangePlayer();
        }

        private void ChangePlayer()
        {
            if (CurrentPlayer == Color.White)
            {
                CurrentPlayer = Color.Black;
            }
            else
            {
                CurrentPlayer = Color.White;
            }
        }

        private void PlacePieces()
        {
            Board.PlacePiece(new Tower(Board, Color.Black), new PositionChess('c', 1).toPosition());


            Board.PlacePiece(new King(Board, Color.White), new PositionChess('d', 4).toPosition());
        }

        public void ValidateOriginPosition(Position pos)
        {
            if (Board.Piece(pos) == null)
            {
                throw new BoardException("There's no Piece in the Origin Position");

            }
            if (CurrentPlayer != Board.Piece(pos).Color)
            {
                throw new BoardException("The Origin Piece isn't yours!");
            }
            if (!Board.Piece(pos).HasPossibleMovements())
            {
                throw new BoardException("There are no possible movements!");
            }
        }

        public void ValidateTargetPosition(Position origin, Position target)
        {
            if (!Board.Piece(origin).CanMoveTo(target))
            {
                throw new BoardException("Invalid target position");
            }
        }
    }
}
