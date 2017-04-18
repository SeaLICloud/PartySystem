using System.Web.Mvc;
using Machine.Specifications;
using TommorrowSoft.Examine.Domian;
using TomorrowSoft.Examine.Web.Controllers;

namespace TommorrowSoft.Examine.Test.集成测试.管理员.管理党费信息
{
    [Subject(typeof (PartyMoneyController), "Delete")]
    internal class 删除个人党费详情 : 数据准备工作
    {
        private Establish _context = () =>
        {
            创建报表(1);
            subject = Action<PartyMoneyController>(x => x.Delete("1"));
        };

        private Because _of = () => _result = subject.Invoke();
        private It _应该被成功删除 = () => repository.IsExisted(new PartyMoney.By(报表(1))).ShouldBeFalse();

        private static ActionResult _result;
    }
}
