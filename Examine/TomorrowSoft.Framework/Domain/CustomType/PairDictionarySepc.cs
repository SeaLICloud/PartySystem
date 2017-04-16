using System.Collections.Generic;
using Machine.Specifications;

namespace TomorrowSoft.Framework.Domain.CustomType
{
    public class PairDictionarySepc
    {
        Establish context =
            () =>
            {
                dataA = new List<A> { new A("1", "A1"), new A("2", "A2") };
                dataB = new List<B> { new B("1", "B1"), new B("3", "B3") };
            };
        Because of =
            () => dictionary = new PairDictionary<string, A, B>(
                x => x.Key, dataA, 
                x => x.Key, dataB);
        It 应该生成三组数据对 =
            () =>
                {
                    dictionary["1"].First.ShouldEqual(dataA[0]);
                    dictionary["1"].Second.ShouldEqual(dataB[0]);
                    dictionary["2"].First.ShouldEqual(dataA[1]);
                    dictionary["2"].Second.ShouldBeNull();
                    dictionary["3"].First.ShouldBeNull();
                    dictionary["3"].Second.ShouldEqual(dataB[1]);
                };

        private static PairDictionary<string, A, B> dictionary;
        private static List<A> dataA;
        private static List<B> dataB;
    }

    public class A
    {
        public A(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; set; }
        public string Value { get; set; }
    }
    
    public class B
    {
        public B(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; set; }
        public string Value { get; set; }
    }
}