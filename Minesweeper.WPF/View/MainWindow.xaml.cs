using Minesweeper.Logic.Models;
using Minesweeper.WPF.Controls;
using Minesweeper.WPF.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Minesweeper.WPF.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ((MainWindowViewModel)DataContext).CreateBoardRequest += (sender, size) =>
            {
                CreateBoard(size.Height, size.Width);
            };
        }

        private void CreateBoard(uint height, uint width)
        {
            Container.Children.Clear();
            Grid grid = new();
            for (uint i = 0; i < height; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }
            for (uint i = 0; i < width; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (uint i = 0; i < height; i++)
            {
                for (uint j = 0; j < width; j++)
                {
                    MineSquare square = new()
                    {
                        Point = new PointU(i, j)
                    };
                    grid.Children.Add(square);
                    int index = grid.Children.Count;
                    grid.Children[index].SetValue(Grid.RowProperty, i);
                    grid.Children[index].SetValue(Grid.ColumnProperty, j);
                }
            }
            Container.Children.Add(grid);
        }
    }
}
