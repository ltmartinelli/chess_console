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
                Match match = new Match();


                Screen.PrintBoard(match.Board);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
