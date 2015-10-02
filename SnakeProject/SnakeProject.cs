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
            Console.BufferHeight = Console.WindowHeight = 25;
            Console.BufferWidth = Console.WindowWidth = 50;
            
            OpeningScreen();
            int type = int.Parse(Console.ReadLine());
            Console.WriteLine();
            Console.WriteLine("Press Enter to see the Instructions!");
            Console.ReadLine();

            Console.Clear();

            Instructions();
            Console.ReadLine();
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
                Console.Write(SnakeType(type));
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
                    Console.Write(SnakeType(type));
                }

                Thread.Sleep(75);
            }
        }
        private static void OpeningScreen()
        {
            Console.SetCursorPosition(9,2);
            Console.WriteLine("Welcome to Snake Game !");
            Console.WriteLine();
            Console.WriteLine("Choose a snake type: \r\n");
            Console.WriteLine("Press 1 for Avenger:  OOOOO");
            Console.WriteLine("Press 2 for Magician:  *****");
            Console.WriteLine("Press 3 for Warrior:  #####");
            Console.WriteLine("Press 4 for Dark Lord:  @@@@@\r\n");
            Console.Write("Press :  ");
            
        }
        private static char SnakeType(int type)
        {
            char snake = ' ';
            switch (type)
            {
                case 1:
                    snake = 'O';
                    break;
                case 2:
                    snake = '*';
                    break;
                case 3:
                    snake = '#';
                    break;
                case 4:
                    snake = '@';
                    break;
            }
            return snake;
        }
        private static void Instructions()
        {
            Console.SetCursorPosition(0,1);
            Console.WriteLine("Instructions");
            Console.WriteLine();
            Console.WriteLine("Move Up = \"W\"\r\nMove Down = \"S\"\r\nMove Left = \"A\"\r\nMove Right = \"D\"\r\n");
            Console.WriteLine("Good Luck ! :)\r\n");
            Console.WriteLine("Press Enter to begin the game !");
            //After making the food Generator we'll end the instructions
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
