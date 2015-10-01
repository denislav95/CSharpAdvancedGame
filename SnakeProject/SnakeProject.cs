using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SnakeProject
{
    class Point
    {
        public int snakeX;
        public int snakeY;
        public Point(int x, int y)
        {
            snakeX = x;
            snakeY = y;
        }
    }

    class SnakeProject
    {
        static void Main(string[] args)
        {
            OpeningScreen();
            Console.Write("Press :  ");
            int type = int.Parse(Console.ReadLine());
            Console.WriteLine("Press Enter to begin!");
            Console.ReadLine();
            Console.Clear();
            Console.CursorVisible = false;

            Point[] directions = new Point[]
            {
                new Point(1, 0), // goingDown
                new Point(-1, 0), // goingUp
                new Point(0, 1), // goingRight
                new Point(0, -1) // goingLeft
            };
            int direction = 2; // holds the direction which the snake is moving

            Queue<Point> SnakeBody = new Queue<Point>();
            for (int i = 0; i < 5; i++)
            {
                SnakeBody.Enqueue(new Point(0, i));
            }
            foreach (Point position in SnakeBody)
            {
                Console.SetCursorPosition(position.snakeY, position.snakeX);
                Console.Write("*");
            }

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo Input = Console.ReadKey();
                    if (Input.Key == ConsoleKey.S)
                    {
                        direction = 0;
                    }
                    if (Input.Key == ConsoleKey.A)
                    {
                        direction = 3;
                    }
                    if (Input.Key == ConsoleKey.W)
                    {
                        direction = 1;
                    }
                    if (Input.Key == ConsoleKey.D)
                    {
                        direction = 2;
                    }
                }
                SnakeBody.Dequeue();
                Point head = SnakeBody.Last();
                Point newDirection = directions[direction];
                Point newHeadPosition = new Point(head.snakeX + newDirection.snakeX, head.snakeY + newDirection.snakeY);
                SnakeBody.Enqueue(newHeadPosition);

                Console.Clear();

                foreach (Point position in SnakeBody)
                {
                    Console.SetCursorPosition(position.snakeY, position.snakeX);
                    Console.Write("*");
                }

                Thread.Sleep(75);
            }
        }
        private static void OpeningScreen()
        {
            Console.WriteLine("Welcome to Snake Game !");
            Console.WriteLine();
            Console.WriteLine("Choose a snake type: ");
            Console.WriteLine("Press 1 for Avenger:  O");
            Console.WriteLine("Press 2 for Magician:  *");
            Console.WriteLine("Press 3 for Warrior:  #");
            Console.WriteLine("Press 4 for Dark Lord:  @");
        }
    }
}


            //for (int row = 0; row < height; row++)
            //{
            //    for (int col = 0; col < width; col++)
            //    {
            //        if (col == 0 || col == width - 1)
            //        {
            //            matrix[row, col] = '#'; // sides
            //        }
            //        if (row == 1 || row == height - 1)
            //        {
            //            matrix[row, col] = '#'; //top & bottom
            //        }
            //        
            //    }
            //}
            //
            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            //while (true)
            //{
            //    ConsoleKeyInfo pressedKey = Console.ReadKey();
            //    if (pressedKey.Key == ConsoleKey.UpArrow || pressedKey.Key == ConsoleKey.DownArrow)
            //    {
            //        if (pressedKey.Key == ConsoleKey.UpArrow)
            //        {
            //            snakeY--;
            //            if (snakeY < 0)
            //            {
            //                snakeY = 0;
            //            }
            //        }
            //        else if (pressedKey.Key == ConsoleKey.DownArrow)
            //        {
            //            snakeY++;
            //        }
            //        Console.Clear();

            //        Console.SetCursorPosition(snakeX, snakeY);
            //        Console.Write("$");
            //    }

            //    else if (pressedKey.Key == ConsoleKey.LeftArrow || pressedKey.Key == ConsoleKey.RightArrow)
            //    {
            //        if (pressedKey.Key == ConsoleKey.LeftArrow)
            //        {
            //            snakeX--;
            //            if (snakeX < 0)
            //            {
            //                snakeX = 0;
            //            }
            //        }
            //        else if (pressedKey.Key == ConsoleKey.RightArrow)
            //        {
            //            snakeX++;
            //        }
            //        Console.Clear();

            //        Console.SetCursorPosition(snakeX, snakeY);
            //        Console.Write("$");
            //    }

            //}
