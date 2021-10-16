using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Minesweeper.WPF.Converters
{
    public class BitmapToBitmapImageConverter
    {
        public static BitmapImage ToBitmapImage(Bitmap bitmap)
        {
            using MemoryStream memory = new();
            bitmap.Save(memory, ImageFormat.Bmp);
            memory.Position = 0;
            BitmapImage bitmapimage = new();
            bitmapimage.BeginInit();
            bitmapimage.StreamSource = memory;
            bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapimage.EndInit();

            return bitmapimage;
        }
    }
}
