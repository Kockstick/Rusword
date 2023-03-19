using ColorPickerLib.Controls;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Rusword
{
    /// <summary>
    /// Логика взаимодействия для FontPanel.xaml
    /// </summary>
    public partial class FontPanel : Border
    {
        public MainWindow mainWindow;
        private List<String> fonts = new List<String>()
        {
            "Arial",
            "Times New Roman",
            "Verdana"
        };

        public FontPanel()
        {
            InitializeComponent();
        }

        private void fontComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mainWindow == null)
                return;

            FontFamily font = getFont(fontComboBox.SelectedIndex);

            if (mainWindow.RichTextBox.Selection.Text != "")
            {
                mainWindow.RichTextBox.Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, font);
                return;
            }

            mainWindow.RichTextBox.FontFamily = font;
        }

        private FontFamily getFont(int id)
        {
            for (int i = 0; i < fonts.Count; i++)
            {
                if (i == id)
                    return new FontFamily(fonts[i]);
            }

            return new FontFamily(fonts[0]);
        }

        public void AddFont(String fontName)
        {
            if (fonts.Contains(fontName))
                return;

            fonts.Add(fontName);
        }

        private void FontSizeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string t = FontSizeTextBox.Text;

            if (t == "")
                return;

            if (!Double.TryParse(t, out double size))
                return;

            if (size <= 0)
                return;

            if (mainWindow.RichTextBox.Selection.Text != "")
            {
                mainWindow.RichTextBox.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, size);
                return;
            }

            mainWindow.RichTextBox.FontSize = size;
        }

        private void FontSizeTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string t = e.Text;

            if (!(Char.IsDigit(t, 0) || (t == ".")
               && (!FontSizeTextBox.Text.Contains(".")
               && FontSizeTextBox.Text.Length != 0)))
            {
                e.Handled = true;
            }
        }

        private void FontStyleComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mainWindow == null)
                return;

            switch (((ComboBox)sender).SelectedIndex)
            {
                case 0:
                    mainWindow.RichTextBox.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
                    break;
                case 1:
                    mainWindow.RichTextBox.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
                    break;
            }
        }

        private void colorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if(mainWindow == null) 
                return;

            ColorPicker picker = (ColorPicker)sender;
            Brush brush = new SolidColorBrush((Color)picker.SelectedColor);
            mainWindow.RichTextBox.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, brush);
        }
    }
}
