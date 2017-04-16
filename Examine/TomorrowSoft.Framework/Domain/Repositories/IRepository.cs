using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using LinqSpecs;
using TomorrowSoft.Framework.Domain.Bases;
using TomorrowSoft.Framework.Domain.CustomType;

namespace TomorrowSoft.Framework.Domain.Repositories
{
    public interface IRepository
    {
        TEntity GetById<TEntity>(Guid id);

        IEnumerable<TEntity> All<TEntity>();

        IPagedList<TEntity> All<TEntity>(int? pageSize, int? currentPage);
        IPagedList<TEntity> AllOrderByDescending<TEntity, TKey>(Func<TEntity, TKey> keySelector, int? pageSize, int? currentPage);

        void Remove<TEntity>(TEntity entity) where TEntity : IEntity;

        void Save<TEntity>(TEntity entity) where TEntity : IEntity;

        void Save<TEntity>(IEnumerable<TEntity> entities) where TEntity : IEntity;

        /// <summary>
        /// 通常在做数据库切换时用于数据库导入导出
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        void Insert<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

        IEnumerable<TEntity> FindAll<TEntity>(params Specification<TEntity>[] specifications);

        IPagedList<TEntity> FindAll<TEntity>(int? pageSize, int? currentPage, params Specification<TEntity>[] specifications);

        IPagedList<TEntity> FindAllOrderByDescending<TEntity, TKey>(Func<TEntity,TKey> keySelector, int? pageSize, int? currentPage, params Specification<TEntity>[] specifications);

        TEntity FindOne<TEntity>(params Specification<TEntity>[] specifications);

        int GetCount<TEntity>(params Specification<TEntity>[] specifications);

        bool IsExisted<TEntity>(params Specification<TEntity>[] specifications);

        TEntity First<TEntity>();

        TResult Max<TEntity, TResult>(Expression<Func<TEntity, TResult>> selector,
                                      params Specification<TEntity>[] specifications);

        void ExcuteCommand(string sql);
    }
}