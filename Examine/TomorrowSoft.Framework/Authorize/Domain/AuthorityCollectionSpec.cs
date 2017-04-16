using System.Collections.Generic;
using Machine.Specifications;

namespace TomorrowSoft.Framework.Authorize.Domain
{
    public class AuthorityCollectionSpec
    {
        Establish baseContext =
            () =>
            {
                Role = new Role(RoleIdentifier.of("Role"));
                Collection = new List<IAuthority>
                    {
                        new RoleAuthority(Role, new Function {Area="", Controller = "X", Action = "A"}) {IsAuthorized = true},
                        new RoleAuthority(Role, new Function {Area="", Controller = "X", Action = "B|C"}) {IsAuthorized = true},
                        new RoleAuthority(Role, new Function {Area="", Controller = "X", Action = ""}) {IsAuthorized = true},
                        new RoleAuthority(Role, new Function {Area="", Controller = "Y", Action = "A"}) {IsAuthorized = true}
                    };
            };
        protected static IEnumerable<IAuthority> Collection;
        protected static Role Role;
        protected static bool Result;
    }

    [Subject(typeof(IEnumerable<IAuthority>), "Permit")]
    public class when_collection_is_null : AuthorityCollectionSpec
    {
        Establish context = () => Collection = new List<IAuthority>();
        Because of = () => Result = Collection.Permit("", "", "");
        It should_not_permit = () => Result.ShouldBeFalse();
    }

    [Subject(typeof(IEnumerable<IAuthority>), "Permit")]
    public class when_authority_contain_single_action : AuthorityCollectionSpec
    {
        Because of = () => Result = Collection.Permit("", "X", "A");
        It should_permit = () => Result.ShouldBeTrue();
    }

    [Subject(typeof(IEnumerable<IAuthority>), "Permit")]
    public class when_authority_contain_double_action : AuthorityCollectionSpec
    {
        Because of = () => Result = Collection.Permit("", "X", "B") && Collection.Permit("", "X", "C");
        It should_permit = () => Result.ShouldBeTrue();
    }

    [Subject(typeof(IEnumerable<IAuthority>), "Permit")]
    public class when_authority_not_contain_action_but_contain_empty_action : AuthorityCollectionSpec
    {
        Because of = () => Result = Collection.Permit("", "X", "G");
        It should_equal_authority_of_controller = () => Result.ShouldEqual(Collection.Permit("", "X", ""));
    }
    
    [Subject(typeof(IEnumerable<IAuthority>), "Permit")]
    public class when_authority_not_contain_action_and_not_contain_empty_action : AuthorityCollectionSpec
    {
        Because of = () => Result = Collection.Permit("", "Y", "G");
        It should_equal_authority_of_controller = () => Result.ShouldBeFalse();
    }
}