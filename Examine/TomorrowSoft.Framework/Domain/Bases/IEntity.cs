using System;

namespace TomorrowSoft.Framework.Domain.Bases
{
    public interface IEntity
    {
        Guid DBID { get; }
        DateTime CreateTime { get; set; }
        DateTime UpdateTime { get; set; }
        string CreateUser { get; set; }
        string UpdateUser { get; set; }
        bool CanBeDelete { get; } 
    }
}