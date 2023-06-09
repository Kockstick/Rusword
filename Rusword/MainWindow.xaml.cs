﻿using System;
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

namespace Rusword
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ToolsRow.mainWindow = this;
            FontPanel.mainWindow = this;
            ParagraphPanel.mainWindow = this;

            var parafraphs = RichTextBox.Document.Blocks.OfType<Paragraph>();
            foreach (Paragraph paragraph in parafraphs)
                paragraph.LineHeight = 1;

            RichTextBox.FontFamily = new FontFamily("Arial");
        }

        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
