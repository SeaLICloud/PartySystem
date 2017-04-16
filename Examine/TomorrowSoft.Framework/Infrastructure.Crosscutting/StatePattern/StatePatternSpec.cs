using System;
using Machine.Specifications;
using TomorrowSoft.Framework.Domain.Bases;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.Container;

namespace TomorrowSoft.Framework.Infrastructure.Crosscutting.StatePattern
{
    public class StatePatternSpec
    {
        Establish context =
            () =>
                {
                    entity = new TestEntity();
                };

        protected static TestEntity entity;
        protected static State<TestEntity> state;
    }

    public class 当X属性等于2和Y属性等于0时 : StatePatternSpec
    {
        Establish context =
            () =>
            {
                entity.X = 2;
                entity.Y = 0;
            };

        Because of = () => state = StateFactory.GetCurrentState(entity);

        It 应该是State2状态 = 
            () =>
                {
                    state.ShouldBeOfType<State2>();
                    state.Name.ShouldEqual("State2");
                };
    }

    public class 当X属性等于1和Y属性等于1时 : StatePatternSpec
    {
        Establish context =
            () =>
            {
                entity.X = 1;
                entity.Y = 1;
            };

        Because of = () => state = StateFactory.GetCurrentState(entity);

        It 应该是State11状态 = 
            () =>
                {
                    state.ShouldBeOfType<State11>();
                    state.Name.ShouldEqual("State11");
                };
    }

    public class 当X属性等于1和Y属性等于2时 : StatePatternSpec
    {
        Establish context =
            () =>
            {
                entity.X = 1;
                entity.Y = 2;
            };

        Because of = () => state = StateFactory.GetCurrentState(entity);

        It 应该是State12状态 = 
            () =>
                {
                    state.ShouldBeOfType<State12>();
                    state.Name.ShouldEqual("State12");
                };
    }

    public class 当X属性等于2时执行禁止的操作 : StatePatternSpec
    {
        Establish context = 
            () =>
                {
                    entity.X = 2;
                    state = StateFactory.GetCurrentState(entity) as TestBaseState;  
                };
        Because of = () => exception = Catch.Exception(() => (state as TestBaseState).Operation(entity));
        It 应该抛出正确的异常 =
            () =>
                {
                    exception.ShouldBeOfType<InvalidOperationInStateException>();
                    exception.Message.ShouldEqual("在【State2】状态下，不允许执行【测试】操作！");
                };
        private static Exception exception;
    }

    public class 当X属性等于1和Y属性等于1时执行允许的操作 : StatePatternSpec
    {
        Establish context = 
            () =>
                {
                    entity.X = 1;
                    entity.Y = 1;
                    state = StateFactory.GetCurrentState(entity) as TestBaseState;
                };
        Because of = () => (state as TestBaseState).Operation(entity);
        private It 应该成功执行 = () => {};
    }

    public abstract class TestBaseState : State<TestEntity>
    {
        public virtual void Operation(TestEntity entity)
        {
            throw new InvalidOperationInStateException(this, "测试");
        }
    }

    public abstract class State1 : TestBaseState
    {
        public override bool IsMatch(TestEntity entity)
        {
            return entity.X == 1;
        }
    }

    [RegisterToContainer("State2")]
    public class State2 : TestBaseState
    {
        public override bool IsMatch(TestEntity entity)
        {
            return entity.X == 2;
        }

        public override string Name
        {
            get { return "State2"; }
        }
    }

    [RegisterToContainer("State11")]
    public class State11 : State1
    {
        public override bool IsMatch(TestEntity entity)
        {
            return base.IsMatch(entity) && entity.Y == 1;
        }

        public override string Name
        {
            get { return "State11"; }
        }

        public override void Operation(TestEntity entity)
        {
            
        }
    }

    [RegisterToContainer("State12")]
    public class State12 : State1
    {
        public override bool IsMatch(TestEntity entity)
        {
            return base.IsMatch(entity) && entity.Y == 2;
        }

        public override string Name
        {
            get { return "State12"; }
        }
    }

    public class TestEntity : Entity<TestEntity>
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}