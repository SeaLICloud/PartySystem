using System;
using System.Collections.Generic;
using Machine.Specifications;
using TomorrowSoft.Framework.Domain.Exceptions;

namespace TomorrowSoft.Framework.Authorize.Domain
{
    public class ISortableCollectionSpec
    {
        Establish context=
            () =>
            {
                entity3 = new TestEntity() { OrderNumber = 3 };
                entity2 = new TestEntity() { OrderNumber = 2 };
                entity1 = new TestEntity() { OrderNumber = 1 };
                Collection = new List<TestEntity> {entity3, entity2, entity1};
            };

        protected static IList<TestEntity> Collection;
        protected static TestEntity entity1;
        protected static TestEntity entity2;
        protected static TestEntity entity3;
        protected static Exception exception;
        protected static double result;
    }

    public class TestEntity : ISortable
    {
        public double OrderNumber { get; set; }
    }

    [Subject(typeof (IEnumerable<TestEntity>), "MoveUp")]
    public class when_move_the_third_up : ISortableCollectionSpec
    {
        Because of = () => Collection.MoveUp(entity1);
        It should_move_to_2_position =
            () =>
            {
                entity1.OrderNumber.ShouldEqual(2.5);
            };
    }

    [Subject(typeof(IEnumerable<TestEntity>), "MoveUp")]
    public class when_move_the_second_up : ISortableCollectionSpec
    {
        Because of = () => Collection.MoveUp(entity2);
        It should_moved_to_3_position =
            () =>
            {
                entity2.OrderNumber.ShouldEqual(4);
            };
    }

    [Subject(typeof(IEnumerable<TestEntity>), "MoveUp")]
    public class when_move_the_first_up : ISortableCollectionSpec
    {
        Because of = () => exception = Catch.Exception(() => Collection.MoveUp(entity3));
        It should_keep_position =
            () =>
            {
                entity3.OrderNumber.ShouldEqual(3);
                exception.ShouldBeOfType<DomainWarningException>();
                exception.Message.ShouldEqual("已经是第一个了！");
            };
    }

    [Subject(typeof (IEnumerable<TestEntity>), "MoveDown")]
    public class when_move_the_first_down : ISortableCollectionSpec
    {
        Because of=()=>Collection.MoveDown(entity3);

        It should_move_to_2_position=
            () =>
            {
                entity3.OrderNumber.ShouldEqual(1.5);
            };
    }

    [Subject(typeof(IEnumerable<TestEntity>), "MoveDown")]
    public class when_move_the_second_down : ISortableCollectionSpec
    {
        Because of = () => Collection.MoveDown(entity2);

        It should_move_to_3_position =
            () =>
            {
                entity2.OrderNumber.ShouldEqual(0);
            };
    }

    [Subject(typeof (IEnumerable<TestEntity>), "MoveDown")]
    public class when_move_the_third_down : ISortableCollectionSpec
    {
        Because of = () => exception = Catch.Exception(() => Collection.MoveDown(entity1));

        It should_keep_position =
           () =>
           {
               entity1.OrderNumber.ShouldEqual(1);
               exception.ShouldBeOfType<DomainWarningException>();
               exception.Message.ShouldEqual("已经是最后一个了！");
           };
    }

    [Subject(typeof (IEnumerable<TestEntity>), "MoveTop")]
    public class when_move_the_second_to_top : ISortableCollectionSpec
    {
        Because of = () => Collection.MoveTop(entity2);
        It should_become_first=
            () =>
            {
                entity2.OrderNumber.ShouldEqual(4);
            };
    }

    [Subject(typeof(IEnumerable<TestEntity>), "MoveTop")]
    public class when_move_the_first_to_top : ISortableCollectionSpec
    {
        Because of = () => exception = Catch.Exception(() => Collection.MoveTop(entity3));
        It should_keep_position =
            () =>
            {
                entity3.OrderNumber.ShouldEqual(3);
                exception.ShouldBeOfType<DomainWarningException>();
                exception.Message.ShouldEqual("已经是第一个了！");
            };
    }

    [Subject(typeof(IEnumerable<TestEntity>), "NewOrderNumber")]
    public class when_collection_is_empty : ISortableCollectionSpec
    {
        Establish context = () => Collection = new List<TestEntity>();
        Because of = () => result = Collection.NewOrderNumber();
        It should_equal_default_value_1 = () => result.ShouldEqual(1);
    }
    
    [Subject(typeof(IEnumerable<TestEntity>), "NewOrderNumber")]
    public class when_collection_is_not_empty : ISortableCollectionSpec
    {
        Because of = () => result = Collection.NewOrderNumber();
        It should_equal_new_max_value = () => result.ShouldEqual(4);
    }
}