using System;

namespace TomorrowSoft.Framework.Infrastructure.Crosscutting.Extensions
{
    public static class DateTimeExtensionMethods
    {
         public static int Age(this DateTime date)
         {
             DateTime now = DateTime.Now;
             int age = DateTime.Now.Year - date.Year;
             if (now.Month == date.Month)
                 age = (now.Day < date.Day) ? age - 1 : age;
             else if (now.Month < date.Month)
                 age = age - 1;
             return age;
         }

        public static DateTime SqlServerMinValue(this DateTime dateTime)
        {
            return new DateTime(1753, 1, 1, 12, 0, 0);
        }

        public static string To(this DateTime dateTime)
        {
            if (dateTime == new DateTime().SqlServerMinValue() || dateTime == DateTime.MinValue)
                return "";
            return dateTime.ToString();
        }

        public static string To(this DateTime dateTime, string format)
        {
            if (dateTime == new DateTime().SqlServerMinValue() || dateTime == DateTime.MinValue)
                return "";
            return dateTime.ToString(format);
        }
    }
}