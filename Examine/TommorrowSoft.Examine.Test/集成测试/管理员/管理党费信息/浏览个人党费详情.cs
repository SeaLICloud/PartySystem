using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Machine.Specifications;
using TommorrowSoft.Examine.Domian;
using TomorrowSoft.Examine.Web.Controllers;

namespace TommorrowSoft.Examine.Test.集成测试.管理员.管理党费信息
{
    [Subject(typeof (PartyMoneyController), "Index")]
    internal class 浏览个人党费详情 : 数据准备工作
    {
        private static ActionResult Result { get; set; }

        private Establish _context = () =>
        {
            创建报表(1);
            创建报表(2);
            创建报表(3);
            创建报表(4);
            创建报表(5);
            subject = Action<PartyMoneyController>(x => x.Index());
        };

        private Because _of = () => Result = subject.Invoke();

        private It _应该被成功浏览 = () =>
        {
            var partyMoneyCollection =((ViewResult) Result).ViewData.Model as IEnumerable<PartyMoney>;
            if (partyMoneyCollection == null) return;
            var collection = partyMoneyCollection as PartyMoney[] ?? partyMoneyCollection.ToArray();
            collection.Select(x=>x.Id).ShouldContain(报表(1));
            collection.Select(x=>x.Id).ShouldContain(报表(2));
            collection.Select(x=>x.Id).ShouldContain(报表(3));
            collection.Select(x=>x.Id).ShouldContain(报表(4));
            collection.Select(x=>x.Id).ShouldContain(报表(5));
        };
    }
}
