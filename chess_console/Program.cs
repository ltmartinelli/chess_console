using System;
using board;
using chess;

namespace chess_console
{
    class Program
    {

        static void Main(string[] args)
        {

            try
            {
                Board board = new Board(8, 8);

                board.PlacePiece(new Tower(board, Color.Black), new Position(0, 0));
                board.PlacePiece(new King(board, Color.Black), new Position(0, 1));



                Screen.PrintBoard(board);

            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);   
            }



        }
    }
}
