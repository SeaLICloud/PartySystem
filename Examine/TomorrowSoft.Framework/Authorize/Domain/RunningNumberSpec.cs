using System;
using Machine.Specifications;

namespace TomorrowSoft.Framework.Authorize.Domain
{
    public class RunningNumberSpec
    {
        protected static RunningNumber sn;
        protected const string Key = "Test";
        protected static string nextNumber;
    }

    #region yyyyMMdd000
    public class 当流水号掩码为yyyyMMdd000时 : RunningNumberSpec
    {
        Establish context = () => sn = new RunningNumber(Key, Mask);
        protected static RunningNumberMask Mask = new RunningNumberMask("", "yyyyMMdd", "000");
    }

    [Subject(typeof(RunningNumber), "Next")]
    public class 当生成yyyyMMdd000格式的第一个流水号时 : 当流水号掩码为yyyyMMdd000时
    {
        Because of = () => nextNumber = sn.Next(Convert.ToDateTime("2016-02-01")).LastNumber;
        It 应该得到正确的新流水号 = () => nextNumber.ShouldEqual("20160201001");
    }

    [Subject(typeof(RunningNumber), "Next")]
    public class 当生成当天第一个流水号时 : 当流水号掩码为yyyyMMdd000时
    {
        Establish context = () => sn.LastNumber = "20160101001";
        Because of = () => nextNumber = sn.Next(Convert.ToDateTime("2016-02-01")).LastNumber;
        It 应该得到正确的新流水号 = () => nextNumber.ShouldEqual("20160201001");
    }

    [Subject(typeof(RunningNumber), "Next")]
    public class 当生成当天第二个流水号时 : 当流水号掩码为yyyyMMdd000时
    {
        Establish context = () => sn.LastNumber = "20160201001";
        Because of = () => nextNumber = sn.Next(Convert.ToDateTime("2016-02-01")).LastNumber;
        It 应该得到正确的新流水号 = () => nextNumber.ShouldEqual("20160201002");
    }

    #endregion

    #region yyyyMM000
    public class 当流水号掩码为yyyyMM000时 : RunningNumberSpec
    {
        Establish context = () => sn = new RunningNumber(Key, Mask);
        protected static RunningNumberMask Mask = new RunningNumberMask("", "yyyyMM", "000");
    }

    [Subject(typeof(RunningNumber), "Next")]
    public class 当生成yyyyMM000格式的第一个流水号时 : 当流水号掩码为yyyyMM000时
    {
        Because of = () => nextNumber = sn.Next(Convert.ToDateTime("2016-02-01")).LastNumber;
        It 应该得到正确的新流水号 = () => nextNumber.ShouldEqual("201602001");
    }

    [Subject(typeof(RunningNumber), "Next")]
    public class 当生成当月第一个流水号时 : 当流水号掩码为yyyyMM000时
    {
        Establish context = () => sn.LastNumber = "201601001";
        Because of = () => nextNumber = sn.Next(Convert.ToDateTime("2016-02-01")).LastNumber;
        It 应该得到正确的新流水号 = () => nextNumber.ShouldEqual("201602001");
    }

    [Subject(typeof(RunningNumber), "Next")]
    public class 当生成当月第二个流水号时 : 当流水号掩码为yyyyMM000时
    {
        Establish context = () => sn.LastNumber = "201602001";
        Because of = () => nextNumber = sn.Next(Convert.ToDateTime("2016-02-01")).LastNumber;
        It 应该得到正确的新流水号 = () => nextNumber.ShouldEqual("201602002");
    }

    #endregion

    #region yyyy000

    public class 当流水号掩码为yyyy000时 : RunningNumberSpec
    {
        Establish context = () => sn = new RunningNumber(Key, Mask);
        protected static RunningNumberMask Mask = new RunningNumberMask("", "yyyy", "000");
    }

    [Subject(typeof(RunningNumber), "Next")]
    public class 当生成yyyy000格式的第一个流水号时 : 当流水号掩码为yyyy000时
    {
        Because of = () => nextNumber = sn.Next(Convert.ToDateTime("2016-02-01")).LastNumber;
        It 应该得到正确的新流水号 = () => nextNumber.ShouldEqual("2016001");
    }

    [Subject(typeof(RunningNumber), "Next")]
    public class 当生成今年第一个流水号时 : 当流水号掩码为yyyy000时
    {
        Establish context = () => sn.LastNumber = "2015001";
        Because of = () => nextNumber = sn.Next(Convert.ToDateTime("2016-02-01")).LastNumber;
        It 应该得到正确的新流水号 = () => nextNumber.ShouldEqual("2016001");
    }

    [Subject(typeof(RunningNumber), "Next")]
    public class 当生成今年第二个流水号时 : 当流水号掩码为yyyy000时
    {
        Establish context = () => sn.LastNumber = "2016001";
        Because of = () => nextNumber = sn.Next(Convert.ToDateTime("2016-02-01")).LastNumber;
        It 应该得到正确的新流水号 = () => nextNumber.ShouldEqual("2016002");
    }

    #endregion

    #region PPyyyy000

    public class 当流水号掩码为PPyyyy000时 : RunningNumberSpec
    {
        Establish context = () => sn = new RunningNumber(Key, Mask);
        protected static RunningNumberMask Mask = new RunningNumberMask("PP", "yyyy", "000");
    }

    [Subject(typeof(RunningNumber), "Next")]
    public class 当生成PPyyyy000格式的第一个流水号时 : 当流水号掩码为PPyyyy000时
    {
        Because of = () => nextNumber = sn.Next(Convert.ToDateTime("2016-02-01")).LastNumber;
        It 应该得到正确的新流水号 = () => nextNumber.ShouldEqual("PP2016001");
    }

    [Subject(typeof(RunningNumber), "Next")]
    public class 当生成带前缀PP且今年第一个流水号时 : 当流水号掩码为PPyyyy000时
    {
        Establish context = () => sn.LastNumber = "PP2015001";
        Because of = () => nextNumber = sn.Next(Convert.ToDateTime("2016-02-01")).LastNumber;
        It 应该得到正确的新流水号 = () => nextNumber.ShouldEqual("PP2016001");
    }

    [Subject(typeof(RunningNumber), "Next")]
    public class 当生成带前缀PP且今年第二个流水号时 : 当流水号掩码为PPyyyy000时
    {
        Establish context = () => sn.LastNumber = "PP2016001";
        Because of = () => nextNumber = sn.Next(Convert.ToDateTime("2016-02-01")).LastNumber;
        It 应该得到正确的新流水号 = () => nextNumber.ShouldEqual("PP2016002");
    }

    #endregion
}