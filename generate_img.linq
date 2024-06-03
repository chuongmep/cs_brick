<Query Kind="Program">
  <Namespace>System.Drawing</Namespace>
</Query>

class Program
{
    static void Main(string[] args)
    {
        int[,] matrix = {
            { 1, 0, 1, 0, 1 },
            { 0, 1, 0, 1, 0 },
            { 1, 0, 1, 0, 1 },
            { 0, 1, 0, 1, 0 },
            { 1, 0, 1, 0, 1 }
        };

        string folderPath = @"D:\API\Revit\cs_brick"; // Specify your folder path here
        string fileName = "matrix_image.png";

        CreateAndSaveImageFromMatrix(matrix, folderPath, fileName);
    }

    static void CreateAndSaveImageFromMatrix(int[,] matrix, string folderPath, string fileName)
    {
        int cellSize = 50; // Each cell will be 50x50 pixels
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        int width = cols * cellSize;
        int height = rows * cellSize;

        using (Bitmap bitmap = new Bitmap(width, height))
        {
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White); // Clear the background as white

				for (int i = 0; i < rows; i++)
				{
					for (int j = 0; j < cols; j++)
					{
						Color color = matrix[i, j] == 1 ? Color.Black : Color.White;
						Brush brush = new SolidBrush(color);
						g.FillRectangle(brush, j * cellSize, i * cellSize, cellSize, cellSize);
					}
				}
			}

			Directory.CreateDirectory(folderPath); // Ensure the directory exists
			string filePath = Path.Combine(folderPath, fileName);
			bitmap.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
			Console.WriteLine($"Image saved to {filePath}");
		}
	}
}
