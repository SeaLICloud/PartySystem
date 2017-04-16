using System;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.Extensions;

namespace TomorrowSoft.Framework.Domain.Bases
{
    public class Entity<TEntity> : IEntity where TEntity : IEntity
    {
        public Entity()
        {
            InitPropertyOfDateTimeType();
        }

        /// <summary>
        /// 初始所有时间类型为SqlServer的最小时间
        /// </summary>
        private void InitPropertyOfDateTimeType()
        {
            var type = this.GetType();
            var pies = type.GetProperties();
            foreach(var pi in pies)
            {
                if(pi.PropertyType == typeof(DateTime))
                {
                    pi.SetValue(this, new DateTime().SqlServerMinValue(), null);
                }
            }
        }

        public virtual Guid DBID { get; protected set; }
        public virtual DateTime CreateTime { get; set; }
        public virtual DateTime UpdateTime { get; set; }
        public virtual string CreateUser { get; set; }
        public virtual string UpdateUser { get; set; }
        public virtual bool CanBeDelete{ get { return true; } }
        public virtual string Remark { get; set; }
    }
}