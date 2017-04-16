using System;
using TomorrowSoft.Framework.Domain.Bases;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.Extensions;

namespace TomorrowSoft.Framework.Authorize.Domain
{
    public struct AccountAuthorityIdentifier : IBusinessIdentifier
    {
        /// <summary>
        /// 名称
        /// </summary>
        public AccountIdentifier AccountId { get; private set; }

        /// <summary>
        /// 功能Id
        /// </summary>
        public FunctionIdentifier FunctionId { get; private set; }

        public AccountAuthorityIdentifier(AccountIdentifier accountId, FunctionIdentifier functionId)
            : this()
        {
            AccountId = accountId;
            FunctionId = functionId;
        }

        public static AccountAuthorityIdentifier of(AccountIdentifier accountId, FunctionIdentifier functionId)
        {
            return new AccountAuthorityIdentifier(accountId, functionId);
        }

        public override string ToString()
        {
            return string.Format("{0}/{1}", AccountId, FunctionId);
        }

        public static implicit operator string(AccountAuthorityIdentifier identifier)
        {
            return identifier.ToString();
        }

        public static implicit operator AccountAuthorityIdentifier(string identifier)
        {
            var subs = identifier.Split(new[] { '/' }, 4);
            return of(AccountIdentifier.of(subs[1]), FunctionIdentifier.of(new Guid(subs[3])));
        }
    }
}