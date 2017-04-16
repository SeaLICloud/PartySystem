using TomorrowSoft.Framework.Domain.Exceptions;

namespace TomorrowSoft.Framework.Domain.Bases
{
    public struct AllIdentifier : IBusinessIdentifier
    {
        public override string ToString()
        {
            return "";
        }

        public static implicit operator string(AllIdentifier identifier)
        {
            return identifier.ToString();
        }

        public static implicit operator AllIdentifier(string identifier)
        {
            if (string.IsNullOrEmpty(identifier))
                return AllIdentifier.of();
            throw new DomainException("参数必须是空或空字符串");
        }

        public static AllIdentifier of()
        {
            return new AllIdentifier();
        }
    }
}