using System;
using System.Linq.Expressions;
using LinqSpecs;

namespace TomorrowSoft.Framework.Authorize.Domain
{
    public partial class RunningNumber
    {
        public class By : Specification<RunningNumber>
        {
            private readonly RunningNumberIdentifier _id;

            public By(RunningNumberIdentifier id)
            {
                _id = id;
            }

            public override Expression<Func<RunningNumber, bool>> IsSatisfiedBy()
            {
                return x => x.Id.Key == _id.Key;
            }
        }
    }
}