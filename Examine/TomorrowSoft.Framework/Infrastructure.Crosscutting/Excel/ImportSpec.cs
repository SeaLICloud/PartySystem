using System;
using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.NetFramework.Exceptions;

namespace TomorrowSoft.Framework.Infrastructure.Crosscutting.Excel
{
    public class ImportSpecBase
    {
        protected Establish prepare = () =>  subject = new Import<TestObject> ();
        protected static Import<TestObject> subject;
        protected static IEnumerable<TestObject> result;
        protected static string text = @"字符串,整数,浮点数,时间,布尔,实体
abc,123,3.14,2014-3-17,true,1";
        protected static string text2 = @"类型
子类1
子类2";
        protected static string text3 = @"代码,父节点
abc,
def,abc";
        protected static IList<Parent> parents = new List<Parent>()
                                                     {
                                                         new Parent{Name = "1"},
                                                         new Parent{Name = "2"}
                                                     };
    }

    public class when_import_property_of_string_type : ImportSpecBase
    {
        Establish context = () => subject
            .New(row => new TestObject())
            .Map((obj, row) => obj.StringProperty = row["字符串"].ToString());

        Because of = () => result = subject.MapTo(text);

        It 应该被成功映射 = () => result.Select(x=>x.StringProperty).ShouldContainOnly("abc");
    }

    public class when_import_property_of_int_type : ImportSpecBase
    {
        Establish context = () => subject
            .New(row => new TestObject())
            .Map((obj, row) => obj.IntProperty = Convert.ToInt32(row["整数"]));

        Because of = () => result = subject.MapTo(text);

        It 应该被成功映射 = () => result.Select(x => x.IntProperty).ShouldContainOnly(123);
    }

    public class when_import_property_of_double_type : ImportSpecBase
    {
        Establish context = () => subject
            .New(row => new TestObject())
            .Map((obj, row) => obj.DoubleProperty = Convert.ToDouble(row["浮点数"]));

        Because of = () => result = subject.MapTo(text);

        It 应该被成功映射 = () => result.Select(x => x.DoubleProperty).ShouldContainOnly(3.14);
    }

    public class when_import_property_of_datetime_type : ImportSpecBase
    {
        Establish context = () => subject
            .New(row => new TestObject())
            .Map((obj, row) => obj.TimeProperty = Convert.ToDateTime(row["时间"]));

        Because of = () => result = subject.MapTo(text);

        It 应该被成功映射 = () => result.Select(x => x.TimeProperty).ShouldContainOnly(new DateTime(2014, 3, 17));
    }

    public class when_import_property_of_bool_type : ImportSpecBase
    {
        Establish context = () => subject
            .New(row => new TestObject())
            .Map((obj, row) => obj.BoolProperty = Convert.ToBoolean(row["布尔"]));

        Because of = () => result = subject.MapTo(text);

        It 应该被成功映射 = () => result.Select(x => x.BoolProperty).ShouldContainOnly(true);
    }

    public class when_create_object_with_parameter_constructor : ImportSpecBase
    {
        Establish context = () => subject.New(row => new TestObject(row["字符串"].ToString()));
        Because of = () => result = subject.MapTo(text);
        It 应该被成功映射 = () => result.Select(x => x.StringProperty).ShouldContainOnly("abc");
    }

    public class when_create_object_by_type : ImportSpecBase
    {
        Establish context = 
            () => subject.New(row =>
                {
                    if (row["类型"].ToString() == "子类1")
                        return new SubClass1();
                    return new SubClass2();
                });

        Because of = () => result = subject.MapTo(text2);

        It 应该被正确实例化 =
            () =>
            {
                result.ToArray()[0].ShouldBeOfType<SubClass1>();
                result.ToArray()[1].ShouldBeOfType<SubClass2>();
            };
    }

    public class when_import_property_of_entity : ImportSpecBase
    {
        Establish context = () => subject
            .New(row => new TestObject())
            .Map((obj,row)=>obj.Parent = parents.FirstOrDefault(x=>x.Name.Equals(row["实体"])));
        Because of = () => result = subject.MapTo(text);
        It 应该被成功映射 = () => result.Select(x => x.Parent).ShouldContainOnly(parents[0]);
    }

    public class when_import_data_of_tree_structure : ImportSpecBase
    {
        Establish context = () => subject
            .New((row, collection) =>
                     {
                         var obj = new TestObject();
                         var parent = collection.FirstOrDefault(x => x.StringProperty == row["父节点"].ToString());
                         if (parent != null)
                             parent.Children.Add(obj);
                         return obj;
                     }
                )
            .Map((obj, row) => obj.StringProperty = row["代码"].ToString());
        Because of = () => result = subject.MapTo(text3);
        It 应该被成功映射 = () => result.ToArray()[0].Children.Count.ShouldEqual(1);
    }

    public class when_miss_constructor_map : ImportSpecBase
    {
        Because of = () => exception = Catch.Exception(() => subject.MapTo(text));
        It should_throw_a_exception =
            () =>
            {
                exception.ShouldBeOfType<FrameworkException>();
                exception.Message.ShouldEqual("未指定实例化对象的方式");
            };
        private static Exception exception;
    }

    public class when_input_is_empty : ImportSpecBase
    {
        Because of = () => exception = Catch.Exception(() => subject.MapTo(string.Empty));
        It should_throw_a_exception =
            () =>
            {
                exception.ShouldBeOfType<FrameworkException>();
                exception.Message.ShouldEqual("导入数据为空");
            };
        private static Exception exception;
    }


    public class TestObject
    {
        public TestObject()
        {
            Children = new List<TestObject>();
        }

        public TestObject(string str)
        {
            StringProperty = str;
        }

        public string StringProperty { get; set; }
        public int IntProperty { get; set; }
        public double DoubleProperty { get; set; }
        public DateTime TimeProperty { get; set; }
        public bool BoolProperty { get; set; }
        public IList<TestObject> Children{ get; private set; }
        public Parent Parent { get; set; }
    }

    public class SubClass1 : TestObject { }
    public class SubClass2 : TestObject { }
    public class Parent
    {
        public string Name { get; set; }
    }
}