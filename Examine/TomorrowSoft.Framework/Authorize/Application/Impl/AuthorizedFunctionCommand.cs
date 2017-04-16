using TomorrowSoft.Framework.Authorize.Domain;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.Container;

namespace TomorrowSoft.Framework.Authorize.Application.Impl
{
    [RegisterToContainer]
    public class AuthorizedFunctionCommand : IAuthorizedFunctionCommand
    {
        private readonly Function _function;

        public AuthorizedFunctionCommand(Function function)
        {
            _function = function;
        }

        public IAuthorizedFunctionCommand Area(string area)
        {
            _function.Area = area;
            return this;
        }

        public IAuthorizedFunctionCommand Controller(string controller)
        {
            _function.Controller = controller;
            return this;
        }

        public IAuthorizedFunctionCommand Action(string action)
        {
            _function.Action = action;
            return this;
        }

        public IAuthorizedFunctionCommand Description(string description)
        {
            _function.Description = description;
            return this;
        }

        public IAuthorizedFunctionCommand Group(string group)
        {
            _function.Group = group;
            return this;
        }

        public IAuthorizedFunctionCommand GroupIco(string groupIco)
        {
            _function.GroupIco = groupIco;
            return this;
        }
    }
}