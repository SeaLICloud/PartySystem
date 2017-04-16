using NHibernate;

namespace TomorrowSoft.Framework.Infrastructure.Data.UnitOfWorks
{
    public interface IUnitOfWork
    {
        ISession GetCurrentSession();
        void Commit();
        void Rollback();
        void BindContext();
        void UnBindContext();
    }
}