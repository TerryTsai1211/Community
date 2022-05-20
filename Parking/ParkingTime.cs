using System;

namespace Parking
{
    public class ParkingTime : IComparable<DateTime>
    {
        /// <summary>
        /// 停車時間，精確到分鐘
        /// </summary>
        public DateTime Value { get; private set; }

        public ParkingTime(DateTime parkingTime)
        {
            Value = parkingTime.TrimSecond();
        }

        public int CompareTo(DateTime other)
        {
            return Value.CompareTo(other);
        }

        public override bool Equals(object obj)
        {
            return obj is ParkingTime time &&
                  Value == time.Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        #region 運算子多載

        public static bool operator ==(ParkingTime left, DateTime right)
        {
            return left.Value == right;
        }

        public static bool operator !=(ParkingTime left, DateTime right)
        {
            return !(left.Value == right);
        }

        public static bool operator <(ParkingTime left, DateTime right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <=(ParkingTime left, DateTime right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >(ParkingTime left, DateTime right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >=(ParkingTime left, DateTime right)
        {
            return left.CompareTo(right) >= 0;
        }

        #endregion

        #region 隱式轉型

        public static implicit operator ParkingTime(DateTime parkingTime)
        {
            return new ParkingTime(parkingTime);
        }

        public static implicit operator DateTime(ParkingTime parkingTime)
        {
            return parkingTime.Value;
        }

        #endregion

    }
}
