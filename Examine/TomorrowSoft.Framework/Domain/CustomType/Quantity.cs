using System;
using System.Diagnostics.Contracts;
using TomorrowSoft.Framework.Domain.Bases;
using TomorrowSoft.Framework.Domain.Exceptions;

namespace TomorrowSoft.Framework.Domain.CustomType
{
    /// <summary>
    /// 数量
    /// </summary>
    public struct Quantity
    {
        public Quantity(double amount, string unit) : this()
        {
            Amount = amount;
            Unit = unit;
        }

        /// <summary>
        /// 数字
        /// </summary>
        public double Amount { get; private set; }

        /// <summary>
        /// 计量单位
        /// </summary>
        public string Unit { get; private set; }
        
        public static Quantity operator +(Quantity left, Quantity right)
        {
            if (!left.Unit.Equals(right.Unit))
                throw new DomainErrorException("计量单位必须一致");
            return new Quantity(left.Amount + right.Amount, left.Unit);
        }

        public static Quantity operator -(Quantity left, Quantity right)
        {
            if (!left.Unit.Equals(right.Unit))
                throw new DomainErrorException("计量单位必须一致");
            return new Quantity(left.Amount - right.Amount, left.Unit);
        }

        public static Quantity operator *(Quantity left, double multiplicator)
        {
            return new Quantity(left.Amount*multiplicator, left.Unit);
        }

        public static Quantity operator /(Quantity left, double divisor)
        {
            if(divisor==0)
                throw new DomainErrorException("除数不能为0");
            return new Quantity(left.Amount/divisor, left.Unit);
        }

        public static bool operator >(Quantity left, Quantity right)
        {
            if (!left.Unit.Equals(right.Unit))
                throw new DomainErrorException("计量单位必须一致");
            return left.Amount > right.Amount;
        }

        public static bool operator <(Quantity left, Quantity right)
        {
            if (!left.Unit.Equals(right.Unit))
                throw new DomainErrorException("计量单位必须一致");
            return left.Amount < right.Amount;
        }

        public static bool operator ==(Quantity left, Quantity right)
        {
            if (!left.Unit.Equals(right.Unit))
                throw new DomainErrorException("计量单位必须一致");
            return left.Amount == right.Amount;
        }

        public static bool operator !=(Quantity left, Quantity right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return string.Format("{0}{1}", Amount, Unit);
        }
    }
}