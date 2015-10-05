using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.IO;

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
            string titleSpeed = "0";
            int powerup = 0;
            int speedmodifier = 0;
            Console.Title = "Snake";

            OpeningScreen();
            string type = Console.ReadLine();
            char nextFood = '1';
            Console.WriteLine();
            Console.WriteLine("Press Enter to choose a level!");
            Console.ReadLine();
            Console.Clear();

            Console.SetCursorPosition(0, 3);
            Console.WriteLine("Choose Level from 1 to 9 and press Enter!");
            Console.SetCursorPosition(0, 5);
            Console.Write("Level : ");
            int level = 0;
            string levelAssigner = Console.ReadLine();
            if (int.TryParse(levelAssigner, out level))
            {
                if (level <= 9 && level > 0)
                {
                    level = Convert.ToInt32(levelAssigner);
                }
                else
                {
                    level = 0;
                }
            }
            else
            {
                level = 0;
            }

            Console.SetCursorPosition(0, 7);
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
                        if (direction != 1)
                        {
                            direction = 0;
                        }
                    }
                    if (Input.Key == ConsoleKey.A || Input.Key == ConsoleKey.LeftArrow)
                    {
                        if (direction != 2)
                        {
                            direction = 3;
                        }
                    }
                    if (Input.Key == ConsoleKey.W || Input.Key == ConsoleKey.UpArrow)
                    {
                        if (direction != 0)
                        {
                            direction = 1;
                        }
                    }
                    if (Input.Key == ConsoleKey.D || Input.Key == ConsoleKey.RightArrow)
                    {
                        if (direction != 3)
                        {
                            direction = 2;
                        }
                    }
                    if (Input.Key == ConsoleKey.Spacebar)
                    {
                        direction = 5;
                        Console.SetCursorPosition(20, 11);
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
                        EndScoreResults(score);
                        return;
                    }
                    SnakeBody.Enqueue(newHeadPosition);

                    char[] foodHolder = new char[]
                    {
                        '1', '2', '3', '4', '5', '6', '7', '8', '9', '$', 'S', 'F'
                        // here you can add random bonuses and stuff
                    };


                    if (newHeadPosition.x == food.x && newHeadPosition.y == food.y)
                    {
                        food = new Point(randomGenerator.Next(1, Console.WindowHeight - 1),
                            randomGenerator.Next(1, Console.WindowWidth - 1));
                        score = Score(nextFood, score);

                        nextFood = foodHolder[randomGenerator.Next(0, foodHolder.Length)];
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
                    Console.Title = "Snake - Score: " + score + " Speed: " + titleSpeed;

                    Thread.Sleep(150 - level * 12);

                }
            }
        }

        public static void EndScoreResults(int score)
        {
            var highScore = File.ReadAllText("highscore.txt");
            if (score > int.Parse(highScore))
            {
                Console.WriteLine("New High Score: {0}", score);
                Console.WriteLine();
                File.WriteAllText("highscore.txt", score.ToString());
            }
            else
            {
                Console.WriteLine("Your Score: {0}", score);
                Console.WriteLine();
                Console.SetCursorPosition(13, 13);
                Console.WriteLine("High Score: {0}", int.Parse(highScore));
                Console.WriteLine();
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

        public static char SnakeType(string type, int random)
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
                    snake = bodyHolder[random];
                    break;
            }
            return snake;
        }

        public static void Instructions()
        {
            Console.SetCursorPosition(0, 1);
            Console.WriteLine("Instructions");
            Console.WriteLine();
            Console.WriteLine("Move Up = \"W\" or \"Up Arrow\"\r\nMove Down = \"S\" or \"Down Arrow\"\r\n" +
                              "Move Left = \"A\" or \"Left Arrow\"\r\nMove Right = \"D\" or \"Right Arrow\"\r\n" +
                              "PAUSE = \"Space\"\r\n");
            Console.WriteLine("Numbers Increase score.\r\n");

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
                case '$':
                    score += 25;
                    break;
            }
            return score;
        }
    }
}