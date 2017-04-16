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
    public class BaseActionSpec
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

        protected static IActionSubject subject;
        protected static IRepository repository;

        protected static T A<T>()
        {
            return IoC.Get<T>();
        }

        protected static IActionSubject Action<TController>(Expression<Func<TController, ActionResult>> action)
            where TController : Controller
        {
            return new ActionSubject<TController>(A<TController>(), action);
        }

        protected static T AsModel<T>(ActionResult result)
        {
            return (T) (((ViewResult) result).ViewData.Model);
        }
    }
}