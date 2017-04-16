using System.Linq;
using Machine.Specifications;
using TomorrowSoft.Framework.Authorize.Application;
using TomorrowSoft.Framework.Authorize.Domain;

namespace TomorrowSoft.Framework.Test.权限.授权
{
    [Subject(typeof(ISecurityService), "ImportAuthorities")]
    public class 当导入角色授权数据时 : 测试数据
    {
        Establish context =
            () =>
            {
                创建简单角色("GeneralSection");
                创建简单角色("SectionChief");
                创建简单角色("BureauLeader");
                创建简单角色("Admin");
                创建简单角色("GeneralSectionChief");
            };

        Because of = () => SecurityService.ImportAuthorities(
@"RoleName,Area,Controller,Action,Description,MenuAction,MenuDescription,Group,GroupIco
GeneralSection|Admin|GeneralSectionChief,GeneralSection,Project,Index,项目管理,Index,项目管理,日常工作,th
SectionChief|Admin,SectionChief,Project,Index,查看项目,Index,查看项目,日常工作,th
SectionChief|Admin,SectionChief,Project,ArrangeAuditMember,安排审计组成员,,,日常工作,th",
                SecurityService.GetRoles().ToArray());

        It 应该成功创建了所有功能 =
            () =>
            {
                var functions = repository.FindAll<Function>();

                functions.Count().ShouldEqual(3);

                functions.First().Area.ShouldEqual("GeneralSection");
                functions.First().Controller.ShouldEqual("Project");
                functions.First().Action.ShouldEqual("Index");
                functions.First().Description.ShouldEqual("项目管理");
                functions.First().MenuAction.ShouldEqual("Index");
                functions.First().MenuDescription.ShouldEqual("项目管理");
                functions.First().Group.ShouldEqual("日常工作");
                functions.First().GroupIco.ShouldEqual("th");
                functions.First().IsCreateMenu().ShouldBeTrue();

                functions.Last().Area.ShouldEqual("SectionChief");
                functions.Last().Controller.ShouldEqual("Project");
                functions.Last().Action.ShouldEqual("ArrangeAuditMember");
                functions.Last().Description.ShouldEqual("安排审计组成员");
                functions.Last().MenuAction.ShouldEqual("");
                functions.Last().MenuDescription.ShouldEqual("");
                functions.Last().Group.ShouldEqual("日常工作");
                functions.Last().GroupIco.ShouldEqual("th");
                functions.Last().IsCreateMenu().ShouldBeFalse();
            };

        It 应该为简单角色授予了正确的权限 =
            () =>
            {
                var role1 = repository.FindOne(new Role.By(RoleIdentifier.of("GeneralSection")));
                role1.GetAuthorities().Count().ShouldEqual(3);
                role1.GetAuthorities().Count(x => x.IsAuthorized).ShouldEqual(1);

                var role2 = repository.FindOne(new Role.By(RoleIdentifier.of("SectionChief")));
                role2.GetAuthorities().Count().ShouldEqual(3);
                role2.GetAuthorities().Count(x => x.IsAuthorized).ShouldEqual(2);

                var role3 = repository.FindOne(new Role.By(RoleIdentifier.of("BureauLeader")));
                role3.GetAuthorities().Count().ShouldEqual(3);
                role3.GetAuthorities().Count(x => x.IsAuthorized).ShouldEqual(0);
            };

        It 应该为复合角色授予了正确的权限 =
            () =>
            {
                var role1 = repository.FindOne(new Role.By(RoleIdentifier.of("Admin")));
                role1.GetAuthorities().Count().ShouldEqual(3);
                role1.GetAuthorities().Count(x => x.IsAuthorized).ShouldEqual(3);

                var role2 = repository.FindOne(new Role.By(RoleIdentifier.of("GeneralSectionChief")));
                role2.GetAuthorities().Count().ShouldEqual(3);
                role2.GetAuthorities().Count(x => x.IsAuthorized).ShouldEqual(1);
            };
    }
}