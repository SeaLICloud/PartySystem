using System;
using Machine.Specifications;

namespace TomorrowSoft.Framework.Domain.CustomType
{
    public class PeriodSpec
    {
        protected static Period period1;
        protected static Period period2;

        protected static bool result;

        protected static DateTime dt1 = new DateTime(2014, 1, 1, 1, 0, 0);
        protected static DateTime dt2 = new DateTime(2014, 1, 1, 2, 0, 0);
        protected static DateTime dt3 = new DateTime(2014, 1, 1, 3, 0, 0);
        protected static DateTime dt4 = new DateTime(2014, 1, 1, 4, 0, 0);
        protected static DateTime dt5 = new DateTime(2014, 1, 1, 5, 0, 0);
        protected static DateTime dt6 = new DateTime(2014, 1, 1, 6, 0, 0);
        protected static DateTime dt7 = new DateTime(2014, 1, 1, 7, 0, 0);
        protected static DateTime dt8 = new DateTime(2014, 1, 1, 8, 0, 0);
        protected static DateTime dt9 = new DateTime(2014, 1, 1, 9, 0, 0);
        protected static DateTime dt10 = new DateTime(2014, 1, 1, 10, 0, 0);
        protected static DateTime dt11 = new DateTime(2014, 1, 1, 11, 0, 0);
        protected static DateTime dt12 = new DateTime(2014, 1, 1, 12, 0, 0);
    }

    [Subject(typeof(Period), "IsOverlap")]
    public class 当A的结束时间小于B的开始时间时 : PeriodSpec
    {
        Establish context =
            () =>
            {
                period1 = new Period(dt1, dt2);
                period2 = new Period(dt3, dt4);
            };

        Because of = () => result = period1.IsOverlap(period2);

        It 应该没有重叠 = () => result.ShouldBeFalse();
    }

    [Subject(typeof(Period), "IsOverlap")]
    public class 当A的开始时间大于B的结束时间时 : PeriodSpec
    {
        Establish context =
            () =>
            {
                period1 = new Period(dt3, dt4);
                period2 = new Period(dt1, dt2);
            };

        Because of = () => result = period1.IsOverlap(period2);

        It 应该没有重叠 = () => result.ShouldBeFalse();
    }

    [Subject(typeof(Period), "IsOverlap")]
    public class 当A包含B的结束时间时 : PeriodSpec
    {
        Establish context =
            () =>
            {
                period1 = new Period(dt1, dt3);
                period2 = new Period(dt1, dt2);
            };

        Because of = () => result = period1.IsOverlap(period2);

        It 应该没有重叠 = () => result.ShouldBeTrue();
    }

    [Subject(typeof(Period), "IsOverlap")]
    public class 当A包含B的开始时间时 : PeriodSpec
    {
        Establish context =
            () =>
            {
                period1 = new Period(dt1, dt3);
                period2 = new Period(dt2, dt4);
            };

        Because of = () => result = period1.IsOverlap(period2);

        It 应该没有重叠 = () => result.ShouldBeTrue();
    }

    #region Test After

    [Subject(typeof(Period), "After")]
    public class 当测试时间段未开始_且时间在时间段之前时 : PeriodSpec
    {
        Establish context =
            () =>
            {
                period1 = new Period(dt2, dt3);
            };

        Because of = () => result = period1.After(dt1);

        It 应该是真的 = () => result.ShouldBeTrue();
    }

    [Subject(typeof(Period), "After")]
    public class 当测试时间段未开始_且时间在时间段之内时 : PeriodSpec
    {
        Establish context =
            () =>
            {
                period1 = new Period(dt2, dt3);
            };

        Because of = () => result = period1.After(dt2);

        It 应该是假的 = () => result.ShouldBeFalse();
    }
    
    [Subject(typeof(Period), "After")]
    public class 当测试时间段未开始_且时间在时间段之后时 : PeriodSpec
    {
        Establish context =
            () =>
            {
                period1 = new Period(dt2, dt3);
            };

        Because of = () => result = period1.After(dt4);

        It 应该是假的 = () => result.ShouldBeFalse();
    }

    #endregion

    #region Test Before

    [Subject(typeof(Period), "Before")]
    public class 当测试时间段已结束_且时间在时间段之前时 : PeriodSpec
    {
        Establish context =
            () =>
            {
                period1 = new Period(dt2, dt3);
            };

        Because of = () => result = period1.Before(dt1);

        It 应该是假的 = () => result.ShouldBeFalse();
    }

    [Subject(typeof(Period), "Before")]
    public class 当测试时间段已结束_且时间在时间段之内时 : PeriodSpec
    {
        Establish context =
            () =>
            {
                period1 = new Period(dt2, dt3);
            };

        Because of = () => result = period1.Before(dt3);

        It 应该是假的 = () => result.ShouldBeFalse();
    }

    [Subject(typeof(Period), "Before")]
    public class 当测试时间段已结束_且时间在时间段之后时 : PeriodSpec
    {
        Establish context =
            () =>
            {
                period1 = new Period(dt2, dt3);
            };

        Because of = () => result = period1.Before(dt4);

        It 应该是真的 = () => result.ShouldBeTrue();
    }

    #endregion
}