using TomorrowSoft.Framework.Domain.Exceptions;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.Container;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.NetFramework.Keys;

namespace TomorrowSoft.Framework.Domain.CustomType
{
    public struct PageInfo
    {
        public PageInfo(int total_count, int? pageSize, int? currentPage)
            : this()
        {
            if (!currentPage.HasValue)
                currentPage = 1;
            if (!pageSize.HasValue)
                pageSize = IoC.Get<IConfigurationKeys>().PageSize;

            TotalCount = total_count;
            PageSize = pageSize.Value;
            CurrentPage = currentPage.Value;

            if (TotalCount == 0)
            {
                CurrentPage = 0;
                PageCount = 0;
                Start = 0;
                End = 0;
            }
            else
            {
                PageCount = TotalCount%PageSize == 0 ? TotalCount/PageSize : TotalCount/PageSize + 1;

                //当前页必须在1~最大页数之间
                if (CurrentPage < 1)
                {
                    CurrentPage = 1;
                    throw new DomainErrorException("已经是第一页");
                }
                if (CurrentPage > PageCount)
                {
                    CurrentPage = PageCount;
                    throw new DomainErrorException("已经是最后一页");
                }

                Start = PageSize*(CurrentPage - 1) + 1;
                End = CurrentPage == PageCount ? TotalCount : PageSize*CurrentPage;
            }
        }

        /// <summary>
        /// 记录总数
        /// </summary>
        public int TotalCount { get; private set; }
        /// <summary>
        /// 每页条数
        /// </summary>
        public int PageSize { get; private set; }
        /// <summary>
        /// 当前页号
        /// </summary>
        public int CurrentPage { get; private set; }
        /// <summary>
        /// 页码总数
        /// </summary>
        public int PageCount { get; private set; }
        /// <summary>
        /// 当前页起始记录序号
        /// </summary>
        public int Start { get; private set; }
        /// <summary>
        /// 当前页结束记录序号
        /// </summary>
        public int End { get; private set; }
    }
}