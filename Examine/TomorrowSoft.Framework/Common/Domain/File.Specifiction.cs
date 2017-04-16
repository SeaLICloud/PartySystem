using System;
using System.Linq.Expressions;
using LinqSpecs;

namespace TomorrowSoft.Framework.Common.Domain
{
    public partial class File
    {
        public class By : Specification<File>
        {
            private readonly FileIdentifier _id;


            public By(FileIdentifier id)
            {
                _id = id;
            }

            public override Expression<Func<File, bool>> IsSatisfiedBy()
            {
                return x => x.DBID == new Guid(_id.DBID);
            }
        }
    }
}