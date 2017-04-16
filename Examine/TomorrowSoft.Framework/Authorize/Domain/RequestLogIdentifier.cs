using System;
using TomorrowSoft.Framework.Domain.Bases;

namespace TomorrowSoft.Framework.Authorize.Domain
{
    public struct RequestLogIdentifier : IBusinessIdentifier
    {
        public string SessionId { get; set; }
        public int Index { get; set; }

        public RequestLogIdentifier(string sessionId, int index)
            : this()
        {
            SessionId = sessionId;
            Index = index;
        }

        public static RequestLogIdentifier of(string sessionId, int index)
        {
            return new RequestLogIdentifier(sessionId, index);
        }

        public override string ToString()
        {
            return string.Format("RequestLog/{0}/{1}", SessionId, Index);
        }

        public static implicit operator string(RequestLogIdentifier identifier)
        {
            return identifier.ToString();
        }

        public static implicit operator RequestLogIdentifier(string identifier)
        {
            var subs = identifier.Split(new[] {'/'}, 3);
            return RequestLogIdentifier.of(subs[1], Convert.ToInt32(subs[2]));
        }
    }
}