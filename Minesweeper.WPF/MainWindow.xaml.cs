using Minesweeper.Logic;
using Minesweeper.Logic.Models;
using Minesweeper.WPF.Converters;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Minesweeper.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Game game;
        public MainWindow()
        {
            InitializeComponent();
            game = new(new BoardData(new SizeU(4, 4), 5));
            CreateBoard(4, 4);
        }

        private void CreateBoard(int height, int width)
        {
            Container.Children.Clear();
            Grid grid = new();
            for (int i = 0; i < height; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < width; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Button button = new()
                    {
                        Height = 50,
                        Width = 50,
                        Margin = new Thickness(1.0)
                    };
                    button.PreviewMouseDown += (sender, e) =>
                    {
                        if (e.ChangedButton != MouseButton.Left &&
                            e.ChangedButton != MouseButton.Right)
                        {
                            return;
                        }
                        bool isLong = e.ChangedButton == MouseButton.Right;
                        Button s = (Button)sender;
                        s.Content = new Image
                        {
                            Source = BitmapToBitmapImageConverter.ToBitmapImage(
                                game.HandleClick((uint)(int)s.Tag, isLong))
                        };
                    };
                    button.SetValue(TagProperty, (i * height) + j);
                    grid.Children.Add(button);
                    int index = grid.Children.Count - 1;
                    grid.Children[index].SetValue(Grid.RowProperty, i);
                    grid.Children[index].SetValue(Grid.ColumnProperty, j);
                }
            }
            Container.Children.Add(grid);
        }
    }
}
