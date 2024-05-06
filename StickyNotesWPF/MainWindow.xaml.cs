using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace StickyNotesWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {

        private readonly string fileLocation1 = Environment.ExpandEnvironmentVariables(@"%LOCALAPPDATA%\mystickynotesfile1.rtf");
        private readonly string fileLocation2 = Environment.ExpandEnvironmentVariables(@"%LOCALAPPDATA%\mystickynotesfile2.rtf");
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CloseButton(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void SpellCheckToggle(object sender, RoutedEventArgs e)
        {
            if (WritingBox.SpellCheck.IsEnabled)
            {
                WritingBox.SpellCheck.IsEnabled = false;
                WritingBox2.SpellCheck.IsEnabled = false;
            }
            else
            {
                WritingBox.SpellCheck.IsEnabled = true;
                WritingBox2.SpellCheck.IsEnabled = true;
            }
        }

        private void ClearFormatting_MouseUp(object sender, RoutedEventArgs e)
        {
            if (WritingBox.Selection != null)
            {
                WritingBox.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
                WritingBox.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Normal);
                WritingBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
            }
        }

        private void Bold_MouseUp(object sender, RoutedEventArgs e)
        {
            if (WritingBox.Selection != null)
            {
                MakeTextBold(WritingBox);
            }
        }

        private static void MakeTextBold(System.Windows.Controls.RichTextBox rtb)
        {
            if (!rtb.Selection.GetPropertyValue(TextElement.FontWeightProperty).Equals(FontWeights.Bold))
            {
                rtb.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
            }
            else
            {
                rtb.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
            }
        }

        private void Italic_MouseUp(object sender, RoutedEventArgs e)
        {
            if (WritingBox.Selection != null)
            {
                if (!WritingBox.Selection.GetPropertyValue(TextElement.FontStyleProperty).Equals(FontStyles.Italic))
                {
                    WritingBox.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Italic);
                }
                else
                {
                    WritingBox.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Normal);
                }
            }
        }

        private void Underline_MouseUp(object sender, RoutedEventArgs e)
        {
            if (WritingBox.Selection != null)
            {
                if (!WritingBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty).Equals(TextDecorations.Underline))
                {
                    WritingBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
                }
                else
                {
                    WritingBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
                }
            }
        }

        void SaveRTBContent(Object sender, RoutedEventArgs args)
        {
            Save(fileLocation1, WritingBox);
            Save(fileLocation2, WritingBox2);
        }

        private static void Save(string path, System.Windows.Controls.RichTextBox rtb)
        {
            TextRange text = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            using (FileStream file = new FileStream(path, FileMode.Create))
            {
                text.Save(file, System.Windows.DataFormats.Rtf);
            }
        }

        void LoadRTBContent(Object sender, RoutedEventArgs args)
        {
            Load(fileLocation1, WritingBox);
            Load(fileLocation2, WritingBox2);
        }

        private static void Load(string path, System.Windows.Controls.RichTextBox rtb)
        {
            FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            rtb.Document.Blocks.Clear();
            rtb.Selection.Load(stream, DataFormats.Rtf);
        }

        private async void AutoSave(object sender, TextChangedEventArgs e)
        {
            StatusBox.Text = "•";
            await Task.Delay(300);
            SaveRTBContent(sender, e);
            StatusBox.Text = "";
        }

       
    }
}
