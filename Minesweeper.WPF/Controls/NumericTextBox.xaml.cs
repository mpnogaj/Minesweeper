using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace Minesweeper.WPF.Controls
{
    /// <summary>
    /// Interaction logic for NumericTextBox.xaml
    /// </summary>
    public partial class NumericTextBox : TextBox
    {
        public NumericTextBox()
        {
            InitializeComponent();
        }

        private void ValidateInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
