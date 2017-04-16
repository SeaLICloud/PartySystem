using Machine.Specifications;

namespace TomorrowSoft.Framework.Presentation.Mvc
{
    public class FormValidateRuleSpec
    {
        Establish context = () => subject = Rule.Tag("Name");
        Because of = () =>
                         {
                             rules = subject.ToRules();
                             messs = subject.ToMessages();
                         };
        protected static Rule subject;
        protected static string rules;
        protected static string messs;
    }
    
    [Subject(typeof(Rule))]
    public class 当内容为必填字段时 : FormValidateRuleSpec
    {
        Establish context = () => subject.required("true", "必填字段");
        It 应该得到正确的规则代码 = () => rules.ShouldEqual("Name:{required:true}");
        It 应该得到正确的消息代码 = () => messs.ShouldEqual("Name:{required:\"必填字段\"}");
    }
}