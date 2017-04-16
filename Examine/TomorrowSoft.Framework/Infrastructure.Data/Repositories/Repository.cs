using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using LinqSpecs;
using NHibernate;
using NHibernate.Linq;
using TomorrowSoft.Framework.Domain.Bases;
using TomorrowSoft.Framework.Domain.CustomType;
using TomorrowSoft.Framework.Domain.Repositories;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.Container;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.NetFramework.Exceptions;
using TomorrowSoft.Framework.Infrastructure.Data.UnitOfWorks;

namespace TomorrowSoft.Framework.Infrastructure.Data.Repositories
{
    [RegisterToContainer(typeof (IRepository))]
    public class Repository : IRepository
    {
        private readonly ISession session;

        public Repository()
        {
            session = IoC.Get<IUnitOfWork>().GetCurrentSession();
        }

        #region implement functions

        public IEnumerable<TEntity> All<TEntity>()
        {
            return session.Query<TEntity>().ToList();
        }

        public IPagedList<TEntity> All<TEntity>(int? pageSize, int? currentPage)
        {
            int count = GetCount<TEntity>();
            var pageInfo = new PageInfo(count, pageSize, currentPage);
            List<TEntity> list =
                session.Query<TEntity>()
                    .Skip(pageInfo.PageSize*(pageInfo.CurrentPage - 1))
                    .Take(pageInfo.PageSize)
                    .ToList();
            return new PagedList<TEntity>(list, pageInfo);
        }

        public IPagedList<TEntity> AllOrderByDescending<TEntity, TKey>(Func<TEntity, TKey> keySelector, int? pageSize,
            int? currentPage)
        {
            int count = GetCount<TEntity>();
            var pageInfo = new PageInfo(count, pageSize, currentPage);
            List<TEntity> list = session.Query<TEntity>()
                .OrderByDescending(keySelector)
                .Skip(pageInfo.PageSize*(pageInfo.CurrentPage - 1)).Take(pageInfo.PageSize).ToList();
            return new PagedList<TEntity>(list, pageInfo);
        }

        public void Save<TEntity>(TEntity entity)
            where TEntity : IEntity
        {
            //记录创建时间
            if (entity.DBID == Guid.Empty)
                entity.CreateTime = DateTime.Now;
            //记录最后修改时间
            entity.UpdateTime = DateTime.Now;
            session.SaveOrUpdate(entity);
        }

        public void Save<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : IEntity
        {
            foreach (TEntity entity in entities)
            {
                Save(entity);
            }
        }

        public void Insert<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            foreach (TEntity entity in entities)
            {
                session.SaveOrUpdate(session.Merge(entity));
            }
            session.Flush();
        }

        public void Remove<TEntity>(TEntity entity) where TEntity : IEntity
        {
            if (entity.CanBeDelete)
                session.Delete(entity);
        }

        public TEntity GetById<TEntity>(Guid id)
        {
            return session.Get<TEntity>(id);
        }

        public TEntity FindOne<TEntity>(params Specification<TEntity>[] specifications)
        {
            Specification<TEntity> specification = specifications.Aggregate((pre, next) => pre & next);
            IEnumerable<TEntity> entities = FindAll(specification);
            if (entities.Count() > 1)
                throw new TooManyRowsMatchingException();
            if (entities.Count() <= 0)
                throw new NoRowsMatchingException();
            return entities.Single();
        }

        public int GetCount<TEntity>(params Specification<TEntity>[] specifications)
        {
            if (specifications.Any())
            {
                Specification<TEntity> specification = specifications.Aggregate((pre, next) => pre & next);
                return GetQuery(specification).Count();
            }
            return session.Query<TEntity>().Count();
        }

        public bool IsExisted<TEntity>(params Specification<TEntity>[] specifications)
        {
            Specification<TEntity> specification = specifications.Aggregate((pre, next) => pre & next);
            return GetCount(specification) > 0;
        }

        public TEntity First<TEntity>()
        {
            TEntity entity = session.Query<TEntity>().First();
            if (entity == null)
                throw new NoRowsMatchingException();
            return entity;
        }

        public TResult Max<TEntity, TResult>(Expression<Func<TEntity, TResult>> selector,
            params Specification<TEntity>[] specifications)
        {
            if (specifications.Any())
            {
                Specification<TEntity> specification = specifications.Aggregate((pre, next) => pre & next);
                return GetQuery(specification).Max(selector);
            }
            return session.Query<TEntity>().Max(selector);
        }

        public void ExcuteCommand(string sql)
        {
            IDbCommand command = session.Connection.CreateCommand();
            session.Transaction.Enlist(command);
            command.CommandText = sql;
            command.CommandType = CommandType.Text;
            command.ExecuteNonQuery();
        }

        public IEnumerable<TEntity> FindAll<TEntity>(params Specification<TEntity>[] specifications)
        {
            if (specifications.Any())
            {
                Specification<TEntity> specification = specifications.Aggregate((pre, next) => pre & next);
                return GetQuery(specification).ToList();
            }
            return All<TEntity>();
        }

        public IPagedList<TEntity> FindAll<TEntity>(int? pageSize, int? currentPage,
            params Specification<TEntity>[] specifications)
        {
            if (specifications.Any())
            {
                Specification<TEntity> specification = specifications.Aggregate((pre, next) => pre & next);
                int count = GetCount(specification);
                var pageInfo = new PageInfo(count, pageSize, currentPage);
                List<TEntity> list =
                    GetQuery(specification)
                        .Skip(pageInfo.PageSize*(pageInfo.CurrentPage - 1))
                        .Take(pageInfo.PageSize)
                        .ToList();
                return new PagedList<TEntity>(list, pageInfo);
            }
            return All<TEntity>(pageSize, currentPage);
        }

        public IPagedList<TEntity> FindAllOrderByDescending<TEntity, TKey>(Func<TEntity, TKey> keySelector,
            int? pageSize, int? currentPage, params Specification<TEntity>[] specifications)
        {
            if (specifications.Any())
            {
                Specification<TEntity> specification = specifications.Aggregate((pre, next) => pre & next);
                int count = GetCount(specification);
                var pageInfo = new PageInfo(count, pageSize, currentPage);
                List<TEntity> list = GetQuery(specification)
                    .OrderByDescending(keySelector)
                    .Skip(pageInfo.PageSize*(pageInfo.CurrentPage - 1)).Take(pageInfo.PageSize).ToList();
                return new PagedList<TEntity>(list, pageInfo);
            }
            return AllOrderByDescending(keySelector, pageSize, currentPage);
        }

        #endregion

        private IQueryable<TEntity> GetQuery<TEntity>(Specification<TEntity> specification)
        {
            if (specification == null)
                throw new ArgumentNullException();
            return session.Query<TEntity>()
                .Where(specification.IsSatisfiedBy());
        }
    }
}