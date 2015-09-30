using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class SnakeProject
{
    static void Main(string[] args)
    {
        Console.BufferHeight = Console.WindowHeight = 25;
        Console.BufferWidth = Console.WindowWidth = 100;
        int height = Console.WindowHeight/2;
        int width = Console.WindowWidth;

        char[,] matrix = new char[height, width];
        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                if (col==0 || col == width - 1)
                {
                    matrix[row, col] = '#'; // sides
                }
                if (row == 0 || row == height - 1)
                {
                    matrix[row, col] = '#'; //top & bottom
                }
                
                Console.Write(matrix[row,col]);
            }
            Console.WriteLine();
        }
        int snakeX = 0;
        int snakeY = 0;

        while (true)
        {
            ConsoleKeyInfo pressedKey = Console.ReadKey();
            if (pressedKey.Key == ConsoleKey.UpArrow || pressedKey.Key == ConsoleKey.DownArrow)
            {
                if (pressedKey.Key == ConsoleKey.UpArrow)
                {
                    snakeY--;
                    if (snakeY < 0)
                    {
                        snakeY = 0;
                    }
                }
                else if (pressedKey.Key == ConsoleKey.DownArrow)
                {
                    snakeY++;
                }
                Console.Clear();

                Console.SetCursorPosition(snakeX, snakeY);
                Console.Write(SnakeType(type));
            }

            else if (pressedKey.Key == ConsoleKey.LeftArrow || pressedKey.Key == ConsoleKey.RightArrow)
            {
                if (pressedKey.Key == ConsoleKey.LeftArrow)
                {
                    snakeX--;
                    if (snakeX < 0)
                    {
                        snakeX = 0;
                    }
                }
                else if (pressedKey.Key == ConsoleKey.RightArrow)
                {
                    snakeX++;
                }
                Console.Clear();

                Console.SetCursorPosition(snakeX, snakeY);
                Console.Write(SnakeType(type));
            }

        }
    }
}
