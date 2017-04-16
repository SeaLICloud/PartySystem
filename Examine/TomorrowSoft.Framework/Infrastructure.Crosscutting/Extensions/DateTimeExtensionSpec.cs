using System;
using Machine.Specifications;

namespace TomorrowSoft.Framework.Infrastructure.Crosscutting.Extensions
{
    public class DateTimeExtensionSpec
    {
         
    }

    [Ignore]

    public class 当将生日换算成年龄时
    {
        Because of = () => result = new DateTime(1980, 8, 25).Age();
        It 应该计算出正确的年龄 = () => result.ShouldEqual(33);
        private static int result;
    }
}