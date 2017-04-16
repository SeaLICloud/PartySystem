using System;
using System.Linq.Expressions;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.Container;
using TomorrowSoft.Framework.Infrastructure.Data.UnitOfWorks;

namespace TomorrowSoft.Framework.Infrastructure.Crosscutting.Specs
{
    public class OperationServiceSubject<TService> : IServiceSubject
    {
        private readonly TService service;
        private readonly Expression<Action<TService>> operation;

        public OperationServiceSubject(TService service, Expression<Action<TService>> operation)
        {
            this.service = service;
            this.operation = operation;
        }

        public object Invoke()
        {
            operation.Compile().Invoke(service);
            IoC.Get<IUnitOfWork>().Commit();
            return null;
        }
    }

    public class FunctionServiceSubject<TService, TResult> : IServiceSubject
    {
        private readonly TService service;
        private readonly Expression<Func<TService, TResult>> function;

        public FunctionServiceSubject(TService service, Expression<Func<TService, TResult>> function)
        {
            this.service = service;
            this.function = function;
        }

        public object Invoke()
        {
            var result = function.Compile().Invoke(service);
            IoC.Get<IUnitOfWork>().Commit();
            return result;
        }
    }
}