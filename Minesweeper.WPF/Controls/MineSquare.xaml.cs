using Minesweeper.Logic;
using Minesweeper.Logic.Models;
using Minesweeper.WPF.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Minesweeper.WPF.Controls
{
    /// <summary>
    /// Interaction logic for MineSquare.xaml
    /// </summary>
    public partial class MineSquare : Button
    {
        private PointU? _point;
        public PointU? Point 
        {
            get => _point;
            set
            {
                if(!_point.HasValue)
                {
                    _point = value;
                }
            }
        }

        public MineSquare()
        {
            InitializeComponent();
            SetImage(Logic.Properties.Resources.Default);
            Height = 50;
            Width = 50;
            BorderThickness = new Thickness(1);
            PreviewMouseDown += (sneder, e) =>
            {
                if (Game.Instance == null || Point == null)
                {
                    return;
                }

                switch (e.ChangedButton)
                {
                    case MouseButton.Left:
                        SetImage(Game.Instance.HandleClick(Point.Value, false));
                        break;
                    case MouseButton.Right:
                        SetImage(Game.Instance.HandleClick(Point.Value, true));
                        break;
                    default:
                        return;
                }
            };
        }

        private void SetImage(System.Drawing.Bitmap bmp)
        {
            if(Content is not Image)
            {
                Content = new Image();
            }
            ((Image)Content).Source = BitmapToBitmapImageConverter.ToBitmapImage(bmp);
        }
    }
}
