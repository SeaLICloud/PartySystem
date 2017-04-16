using System;
using System.Linq.Expressions;
using LinqSpecs;

namespace TommorrowSoft.Examine.Domian
{
    public  partial class PartyMoney
    {

        public class By : Specification<PartyMoney>
        {
            public readonly PartyMoneyIdentifier _id;

            public By(PartyMoneyIdentifier id)
            {
               _id = id;
            }

            public override Expression<Func<PartyMoney, bool>> IsSatisfiedBy()
            {
                return x => x.Id.Code == _id.Code;
            }
        }
    }
}