using System;

namespace Parking
{
    public class ParkingFeeBiz
    {
        private int _minutesInOneHour { get; } = 60;

        private decimal _zeroFee { get; } = 0m;

        public decimal CalcFee(DateTime start, DateTime end)
        {
            int totalMinutes = MinuteStat(start, end);
            if (IsFree(totalMinutes))
                return _zeroFee;

            decimal hoursFee = HourFeeStat(totalMinutes);
            decimal minutesFee = MinuteFeeStat(totalMinutes);
            return FeeStat(hoursFee, minutesFee);
        }

        private bool IsFree(int minute)
        {
            return (minute >= 0 && minute <= 10);
        }

        private decimal HourFeeStat(int minutes)
        {
            decimal hourFee = 10;
            return (minutes / _minutesInOneHour) * hourFee;
        }

        private decimal MinuteFeeStat(int minutes)
        {
            int mm = minutes % _minutesInOneHour;
            if (mm == 0)
                return _zeroFee;

            decimal firstHourFee = 7m;
            decimal secondHourFee = 10m;
            return mm <= 30 ? firstHourFee : secondHourFee;
        }

        private decimal FeeStat(decimal hourFee, decimal minutesFee)
        {
            decimal oneDayFeeLimit = 50;
            decimal feeStat = hourFee + minutesFee;
            return feeStat > oneDayFeeLimit ? oneDayFeeLimit : feeStat;
        }

        public int MinuteStat(DateTime startDate, DateTime endDate)
        {
            DateTime startDateTrim = TrimSecond(startDate);
            DateTime endDateTrim = TrimSecond(endDate);
            TimeSpan tsTrim = endDateTrim.Subtract(startDateTrim);
            return (int)tsTrim.TotalMinutes;
        }

        private DateTime TrimSecond(DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0);
        }
    }
}
