using System;
using System.Linq;
using Windows.UI.Xaml.Data;

namespace Miunie.WindowsApp.Converters
{
    public class TokenMaskConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string token)
            {
                if (token == "[NOT CONFIGURED]") return value;

                value = $"{string.Join(string.Empty, token.Take(5))} ****************************** {string.Join(string.Empty, token.TakeLast(5))}";
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
