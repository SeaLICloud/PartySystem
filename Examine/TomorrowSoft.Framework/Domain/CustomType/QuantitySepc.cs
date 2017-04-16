using System;
using Machine.Specifications;
using TomorrowSoft.Framework.Domain.Exceptions;

namespace TomorrowSoft.Framework.Domain.CustomType
{
    public class QuantitySepc
    {
        protected static Quantity q1;
        protected static Quantity q2;
        protected static Quantity result;

        [Subject(typeof(Quantity), "Quantity")]
        public class when_new_a_quantity : QuantitySepc
        {
            Because of = () => result = new Quantity(1, "KB");
            It should_has_corrent_amount = () => result.Amount.ShouldEqual(1);
            It should_has_corrent_unit = () => result.Unit.ShouldEqual("KB");
        }

        [Subject(typeof(Quantity), "+")]
        public class when_add_two_quantities_with_different_units : QuantitySepc
        {
            Establish context =
                () =>
                {
                    q1 = new Quantity(1, "KB");
                    q2 = new Quantity(1, "MB");
                };
            Because of = () => exception = Catch.Exception(() => q1 + q2);
            It should_throw_exception =
                () =>
                {
                    exception.ShouldBeOfType<DomainErrorException>();
                    exception.Message.ShouldEqual("计量单位必须一致");
                };
            private static Exception exception;
        }

        [Subject(typeof(Quantity), "+")]
        public class when_add_two_quantities_with_same_units : QuantitySepc
        {
            Establish context =
                () =>
                {
                    q1 = new Quantity(1, "KB");
                    q2 = new Quantity(1, "KB");
                };
            Because of = () => result = q1 + q2;
            It should_get_their_sum =
                () =>
                {
                    result.Amount.ShouldEqual(2);
                    result.Unit.ShouldEqual("KB");
                };
        }

        [Subject(typeof(Quantity), "-")]
        public class when_sub_two_quantities_with_different_units : QuantitySepc
        {
            Establish context =
                () =>
                {
                    q1 = new Quantity(1, "KB");
                    q2 = new Quantity(1, "MB");
                };
            Because of = () => exception = Catch.Exception(() => q1 - q2);
            It should_throw_exception =
                () =>
                {
                    exception.ShouldBeOfType<DomainErrorException>();
                    exception.Message.ShouldEqual("计量单位必须一致");
                };
            private static Exception exception;
        }

        [Subject(typeof(Quantity), "-")]
        public class when_sub_two_quantities_with_same_units : QuantitySepc
        {
            Establish context =
                () =>
                {
                    q1 = new Quantity(1, "KB");
                    q2 = new Quantity(1, "KB");
                };
            Because of = () => result = q1 - q2;
            It should_get_their_differ =
                () =>
                {
                    result.Amount.ShouldEqual(0);
                    result.Unit.ShouldEqual("KB");
                };
        }

        [Subject(typeof(Quantity), "*")]
        public class when_a_quantities_is_multiplied : QuantitySepc
        {
            Establish context = () => q1 = new Quantity(1, "KB");
            Because of = () => result = q1 * 5;
            It should_get_their_product =
                () =>
                {
                    result.Amount.ShouldEqual(5);
                    result.Unit.ShouldEqual("KB");
                };
        }

        [Subject(typeof(Quantity), "/")]
        public class when_a_quantities_is_divided_by_zero : QuantitySepc
        {
            Establish context = () => q1 = new Quantity(1, "KB");
            Because of = () => exception = Catch.Exception(() => q1 / 0);
            It should_throw_exception =
                () =>
                {
                    exception.ShouldBeOfType<DomainErrorException>();
                    exception.Message.ShouldEqual("除数不能为0");
                };

            private static Exception exception;
        }

        [Subject(typeof(Quantity), "/")]
        public class when_a_quantities_is_divided : QuantitySepc
        {
            Establish context = () => q1 = new Quantity(1, "KB");
            Because of = () => result = q1 / 5;
            It should_get_their_quotient =
                () =>
                {
                    result.Amount.ShouldEqual(0.2);
                    result.Unit.ShouldEqual("KB");
                };
        }
    }
    
    public class QuantityGreaterThanSpec
    {
        protected static Quantity left;
        protected static Quantity right;
        protected static bool result;
        
        [Subject(typeof(Quantity), ">")]
        public class when_left_quantity_greater_than_right_quantity : QuantityGreaterThanSpec
        {
            Establish context = () =>
            {
                left = new Quantity(2, "KB");
                right = new Quantity(1, "KB");
            };
            Because of = () => result = left > right;
            It should_get_correct_result = () => result.ShouldBeTrue();
        }
        
        [Subject(typeof(Quantity), ">")]
        public class when_right_quantity_greater_than_left_quantity : QuantityGreaterThanSpec
        {
            Establish context = () =>
            {
                left = new Quantity(1, "KB");
                right = new Quantity(2, "KB");
            };
            Because of = () => result = left > right;
            It should_get_correct_result = () => result.ShouldBeFalse();
        }

        [Subject(typeof(Quantity), ">")]
        public class when_right_quantity_Equal_left_quantity : QuantityGreaterThanSpec
        {
            Establish context = () =>
            {
                left = new Quantity(1, "KB");
                right = new Quantity(1, "KB");
            };
            Because of = () => result = left > right;
            It should_get_correct_result = () => result.ShouldBeFalse();
        }
    }

    public class QuantityLessThanSpec
    {
        protected static Quantity left;
        protected static Quantity right;
        protected static bool result;

        [Subject(typeof(Quantity), "<")]
        public class when_left_quantity_less_than_right_quantity : QuantityLessThanSpec
        {
            Establish context = () =>
            {
                left = new Quantity(1, "KB");
                right = new Quantity(2, "KB");
            };
            Because of = () => result = left < right;
            It should_get_correct_result = () => result.ShouldBeTrue();
        }

        [Subject(typeof(Quantity), "<")]
        public class when_right_quantity_less_than_left_quantity : QuantityLessThanSpec
        {
            Establish context = () =>
            {
                left = new Quantity(2, "KB");
                right = new Quantity(1, "KB");
            };
            Because of = () => result = left < right;
            It should_get_correct_result = () => result.ShouldBeFalse();
        }

        [Subject(typeof(Quantity), "<")]
        public class when_right_quantity_Equal_left_quantity : QuantityLessThanSpec
        {
            Establish context = () =>
            {
                left = new Quantity(1, "KB");
                right = new Quantity(1, "KB");
            };
            Because of = () => result = left < right;
            It should_get_correct_result = () => result.ShouldBeFalse();
        }
    }

    public class QuantityEqualSpec
    {
        protected static Quantity left;
        protected static Quantity right;
        protected static bool result;

        [Subject(typeof(Quantity), "==")]
        public class when_left_quantity_less_than_right_quantity : QuantityEqualSpec
        {
            Establish context = () =>
            {
                left = new Quantity(1, "KB");
                right = new Quantity(2, "KB");
            };
            Because of = () => result = left == right;
            It should_get_correct_result = () => result.ShouldBeFalse();
        }

        [Subject(typeof(Quantity), "==")]
        public class when_right_quantity_less_than_left_quantity : QuantityEqualSpec
        {
            Establish context = () =>
            {
                left = new Quantity(2, "KB");
                right = new Quantity(1, "KB");
            };
            Because of = () => result = left == right;
            It should_get_correct_result = () => result.ShouldBeFalse();
        }

        [Subject(typeof(Quantity), "==")]
        public class when_right_quantity_Equal_left_quantity : QuantityEqualSpec
        {
            Establish context = () =>
            {
                left = new Quantity(1, "KB");
                right = new Quantity(1, "KB");
            };
            Because of = () => result = left == right;
            It should_get_correct_result = () => result.ShouldBeTrue();
        }
    }

    public class QuantityOverrideSpec
    {
        protected static Quantity quantity;

        public class when_quantity_tostring : QuantityOverrideSpec
        {
            Establish context= () =>
            {
                quantity = new Quantity(1, "天/人");
            };

            It should_get_correct_format=()=>quantity.ToString().ShouldEqual("1天/人");
        }

    }
}