using TomorrowSoft.Framework.Authorize.Domain;

namespace TomorrowSoft.Framework.Authorize.Application
{
    public interface IRoleCommand
    {
        IRoleCommand IsVisible(bool isVisible);
        IRoleCommand Description(string description);
        IRoleCommand MergeAuthoritiesWith(RoleIdentifier id);
    }
}