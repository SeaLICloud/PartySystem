using System;
using System.Data;
using NHibernate;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;
using TomorrowSoft.Framework.Domain.CustomType;

namespace TomorrowSoft.Framework.Infrastructure.Data.UserTypes
{
    public class YearMonthUserType : IUserType
    {
        public new bool Equals(object x, object y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x == null && y == null) return true;
            if (x == null || y == null) return false;
            return x.Equals(y);
        }

        public int GetHashCode(object x)
        {
            return x == null ? 0 : x.GetHashCode();
        }

        public object NullSafeGet(IDataReader rs, string[] names, object owner)
        {
            var value = NHibernateUtil.String.NullSafeGet(rs, names[0]);
            return (value == null) ? YearMonth.Empty : new YearMonth(Convert.ToDateTime(value));
        }

        public void NullSafeSet(IDbCommand cmd, object value, int index)
        {
            if ((YearMonth)value == YearMonth.Empty)
                ((IDataParameter)cmd.Parameters[index]).Value = DBNull.Value;
            else
                ((IDataParameter) cmd.Parameters[index]).Value = (DateTime)(YearMonth)value;
        }

        public object DeepCopy(object value)
        {
            return value;
        }

        public object Replace(object original, object target, object owner)
        {
            return original;
        }

        public object Assemble(object cached, object owner)
        {
            return DeepCopy(cached);
        }

        public object Disassemble(object value)
        {
            return DeepCopy(value);
        }

        public SqlType[] SqlTypes
        {
            get { return new[] { NHibernateUtil.DateTime.SqlType }; }
        }

        public Type ReturnedType
        {
            get { return typeof(YearMonth); }
        }

        public bool IsMutable
        {
            get { return false; }
        }
    }
}