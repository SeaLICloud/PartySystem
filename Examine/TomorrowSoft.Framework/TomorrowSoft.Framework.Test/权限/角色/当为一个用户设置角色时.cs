using System.Linq;
using Machine.Specifications;
using TomorrowSoft.Framework.Authorize.Application;
using TomorrowSoft.Framework.Authorize.Domain;

namespace TomorrowSoft.Framework.Test.权限.角色
{
    [Subject(typeof(IAccountCommand), "Role")]
    public class 当为一个用户设置角色时 : 测试数据
    {
        Establish context =
            () =>
            {
                创建用户(1);
                创建简单角色("1");
                创建简单角色("2"); 
                SecurityService.ImportAuthorities(
 @"RoleName,Area,Controller,Action,Description,MenuAction,MenuDescription,Group,GroupIco
1,GeneralSection,Project,Index,项目管理,Index,项目管理,日常工作,th
2,SectionChief,Project,Index,查看项目,Index,查看项目,日常工作,th",
                 SecurityService.GetRoles().Where(x => x is Role).ToArray());
            };

        Because of = () => SecurityService
            .UpdateAccount(用户(1))
            .AddRole(角色("1"))
            .AddRole(角色("2"));

        It 应该成功设置了用户的角色 =
            () =>
            {
                var account = repository.FindOne(new Account.By(用户(1)));
                account.Roles.Select(x => x.Id).ShouldContainOnly(角色("1"), 角色("2"));
                account.Roles.Select(x => x.Id).ShouldContainOnly(角色("1"), 角色("2"));
            };

        It 应该使用户得到了两个角色的权限 =
            () =>
            {
                var account = repository.FindOne(new Account.By(用户(1)));
                account.GetAuthorities().Count().ShouldEqual(2);
                account.GetAuthorities().Count(x => x.IsAuthorized).ShouldEqual(2);
            };
    }
}