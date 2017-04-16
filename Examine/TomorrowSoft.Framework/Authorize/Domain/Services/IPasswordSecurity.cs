namespace TomorrowSoft.Authorize.Domain.Services
{
    public interface IPasswordSecurity
    {
        byte[] CreateDbPassword(string password);
        bool ComparePasswords(byte[] storedPassword, string password);
    }
}