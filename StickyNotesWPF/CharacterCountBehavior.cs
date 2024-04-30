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
    public class CharacterCountBehavior : DependencyObject
    {
        public static readonly DependencyProperty CharacterCountProperty =
            DependencyProperty.RegisterAttached("CharacterCount", typeof(int), typeof(CharacterCountBehavior), new PropertyMetadata(0));

        public static int GetCharacterCount(DependencyObject obj)
        {
            return (int)obj.GetValue(CharacterCountProperty);
        }

        public static void SetCharacterCount(DependencyObject obj, int value)
        {
            obj.SetValue(CharacterCountProperty, value);
        }

        public static readonly DependencyProperty RichTextBoxProperty =
            DependencyProperty.RegisterAttached("RichTextBox", typeof(RichTextBox), typeof(CharacterCountBehavior), new PropertyMetadata(null, OnRichTextBoxChanged));

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
            RichTextBox richTextBox = e.NewValue as RichTextBox;
            if (richTextBox != null)
            {
                richTextBox.TextChanged += (sender, args) =>
                {
                    string text = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd).Text;
                    int characterCount = text.Length - CountNewLines(text);
                    SetCharacterCount(richTextBox, characterCount);
                };
            }
        }

        private static int CountNewLines(string text)
        {
            int count = 0;
            foreach (char c in text)
            {
                if (c == '\r' || c == '\n')
                {
                    count++;
                }
            }
            return count;
        }
    }
}
