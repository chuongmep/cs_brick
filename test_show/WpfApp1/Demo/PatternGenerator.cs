using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

public class PatternGenerator
{
    public static IEnumerable<int[]> GetCombinations(int n, int k)
    {
        int[] result = new int[k];
        Stack<int> stack = new Stack<int>();
        stack.Push(0);

        while (stack.Count > 0)
        {
            int index = stack.Count - 1;
            int value = stack.Pop();

            while (value < n)
            {
                result[index++] = value++;
                stack.Push(value);
                if (index == k)
                {
                    yield return (int[])result.Clone();
                    break;
                }
            }
        }
    }

    public static IEnumerable<int[,]> CreateAllPatterns(int areaWidth, int areaHeight, int brickWidth, int brickHeight,
        int gapX, int gapY, int percentage)
    {
        int cols = areaWidth / (brickWidth + gapX);
        int rows = areaHeight / (brickHeight + gapY);
        int totalCells = cols * rows;
        int bricksNeeded = totalCells * percentage / 100;

        foreach (var combination in GetCombinations(totalCells, bricksNeeded))
        {
            var pattern = new int[rows, cols];
            foreach (int pos in combination)
            {
                pattern[pos / cols, pos % cols] = 1;
            }

            yield return pattern;
        }
    }

    /// <summary>
    /// Save image depicting the pattern as a PNG file
    /// </summary>
    /// <param name="pattern"></param>
    /// <param name="filename"></param>
    public static void SavePatternAsImage(int[,] pattern, string filename)
    {
        int cellSize = 100; // Pixel size of each cell
        int rows = pattern.GetLength(0);
        int cols = pattern.GetLength(1);
        using (var bitmap = new Bitmap(cols * cellSize, rows * cellSize))
        {
            using (var g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White); // Optional: fill the background if desired

                // Draw each cell
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        using (var brush = new SolidBrush(pattern[i, j] == 1 ? Color.Black : Color.SandyBrown))
                        {
                            g.FillRectangle(brush, j * cellSize, i * cellSize, cellSize, cellSize);
                        }
                    }
                }

                // Draw grid lines (cell borders)
                using (var pen = new Pen(Color.Gray, 2)) // Change the color and thickness as needed
                {
                    for (int i = 0; i <= rows; i++)
                    {
                        g.DrawLine(pen, 0, i * cellSize, cols * cellSize, i * cellSize);
                    }

                    for (int j = 0; j <= cols; j++)
                    {
                        g.DrawLine(pen, j * cellSize, 0, j * cellSize, rows * cellSize);
                    }
                }

                // Draw boundary around the entire image
                using (var pen = new Pen(Color.Black, 2)) // More prominent boundary
                {
                    g.DrawRectangle(pen, 0, 0, cols * cellSize - 1, rows * cellSize - 1);
                }
            }
            using (var ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);
                File.WriteAllBytes(filename, ms.ToArray());
            }

        }
    }

    /// <summary>
    /// Save image depicting the pattern as a PNG file
    /// </summary>
    /// <param name="pattern">Pattern in a jagged array format</param>
    /// <param name="filename">Path to save the PNG file</param>
    public static void SavePatternAsImage(int[][] pattern, string filename)
    {
        int cellSize = 100; // Pixel size of each cell
        int rows = pattern.Length;
        int cols = pattern[0].Length; // Assuming all rows have the same number of columns
        using (var bitmap = new Bitmap(cols * cellSize, rows * cellSize))
        {
            using (var g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White); // Optional: fill the background if desired

                // Draw each cell
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        using (var brush = new SolidBrush(pattern[i][j] == 1 ? Color.Black : Color.SandyBrown))
                        {
                            g.FillRectangle(brush, j * cellSize, i * cellSize, cellSize, cellSize);
                        }
                    }
                }

                // Draw grid lines (cell borders)
                using (var pen = new Pen(Color.Gray, 1)) // Change the color and thickness as needed
                {
                    for (int i = 0; i <= rows; i++)
                    {
                        g.DrawLine(pen, 0, i * cellSize, cols * cellSize, i * cellSize);
                    }

                    for (int j = 0; j <= cols; j++)
                    {
                        g.DrawLine(pen, j * cellSize, 0, j * cellSize, rows * cellSize);
                    }
                }

                // Draw boundary around the entire image
                using (var pen = new Pen(Color.Black, 2)) // More prominent boundary
                {
                    g.DrawRectangle(pen, 0, 0, cols * cellSize - 1, rows * cellSize - 1);
                }
            }

            using (var ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);
                File.WriteAllBytes(filename, ms.ToArray());
            }
        }
    }
}