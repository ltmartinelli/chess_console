using System;
using Boardgame;
using chess;

namespace chess_console
{
    class Program
    {

        static void Main(string[] args)
        {
            try
            {
                Match match = new Match();

                while (!match.HasFinished)
                {
                    Console.Clear();

                    Screen.PrintBoard(match.Board);

                    Console.Write("Origin: ");
                    Position origin = Screen.ReadPosition().toPosition();

                    bool[,] possiblePositions = match.Board.Piece(origin).PossibleMovements();


                    Console.Clear();
                    Screen.PrintBoard(match.Board, possiblePositions);


                    Console.Write("Target: ");
                    Position target = Screen.ReadPosition().toPosition();

                    match.ExecuteMovement(origin, target);





                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
