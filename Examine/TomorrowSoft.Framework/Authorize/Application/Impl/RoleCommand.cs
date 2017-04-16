using System;
using TomorrowSoft.Framework.Authorize.Domain;
using TomorrowSoft.Framework.Domain.Repositories;

namespace TomorrowSoft.Framework.Authorize.Application.Impl
{
    public class RoleCommand : IRoleCommand
    {
        private readonly Role _role;
        private readonly IRepository _repository;

        public RoleCommand(Role role, IRepository repository)
        {
            _role = role;
            _repository = repository;
        }

        public IRoleCommand IsVisible(bool isVisible)
        {
            _role.IsVisible = isVisible;
            return this;
        }

        public IRoleCommand Description(string description)
        {
            _role.Description = description;
            return this;
        }

        public IRoleCommand MergeAuthoritiesWith(RoleIdentifier id)
        {
            var other = _repository.FindOne(new Role.By(id));
            _role.MergeAuthoritiesWith(other);
            return this;
        }
    }
}