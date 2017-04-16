using System;

namespace TomorrowSoft.Framework.Authorize.Domain
{
    /// <summary>
    /// 流水号掩码
    /// </summary>
    public struct RunningNumberMask
    {
        public RunningNumberMask(string prefix, string time, string serial) : this()
        {
            Prefix = prefix;
            Time = time;
            Serial = serial;
        }

        /// <summary>
        /// 前缀部分
        /// </summary>
        public string Prefix { get; private set; }
        /// <summary>
        /// 时间部分，y表示年，M表示月，d表示日期
        /// </summary>
        public string Time { get; private set; }
        /// <summary>
        /// 流水部分, 必须是全0
        /// </summary>
        public string Serial { get; private set; }

        public override string ToString()
        {
            return string.Format("{0}{1}{2}", Prefix, Time, Serial);
        }

        public string Next(string lastNumber, DateTime dt)
        {
            var lastPrefixAndTime = lastNumber.Length < Prefix.Length + Time.Length 
                ? "" 
                : lastNumber.Substring(0, Prefix.Length + Time.Length);
            var nextPrefixAndTime = string.Format("{0}{1}", Prefix, dt.ToString(Time));
            int serial = 0;
            if (lastPrefixAndTime == nextPrefixAndTime)
                serial = Convert.ToInt32(lastNumber.Substring(Prefix.Length + Time.Length));
            return string.Format("{0}{1}", nextPrefixAndTime, (serial + 1).ToString(Serial));
        }
    }
}