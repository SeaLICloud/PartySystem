using System;
using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;
using TomorrowSoft.Framework.Domain.Bases;
using TomorrowSoft.Framework.Domain.Exceptions;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.Extensions;

namespace TomorrowSoft.Framework.Domain.CustomType
{
    /// <summary>
    /// 年月类型
    /// </summary>
    public struct YearMonth : IEquatable<YearMonth>
    {
        public static YearMonth Now = new YearMonth(DateTime.Today.Year, DateTime.Today.Month);

        public YearMonth(int year)
            : this(year, 1)
        {
        }

        public static YearMonth Empty
        {
            get { return new YearMonth(new DateTime().SqlServerMinValue()); }
        }

        public YearMonth(int year, int month) : this()
        {
            if(year < 0 || year > 9999)
                throw new ArgumentException("年的取值区间为0~9999");
            if(month < 1 || month > 12)
                throw new ArgumentException("月的取值区间为1~12");
            Month = month;
            Year = year;
        }

        public YearMonth(DateTime dateTime) : this()
        {
            Year = dateTime.Year;
            Month = dateTime.Month;
        }

        /// <summary>
        /// 年
        /// </summary>
        public int Year { get; private set; }

        /// <summary>
        /// 月
        /// </summary>
        public int Month { get; private set; }

        /// <summary>
        /// YearMonth -> string
        /// </summary>
        /// <param name="ym"></param>
        /// <returns></returns>
        public static implicit operator string(YearMonth ym)
        {
            return string.Format("{0:0000}{1:00}", ym.Year, ym.Month);
        }

        /// <summary>
        /// string -> YearMonth
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static implicit operator YearMonth(string s)
        {
            if (!Regex.IsMatch(s, @"^\d{6}$"))
                throw new DomainErrorException("字符串必须是6位数字");
            return new YearMonth(int.Parse(s.Substring(0, 4)), int.Parse(s.Substring(4, 2)));
        }

        /// <summary>
        /// DateTime -> YearMonth
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static implicit operator YearMonth(DateTime dateTime)
        {
            return new YearMonth(dateTime);
        }

        /// <summary>
        /// YearMonth -> DateTime
        /// </summary>
        /// <param name="ym"></param>
        /// <returns></returns>
        public static implicit operator DateTime(YearMonth ym)
        {
            return new DateTime(ym.Year, ym.Month, 1);
        }
        
        public override string ToString()
        {
            if (this == YearMonth.Empty)
                return "";
            return string.Format("{0}年{1}月", Year, Month);
        }

        public string To(string format)
        {
            if (this == YearMonth.Empty)
                return "";
            return ((DateTime) this).To(format);
        }

        /// <summary>
        /// 下个月
        /// </summary>
        public YearMonth NextMonth { get { return this + 1; } }

        /// <summary>
        /// 上个月
        /// </summary>
        public YearMonth LastMonth { get { return this - 1; } }

        public bool Equals(YearMonth other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Year == Year && other.Month == Month;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == typeof(YearMonth) && Equals((YearMonth)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = base.GetHashCode();
                hashCode = (hashCode*397) ^ Year;
                hashCode = (hashCode*397) ^ Month;
                return hashCode;
            }
        }

        #region 操作符重载：< > <= >= + - == !=

        public static bool operator <(YearMonth x, YearMonth y)
        {
            return (x.Year * 12 + x.Month) < (y.Year * 12 + y.Month);
        }

        public static bool operator >(YearMonth x, YearMonth y)
        {
            return (x.Year * 12 + x.Month) > (y.Year * 12 + y.Month);
        }

        public static bool operator <=(YearMonth x, YearMonth y)
        {
            return (x.Year * 12 + x.Month) <= (y.Year * 12 + y.Month);
        }

        public static bool operator >=(YearMonth x, YearMonth y)
        {
            return (x.Year * 12 + x.Month) >= (y.Year * 12 + y.Month);
        }

        public static bool operator ==(YearMonth x, YearMonth y)
        {
            return x.Year * 12 + x.Month == y.Year * 12 + y.Month;
        }

        public static bool operator !=(YearMonth x, YearMonth y)
        {
            return !(x == y);
        }

        public static YearMonth operator +(YearMonth x, int n)
        {
            var i = 12 * x.Year + x.Month + n;
            var month = i % 12 == 0 ? 12 : i % 12;
            var year = i % 12 == 0 ? i / 12 - 1 : i / 12;
            return new YearMonth(year, month);
        }

        public static YearMonth operator -(YearMonth x, int n)
        {
            var i = 12 * x.Year + x.Month - n;
            var month = i % 12 == 0 ? 12 : i % 12;
            var year = i % 12 == 0 ? i / 12 - 1 : i / 12;
            return new YearMonth(year, month);
        }

        #endregion
    }
}