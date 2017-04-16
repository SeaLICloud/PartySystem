using System;
using System.Linq.Expressions;
using LinqSpecs;

namespace TomorrowSoft.Framework.Authorize.Domain
{
    public partial class Version
    {
        public class ByType : Specification<Version>
        {
            private readonly string _type;

            public ByType(string type)
            {
                _type = type;
            }

            public override Expression<Func<Version, bool>> IsSatisfiedBy()
            {
                return x => x.Type == _type;
            }
        }
    }
}