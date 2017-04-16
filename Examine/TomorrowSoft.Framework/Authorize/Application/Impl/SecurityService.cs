using System;
using System.Collections.Generic;
using System.Linq;
using TomorrowSoft.Authorize.Domain.Services;
using TomorrowSoft.Framework.Authorize.Domain;
using TomorrowSoft.Framework.Domain.Exceptions;
using TomorrowSoft.Framework.Domain.Repositories;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.Container;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.Excel;

namespace TomorrowSoft.Framework.Authorize.Application.Impl
{
    [RegisterToContainer]
    public partial class SecurityService : ISecurityService
    {
        private readonly IRepository repository;
        private readonly IPasswordSecurity passwordSecurity;

        public SecurityService(IRepository repository, IPasswordSecurity passwordSecurity)
        {
            this.repository = repository;
            this.passwordSecurity = passwordSecurity;
        }
    }
}