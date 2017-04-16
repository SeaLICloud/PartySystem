using System;
using Machine.Specifications;
using TomorrowSoft.Framework.Domain.Exceptions;

namespace TomorrowSoft.Framework.Domain.CustomType
{
    public class PageInfoSpec
    {
        protected static PageInfo subject;
    }

    public class 当_共200条_每页20条_当前是第10页_时 : PageInfoSpec
    {
        Establish context = () => subject = new PageInfo(200, 20, 10);
        It 应该得到正确的分页参数 =
            () =>
            {
                subject.TotalCount.ShouldEqual(200);
                subject.PageSize.ShouldEqual(20);
                subject.CurrentPage.ShouldEqual(10);
                subject.PageCount.ShouldEqual(10);
                subject.Start.ShouldEqual(181);
                subject.End.ShouldEqual(200);
            };
    }

    public class 当_共199条_每页20条_当前是第10页_时 : PageInfoSpec
    {
        Establish context = () => subject = new PageInfo(199, 20, 10);
        It 应该得到正确的分页参数 =
            () =>
                {
                    subject.TotalCount.ShouldEqual(199);
                    subject.PageSize.ShouldEqual(20);
                    subject.CurrentPage.ShouldEqual(10);
                    subject.PageCount.ShouldEqual(10);
                    subject.Start.ShouldEqual(181);
                    subject.End.ShouldEqual(199);
                };
    }

    public class 当_共199条_每页20条_当前是第11页_时 : PageInfoSpec
    {
        Establish context = () => exception = Catch.Exception( ()=> subject = new PageInfo(199, 20, 11));
        It 应该得到正确的分页参数 =
            () =>
            {
                exception.ShouldBeOfType<DomainErrorException>();
                exception.Message.ShouldEqual("已经是最后一页");
            };

        private static Exception exception;
    }

    public class 当_共199条_每页20条_当前是第0页_时 : PageInfoSpec
    {
        Establish context = () => exception = Catch.Exception( ()=> subject = new PageInfo(199, 20, 0));
        It 应该得到正确的分页参数 =
            () =>
            {
                exception.ShouldBeOfType<DomainErrorException>();
                exception.Message.ShouldEqual("已经是第一页");
            };

        private static Exception exception;
    }

    public class 当_共0条_每页20条_当前是第1页_时 : PageInfoSpec
    {
        Establish context = () => subject = new PageInfo(0, 20, 1);
        It 应该得到正确的分页参数 =
            () =>
            {
                subject.TotalCount.ShouldEqual(0);
                subject.PageSize.ShouldEqual(20);
                subject.CurrentPage.ShouldEqual(0);
                subject.PageCount.ShouldEqual(0);
                subject.Start.ShouldEqual(0);
                subject.End.ShouldEqual(0);
            };
    }
}