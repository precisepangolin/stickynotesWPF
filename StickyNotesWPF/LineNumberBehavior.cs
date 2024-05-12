using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows;

namespace StickyNotesWPF
{
    public class LineNumberBehavior : DependencyObject
    {
        public static readonly DependencyProperty LineNumberProperty =
            DependencyProperty.RegisterAttached("LineNumber", typeof(string), typeof(LineNumberBehavior), new PropertyMetadata(default(string)));

        public static string GetLineNumber(DependencyObject obj)
        {
            return (string)obj.GetValue(LineNumberProperty);
        }

        public static void SetLineNumber(DependencyObject obj, string value)
        {
            obj.SetValue(LineNumberProperty, value);
        }

        public static readonly DependencyProperty RichTextBoxProperty =
            DependencyProperty.RegisterAttached("RichTextBox", typeof(RichTextBox), typeof(LineNumberBehavior), new PropertyMetadata(null, OnRichTextBoxChanged));

        public static RichTextBox GetRichTextBox(DependencyObject obj)
        {
            return (RichTextBox)obj.GetValue(RichTextBoxProperty);
        }

        public static void SetRichTextBox(DependencyObject obj, RichTextBox value)
        {
            obj.SetValue(RichTextBoxProperty, value);
        }

        private static void OnRichTextBoxChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RichTextBox richTextBox = (RichTextBox)e.NewValue;
            if (richTextBox != null)
            {
                richTextBox.TextChanged += (sender, args) =>
                {
                    string text = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd).Text;
                    string lineNumber = "";
                    for (int i =1; i < CountNewLines(text); i++)
                    {
                        lineNumber += i + "\\n";
                    }
                    SetLineNumber(richTextBox, lineNumber);
                };
            }
        }

        private static int CountNewLines(string text)
        {
            int count = 1;
            foreach (char c in text)
            {
                if (c == '\n')
                {
                    count++;
                }
            }
            return count;
        }
    }
}
