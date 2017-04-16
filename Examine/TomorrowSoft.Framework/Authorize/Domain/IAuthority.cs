using System.Collections.Generic;

namespace TomorrowSoft.Framework.Authorize.Domain
{
    public interface IAuthority
    {
        string Identifier { get; }
        Function Function { get; }
        bool IsAuthorized { get; set; }
    }
}