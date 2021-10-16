using Minesweeper.Logic;
using Minesweeper.Logic.Models;
using Minesweeper.WPF.View;
using Minesweeper.WPF.ViewModel.Command;
using System;

namespace Minesweeper.WPF.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private BoardData? _currentData;

        public EventHandler<SizeU>? CreateBoardRequest { get; set; }

        public RelayCommand RestartGameCommand { get; private set; }
        public RelayCommand StartNewGameCommand { get; private set;  }

        public MainWindowViewModel()
        {
            RestartGameCommand = new RelayCommand(() =>
            {
                PrepareGame(_currentData!.Value);
            }, () => _currentData != null);
            StartNewGameCommand = new RelayCommand(() =>
            {
                BoardData? boardData = BoardDataDialog.GetBoardData();
                if (boardData != null)
                {
                    _currentData = boardData;
                    PrepareGame(boardData!.Value);
                }
            }, () => true);
        }

        private void PrepareGame(BoardData boardData)
        {
            Game.PrepareGame(boardData);
            CreateBoardRequest?.Invoke(this, new SizeU(boardData.Height, boardData.Width));
        }
    }
}
