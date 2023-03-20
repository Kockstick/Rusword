using ColorPickerLib.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public FontPanel()
        {
            InitializeComponent();
            fontComboBox.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            fontSizeComboBox.ItemsSource = new List<double>() { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
        }

        private void fontComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mainWindow == null)
                return;

            if (fontComboBox.SelectedItem == null)
                return;

            if (mainWindow.RichTextBox.Selection.Text != null)
            {
                mainWindow.RichTextBox.Selection.ApplyPropertyValue(RichTextBox.FontFamilyProperty, fontComboBox.SelectedItem);
            }

            Style style = new Style(typeof(RichTextBox));
            style.Setters.Add(new Setter(RichTextBox.FontFamilyProperty, fontComboBox.SelectedItem));
            mainWindow.RichTextBox.Style = style;
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

        private void fontSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(mainWindow == null)
                return;

            string t = fontSizeComboBox.SelectedItem.ToString();

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
    }
}
