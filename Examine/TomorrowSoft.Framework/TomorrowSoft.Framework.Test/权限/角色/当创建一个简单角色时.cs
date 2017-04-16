using Machine.Specifications;
using TomorrowSoft.Framework.Authorize.Application;
using TomorrowSoft.Framework.Authorize.Domain;

namespace TomorrowSoft.Framework.Test.权限.角色
{
    [Subject(typeof(ISecurityService), "CreateRole")]
    public class 当创建一个简单角色时 : 测试数据
    {
        Because of = () => SecurityService
            .CreateRole(角色("1").RoleName)
            .Description(Description)
            .IsVisible(true);

        It 应该添加成功 =
            () =>
                {
                    var role = repository.FindOne(new Role.By(角色("1")));
                    role.ShouldNotBeNull();
                    role.ShouldBeOfType<Role>();
                    role.Description.ShouldEqual(Description);
                    role.IsVisible.ShouldBeTrue();
                };

        private static string Description = "Description";
    }
}