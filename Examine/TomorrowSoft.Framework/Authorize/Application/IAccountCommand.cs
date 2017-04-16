using TomorrowSoft.Framework.Authorize.Domain;

namespace TomorrowSoft.Framework.Authorize.Application
{
    public interface IAccountCommand
    {
        IAccountCommand ChangePassword(string oldPassword, string newPassword, string confirmPassword);
        IAccountCommand ResetPassword(string password);
        IAccountCommand Email(string email);
        IAccountCommand UseAccountAuthority();
        IAccountCommand UseRoleAuthority();
        IAccountCommand AddRole(RoleIdentifier id);
    }
}
