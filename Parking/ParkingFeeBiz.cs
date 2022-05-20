using System;

namespace Parking
{
    public class ParkingFeeBiz
    {
        public static int StatWorkFlow(DateTime StartDate, DateTime EndDate)
        {
            DateTime StartDateTrim = TrimSecond(StartDate);
            DateTime EndDateTrim = TrimSecond(EndDate);

            var tsTrim = EndDateTrim.Subtract(StartDateTrim);

            return (int)tsTrim.TotalMinutes;
        }

        private static DateTime TrimSecond(DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0);
        }
    }
}
