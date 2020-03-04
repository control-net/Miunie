using Miunie.Core.Entities.Views;
using System;
using System.Collections.Generic;
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

                value = $"{token.Substring(0, 5)} ****************************** {token.Substring(token.Length - 5)}";
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
