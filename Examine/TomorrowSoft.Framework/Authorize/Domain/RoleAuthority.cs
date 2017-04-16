using TomorrowSoft.Framework.Domain.Bases;

namespace TomorrowSoft.Framework.Authorize.Domain
{
    /// <summary>
    /// 角色权限
    /// </summary>
    public partial class RoleAuthority : Entity<RoleAuthority>, IAuthority
    {
        public virtual RoleAuthorityIdentifier Id
        {
            get { return RoleAuthorityIdentifier.of(RoleId, Function.Id); }
        }

        protected RoleAuthority()
        {
        }

        public RoleAuthority(Role role, Function function) : this()
        {
            RoleId = role.Id;
            Function = function;
        }

        public virtual RoleIdentifier RoleId { get; set; }

        public virtual string Identifier { get { return Id; } }

        /// <summary>
        /// 可授权的功能
        /// </summary>
        public virtual Function Function { get; set; }

        /// <summary>
        /// 是否已授权
        /// </summary>
        public virtual bool IsAuthorized { get; set; }

        /// <summary>
        /// 改变权限的授予情况
        /// </summary>
        public virtual void Change()
        {
            IsAuthorized = !IsAuthorized;
        }
    }
}