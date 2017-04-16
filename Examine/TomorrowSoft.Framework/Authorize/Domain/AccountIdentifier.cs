using TomorrowSoft.Framework.Domain.Bases;

namespace TomorrowSoft.Framework.Authorize.Domain
{
    public struct AccountIdentifier : IBusinessIdentifier
    {
        public string UserName { get; private set; }

        public AccountIdentifier(string username) : this()
        {
            UserName = username;
        }

        public static AccountIdentifier of(string username)
        {
            return new AccountIdentifier(username);
        }

        public override string ToString()
        {
            return string.Format("Account/{0}", UserName);
        }

        public static implicit operator string(AccountIdentifier identifier)
        {
            return identifier.ToString();
        }

        public static implicit operator AccountIdentifier(string identifier)
        {
            var subs = identifier.Split(new[] { '/' }, 2);
            return AccountIdentifier.of(subs[1]);
        }
    }
}