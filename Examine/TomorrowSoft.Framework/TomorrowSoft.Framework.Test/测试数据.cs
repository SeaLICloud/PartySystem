using Machine.Specifications;
using TomorrowSoft.Framework.Authorize.Application;
using TomorrowSoft.Framework.Authorize.Domain;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.Specs;

namespace TomorrowSoft.Framework.Test
{
    public class 测试数据 : BaseServiceSpec
    {
        protected Establish basecontext =
            () =>
                {
                    SecurityService = A<ISecurityService>();
                };

        protected static ISecurityService SecurityService;

        protected static RoleIdentifier 角色(string roleName)
        {
            return RoleIdentifier.of(roleName);
        }

        protected static AccountIdentifier 用户(int no)
        {
            return AccountIdentifier.of(no.ToString());
        }

        protected static void 创建简单角色(string roleName)
        {
            SecurityService.CreateRole(roleName);
        }

        protected static void 创建用户(int no)
        {
            SecurityService.CreateAccount(no.ToString());
        }
    }
}