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
            Console.ReadKey();
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
        private static bool yourTurn = true;
        private static Regex regex = new Regex(@"[abc][012]", RegexOptions.IgnoreCase);

        public TicTacToeGame()
        {

        }

        public void Start()
        {
            yourTurn = true;
            ShowBoard();
            while (!isFinished())
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
            if (gameBoard[x, y] == 0) gameBoard[x, y] = 1;
        }

        //private void moveAI()
        //{
        //    Console.Write("AI Input: ");
        //    string inline = Console.ReadLine();
        //    if (!regex.IsMatch(inline)) return;
        //    char[] chars = inline.ToCharArray();
        //    int x = 0, y = 0;
        //    switch (chars[0])
        //    {
        //        case 'a':
        //            y = 0;
        //            break;
        //        case 'b':
        //            y = 1;
        //            break;
        //        case 'c':
        //            y = 2;
        //            break;
        //    }
        //    switch (chars[1])
        //    {
        //        case '0':
        //            x = 0;
        //            break;
        //        case '1':
        //            x = 1;
        //            break;
        //        case '2':
        //            x = 2;
        //            break;
        //    }
        //    if (gameBoard[x, y] == 0) gameBoard[x, y] = 2;
        //}

        private void moveAI()
        {
            Console.Write("AI`s turn: ");
            int[,] points =
            {
                {0,0,0 },
                {0,0,0 },
                {0,0,0 }
            };
            int[] position = { 0, 0 };
            int[] line = { 0, 0, 0 };
            int maxPoint = -1;
            char a = default(char);

            for (int x = 0; x <= 2; x++)
            {
                for (int y = 0; y <= 2; y++)
                {
                    if (gameBoard[x, y] != 0)
                    {
                        points[x, y] = -1;
                        continue;
                    }

                    //row
                    line[0] = gameBoard[x, 0];
                    line[1] = gameBoard[x, 1];
                    line[2] = gameBoard[x, 2];
                    points[x, y] += calcPoint(line);

                    //column
                    line[0] = gameBoard[0, y];
                    line[1] = gameBoard[1, y];
                    line[2] = gameBoard[2, y];
                    points[x, y] += calcPoint(line);

                    //top-left to bottom-right
                    if (x == y)
                    {
                        line[0] = gameBoard[0, 0];
                        line[1] = gameBoard[1, 1];
                        line[2] = gameBoard[2, 2];
                        points[x, y] += calcPoint(line);
                    }

                    //top-right to bottom-left
                    if (x + y == 2)
                    {
                        line[0] = gameBoard[0, 2];
                        line[1] = gameBoard[1, 1];
                        line[2] = gameBoard[2, 0];
                        points[x, y] += calcPoint(line);
                    }

                    //center
                    if (x == 1)
                    {
                        points[x, y] += 1;
                    }
                    if (y == 1)
                    {
                        points[x, y] += 1;
                    }

                    if (points[x, y] > maxPoint)
                    {
                        maxPoint = points[x, y];
                        position[0] = x;
                        position[1] = y;
                    }
                }
            }

            gameBoard[position[0], position[1]] = 2;

            switch (position[1])
            {
                case 0:
                    a = 'a';
                    break;
                case 1:
                    a = 'b';
                    break;
                case 2:
                    a = 'c';
                    break;
            }

            Console.WriteLine("AI move to [" + a + "," + position[0] + "]");
        }


        private int calcPoint(int[] line)
        {
            int point = 0;
            if (line.Count(n => n == 2) == 2)
            {
                point += 1000;
            }
            if (line.Count(n => n == 1) == 2)
            {
                point += 900;
            }
            if (line.Count(n => n == 2) == 1 && line.Count(n => n == 1) == 0)
            {
                point += 100;
            }
            if (line.Count(n => n == 2) == 0 && line.Count(n => n == 1) == 1)
            {
                point += 90;
            }
            if (line.Count(n => n == 0) == 3)
            {
                point += 10;
            }
            return point;
        }


        private bool isFinished()
        {
            for (int x = 0; x <= 2; x++)
            {
                if (gameBoard[x, 0] == gameBoard[x, 1] && gameBoard[x, 1] == gameBoard[x, 2])
                {
                    if (gameBoard[x, 0] == 1)
                    {
                        Console.WriteLine("Player Win!");
                        return true;
                    }
                    else if (gameBoard[x, 0] == 2)
                    {
                        Console.WriteLine("AI Win!");
                        return true;
                    }
                }
                if (gameBoard[0, x] == gameBoard[1, x] && gameBoard[1, x] == gameBoard[2, x])
                {
                    if (gameBoard[0, x] == 1)
                    {
                        Console.WriteLine("Player Win!");
                        return true;
                    }
                    else if (gameBoard[0, x] == 2)
                    {
                        Console.WriteLine("AI Win!");
                        return true;
                    }
                }
            }
            if (gameBoard[0, 0] == gameBoard[1, 1] && gameBoard[1, 1] == gameBoard[2, 2])
            {
                if (gameBoard[0, 0] == 1)
                {
                    Console.WriteLine("Player Win!");
                    return true;
                }
                else if (gameBoard[0, 0] == 2)
                {
                    Console.WriteLine("AI Win!");
                    return true;
                }
            }
            if (gameBoard[0, 2] == gameBoard[1, 1] && gameBoard[1, 1] == gameBoard[2, 0])
            {
                if (gameBoard[0, 2] == 1)
                {
                    Console.WriteLine("Player Win!");
                    return true;
                }
                else if (gameBoard[0, 2] == 2)
                {
                    Console.WriteLine("AI Win!");
                    return true;
                }
            }
            foreach (int x in gameBoard)
            {
                if (x == 0) return false;
            }
            Console.WriteLine("Draw Game");
            return true;
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
