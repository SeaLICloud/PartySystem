using Machine.Specifications;

namespace TomorrowSoft.Framework.Domain.CustomType
{
    public class PersentSpec
    {
        protected static Persent subject;
    }

    public class 当将比例对象转成字符串时 : PersentSpec
    {
        Establish context = () => subject = new Persent { Value = 0.4 };

        It 应该生成正确的字符串 = () => subject.ToString().ShouldEqual("40%");
    }

    public class 当将字符串转成比例对象时 : PersentSpec
    {
        Establish context = () => subject = Persent.of("40%");

        It 应该解析得到正确的数值 = () => subject.Value.ShouldEqual(0.4);

        It 应该获得正确的分子部分 = () => subject.Numerator.ShouldEqual(40);
    }
}