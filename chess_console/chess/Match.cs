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
        public Piece VulnerableEnPassant { get; private set; }


        public Match()
        {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            HasFinished = false;
            Check = false;
            Pieces = new HashSet<Piece>();
            VulnerableEnPassant = null;
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

            //Special PLay Minor Castling
            if (p is King && target.Row == origin.Row + 2)
            {
                Position originT = new Position(origin.Line, origin.Row + 3);
                Position targetT = new Position(origin.Line, origin.Row + 1);
                Piece t = Board.RemovePiece(originT);
                t.IncrementMovements();
                Board.PlacePiece(t, targetT);
            }

            //Special PLay Major Castling
            if (p is King && target.Row == origin.Row - 2)
            {
                Position originT = new Position(origin.Line, origin.Row - 4);
                Position targetT = new Position(origin.Line, origin.Row - 1);
                Piece t = Board.RemovePiece(originT);
                t.IncrementMovements();
                Board.PlacePiece(t, targetT);
            }

            //En Passant

            if (p is Pawn)
            {
                if (origin.Row != target.Row && capturedPiece == null)
                {
                    Position posP;

                    if (p.Color == Color.White)
                    {
                        posP = new Position(target.Line+1, target.Row);
                    }
                    else
                    {
                        posP = new Position(target.Line-1, target.Row);
                    }

                    capturedPiece = Board.RemovePiece(posP);
                    CapturedPieces.Add(capturedPiece);
                    

                }
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


            //Special PLay Minor Castling
            if (p is King && target.Row == origin.Row + 2)
            {
                Position originT = new Position(origin.Line, origin.Row + 3);
                Position targetT = new Position(origin.Line, origin.Row + 1);
                Piece t = Board.RemovePiece(targetT);
                t.DecrementMovements();
                Board.PlacePiece(t, originT);
            }
            //Special PLay Major Castling
            if (p is King && target.Row == origin.Row - 2)
            {
                Position originT = new Position(origin.Line, origin.Row - 4);
                Position targetT = new Position(origin.Line, origin.Row - 1);
                Piece t = Board.RemovePiece(targetT);
                t.DecrementMovements();
                Board.PlacePiece(t, originT);
            }
            //En Passant
            if (p is Pawn)
            {
                if(origin.Row != target.Row && capturedPiece == VulnerableEnPassant)
                {
                    Piece pawn = Board.RemovePiece(target);
                    Position posP;
                    if (p.Color == Color.White)
                    {
                        posP = new Position(3, target.Row);
                    }
                    else
                    {
                        posP = new Position(4, target.Row);
                    }
                    Board.PlacePiece(pawn, posP);
                }
            }


        }

        public void ExecutePlay(Position origin, Position target)
        {
            Piece capturedPiece = ExecuteMovement(origin, target);

            if (IsInCheck(CurrentPlayer))
            {
                UndoMovement(origin, target, capturedPiece);
                throw new BoardException("You can't put your King in Check!");
            }

            Piece p = Board.Piece(target);

            //Promotion
            
            if (p is Pawn)
            {
                if (p.Color == Color.White && target.Line == 0 || p.Color == Color.Black && target.Line == 7)
                {
                    p = Board.RemovePiece(target);
                    Pieces.Remove(p);
                    Piece queen = new Queen(Board, p.Color);
                    Board.PlacePiece(queen, target);
                    Pieces.Add(queen);
                }
            }


            if (IsInCheck(Opponent(CurrentPlayer)))
            {
                Check = true;
            }
            else
            {
                Check = false;
            }

            if (TestCheckMate(Opponent(CurrentPlayer)))
            {
                HasFinished = true;
            }
            else
            {
                Turn++;
                ChangePlayer();
            }

           

            //Special Play En Passant
            if (p is Pawn && target.Line == origin.Line - 2 || target.Line == origin.Line + 2)
            {
                VulnerableEnPassant = p;
            }
            else
            {
                VulnerableEnPassant = null;
            }


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
            PlaceNewPiece('a', 1, new Tower(Board, Color.White));
            PlaceNewPiece('b', 1, new Knight(Board, Color.White));
            PlaceNewPiece('c', 1, new Bishop(Board, Color.White));
            PlaceNewPiece('d', 1, new Queen(Board, Color.White));
            PlaceNewPiece('e', 1, new King(Board, Color.White, this));
            PlaceNewPiece('f', 1, new Bishop(Board, Color.White));
            PlaceNewPiece('g', 1, new Knight(Board, Color.White));
            PlaceNewPiece('h', 1, new Tower(Board, Color.White));
            PlaceNewPiece('b', 2, new Pawn(Board, Color.White, this));
            PlaceNewPiece('c', 2, new Pawn(Board, Color.White, this));
            PlaceNewPiece('d', 2, new Pawn(Board, Color.White, this));
            PlaceNewPiece('a', 2, new Pawn(Board, Color.White, this));
            PlaceNewPiece('e', 2, new Pawn(Board, Color.White, this));
            PlaceNewPiece('f', 2, new Pawn(Board, Color.White, this));
            PlaceNewPiece('g', 2, new Pawn(Board, Color.White, this));
            PlaceNewPiece('h', 2, new Pawn(Board, Color.White, this));
            
            PlaceNewPiece('a', 8, new Tower(Board, Color.Black));
            PlaceNewPiece('b', 8, new Knight(Board, Color.Black));
            PlaceNewPiece('c', 8, new Bishop(Board, Color.Black));
            PlaceNewPiece('d', 8, new Queen(Board, Color.Black));
            PlaceNewPiece('e', 8, new King(Board, Color.Black, this));
            PlaceNewPiece('f', 8, new Bishop(Board, Color.Black));
            PlaceNewPiece('g', 8, new Knight(Board, Color.Black));
            PlaceNewPiece('h', 8, new Tower(Board, Color.Black));
            PlaceNewPiece('a', 7, new Pawn(Board, Color.Black, this));
            PlaceNewPiece('b', 7, new Pawn(Board, Color.Black, this));
            PlaceNewPiece('c', 7, new Pawn(Board, Color.Black, this));
            PlaceNewPiece('d', 7, new Pawn(Board, Color.Black, this));
            PlaceNewPiece('e', 7, new Pawn(Board, Color.Black, this));
            PlaceNewPiece('f', 7, new Pawn(Board, Color.Black, this));
            PlaceNewPiece('g', 7, new Pawn(Board, Color.Black, this));
            PlaceNewPiece('h', 7, new Pawn(Board, Color.Black, this));

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

        public bool TestCheckMate(Color color)
        {
            if (!IsInCheck(color))
            {
                return false;
            }
            foreach (Piece x in PiecesInPlayOfColor(color))
            {
                bool[,] mat = x.PossibleMovements();
                for (int i = 0; i < Board.Lines; i++)
                {
                    for (int j = 0; j < Board.Rows; j++)
                    {
                        if (mat[i, j])
                        {
                            Position origin = x.Position;
                            Position target = new Position(i, j);
                            Piece capturedPiece = ExecuteMovement(origin, target);
                            bool testCheck = IsInCheck(color);
                            UndoMovement(origin, target, capturedPiece);
                            if (!testCheck)
                            {
                                return false;
                            }

                        }
                    }
                }
            }

            return true;
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
