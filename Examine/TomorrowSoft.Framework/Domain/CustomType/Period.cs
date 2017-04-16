using System;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.Extensions;

namespace TomorrowSoft.Framework.Domain.CustomType
{
    public struct Period
    {
        public Period(DateTime beginTime, DateTime endTime) : this()
        {
            BeginTime = beginTime;
            EndTime = endTime;
        }

        public DateTime BeginTime { get; private set; }
        public DateTime EndTime { get; private set; }

        public bool IsOverlap(Period other)
        {
            if (this.EndTime < other.BeginTime || this.BeginTime > other.EndTime)
                return false;
            return true;
        }

        public bool IsContain(Period other)
        {
            if (this.BeginTime >= other.BeginTime && this.EndTime <= other.EndTime)
                return true;
            return false;
        }

        public bool IsContain(DateTime dateTime)
        {
            if (dateTime >= this.BeginTime && dateTime < this.EndTime)
                return true;
            return false;
        }
        
        public bool Before(DateTime dateTime)
        {
            return dateTime > EndTime;
        }
        
        public bool After(DateTime dateTime)
        {
            return dateTime < BeginTime;
        }

        public string ToString(string dateFormat, string timeFormat, string timeStyle)
        {
            var dateTimeTime = string.IsNullOrEmpty(timeStyle) ? "{0} {1} ~ {2}" : string.Format("{{0}} <{0}>{{1}} ~ {{2}}</{0}>", timeStyle);
            var dateTimeDateTime = string.IsNullOrEmpty(timeStyle) ? "{0} {1} ~ {2} {3}" : string.Format("{{0}} <{0}>{{1}}</{0}> ~ {{2}} <{0}>{{3}}</{0}>", timeStyle);

            if (BeginTime.Date.Equals(EndTime.Date))
                return string.Format(dateTimeTime,
                                     BeginTime.ToString(dateFormat),
                                     BeginTime.ToString(timeFormat),
                                     EndTime.ToString(timeFormat));
            else
                return string.Format(dateTimeDateTime,
                                     BeginTime.ToString(dateFormat),
                                     BeginTime.ToString(timeFormat),
                                     EndTime.ToString(dateFormat),
                                     EndTime.ToString(timeFormat));
        }

        public static Period Default
        {
            get { return new Period(new DateTime().SqlServerMinValue(), new DateTime().SqlServerMinValue()); }
        }
    }
}