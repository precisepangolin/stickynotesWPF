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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StickyNotesWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBlock1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("You clicked me at " + e.GetPosition(this).ToString());
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

        private void Bold_MouseUp(object sender, RoutedEventArgs e)
        {
            if (WritingBox.Selection != null)
            {
                if (!WritingBox.Selection.GetPropertyValue(TextElement.FontWeightProperty).Equals(FontWeights.Bold))
                {
                    WritingBox.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
                }
                else
                {
                    WritingBox.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
                }
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

        void SaveRTBContent(Object sender, RoutedEventArgs args)
        {
            var fileLocation = Environment.ExpandEnvironmentVariables(@"%LOCALAPPDATA%\testing.rtf");
            TextRange text = new TextRange(WritingBox.Document.ContentStart, WritingBox.Document.ContentEnd);
            using(FileStream file = new FileStream(fileLocation, FileMode.Create))
            {
                text.Save(file, System.Windows.DataFormats.Rtf);
            }
        }


    }
}
