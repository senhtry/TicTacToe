using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            TicTacToeGame t = new TicTacToeGame();
            t.ShowBoard();
            Console.ReadKey();
        }
    }

    class TicTacToeGame
    {
        private static int[,] gameBoard =
        {
            {0,1,0},
            {0,2,1},
            {0,0,0}
        };
        private static string[] oxIcon = { " ", "o", "x" };
        private static bool yourTurn = true;
        private static bool isFinished = false;

        public TicTacToeGame()
        {

        }

        public void ShowBoard()
        {
            Console.WriteLine("     a   b   c  ");
            for (int i = 0; i <= 2; i++)
            {
                Console.WriteLine("   |---|---|---|");
                Console.Write(i + "  |");
                for (int j = 0; j <= 2; j++)
                {
                    Console.Write(" " + ConvertToOX(gameBoard[i, j]) + " |");
                }
                Console.WriteLine();
            }
            Console.WriteLine("   |---|---|---|");
        }

        private string ConvertToOX(int x)
        {
            string result;
            if (x >= 0 && x <= 2) result = oxIcon[x];
            else throw new ArgumentOutOfRangeException();
            return result;
        }
    }
}
