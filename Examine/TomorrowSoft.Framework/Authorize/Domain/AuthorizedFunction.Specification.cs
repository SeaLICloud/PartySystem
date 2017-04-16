using System;
using System.Linq.Expressions;
using LinqSpecs;

namespace TomorrowSoft.Framework.Authorize.Domain
{
    public partial class Function
    {
        public class By : Specification<Function>
        {
            private readonly FunctionIdentifier _identifier;

            public By(FunctionIdentifier identifier)
            {
                _identifier = identifier;
            }

            public override Expression<Func<Function, bool>> IsSatisfiedBy()
            {
                return x => x.DBID == _identifier.DBID;
            }
        }
    }
}