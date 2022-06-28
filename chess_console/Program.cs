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
                    try
                    {

                        Console.Clear();
                        Screen.PrintMatch(match);

                        Console.Write("Origin: ");
                        Position origin = Screen.ReadPosition().toPosition();
                        match.ValidateOriginPosition(origin);

                        bool[,] possiblePositions = match.Board.Piece(origin).PossibleMovements();


                        Console.Clear();
                        Screen.PrintBoard(match.Board, possiblePositions);


                        Console.Write("Target: ");
                        Position target = Screen.ReadPosition().toPosition();
                        match.ValidateTargetPosition(origin, target);

                        match.ExecutePlay(origin, target);
                    }
                    catch(BoardException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }





                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
