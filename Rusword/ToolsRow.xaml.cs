using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using ColorPickerLib;

namespace Rusword
{
    /// <summary>
    /// Логика взаимодействия для ToolsRow.xaml
    /// </summary>
    public partial class ToolsRow : Canvas
    {
        public MainWindow mainWindow;
        public ToolsRow()
        {
            InitializeComponent();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchTextBox.Text;
            var richTextBox = mainWindow.RichTextBox;

            TextPointer startPosition = richTextBox.Document.ContentStart;
            TextRange searchRange = new TextRange(startPosition, richTextBox.Document.ContentEnd);
            TextPointer foundPosition = searchRange.Start.GetInsertionPosition(LogicalDirection.Forward);

            while (foundPosition != null)
            {
                if (foundPosition.GetPointerContext(LogicalDirection.Forward) == TextPointerContext.Text)
                {
                    string textRun = foundPosition.GetTextInRun(LogicalDirection.Forward);

                    int indexInRun = textRun.IndexOf(searchText);

                    if (indexInRun >= 0)
                    {
                        TextPointer start = foundPosition.GetPositionAtOffset(indexInRun);
                        TextPointer end = start.GetPositionAtOffset(searchText.Length);
                        richTextBox.Selection.Select(start, end);
                        return;
                    }
                }

                foundPosition = foundPosition.GetNextInsertionPosition(LogicalDirection.Forward);
            }
        }

        private void ExportBtn_Click(object sender, RoutedEventArgs e)
        {
            string text = new TextRange(mainWindow.RichTextBox.Document.ContentStart, mainWindow.RichTextBox.Document.ContentEnd).Text;

            SaveFileDialog saveDialog = new SaveFileDialog();
            if (saveDialog.ShowDialog() == true)
            {
                using (StreamWriter writer = new StreamWriter(saveDialog.FileName))
                {
                    writer.Write(text);
                }
            }
        }

        private void LoadFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new ArgumentException("File does not exist", nameof(filePath));
            }

            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                var range = new TextRange(mainWindow.RichTextBox.Document.ContentStart, mainWindow.RichTextBox.Document.ContentEnd);
                range.Load(stream, DataFormats.Rtf);
            }
        }

        private void ImportBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            openFileDialog.Title = "Import Text File";

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                LoadFile(filePath);
            }
        }
    }
}
