using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            TicTacToeGame t = new TicTacToeGame();
            t.Start();
        }
    }

    class TicTacToeGame
    {
        private static int[,] gameBoard =
        {
            {0,0,0},
            {0,0,0},
            {0,0,0}
        };
        private static string[] oxIcon = { " ", "o", "x" };
        public static bool yourTurn = true;
        public static bool isFinished = false;
        private static Regex regex = new Regex(@"[abc][012]", RegexOptions.IgnoreCase);

        public TicTacToeGame()
        {

        }

         public void Start()
        {
            yourTurn = true;
            ShowBoard();
            while (!isFinished)
            {
                if (yourTurn)
                {
                    movePlayer();
                }
                else
                {
                    moveAI();
                }
                ShowBoard();
                yourTurn = !yourTurn;
            }
        }

        private void movePlayer()
        {
            Console.Write("Player Input: ");
            string inline = Console.ReadLine();
            if (!regex.IsMatch(inline)) return;
            char[] chars = inline.ToCharArray();
            int x = 0, y = 0;
            switch (chars[0])
            {
                case 'a':
                    y = 0;
                    break;
                case 'b':
                    y = 1;
                    break;
                case 'c':
                    y = 2;
                    break;
            }
            switch (chars[1])
            {
                case '0':
                    x = 0;
                    break;
                case '1':
                    x = 1;
                    break;
                case '2':
                    x = 2;
                    break;
            }
            gameBoard[x, y] = 1;
        }

        private void moveAI()
        {
            Console.Write("AI Input: ");
            string inline = Console.ReadLine();
            if (!regex.IsMatch(inline)) return;
            char[] chars = inline.ToCharArray();
            int x = 0, y = 0;
            switch (chars[0])
            {
                case 'a':
                    y = 0;
                    break;
                case 'b':
                    y = 1;
                    break;
                case 'c':
                    y = 2;
                    break;
            }
            switch (chars[1])
            {
                case '0':
                    x = 0;
                    break;
                case '1':
                    x = 1;
                    break;
                case '2':
                    x = 2;
                    break;
            }
            gameBoard[x, y] = 2;
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
