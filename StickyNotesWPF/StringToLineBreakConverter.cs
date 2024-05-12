using System;
using System.Globalization;
using System.Windows.Data;

namespace StickyNotesWPF
{
    public class StringToLineBreakConverter : IValueConverter
    {
        private static StringToLineBreakConverter _instance;

        public static StringToLineBreakConverter Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new StringToLineBreakConverter();
                return _instance;
            }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue)
            {
                return stringValue.Replace("\\n", Environment.NewLine);
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}