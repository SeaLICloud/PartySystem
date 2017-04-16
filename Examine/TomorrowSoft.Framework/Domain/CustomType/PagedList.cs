using System.Collections.Generic;

namespace TomorrowSoft.Framework.Domain.CustomType
{
    public class PagedList<T> : List<T>, IPagedList<T>
    {
        public PagedList(IEnumerable<T> collection, PageInfo pageInfo) : base(collection)
        {
            PageInfo = pageInfo;
        }

        public PageInfo PageInfo { get; private set; }
    }
}