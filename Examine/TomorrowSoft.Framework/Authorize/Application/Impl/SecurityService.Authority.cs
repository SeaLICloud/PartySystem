using System;
using System.Collections.Generic;
using System.Linq;
using TomorrowSoft.Framework.Authorize.Domain;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.Excel;

namespace TomorrowSoft.Framework.Authorize.Application.Impl
{
    public partial class SecurityService
    {
        public IEnumerable<IAuthority> GetAuthorities()
        {
            return repository.All<RoleAuthority>()
                .Distinct()
                .OrderBy(x => x.Id.RoleId)
                .ThenBy(x => x.Function.Area)
                .ThenBy(x => x.Function.Controller);
        }

        public IEnumerable<IAuthority> GetAuthorities(string[] roles)
        {
            return repository.FindAll(new RoleAuthority.ByRoles(roles));
        }
        
        public IEnumerable<IAuthority> GetAuthorities(AccountIdentifier id)
        {
            var account = GetAccount(id);
            //if (account.UseRoleAuthority)
            //{
            //    var roles = GetRoles(id.UserName);
            //    return repository.FindAll(new RoleAuthority.ByRoles(roles.ToArray()));
            //}
            //return repository.FindAll(new RoleAuthority.ByAccount(id));
            return account.GetAuthorities();
        }

        public void ImportAuthorities(string data, params Role[] roles)
        {
            var import = new Import<RoleAuthority>()
                .New(row => new RoleAuthority(new Role(RoleIdentifier.of(row["RoleName"].ToString())), new Function()))
                .Map((obj, row) => obj.Function.Area = row["Area"].ToString())
                .Map((obj, row) => obj.Function.Controller = row["Controller"].ToString())
                .Map((obj, row) => obj.Function.Action = row["Action"].ToString())
                .Map((obj, row) => obj.Function.Description = row["Description"].ToString())
                .Map((obj, row) => obj.Function.MenuAction = row["MenuAction"].ToString())
                .Map((obj, row) => obj.Function.MenuDescription = row["MenuDescription"].ToString())
                .Map((obj, row) => obj.Function.Group = row["Group"].ToString())
                .Map((obj, row) => obj.Function.GroupIco = row["GroupIco"].ToString());
            var authoritiesForRoles = import.MapTo(data);

            //保存功能到数据库
            repository.Save(authoritiesForRoles.Select(x => x.Function));

            //保存授权
            foreach (var role in roles)
            {
                foreach (var item in authoritiesForRoles)
                {
                    var roleNames = item.Id.RoleId.RoleName.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);
                    role.AddAuthority(new RoleAuthority(role, item.Function)
                                          {
                                              IsAuthorized = roleNames.Contains(role.Id.RoleName)
                                          });
                }
            }
        }

        public string ChangeRoleAuthority(RoleAuthorityIdentifier id)
        {
            var authority = repository.FindOne(new RoleAuthority.By(id));
            authority.Change();
            return string.Format("{0}【{1}】功能！",
                                 authority.IsAuthorized ? "开启" : "关闭",
                                 authority.Function.Description);
        }

        public RoleAuthority GetRoleAuthority(RoleAuthorityIdentifier id)
        {
            return repository.FindOne(new RoleAuthority.By(id));
        }

        public string ChangeAccountAuthority(AccountAuthorityIdentifier id)
        {
            var authority = repository.FindOne(new AccountAuthority.By(id));
            authority.Change();
            return string.Format("{0}【{1}】功能！",
                                 authority.IsAuthorized ? "开启" : "关闭",
                                 authority.Function.Description);
        }

        public AccountAuthority GetAccountAuthority(AccountAuthorityIdentifier id)
        {
            return repository.FindOne(new AccountAuthority.By(id));
        }

        public IFunctionCommand CreateFunction()
        {
            var function = new Function();
            repository.Save(function);
            return new FunctionCommand(function);
        }

        public bool Permit(string userName, string area, string controller, string action)
        {
            var account = GetAccount(AccountIdentifier.of(userName));
            return account.GetAuthorities().Permit(area, controller, action);
        }
    }
}