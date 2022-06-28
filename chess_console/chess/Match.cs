using System;
using Boardgame;
using chess;
using System.Collections.Generic;

namespace chess
{
    class Match
    {

        public Board Board { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool HasFinished { get; private set; }
        private HashSet<Piece> Pieces;
        private HashSet<Piece> CapturedPieces;


        public Match()
        {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Pieces = new HashSet<Piece>();
            CapturedPieces = new HashSet<Piece>();
            PlacePieces();
            HasFinished = false;
        }

        public void ExecuteMovement(Position origin, Position target)
        {
            Piece p = Board.RemovePiece(origin);
            p.IncrementMovements();
            Piece capturedPiece = Board.RemovePiece(target);
            Board.PlacePiece(p, target);

            if (capturedPiece != null)
            {
                CapturedPieces.Add(capturedPiece);
            }

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
        
        private void PlacePieces()
        {
            PlaceNewPiece('c', 1, new Tower(Board, Color.White));
                   
        }

        public HashSet<Piece> CapturedPiecesOfColor(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in CapturedPieces)
            {
                if (x.Color == color)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Piece> PiecesInPlayOfColor(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in Pieces)
            {
                if (x.Color == color)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(CapturedPiecesOfColor(color));
            return aux;
        }



        public void PlaceNewPiece(char row, int line, Piece piece)
        {
            Board.PlacePiece(piece, new PositionChess(row, line).toPosition());
            Pieces.Add(piece);
        }
        
    }
}
