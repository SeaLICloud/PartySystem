using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Machine.Specifications;

namespace TomorrowSoft.Framework.Infrastructure.Crosscutting.Extensions
{
    public class EnumExtensionSpec
    {
         
    }

    public class 当获取枚举型成员的文本描述时
    {
        Because of = () => result = EnumTestType.Value1.Text();
        It 应该得到正确的文本描述 = () => result.ShouldEqual("Text1");
        private static string result;
    }

    public class 当获取枚举型成员的样式描述时
    {
        Because of = () => result = EnumTestType.Value1.Class();
        It 应该得到正确的文本描述 = () => result.ShouldEqual("Class1");
        private static string result;
    }

    public class 当根据枚举型生成下拉框选项列表时
    {
        Because of = () => result = typeof(EnumTestType).ToSelectListItems();
        It 应该成功生成 =
            () =>
                {
                    result.First().Text.ShouldEqual(EnumTestType.Value1.Text());
                    result.First().Value.ShouldEqual(EnumTestType.Value1.Value());
                    result.Last().Text.ShouldEqual(EnumTestType.Value2.Text());
                    result.Last().Value.ShouldEqual(EnumTestType.Value2.Value());
                };
        private static IEnumerable<SelectListItem> result;
    }

    public class 当根据枚举型生成有初始选中项的下拉框选项列表时
    {
        Because of = () => result = typeof(EnumTestType).ToSelectListItems(EnumTestType.Value2.Value());
        It 应该成功生成 = () => result.Last().Selected.ShouldBeTrue();
        private static IEnumerable<SelectListItem> result;
    }

    public class 当根据枚举型生成枚举项列表时
    {
        Because of = () => result = typeof(EnumTestType).ToList<EnumTestType>();
        It 应该成功生成 = () => result.ShouldContainOnly(EnumTestType.Value1, EnumTestType.Value2);
        private static IEnumerable<EnumTestType> result;
            
    }

    public enum EnumTestType
    {
        [EnumText("Text1")]
        [EnumClass("Class1")]
        Value1,
        [EnumText("Text2")]
        [EnumClass("Class2")]
        Value2,
    }
}