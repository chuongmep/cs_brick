using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using Path = System.IO.Path;

namespace WpfApp1;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public static string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "test_generated_images");
    public ObservableCollection<ImageItem> ImageItems { get; set; } = new ObservableCollection<ImageItem>();

    public MainWindow()
    {
        InitializeComponent();
        GenerateAndLoadImages();
        ImagesControl.ItemsSource = ImageItems;
    }

    private void Test()
    {
        string fileName = Guid.NewGuid().ToString();
        string filename = Path.Combine(folderPath, $"{fileName}.png");
        int[,] pattern1 = new int[,] { { 0, 1, 0, 1, 0 }, { 1, 0, 1, 0, 0 } };
        int[,] pattern2 = new int[,]
        {
            { 0, 1, 0, 1 },
            { 0, 0, 1, 0 },
            { 0, 1, 0, 1 },
            { 0, 0, 0, 0 }
        };
        int[,] pattern3 = new int[,]
        {
            { 0, 1, 0, 1, 0, 1 },
            { 0, 0, 1, 0, 0, 0 },
            { 0, 1, 0, 1, 0, 1 },
            { 0, 0, 0, 0, 0, 0 },
            { 0, 1, 0, 1, 0, 1 },
            { 0, 0, 0, 0, 0, 0 },
        };
        int[,] pattern4 = new int[,]
        {
            { 0, 1, 0, 1 },
            { 1, 0, 1, 0 },
        };
        int[,] pattern5 = new int[,]
        {
            { 1, 0 },
            { 0, 0 },
        };
        PatternGenerator.SavePatternAsImage(pattern4, filename);
    }

    private void GenerateAndLoadImages()
    {
        int widthArea = 60;
        int heightArea = 60;
        int brickWidth = 10;
        int brickHeight = 10;
        int gapX = 10;
        int gapY = 10;
        int percentage = 50;
        var patterns =
            PatternGenerator.CreateAllPatterns(widthArea, heightArea, brickWidth, brickHeight, gapX, gapY, percentage);
        int index = 0;
        // create folder if it doesn't exist
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        // delete if any image contains

        foreach (var pattern in patterns)
        {
            string filename = Path.Combine(folderPath, $"pattern_{index++}.png");
            PatternGenerator.SavePatternAsImage(pattern, filename);
            ImageItems.Add(new ImageItem { ImagePath = filename, IsSelected = false });
        }
    }

    private void Regenerate(object sender, RoutedEventArgs e)
    {
        // delete all images
        int widthArea = int.Parse(areaWidth.Text);
        int heightArea = int.Parse(areaHeight.Text);
        int brickWidth = int.Parse(this.brickWidth.Text);
        int brickHeight = int.Parse(this.brickHeight.Text);
        int gapX = brickWidth;
        int gapY = brickHeight;
        int percentage = int.Parse(this.percentage.Text);
        var patterns =
            PatternGenerator.CreateAllPatterns(widthArea, heightArea, brickWidth, brickHeight, gapX, gapY, percentage);
        int index = 0;
        // create folder if it doesn't exist
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // delete if any image contains
        ImageItems.Clear();
        foreach (var pattern in patterns)
        {
            string guid = Guid.NewGuid().ToString();
            string filename = Path.Combine(folderPath, $"{guid}.png");
            PatternGenerator.SavePatternAsImage(pattern, filename);
            ImageItems.Add(new ImageItem { ImagePath = filename, IsSelected = false });
        }
        // clear and reload

        ImagesControl.ItemsSource = null;
        ImagesControl.ItemsSource = ImageItems;
    }
}