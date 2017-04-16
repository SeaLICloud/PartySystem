namespace TomorrowSoft.Framework.Authorize.Application
{
    public interface IAuthorizedFunctionCommand
    {
        IAuthorizedFunctionCommand Area(string area);
        IAuthorizedFunctionCommand Controller(string controller);
        IAuthorizedFunctionCommand Action(string action);
        IAuthorizedFunctionCommand Description(string description);
        IAuthorizedFunctionCommand Group(string group);
        IAuthorizedFunctionCommand GroupIco(string groupIco); 
    }
}