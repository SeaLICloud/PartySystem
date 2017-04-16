using System.Collections.Generic;
using System.Linq;
using TomorrowSoft.Framework.Domain.Bases;
using TomorrowSoft.Framework.Domain.Exceptions;

namespace TomorrowSoft.Framework.Authorize.Domain
{
    public partial class Account : Entity<Account>
    {
        public virtual AccountIdentifier Id { get; protected set; }

        protected Account()
        {
            accountauthorities = new List<AccountAuthority>();
            roles = new List<Role>();
            UseRoleAuthority = true;
        }

        public Account(AccountIdentifier identifier) : this()
        {
            Id = identifier;
        }
        
        /// <summary>
        /// 注册邮箱
        /// </summary>
        public virtual string Email { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public virtual byte[] Password { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public virtual IEnumerable<Role> Roles
        {
            get { return roles; }
        }
        private IList<Role> roles;

        /// <summary>
        /// 用户权限
        /// </summary>
        public virtual IEnumerable<AccountAuthority> AccountAuthorities
        {
            get { return accountauthorities; }
        }
        private IList<AccountAuthority> accountauthorities;

        public virtual IEnumerable<IAuthority> GetAuthorities()
        {
            return UseRoleAuthority ? Roles.GetAuthorities() : AccountAuthorities;
        }

        /// <summary>
        /// 使用角色授权
        /// </summary>
        public virtual bool UseRoleAuthority { get; set; }
        
        /// <summary>
        /// 将用户添加到角色
        /// </summary>
        /// <param name="role">角色</param>
        public virtual void AddRole(Role role)
        {
            if(roles.Count(x=>x.Id == role.Id)>0)
                throw new DomainErrorException("用户已经是该角色");
            roles.Add(role);
        }

        public virtual bool IsInRole(string roleName)
        {
            return roles.Any(x => x.Id.RoleName == roleName);
        }

        /// <summary>
        /// 清空个人权限
        /// </summary>
        public virtual void ClearAccountAuthorities()
        {
            accountauthorities.Clear();
        }

        /// <summary>
        /// 从角色权限复制到个人权限
        /// </summary>
        /// <param name="authorities"></param>
        public virtual void CopyAccoutAuthorities(IEnumerable<IAuthority> authorities)
        {
            foreach (var authority in authorities)
            {
                accountauthorities.Add(
                    new AccountAuthority(this, authority.Function)
                        {
                            IsAuthorized = authority.IsAuthorized
                        });
            }
        }
    }
}