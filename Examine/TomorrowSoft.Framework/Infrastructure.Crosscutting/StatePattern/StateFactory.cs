using System;
using System.Collections.Generic;
using System.Linq;
using TomorrowSoft.Framework.Domain.Bases;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.Container;

namespace TomorrowSoft.Framework.Infrastructure.Crosscutting.StatePattern
{
    public class StateFactory
    {
        public static State<T> GetCurrentState<T>(T entity) where T : Entity<T>
        {
            IEnumerable<State<T>> states = IoC.Current.ResolveAll(typeof (State<T>)).Select(x=>x as State<T>);
            return states.FirstOrDefault(x => x.IsMatch(entity));
        }

        public static State<T> GetCurrentState<T>(T entity, DateTime dateTime) where T : Entity<T>
        {
            IEnumerable<State<T>> states = IoC.Current.ResolveAll(typeof(State<T>)).Select(x => x as State<T>);
            return states.FirstOrDefault(x => x.IsMatch(entity, dateTime));
        }
    }
}