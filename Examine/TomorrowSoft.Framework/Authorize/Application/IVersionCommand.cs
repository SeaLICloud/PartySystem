namespace TomorrowSoft.Framework.Authorize.Application
{
    public interface IVersionCommand
    {
        IVersionCommand VersionNumber(string versionNumber);
        IVersionCommand SoftwareTitle(string softwareTitle);
    }
}