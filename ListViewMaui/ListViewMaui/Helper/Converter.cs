using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Color = Microsoft.Maui.Graphics.Color;

#nullable disable
namespace ListViewMaui
{
    public class DatetimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return string.Empty;

            var datetime = (DateTime)value;
            int compare = datetime.Date.CompareTo(DateTime.Now.Date);

            if (compare == 0)
            {
                return datetime.ToLocalTime().ToString("t");
            }
            else if (datetime.Date.CompareTo(DateTime.Now.AddDays(-1).Date) == 0)
            {
                return "Yesterday";
            }
            else
            {
                return datetime.ToString("MMM dd");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class FontAttributeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isOpened = (bool)value;

            if (!isOpened)
            {
                return FontAttributes.Bold;
            }

            return FontAttributes.None;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class TextColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isOpened = (bool)value;

            if (!isOpened)
            {
                return Color.FromArgb("#007FFF");
            }

            return Color.FromArgb("#666666");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class GroupHeaderTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((GroupName)value)
            {
                case GroupName.ThisWeek:
                    return "This Week";
                case GroupName.LastWeek:
                    return "Last Week";
                case GroupName.ThisMonth:
                    return "This Month";
                case GroupName.LastMonth:
                    return "Last Month";
                default:
                    return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
