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
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BufferHeight = Console.WindowHeight = 25;
            Console.BufferWidth = Console.WindowWidth = 50;

            int score = 0;
            Console.Title = "Snake";
            OpeningScreen();
            string type = Console.ReadLine();
            char nextFood = '1';
            Console.WriteLine();
            Console.WriteLine("Press Enter to see the Instructions!");
            Console.ReadLine();

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Instructions();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ReadLine();
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Yellow;

            Random randomGenerator = new Random();
            int randomSnake = randomGenerator.Next(0, 4);

            Point food = new Point(randomGenerator.Next(0, Console.WindowHeight),
                randomGenerator.Next(0, Console.WindowWidth));
            Console.SetCursorPosition(food.y, food.x);
            Console.Write(nextFood);

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
                    if (Input.Key == ConsoleKey.S || Input.Key == ConsoleKey.DownArrow)
                    {
                        direction = 0;
                    }
                    if (Input.Key == ConsoleKey.A || Input.Key == ConsoleKey.LeftArrow)
                    {
                        direction = 3;
                    }
                    if (Input.Key == ConsoleKey.W || Input.Key == ConsoleKey.UpArrow)
                    {
                        direction = 1;
                    }
                    if (Input.Key == ConsoleKey.D || Input.Key == ConsoleKey.RightArrow)
                    {
                        direction = 2;
                    }
                    if (Input.Key == ConsoleKey.Spacebar)
                    {
                        direction = 5;
                        Console.SetCursorPosition(20,11);
                        Console.WriteLine("PAUSE !");
                    }
                }
                if (direction != 5)
                {
                    Point head = SnakeBody.Last();
                    Point newDirection = directions[direction];
                    Point newHeadPosition = new Point(head.x + newDirection.x, head.y + newDirection.y);

                    if (newHeadPosition.x < 0 || newHeadPosition.y < 0 ||
                        newHeadPosition.x >= Console.WindowHeight || newHeadPosition.y >= Console.WindowWidth)
                    {
                        Console.SetCursorPosition(13, 9);
                        Console.WriteLine("Sorry Dude, GAME OVER !");
                        Console.WriteLine();
                        Console.SetCursorPosition(13, 11);
                        Console.WriteLine("Your Score is: {0}", score);
                        Console.WriteLine();
                        return;
                    }
                    SnakeBody.Enqueue(newHeadPosition);

                    char[] foodHolder = new char[]
                    {
                        '1', '2', '3', '4', '5', '6', '7', '8', '9', 'D', 'T', 'R'
                        // here you can add random bonuses and stuff
                    };


                    if (newHeadPosition.x == food.x && newHeadPosition.y == food.y)
                    {
                        food = new Point(randomGenerator.Next(1, Console.WindowHeight - 1),
                            randomGenerator.Next(1, Console.WindowWidth - 1));

                        score = Score(nextFood, score);

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
                    Console.Title = "Snake - Score: " + score;
                    if (score < 550)
                    {
                        Thread.Sleep(150 - score/5);
                            //increasing the speed by substracting the current score from the sleep time
                    }
                    else
                    {
                        Thread.Sleep(150 - 110); //Max snake speed (can't go below this border)
                    }
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
            Console.WriteLine("Move Up = \"W\" or \"UpArrow\"\r\nMove Down = \"S\" or \"DownArrow\"\r\n" +
                              "Move Left = \"A\" or \"LeftArrow\"\r\nMove Right = \"D\" or \"RightArrow\"\r\n" +
                              "PAUSE = \"Space\"\r\n");
            Console.WriteLine("Numbers Increase score.");
            Console.WriteLine("D - doubles the score.");
            Console.WriteLine("T - triples it.");
            Console.WriteLine("R - divides the score.");
            Console.WriteLine("Difficulty increases as score goes up!\r\n");
            Console.WriteLine("Good Luck ! :)\r\n");
            Console.WriteLine("Press Enter to begin the game !");
        }

        static int Score(char nextFood, int score)
        {
            switch (nextFood)
            {
                case '1':
                    score += 1;
                    break;
                case '2':
                    score += 2;
                    break;
                case '3':
                    score += 3;
                    break;
                case '4':
                    score += 4;
                    break;
                case '5':
                    score += 5;
                    break;
                case '6':
                    score += 6;
                    break;
                case '7':
                    score += 7;
                    break;
                case '8':
                    score += 8;
                    break;
                case '9':
                    score += 9;
                    break;
                case 'D':
                    score *= 2; //Doubles the score
                    break;
                case 'T':
                    score *= 3; //Triples the score
                    break;
                case 'R':
                    score /= 2; //Divides the score
                    break;
            }
            return score;
        }
    }
}


