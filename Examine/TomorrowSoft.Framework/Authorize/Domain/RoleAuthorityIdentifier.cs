using System;
using TomorrowSoft.Framework.Domain.Bases;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.Extensions;

namespace TomorrowSoft.Framework.Authorize.Domain
{
    public struct RoleAuthorityIdentifier : IBusinessIdentifier
    {
        /// <summary>
        /// 名称
        /// </summary>
        public RoleIdentifier RoleId { get; private set; }

        /// <summary>
        /// 功能Id
        /// </summary>
        public FunctionIdentifier FunctionId { get; private set; }

        public RoleAuthorityIdentifier(RoleIdentifier roleId, FunctionIdentifier functionId)
            : this()
        {
            RoleId = roleId;
            FunctionId = functionId;
        }

        public static RoleAuthorityIdentifier of(RoleIdentifier roleId, FunctionIdentifier functionId)
        {
            return new RoleAuthorityIdentifier(roleId, functionId);
        }

        public override string ToString()
        {
            return string.Format("{0}/{1}", RoleId, FunctionId);
        }

        public static implicit operator string(RoleAuthorityIdentifier identifier)
        {
            return identifier.ToString();
        }

        public static implicit operator RoleAuthorityIdentifier(string identifier)
        {
            var subs = identifier.Split(new[] { '/' }, 4);
            return of(RoleIdentifier.of(subs[1]), FunctionIdentifier.of(new Guid(subs[3])));
        }
    }
}