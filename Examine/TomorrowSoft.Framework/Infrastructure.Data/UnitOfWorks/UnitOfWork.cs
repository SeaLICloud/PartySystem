using NHibernate;
using NHibernate.Context;

namespace TomorrowSoft.Framework.Infrastructure.Data.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ISessionFactory _sessionFactory;

        public UnitOfWork(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public ISession GetCurrentSession()
        {
            return _sessionFactory.GetCurrentSession();
        }

        public void Commit()
        {
            var transaction = GetCurrentSession().Transaction;
            if (transaction.IsActive && !transaction.WasRolledBack && !transaction.WasCommitted)
                transaction.Commit();
        }

        public void Rollback()
        {
            var transaction = GetCurrentSession().Transaction;
            if (transaction.IsActive && !transaction.WasRolledBack && !transaction.WasCommitted)
                transaction.Rollback();
        }

        public void BindContext()
        {
            if (!CurrentSessionContext.HasBind(_sessionFactory))
            {
                var session = _sessionFactory.OpenSession();
                session.BeginTransaction();
                CurrentSessionContext.Bind(session);
            }
        }

        private void Close()
        {
            var session = GetCurrentSession();
            if (session.IsOpen)
                session.Close();
        }

        public void UnBindContext()
        {
            if (CurrentSessionContext.HasBind(_sessionFactory))
            {
                Commit();
                Close();
                CurrentSessionContext.Unbind(_sessionFactory);
            }
        }
    }
}