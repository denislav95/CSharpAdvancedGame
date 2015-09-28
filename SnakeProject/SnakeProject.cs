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
        List<char> snake = new List<char>();
        snake.Add('@');
        
        //snake.Add();
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
    }
}
