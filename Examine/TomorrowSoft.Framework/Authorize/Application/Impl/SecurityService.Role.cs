using System.Collections.Generic;
using TomorrowSoft.Framework.Authorize.Domain;
using TomorrowSoft.Framework.Domain.Exceptions;

namespace TomorrowSoft.Framework.Authorize.Application.Impl
{
    public partial class SecurityService
    {
        public IEnumerable<Role> GetRoles()
        {
            return repository.All<Role>();
        }

        public Role GetRole(RoleIdentifier id)
        {
            if (!repository.IsExisted(new Role.By(id)))
                throw new DomainErrorException("角色不存在");
            return repository.FindOne(new Role.By(id));
        }

        public IRoleCommand CreateRole(string roleName)
        {
            var role = new Role(RoleIdentifier.of(roleName));
            repository.Save(role);
            return new RoleCommand(role, repository);
        }

        public IEnumerable<Account> GetAccountsByRole(string roleName)
        {
            return repository.FindAll(new Account.ByRoleName(roleName));
        }

        public IRoleCommand UpdateRole(RoleIdentifier id)
        {
            return new RoleCommand(GetRole(id), repository);
        }
    }
}