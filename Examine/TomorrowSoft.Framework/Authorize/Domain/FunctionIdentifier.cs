using System;
using TomorrowSoft.Framework.Domain.Bases;

namespace TomorrowSoft.Framework.Authorize.Domain
{
    public struct FunctionIdentifier : IBusinessIdentifier
    {
        public Guid DBID { get; private set; }

        public FunctionIdentifier(Guid dbid) : this()
        {
            DBID = dbid;
        }

        public static FunctionIdentifier of(Guid dbid)
        {
            return new FunctionIdentifier(dbid);
        }

        public override string ToString()
        {
            return string.Format("Function/{0}", DBID);
        }

        public static implicit operator string(FunctionIdentifier identifier)
        {
            return identifier.ToString();
        }

        public static implicit operator FunctionIdentifier(string identifier)
        {
            var subs = identifier.Split(new[] { '/' }, 2);
            return FunctionIdentifier.of(new Guid(subs[1]));
        }
    }
}