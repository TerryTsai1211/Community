using System;
using System.Collections.Generic;
using System.Linq;

namespace Parking
{

    public class SingleDayFee : IEquatable<SingleDayFee>
    {
        public DateTime StartTime { get; set; } // 精確到分鐘的入場時間
        public DateTime EndTime { get; set; } // 精確到分鐘的離場時間
        public int Fee { get; set; } // 本日應收取費用

        public override bool Equals(object obj)
        {
            return Equals(obj as SingleDayFee);
        }

        public bool Equals(SingleDayFee other)
        {
            return other != null &&
                   StartTime == other.StartTime &&
                   EndTime == other.EndTime &&
                   Fee == other.Fee;
        }

        public override int GetHashCode()
        {
            int hashCode = 933646964;
            hashCode = hashCode * -1521134295 + StartTime.GetHashCode();
            hashCode = hashCode * -1521134295 + EndTime.GetHashCode();
            hashCode = hashCode * -1521134295 + Fee.GetHashCode();
            return hashCode;
        }
    }

    public class ParkingFeeBiz
    {
        private int _minutesInHalfHour { get; } = 30;
        private int _minutesInOneHour { get; } = 60;
        private decimal _zeroFee { get; } = 0m;
        private decimal _oneDayFeeLimit { get; } = 50m;

        // 傳入的是時間起迄, 並非分鐘數
        public IEnumerable<SingleDayFee> CalcFeeForMultiDays_STD(DateTime start, DateTime end)
        {
            if (start > end)
            {
                throw new Exception("起始日期早於結束日期");
            }

            var result = new List<(DateTime start, DateTime end)>();

            if (IsSameDay(start, end))
            {
                result.Add((start, end));
            }
            else
            {
                result.AddRange(TimeTable(start, end));
            }

            return result.Select(dtRange => new SingleDayFee()
            {
                StartTime = dtRange.start,
                EndTime = dtRange.end,
                Fee = (int)CalcFee(dtRange.start, dtRange.end)
            });
        }

        private List<(DateTime start, DateTime end)> TimeTable(DateTime start, DateTime end)
        {
            DateTime startDateOnly = DateOnly(start);
            DateTime endDateOnly = DateOnly(end);

            var result = new List<(DateTime start, DateTime end)>();
            InsertStart();
            InsertRange();
            InsertEnd();
            return result;

            void InsertStart()
            {
                result.Add((start, DayEndTime(start)));
            }

            void InsertRange()
            {
                var loop = Enumerable.Range(1, endDateOnly.Subtract(startDateOnly).Days - 1)
                .Select(offset => startDateOnly.AddDays(offset))
                .Select(dt => (dt, DayEndTime(dt)));

                result.AddRange(loop);
            }

            void InsertEnd()
            {
                result.Add((endDateOnly, end));
            }
        }

        private bool IsSameDay(DateTime start, DateTime end)
        {
            return DateOnly(start) == DateOnly(end);
        }

        private DateTime DayEndTime(DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 0);
        }

        private DateTime DateOnly(DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day);
        }

        #region Q2 Code
        public decimal CalcFee(DateTime start, DateTime end)
        {
            int totalMinutes = MinuteStat(start, end);

            // Q3 新增，避免跨日計算停一整天還要往下跑計算
            if (IsFullDay(totalMinutes))
                return _oneDayFeeLimit;

            if (IsFree(totalMinutes))
                return _zeroFee;

            decimal hoursFee = HourFeeStat(totalMinutes);
            decimal minutesFee = MinuteFeeStat(totalMinutes);
            return FeeStat(hoursFee, minutesFee);
        }

        private bool IsFullDay(int totalMinutes)
        {
            // Allen 老師 Q2 提示一整天為 1439 分鐘
            int MinuutesInOneDay = 1439;
            return (totalMinutes == MinuutesInOneDay);
        }

        private bool IsFree(int totalMinutes)
        {
            return (totalMinutes >= 0 && totalMinutes <= 10);
        }

        private decimal HourFeeStat(int totalMinutes)
        {
            decimal hourFee = 10;
            return (totalMinutes / _minutesInOneHour) * hourFee;
        }

        private decimal MinuteFeeStat(int totalMinutes)
        {
            int mm = totalMinutes % _minutesInOneHour;
            if (mm == 0)
                return _zeroFee;

            decimal firstHourFee = 7m;
            decimal secondHourFee = 10m;
            return mm <= _minutesInHalfHour ? firstHourFee : secondHourFee;
        }

        private decimal FeeStat(decimal hourFee, decimal minutesFee)
        {
            decimal feeStat = hourFee + minutesFee;
            return Math.Min(_oneDayFeeLimit, feeStat);
        }
        #endregion

        #region Q1 Code
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
        #endregion
    }
}
