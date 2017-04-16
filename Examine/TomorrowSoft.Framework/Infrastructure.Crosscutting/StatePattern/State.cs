using System;
using TomorrowSoft.Framework.Domain.Bases;

namespace TomorrowSoft.Framework.Infrastructure.Crosscutting.StatePattern
{
    public abstract class State<T> : IState
        where T:Entity<T>
    {
        public virtual bool IsMatch(T entity)
        {
            return true;
        }

        public virtual bool IsMatch(T entity, DateTime dateTime)
        {
            return true;
        }
        public abstract string Name { get; }
    }
}