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
        public bool Check { get; private set; }


        public Match()
        {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            HasFinished = false;
            Check = false;
            Pieces = new HashSet<Piece>();
            CapturedPieces = new HashSet<Piece>();

            PlacePieces();
        }

        public Piece ExecuteMovement(Position origin, Position target)
        {
            Piece p = Board.RemovePiece(origin);
            p.IncrementMovements();
            Piece capturedPiece = Board.RemovePiece(target);
            Board.PlacePiece(p, target);

            if (capturedPiece != null)
            {
                CapturedPieces.Add(capturedPiece);
            }

            return capturedPiece;

        }

        public void UndoMovement(Position origin, Position target, Piece capturedPiece)
        {
            Piece p = Board.RemovePiece(target);
            p.DecrementMovements();
            if (capturedPiece != null)
            {
                Board.PlacePiece(capturedPiece, target);
                CapturedPieces.Remove(capturedPiece);
            }
            Board.PlacePiece(p, origin);
        }

        public void ExecutePlay(Position origin, Position target)
        {
            Piece capturedPiece = ExecuteMovement(origin, target);

            if (IsInCheck(CurrentPlayer)){
                UndoMovement(origin, target, capturedPiece);
                throw new BoardException("You can't put your King in Check!");
            }
            if (IsInCheck(Opponent(CurrentPlayer)))
            {
                Check = true;
            }
            else
            {
                Check = false;
            }

            
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

        private Color Opponent(Color color)
        {
            if (color == Color.White)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }

        }

        private Piece King(Color color)
        {
            foreach (Piece x in PiecesInPlayOfColor(color))
            {
                if (x is King)
                {
                    return x;
                }
            }
            return null;
        }

        public bool IsInCheck(Color color)
        {
            Piece k = King(color);


            foreach (Piece x in PiecesInPlayOfColor(Opponent(color)))
            {
                bool[,] mat = x.PossibleMovements();
                if (mat[k.Position.Line, k.Position.Row])
                {
                    return true;
                }
            }

            return false;

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
