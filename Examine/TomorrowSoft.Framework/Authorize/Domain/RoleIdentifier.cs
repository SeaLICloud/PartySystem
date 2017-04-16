using TomorrowSoft.Framework.Domain.Bases;

namespace TomorrowSoft.Framework.Authorize.Domain
{
    public struct RoleIdentifier : IBusinessIdentifier
    {
        public string RoleName { get; private set; }

        public RoleIdentifier(string rolename)
            : this()
        {
            RoleName = rolename;
        }

        public static RoleIdentifier of(string rolename)
        {
            return new RoleIdentifier(rolename);
        }

        public override string ToString()
        {
            return string.Format("Role/{0}", RoleName);
        }

        public static implicit operator string(RoleIdentifier identifier)
        {
            return identifier.ToString();
        }

        public static implicit operator RoleIdentifier(string identifier)
        {
            var subs = identifier.Split(new[] { '/' }, 2);
            return RoleIdentifier.of(subs[1]);
        }
    }
}