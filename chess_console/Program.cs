using System;
using board;
using chess;

namespace chess_console
{
    class Program
    {

        static void Main(string[] args)
        {

            Board board = new Board(8, 8);

            board.PlacePiece(new Tower(board,Color.Black),new Position(0,0));
            board.PlacePiece(new King(board, Color.Black), new Position(2, 4));



            Screen.PrintBoard(board);




        }
    }
}
