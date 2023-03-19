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
using System.Windows.Shapes;

namespace Rusword
{
    public partial class ParagraphPanel : Border
    {
        public MainWindow mainWindow;
        public ParagraphPanel()
        {
            InitializeComponent();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string t = e.Text;
            TextBox textBox = (TextBox)sender;

            if (!(Char.IsDigit(t, 0) || (t == ".")
               && (!textBox.Text.Contains(".")
               && textBox.Text.Length != 0)))
            {
                e.Handled = true;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string data = textBox.Tag as string;

            if (Double.TryParse(textBox.Text, out double d))
                SetPadding(data, d);
        }

        private void SetPadding(string side, double value)
        {
            if (mainWindow == null)
                return;

            var ritchPaddings = mainWindow.RichTextBox.Padding;
            Thickness thickness = new Thickness();

            switch (side)
            {
                case "Up":
                    thickness = new Thickness(ritchPaddings.Left, value, ritchPaddings.Right, ritchPaddings.Bottom);
                    ritchPaddings = thickness;
                    break;
                case "Left":
                    thickness = new Thickness(value, ritchPaddings.Top, ritchPaddings.Right, ritchPaddings.Bottom);
                    ritchPaddings = thickness;
                    break;
                case "Right":
                    thickness = new Thickness(ritchPaddings.Left, ritchPaddings.Top, value, ritchPaddings.Bottom);
                    ritchPaddings = thickness;
                    break;
                case "Down":
                    thickness = new Thickness(ritchPaddings.Left, ritchPaddings.Top, ritchPaddings.Right, value);
                    ritchPaddings = thickness;
                    break;
            }

            mainWindow.RichTextBox.Padding = ritchPaddings;
        }
    }
}
