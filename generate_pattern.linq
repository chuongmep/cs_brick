<Query Kind="Program" />

using System;

class Program
{
    static void Main(string[] args)
	{
		int areaWidth = 60;  // mm, can be 50
		int areaHeight = 60; // mm, 50
		int brickWidth = 10; // mm
		int brickHeight = 10; // mm
		int gapX = 10;        // mm
		int gapY = 10;        // mm
		int percentage = 30;  // percent

		int[,] pattern = CreateUniformPattern(areaWidth, areaHeight, brickWidth, brickHeight, gapX, gapY, percentage);
		PrintPattern(pattern);
	}

	static int[,] CreateUniformPattern(int areaWidth, int areaHeight, int brickWidth, int brickHeight, int gapX, int gapY, int percentage)
	{
		int cols = areaWidth / (brickWidth + gapX);
		int rows = areaHeight / (brickHeight + gapY);
		int totalCells = cols * rows;
		int bricksNeeded = (int)(totalCells * (percentage / 100.0));

		int[,] patternGrid = new int[rows, cols];
		Random random = new Random();

		for (int i = 0; i < bricksNeeded; i++)
		{
			int randRow, randCol;
			do
			{
				randRow = random.Next(rows);
				randCol = random.Next(cols);
			} while (patternGrid[randRow, randCol] == 1);

			patternGrid[randRow, randCol] = 1;
		}

		return patternGrid;
	}

	static void PrintPattern(int[,] grid)
	{
		for (int i = 0; i < grid.GetLength(0); i++)
		{
			for (int j = 0; j < grid.GetLength(1); j++)
			{
				Console.Write(grid[i, j] + " ");
			}
			Console.WriteLine();
		}
	}
}
