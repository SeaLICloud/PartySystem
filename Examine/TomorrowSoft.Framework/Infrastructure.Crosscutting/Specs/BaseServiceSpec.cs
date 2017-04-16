using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using Machine.Specifications;
using NHibernate;
using TomorrowSoft.Framework.Domain.Repositories;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.Container;
using TomorrowSoft.Framework.Infrastructure.Data.SessionFactories;
using TomorrowSoft.Framework.Infrastructure.Data.UnitOfWorks;

namespace TomorrowSoft.Framework.Infrastructure.Crosscutting.Specs
{
    public class BaseServiceSpec
    {
        protected Establish firstContext =
            () =>
            {
                Database.InitTestDatabase();
                IoC.Get<IUnitOfWork>().BindContext();
                repository = A<IRepository>();
            };

        protected Cleanup ofter =
            () =>
            {
                IoC.Get<IUnitOfWork>().UnBindContext();
            };

        protected static IServiceSubject subject;
        protected static IRepository repository;

        protected static T A<T>()
        {
            return IoC.Get<T>();
        }

        protected static IServiceSubject Action<TService>(Expression<Action<TService>> operation)
        {
            return new OperationServiceSubject<TService>(A<TService>(), operation);
        } 

        protected static IServiceSubject Function<TService>(Expression<Func<TService, object>> function)
        {
            return new FunctionServiceSubject<TService, object>(A<TService>(), function);
        }
    }
}