using System.Collections.Generic;
using System.Linq;
using TomorrowSoft.Framework.Domain.Exceptions;

namespace TomorrowSoft.Framework.Authorize.Domain
{
    public  interface ISortable
    {
        double OrderNumber { get; set; }
    }

    public static class SortableCollection
    {
        public static void MoveUp<TEntity>(this IEnumerable<TEntity> collections, TEntity entity)
            where TEntity : ISortable
        {

            var list = collections.ToList();
            var index = list.IndexOf(entity);
            if (index == 0)
                throw new DomainWarningException("已经是第一个了！");
            else if (index == 1)
                entity.OrderNumber = (int)(list[index - 1].OrderNumber + 1);
            else
                entity.OrderNumber = (list[index - 1].OrderNumber + list[index - 2].OrderNumber) / 2;

        }

        public static void MoveDown<TEntity>(this IEnumerable<TEntity> collections, TEntity entity)
            where TEntity : ISortable
        {
            var list = collections.ToList();
            var index = list.IndexOf(entity);
            if (index == list.Count - 1)
                throw new DomainWarningException("已经是最后一个了！");
            if (index == list.Count - 2)
                entity.OrderNumber = (int)((list[index + 1]).OrderNumber / 2);
            else
                entity.OrderNumber = (list[index + 1].OrderNumber + list[index + 2].OrderNumber) / 2;
        }

        public static void MoveTop<TEntity>(this IEnumerable<TEntity> collections, TEntity entity)
            where TEntity : ISortable
        {
            var list = collections.ToList();
            var index = list.IndexOf(entity);
            if (index == 0)
                throw new DomainWarningException("已经是第一个了！");
            entity.OrderNumber = (int)list[0].OrderNumber + 1;
        }

        public static double NewOrderNumber<TEntity>(this IEnumerable<TEntity> collections)
            where TEntity : ISortable
        {
            if (collections == null || !collections.Any())
                return 1;
            return (int)(collections.Max(x => x.OrderNumber) + 1);
        }
    }
}