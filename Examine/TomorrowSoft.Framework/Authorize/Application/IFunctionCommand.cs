using TomorrowSoft.Framework.Authorize.Domain;

namespace TomorrowSoft.Framework.Authorize.Application
{
    public interface IFunctionCommand
    {
        Function Function { get; }
        IFunctionCommand Area(string area);
        IFunctionCommand Controller(string controller);
        IFunctionCommand Action(string action);
        IFunctionCommand Description(string description);
        IFunctionCommand Group(string @group);
        IFunctionCommand GroupIco(string groupIco);
        IFunctionCommand MenuAction(string menuAction);
        IFunctionCommand MenuDescription(string menuDescription);
    }
}