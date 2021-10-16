using Minesweeper.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.WPF.ViewModel
{
    public class BoardDataDialogViewModel : ViewModelBase
    {
        private uint _height;
        public uint Height
        {
            get => _height;
            set => SetProperty(ref _height, value);
        }

        private uint _width;
        public uint Width
        {
            get => _width;
            set => SetProperty(ref _width, value);
        }

        private uint _mines;
        public uint Mines
        {
            get => _mines;
            set => SetProperty(ref _mines, value);
        }

        public BoardData ConstructBoardData()
        {
            return new(new SizeU(Height, Width), Mines);
        }
    }
}
