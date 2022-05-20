using System;

namespace Parking
{
    public static class DateTimeExtension
    {
        public static DateTime TrimSecond(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0);
        }

        public static bool IsSunOrSat(this DateTime dt)
        {
            return (dt.DayOfWeek == DayOfWeek.Sunday || dt.DayOfWeek == DayOfWeek.Saturday);
        }
    }
}
