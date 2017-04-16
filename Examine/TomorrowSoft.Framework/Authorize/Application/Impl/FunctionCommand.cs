using TomorrowSoft.Framework.Authorize.Domain;

namespace TomorrowSoft.Framework.Authorize.Application.Impl
{
    public class FunctionCommand : IFunctionCommand
    {
        public FunctionCommand(Function function)
        {
            Function = function;
        }

        public Function Function { get; private set; }

        public IFunctionCommand Area(string area)
        {
            Function.Area = area;
            return this;
        }

        public IFunctionCommand Controller(string controller)
        {
            Function.Controller = controller;
            return this;
        }

        public IFunctionCommand Action(string action)
        {
            Function.Action = action;
            return this;
        }

        public IFunctionCommand Description(string description)
        {
            Function.Description = description;
            return this;
        }

        public IFunctionCommand Group(string @group)
        {
            Function.Group = @group;
            return this;
        }

        public IFunctionCommand GroupIco(string groupIco)
        {
            Function.GroupIco = groupIco;
            return this;
        }

        public IFunctionCommand MenuAction(string menuAction)
        {
            Function.MenuAction = menuAction;
            return this;
        }

        public IFunctionCommand MenuDescription(string menuDescription)
        {
            Function.MenuDescription = menuDescription;
            return this;
        }
    }
}