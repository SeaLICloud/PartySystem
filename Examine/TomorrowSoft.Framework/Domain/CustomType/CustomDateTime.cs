using System;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.Container;

namespace TomorrowSoft.Framework.Domain.CustomType
{
    [RegisterToContainer]
    public class CustomDateTime : IDateTime
    {
        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}