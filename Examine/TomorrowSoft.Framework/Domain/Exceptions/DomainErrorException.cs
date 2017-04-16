namespace TomorrowSoft.Framework.Domain.Exceptions
{
    public class DomainErrorException : DomainException
    {
        public DomainErrorException(string message) : base(message)
        {
        }
    }
}