using TomorrowSoft.Framework.Domain.Bases;
using TomorrowSoft.Framework.Domain.Exceptions;

namespace TomorrowSoft.Framework.Authorize.Domain
{
    public struct RunningNumberIdentifier : IBusinessIdentifier
    {
        private const string IdKey = "RunningNumber";

        public RunningNumberIdentifier(string key)
            : this()
        {
            Key = key;
        }

        public string Key { get; private set; }

        public static RunningNumberIdentifier of(string key)
        {
            return new RunningNumberIdentifier(key);
        }

        public override string ToString()
        {
            return string.Format("{0}/{1}",IdKey, Key);
        }

        public static implicit operator string(RunningNumberIdentifier id)
        {
            return id.ToString();
        }

        public static implicit operator RunningNumberIdentifier(string id)
        {
            var sub = id.Split(new[] {'/'}, 2);
            if (sub[0] != IdKey)
                throw new DomainErrorException("不是流水号的Id");
            return RunningNumberIdentifier.of(sub[1]);
        }
    }
}