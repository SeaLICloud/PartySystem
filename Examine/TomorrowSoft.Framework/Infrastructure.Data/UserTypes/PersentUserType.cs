using System;
using System.Data;
using NHibernate;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;
using TomorrowSoft.Framework.Domain.CustomType;

namespace TomorrowSoft.Framework.Infrastructure.Data.UserTypes
{
    public class PersentUserType : IUserType
    {
        public new bool Equals(object x, object y)
        {
            const double epsilon = 0.000001;

            if (x is Persent
                && y is Persent
                && Math.Abs(((Persent) x).Value - ((Persent) y).Value) < epsilon)
                return true;
            return false;
        }

        public int GetHashCode(object x)
        {
            return x.GetHashCode();
        }

        public object NullSafeGet(IDataReader rs, string[] names, object owner)
        {
            string value = (string)NHibernateUtil.String.NullSafeGet(rs, names[0]);
                return (Persent)value;
        }

        public void NullSafeSet(IDbCommand cmd, object value, int index)
        {
            if(value == null)
                ((IDataParameter)cmd.Parameters[index]).Value = DBNull.Value;
            ((IDataParameter)cmd.Parameters[index]).Value = value.ToString();
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
            get { return new[] { NHibernateUtil.String.SqlType }; }
        }

        public Type ReturnedType
        {
            get { return typeof(Persent); }
        }

        public bool IsMutable
        {
            get { return false; }
        }
    }
}