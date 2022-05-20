using System;
using System.Collections.Generic;
using System.Linq;

namespace Parking
{

    public class ParkingFee
    {
        public IEnumerable<SingleDayFee> Items { get; private set; }
        public int TotalFee { get; }

        public ParkingFee(IEnumerable<SingleDayFee> items)
        {
            this.Items = items;
            this.TotalFee = items.Sum(s => s.Fee);
        }
    }

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

    public static class ParkingHelper
    {
        public const int MinutesInHalfHour = 30;
        public const int MinutesInOneHour = 60;
        public const int ZeroMinute = 0;
        public const decimal ZeroFee = 0;

        /// <summary>
        /// 停車場時間計算，不考慮秒
        /// </summary>
        /// <param name="start">起始時間</param>
        /// <param name="end">結束時間</param>
        /// <returns></returns>
        public static int TotalMinutesStat(DateTime start, DateTime end)
        {
            DateTime startTrim = start.TrimSecond();
            DateTime endTrim = end.TrimSecond();
            return (int)endTrim.Subtract(startTrim).TotalMinutes;
        }

        /// <summary>
        /// 無條件進位 1 小時 (60 分鐘)
        /// 收費原則 B：不足一小時視為一小時
        /// 收費原則 C：第一小時為 20 元、第二小時起每小時 30 元
        /// </summary>
        /// <param name="totalMinutes">總分鐘數</param>
        /// <returns></returns>
        public static int MinutesPlus(int totalMinutes)
        {
            if (totalMinutes % MinutesInOneHour == ZeroMinute)
                return totalMinutes;

            return ((totalMinutes / MinutesInOneHour) * MinutesInOneHour) + MinutesInOneHour;
        }

        public static decimal HoursFeeStat(int totalMinutes, decimal oneHourFee)
        {
            return (totalMinutes / MinutesInOneHour) * oneHourFee;
        }

        public static decimal FeeStat(decimal oneDayFeeMax, params decimal[] feeItems)
        {
            decimal feeTotal = feeItems.Sum();
            return Math.Min(oneDayFeeMax, feeTotal);
        }
    }

    #region Interface

    public interface IMinutesStat
    {
        int TotalMinutes(DateTime start, DateTime end);
    }

    public interface IFeeStat
    {
        decimal CalcFee(DateTime start, DateTime end);
    }

    #endregion

    #region MinutesStat

    public class ParkingAMinutesStat : IMinutesStat
    {
        public int TotalMinutes(DateTime start, DateTime end)
        {
            int totalMinutes = ParkingHelper.TotalMinutesStat(start, end);
            return totalMinutes;
        }
    }

    public class ParkingBMinutesStat : IMinutesStat
    {
        public int TotalMinutes(DateTime start, DateTime end)
        {
            int totalMinutes = ParkingHelper.TotalMinutesStat(start, end);
            return ParkingHelper.MinutesPlus(totalMinutes);
        }
    }

    public class ParkingCMinutesStat : IMinutesStat
    {
        public int TotalMinutes(DateTime start, DateTime end)
        {
            int totalMinutes = ParkingHelper.TotalMinutesStat(start, end);
            return ParkingHelper.MinutesPlus(totalMinutes);
        }
    }

    #endregion

    #region FeeStat
    public class ParkingAFeeStat : IFeeStat
    {

        private readonly IMinutesStat _minutesStat;

        public ParkingAFeeStat()
        {
            _minutesStat = new ParkingAMinutesStat();
        }

        public decimal CalcFee(DateTime start, DateTime end)
        {
            int totalMinutes = _minutesStat.TotalMinutes(start, end);

            decimal oneDayFeeMax = 50m;

            if (IsFree(totalMinutes))
                return ParkingHelper.ZeroFee;

            if (IsOneDayFeeMax(totalMinutes))
                return oneDayFeeMax;

            decimal hoursFee = HourFeeStat(totalMinutes);
            decimal minutesFee = MinuteFeeStat(totalMinutes);
            return ParkingHelper.FeeStat(oneDayFeeMax, hoursFee, minutesFee);
        }

        private bool IsOneDayFeeMax(int totalMinutes)
        {
            int minutesOfOneDayFeeMax = 300;
            return (totalMinutes >= minutesOfOneDayFeeMax);
        }

        private bool IsFree(int totalMinutes)
        {
            return (totalMinutes >= 0 && totalMinutes <= 10);
        }

        private decimal HourFeeStat(int totalMinutes)
        {
            decimal oneHourFee = 10;
            return ParkingHelper.HoursFeeStat(totalMinutes, oneHourFee);
        }

        private decimal MinuteFeeStat(int totalMinutes)
        {
            int mm = totalMinutes % ParkingHelper.MinutesInOneHour;
            if (mm == ParkingHelper.ZeroMinute)
                return ParkingHelper.ZeroFee;

            decimal firstHourFee = 7m;
            decimal secondHourFee = 10m;
            return mm <= ParkingHelper.MinutesInHalfHour ? firstHourFee : secondHourFee;
        }
    }

    public class ParkingBFeeStat : IFeeStat
    {
        private readonly IMinutesStat _minutesStat;

        public ParkingBFeeStat()
        {
            _minutesStat = new ParkingBMinutesStat();
        }

        public decimal CalcFee(DateTime start, DateTime end)
        {
            int totalMinutes = _minutesStat.TotalMinutes(start, end);

            if (start.IsSunOrSat() == false)
                return ParkingACalcFee(start, end);

            decimal oneDayFeeMax = 250;
            decimal hoursFee = HourFeeStat(totalMinutes);
            return ParkingHelper.FeeStat(oneDayFeeMax, hoursFee);
        }

        private decimal HourFeeStat(int totalMinutes)
        {
            decimal oneHourFee = 15m;
            return ParkingHelper.HoursFeeStat(totalMinutes, oneHourFee);
        }

        private decimal ParkingACalcFee(DateTime start, DateTime end)
        {
            var feeStat = new ParkingAFeeStat();
            return feeStat.CalcFee(start, end);
        }
    }

    public class ParkingCFeeStat : IFeeStat
    {
        private readonly IMinutesStat _minutesStat;

        public ParkingCFeeStat()
        {
            _minutesStat = new ParkingCMinutesStat();
        }

        public decimal CalcFee(DateTime start, DateTime end)
        {
            int totalMinutes = _minutesStat.TotalMinutes(start, end);

            decimal oneDayFeeMax = 300m;

            if (IsFree(start, totalMinutes))
                return ParkingHelper.ZeroFee;

            if (IsOneDayFeeMax(totalMinutes))
                return oneDayFeeMax;

            int firstRangeMinutes = 60;
            decimal firstRangeFee = 20m;

            if (totalMinutes <= firstRangeMinutes)
                return firstRangeFee;

            decimal secondRangeFee = SecondRangeFeeStat(totalMinutes, firstRangeMinutes);
            return ParkingHelper.FeeStat(oneDayFeeMax, firstRangeFee, secondRangeFee);
        }

        private bool IsOneDayFeeMax(int totalMinutes)
        {
            int minutesOfOneDayFeeMax = 600;
            return (totalMinutes >= minutesOfOneDayFeeMax);
        }

        private bool IsFree(DateTime dt, int totalMinutes)
        {
            return (totalMinutes == ParkingHelper.ZeroMinute ||
              dt.IsSunOrSat());
        }

        private decimal SecondRangeFeeStat(int totalMinutes, int firstRangeMinutes)
        {
            decimal secondRangeFee = 30m;
            int secondRangeMinutes = (totalMinutes - firstRangeMinutes);
            return ParkingHelper.HoursFeeStat(secondRangeMinutes, secondRangeFee);
        }
    }

    #endregion

    public class Parking
    {
        private readonly IFeeStat _feeStat;

        public Parking(IFeeStat feeStat)
        {
            _feeStat = feeStat;
        }

        public ParkingFee CalcParkingFee(DateTime start, DateTime end)
        {
            if (start > end)
            {
                throw new Exception("起始日期早於結束日期");
            }

            var detail = CalcFeeForMultiDays(start, end);
            return new ParkingFee(detail);
        }

        public IEnumerable<SingleDayFee> CalcFeeForMultiDays(DateTime start, DateTime end)
        {
            if (start > end)
            {
                throw new Exception("起始日期早於結束日期");
            }

            var timeTable = TimeTable(start, end);
            return SingleDayFeeMapper(timeTable);
        }

        private IEnumerable<SingleDayFee> SingleDayFeeMapper(List<(DateTime start, DateTime end)> timeTable)
        {
            return timeTable.Select(dtRange => new SingleDayFee()
            {
                StartTime = dtRange.start,
                EndTime = dtRange.end,
                Fee = (int)_feeStat.CalcFee(dtRange.start, dtRange.end)
            });
        }

        private List<(DateTime start, DateTime end)> TimeTable(DateTime start, DateTime end)
        {
            DateTime startDateOnly = start.Date;
            DateTime endDateOnly = end.Date;

            var result = new List<(DateTime start, DateTime end)>();
            if (IsSameDay())
            {
                InsertSingle();
                return result;
            }

            InsertStart();
            InsertRange();
            InsertEnd();
            return result;

            bool IsSameDay()
            {
                return startDateOnly == endDateOnly;
            }

            void InsertSingle()
            {
                result.Add((start, end));
            }

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

        private DateTime DayEndTime(DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 0);
        }
    }
}