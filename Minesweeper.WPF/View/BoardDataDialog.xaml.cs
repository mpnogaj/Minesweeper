using Minesweeper.Logic.Models;
using Minesweeper.WPF.ViewModel;
using System;
using System.Windows;

namespace Minesweeper.WPF.View
{
    /// <summary>
    /// Interaction logic for BoardSizeDialog.xaml
    /// </summary>
    public partial class BoardDataDialog : Window
    {
        public BoardData BoardData { get; private set; }

        public BoardDataDialog()
        {
            InitializeComponent();
            DataContext = new BoardDataDialogViewModel();
        }

        public static BoardData? GetBoardData()
        {
            BoardDataDialog dialog = new();
            bool? okPressed = dialog.ShowDialog();
            return okPressed.HasValue && okPressed.Value ?
                dialog.BoardData : null;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BoardData = ((BoardDataDialogViewModel)DataContext).ConstructBoardData();
                DialogResult = true;
                Close();
            }
            catch(ArgumentException ex)
            {
                string message = string.Empty;
                switch (ex.ParamName)
                {
                    case "mines":
                        message = "Invalid number of mines";
                        break;
                    case "size":
                        message = "Invalid board size";
                        break;
                }
                MessageBox.Show(message);
            }
        }
    }
}
