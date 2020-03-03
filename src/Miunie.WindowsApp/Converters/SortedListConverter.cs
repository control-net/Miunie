using Miunie.Core.Entities.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Data;

namespace Miunie.WindowsApp.Converters
{
    public class SortedListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is IEnumerable<MessageView> messages)
            {
                value = messages.OrderBy(x => x.TimeStamp);
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
