using System.Collections.Generic;

namespace TomorrowSoft.Framework.Domain.CustomType
{
    public interface IPagedList<T> : IList<T>
    {
        PageInfo PageInfo { get; }
    }
}