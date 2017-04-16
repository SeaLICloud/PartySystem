using System;
using System.Collections.Generic;
using TomorrowSoft.Framework.Authorize.Domain;
using TomorrowSoft.Framework.Domain.Exceptions;

namespace TomorrowSoft.Framework.Authorize.Application.Impl
{
    public partial class SecurityService
    {
        public IEnumerable<Account> GetAccounts()
        {
            return repository.FindAll<Account>();
        }

        public Account GetAccount(AccountIdentifier id)
        {
            if(!repository.IsExisted(new Account.By(id)))
                throw new DomainErrorException("用户名不存在");
            return repository.FindOne(new Account.By(id));
        }

        public IAccountCommand CreateAccount(string userName)
        {
            if (IsDuplicateUserName(userName))
                throw new DomainErrorException("用户名已存在！");

            var user = new Account(AccountIdentifier.of(userName));
            repository.Save(user);
            return new AccountCommand(user, passwordSecurity, repository);
        }

        public IAccountCommand UpdateAccount(AccountIdentifier id)
        {
            return new AccountCommand(GetAccount(id), passwordSecurity, repository);
        }

        public void DeleteAccount(AccountIdentifier id)
        {
            if (id.UserName == "admin" || id.UserName == "TomorrowSoft")
                throw new Exception("管理员不能删除");
            repository.Remove(GetAccount(id));
        }
        
        public bool ValidateAccount(string userName, string password, out string errorMessage)
        {
            var id = AccountIdentifier.of(userName);
            if (!repository.IsExisted(new Account.By(id)))
            {
                errorMessage = "用户名不存在";
                return false;
            }
            var user = GetAccount(id);
            if (!passwordSecurity.ComparePasswords(user.Password, password))
            {
                errorMessage = "用户名或密码不正确";
                return false;
            }
            errorMessage = "";
            return true;
        }
        
        public bool IsDuplicateUserName(string userName)
        {
            return repository.IsExisted(new Account.By(AccountIdentifier.of(userName)));
        }
    }
}