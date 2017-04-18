using System.Globalization;

namespace TommorrowSoft.Examine.Test.集成测试
{
    public class 数据准备工作 : 测试对象
    {
        public static void 创建报表(int no)
        {
            AdminService.CreatePartyMoney(no.ToString(CultureInfo.InvariantCulture));
        }
    }
}