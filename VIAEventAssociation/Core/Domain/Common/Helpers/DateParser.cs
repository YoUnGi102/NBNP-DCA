using System.Globalization;

namespace Domain.Common.Helpers;

public class DateParser
{
    private const string DateFormat = "yyyy-MM-dd HH:mm:ss";

    public static DateTime ParseDate(string dateString)
    {
        return DateTime.ParseExact(dateString, DateFormat, CultureInfo.InvariantCulture);
    }

    public static string ToString(DateTime date)
    {
        return date.ToString(DateFormat, CultureInfo.InvariantCulture);
    }
}