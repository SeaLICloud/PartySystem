using LinqSpecs;
using TomorrowSoft.Framework.Domain.Bases;

namespace TomorrowSoft.Framework.Authorize.Domain
{
    public partial class Function : Entity<Function>
    {
        public virtual FunctionIdentifier Id
        {
            get { return FunctionIdentifier.of(DBID); }
        }

        /// <summary>
        /// 子系统名称
        /// </summary>
        public virtual string Area { get; set; }

        /// <summary>
        /// Controller名称
        /// </summary>
        public virtual string Controller { get; set; }

        /// <summary>
        /// Action名称
        /// </summary>
        public virtual string Action { get; set; }
        /// <summary>
        /// 权限描述
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// 所属权限组名
        /// </summary>
        public virtual string Group { get; set; }

        /// <summary>
        /// 权限组图标
        /// </summary>
        public virtual string GroupIco { get; set; }

        /// <summary>
        /// 菜单对应的Action
        /// </summary>
        public virtual string MenuAction { get; set; }

        /// <summary>
        /// 菜单描述
        /// </summary>
        public virtual string MenuDescription { get; set; }

        /// <summary>
        /// 是否生成菜单
        /// </summary>
        /// <returns></returns>
        public virtual bool IsCreateMenu()
        {
            return !string.IsNullOrEmpty(MenuAction);
        }
    }
}