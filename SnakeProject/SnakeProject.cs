using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace SnakeProject
{
    class Point
    {
        public int x;
        public int y;
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    class SnakeProject
    {
        static void Main(string[] args)
        {
            Console.BufferHeight = Console.WindowHeight = 25;
            Console.BufferWidth = Console.WindowWidth = 50;
            int SCORE = 0;
            Console.Title = "Snake";
            OpeningScreen();
            string type = Console.ReadLine();
            char nextFood = '1';
            Console.WriteLine();
            Console.WriteLine("Press Enter to see the Instructions!");
            Console.ReadLine();

            Console.Clear();

            Instructions();
            Console.ReadLine();
            Console.CursorVisible = false;

            Random randomGenerator = new Random();
            int randomSnake = randomGenerator.Next(0, 4);
            
            Point food = new Point(randomGenerator.Next(0, Console.WindowHeight),
                randomGenerator.Next(0, Console.WindowWidth));
            Console.SetCursorPosition(food.y, food.x);
            Console.Write("$");

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
                Console.SetCursorPosition(position.y, position.x);
                Console.Write(SnakeType(type, randomSnake));
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

                Point head = SnakeBody.Last();
                Point newDirection = directions[direction];
                Point newHeadPosition = new Point(head.x + newDirection.x, head.y + newDirection.y);

                if (newHeadPosition.x < 0 || newHeadPosition.y < 0 ||
                    newHeadPosition.x >= Console.WindowHeight || newHeadPosition.y >= Console.WindowWidth)
                {
                    Console.SetCursorPosition(13, 11);
                    Console.WriteLine("Sorry Dude, GAME OVER !");
                    Console.WriteLine();
                    Console.SetCursorPosition(13, 13);
                    Console.WriteLine("Your Score is {0}", SCORE);
                    Console.WriteLine();
                    return;
                }

                SnakeBody.Enqueue(newHeadPosition);

                char[] foodHolder = new char[]
                {
                    '1', '2', '3', '4', '5', '6', '7', '8','9', 'D', 'T' // here you can add random bonuses and stuff
                };
                

                if (newHeadPosition.x == food.x && newHeadPosition.y == food.y)
                {
                    food = new Point(randomGenerator.Next(1, Console.WindowHeight-1),
                randomGenerator.Next(1, Console.WindowWidth-1));
                   
                    switch (nextFood)
                    {
                        case '1':
                            SCORE += 1;
                            break;
                        case '2':
                            SCORE += 2;
                            break;
                        case '3':
                            SCORE += 3;
                            break;
                        case '4':
                            SCORE += 4;
                            break;
                        case '5':
                            SCORE += 5;
                            break;
                        case '6':
                            SCORE += 6;
                            break;
                        case '7':
                            SCORE += 7;
                            break;
                        case '8':
                            SCORE += 8;
                            break;
                        case '9':
                            SCORE += 9;
                            break;
                        case 'D':
                            SCORE *= 2; //Doubles the score
                            break;
                        case 'T':
                            SCORE *= 3; //Triples the score
                            break;
                    }
                    nextFood = foodHolder[randomGenerator.Next(0, 11)];
                }
                else
                {
                    SnakeBody.Dequeue();
                }

                Console.Clear();

                foreach (Point position in SnakeBody)
                {
                    Console.SetCursorPosition(position.y, position.x);
                    Console.Write(SnakeType(type, randomSnake));
                }
                Console.SetCursorPosition(food.y, food.x);
                Console.Write(nextFood);
                Console.Title = "Snake - Score: " + SCORE;
                if (SCORE < 550)
                {
                    Thread.Sleep(150 - SCORE/5); //increasing the speed by substracting the current score from the sleep time
                }
                else
                {
                    Thread.Sleep(150 - 110); //Max snake speed (can't go below this border)
                }

            }
        }
        private static void OpeningScreen()
        {
            Console.SetCursorPosition(9, 2);
            Console.WriteLine("Welcome to Snake Game !");
            Console.WriteLine();
            Console.WriteLine("Choose a snake type: \r\n");
            Console.WriteLine("Press 1 for: OOOOO");
            Console.WriteLine("Press 2 for: *****");
            Console.WriteLine("Press 3 for: #####");
            Console.WriteLine("Press 4 for: @@@@@");
            Console.WriteLine();
            Console.WriteLine("(Leave blank for a random snake body)\r\n");
            Console.Write("Press :  ");

        }
        public static char[] bodyHolder = new char[]
                {
                    'O', '*', '#', '@'
                };
        
        private static char SnakeType(string type, int random)
        {
            char snake = ' ';
            
            switch (type)
            {
                case "1":
                    snake = bodyHolder[0];
                    break;
                case "2":
                    snake = bodyHolder[1];
                    break;
                case "3":
                    snake = bodyHolder[2];
                    break;
                case "4":
                    snake = bodyHolder[3];
                    break;
                default:
                    snake = bodyHolder[random]; // not finished
                    break;
            }
            return snake;
        }
        private static void Instructions()
        {
            Console.SetCursorPosition(0, 1);
            Console.WriteLine("Instructions");
            Console.WriteLine();
            Console.WriteLine("Move Up = \"W\"\r\nMove Down = \"S\"\r\nMove Left = \"A\"\r\nMove Right = \"D\"\r\n");
            Console.WriteLine("Numbers Increase score.");
            Console.WriteLine("D - doubles the score.");
            Console.WriteLine("T - triples it.");
            Console.WriteLine("Difficulty increases as score goes up!");
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
//            y--;
//            if (y < 0)
//            {
//                y = 0;
//            }
//        }
//        else if (pressedKey.Key == ConsoleKey.DownArrow)
//        {
//            y++;
//        }
//        Console.Clear();

//        Console.SetCursorPosition(x, y);
//        Console.Write("$");
//    }

//    else if (pressedKey.Key == ConsoleKey.LeftArrow || pressedKey.Key == ConsoleKey.RightArrow)
//    {
//        if (pressedKey.Key == ConsoleKey.LeftArrow)
//        {
//            x--;
//            if (x < 0)
//            {
//                x = 0;
//            }
//        }
//        else if (pressedKey.Key == ConsoleKey.RightArrow)
//        {
//            x++;
//        }
//        Console.Clear();

//        Console.SetCursorPosition(x, y);
//        Console.Write("$");
//    }

//}
