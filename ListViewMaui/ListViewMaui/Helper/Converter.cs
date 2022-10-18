using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private bool IsThisWeek(DateTime groupName)
        {
            var groupWeekSunDay = groupName.AddDays(-(int)groupName.DayOfWeek).Day;
            var currentSunday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek).Day;

            return currentSunday == groupWeekSunDay;
        }
    }
}
