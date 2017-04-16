using System;
using System.Reflection;
using System.Text;
using Machine.Specifications;
using TomorrowSoft.Framework.Domain.Bases;

namespace TomorrowSoft.Framework.Application.Maps
{
    public class BaseDtoMapSpec<TDtoMap, TEntity, TDto> 
        where TDtoMap : DtoMap<TEntity, TDto>, new() 
        where TEntity : Entity<TEntity>
    {
        protected Establish context = () => subject = new TDtoMap();
        protected Because of = () => dto = subject.To(entity);

        protected static TDtoMap subject;
        protected static TEntity entity;
        protected static TDto dto;

        protected static TEntity A(TEntity entity)
        {
            var pies = typeof (TEntity).GetProperties();
            foreach(var pi in pies)
            {
                if (Value(pi) != null && pi.CanWrite)
                    pi.SetValue(entity, Value(pi), null);
            }
            return entity;
        }

        protected static T A<T>(T entity) where T : Entity<T>
        {
            var pies = typeof(T).GetProperties();
            foreach (var pi in pies)
            {
                if (Value(pi) != null && pi.CanWrite)
                    pi.SetValue(entity, Value(pi), null);
            }
            return entity;
        }

        private static object Value(PropertyInfo pi)
        {
            if (pi.PropertyType == typeof(DateTime?))
                return CreateDateTime();
            switch (pi.PropertyType.Name.ToLower())
            {
                case "string":
                    return CreateString();
                case "int":
                    return CreateInt32();
                case "decimal":
                    return CreateDecimal();
                case "datetime":
                    return CreateDateTime();
                default:
                    return null;
            }
        }

        private static string CreateString()
        {
            var rd = new Random(Guid.NewGuid().GetHashCode());
            var bytes = new byte[20];
            rd.NextBytes(bytes);
            return Convert.ToBase64String(bytes);
        }

        private static DateTime CreateDateTime()
        {
            var rd = new Random(Guid.NewGuid().GetHashCode());
            var year = rd.Next(1960, 1991);
            var month = rd.Next(1, 13);
            var day = rd.Next(1, 28);
            return new DateTime(year, month, day);
        }

        private static int CreateInt32()
        {
            var rd = new Random(Guid.NewGuid().GetHashCode());
            return rd.Next(0, 10000);
        }

        private static decimal CreateDecimal()
        {
            var rd = new Random(Guid.NewGuid().GetHashCode());
            return Convert.ToDecimal(rd.NextDouble() + CreateInt32());
        }
    }
}