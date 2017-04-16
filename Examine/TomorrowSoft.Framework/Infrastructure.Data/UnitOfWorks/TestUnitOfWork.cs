using System;
using NHibernate;
using NHibernate.Context;

namespace TomorrowSoft.Framework.Infrastructure.Data.UnitOfWorks
{
    public class TestUnitOfWork : IUnitOfWork
    {
        private readonly ISessionFactory _sessionFactory;
        private readonly ISession _session;

        public TestUnitOfWork(ISessionFactory sessionFactory, ISession session)
        {
            _sessionFactory = sessionFactory;
            _session = session;
        }

        public ISession GetCurrentSession()
        {
            return _session;
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
                var session = GetCurrentSession();
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