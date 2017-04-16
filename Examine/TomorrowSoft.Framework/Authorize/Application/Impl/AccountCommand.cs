using System;
using System.Linq;
using TomorrowSoft.Authorize.Domain.Services;
using TomorrowSoft.Framework.Authorize.Domain;
using TomorrowSoft.Framework.Domain.Repositories;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.Container;

namespace TomorrowSoft.Framework.Authorize.Application.Impl
{
    [RegisterToContainer]
    public class AccountCommand : IAccountCommand
    {
        private readonly Account account;
        private readonly IPasswordSecurity passwordSecurity;
        private readonly IRepository repository;

        public AccountCommand(Account account, IPasswordSecurity passwordSecurity, IRepository repository)
        {
            this.account = account;
            this.passwordSecurity = passwordSecurity;
            this.repository = repository;
        }

        public IAccountCommand ChangePassword(string oldPassword, string newPassword, string confirmPassword)
        {
            if(newPassword!=confirmPassword)
                throw new ApplicationException("两次输入的密码不一致");
            if (!passwordSecurity.ComparePasswords(account.Password, oldPassword))
                throw new ApplicationException("旧密码不正确");
            account.Password = passwordSecurity.CreateDbPassword(newPassword);
            return this;
        }

        public IAccountCommand ResetPassword(string password)
        {
            account.Password = passwordSecurity.CreateDbPassword(password);
            return this;
        }

        public IAccountCommand Email(string email)
        {
            account.Email = email;
            return this;
        }

        public IAccountCommand UseAccountAuthority()
        {
            if(account.UseRoleAuthority)
            {
                account.CopyAccoutAuthorities(account.GetAuthorities());
                account.UseRoleAuthority = false;
                //var authorities = repository.FindAll(new RoleAuthority.ByRoles(account.GetRoles().ToArray()));
                //foreach(var authority in authorities)
                //{
                //    var accountAuthority = RoleAuthority.ForAccount(account.Identifier.UserName, authority.Function);
                //    accountAuthority.IsAuthorized = authority.IsAuthorized;
                //    repository.Save(accountAuthority);
                //}
            }
            return this;
        }

        public IAccountCommand UseRoleAuthority()
        {
            if (!account.UseRoleAuthority)
            {
                account.ClearAccountAuthorities();
                account.UseRoleAuthority = true;
            }
            return this;
        }

        public IAccountCommand AddRole(RoleIdentifier id)
        {
            var role = repository.FindOne(new Role.By(id));
            account.AddRole(role);
            return this;
        }
    }
}