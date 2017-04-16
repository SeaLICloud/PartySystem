using TomorrowSoft.Framework.Domain.Bases;

namespace TomorrowSoft.Framework.Authorize.Domain
{
    /// <summary>
    /// 角色权限
    /// </summary>
    public partial class AccountAuthority : Entity<AccountAuthority>, IAuthority
    {
        public virtual AccountAuthorityIdentifier Id
        {
            get { return AccountAuthorityIdentifier.of(AccountId, Function.Id); }
        }

        protected AccountAuthority()
        {
        }

        public AccountAuthority(Account account, Function function)
            : this()
        {
            AccountId = account.Id;
            Function = function;
        }

        public virtual AccountIdentifier AccountId { get; protected set; }

        public virtual string Identifier { get { return Id; } }

        /// <summary>
        /// 可授权的功能
        /// </summary>
        public virtual Function Function { get; protected set; }

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