using System;
using System.Configuration;

namespace TomorrowSoft.Framework.Domain.CustomType
{
    public struct Persent
    {
        public static Persent of(string data)
        {
            return (Persent) data;
        }

        /// <summary>
        /// 值。通常用于计算，如40%，该值为0.4
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// 分子部分。如40%，则该值为40
        /// </summary>
        public double Numerator { get { return Value*100; } }

        public override string ToString()
        {
            return string.Format(@"{0}%", Numerator);
        }

        public static implicit operator string(Persent persent)
        {
            return persent.ToString();
        }

        public static implicit operator Persent(string data)
        {
            if(!data.Contains("%"))
                throw new ArgumentException("比例设置不正确");
            var persent = new Persent()
                {
                    Value = Convert.ToDouble(data.Substring(0, data.Length-1))/100
                };
            return persent;
        }
    }
}

