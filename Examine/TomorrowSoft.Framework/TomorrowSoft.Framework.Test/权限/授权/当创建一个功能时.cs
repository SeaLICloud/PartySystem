using Machine.Specifications;
using TomorrowSoft.Framework.Authorize.Application;
using TomorrowSoft.Framework.Authorize.Domain;

namespace TomorrowSoft.Framework.Test.权限.授权
{
    [Subject(typeof(ISecurityService), "CreateFunction")]
    public class 当创建一个功能时 : 测试数据
    {
        Because of =
            () =>
            {
                var command = SecurityService.CreateFunction()
                    .Area(area)
                    .Controller(controller)
                    .Action(action)
                    .Description(description)
                    .MenuAction(menuAction)
                    .MenuDescription(menuDescription)
                    .Group(@group)
                    .GroupIco(groupIco);
                功能 = command.Function.Id;
            };

        It 应该成功添加了功能 =
            () =>
            {
                var function = repository.FindOne(new Function.By(功能));
                function.Area.ShouldEqual(area);
                function.Controller.ShouldEqual(controller);
                function.Action.ShouldEqual(action);
                function.Description.ShouldEqual(description);
                function.MenuAction.ShouldEqual(menuAction);
                function.MenuDescription.ShouldEqual(menuDescription);
                function.Group.ShouldEqual(@group);
                function.GroupIco.ShouldEqual(groupIco);
            };

        private static FunctionIdentifier 功能;
        private static string area = "area";
        private static string controller = "controller";
        private static string action = "action";
        private static bool isDefaultAction = true;
        private static string description = "description";
        private static string @group = "group";
        private static string groupIco = "groupIco";
        private static string menuAction = "menuAction";
        private static string menuDescription = "menuDescription";
    }
}