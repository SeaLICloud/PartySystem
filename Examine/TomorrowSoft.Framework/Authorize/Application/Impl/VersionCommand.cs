using TomorrowSoft.Framework.Authorize.Domain;

namespace TomorrowSoft.Framework.Authorize.Application.Impl
{
    public class VersionCommand : IVersionCommand
    {
        public Version Version { get; private set; }

        public VersionCommand(Version version)
        {
            Version = version;
        }

        public IVersionCommand VersionNumber(string versionNumber)
        {
            Version.VersionNumber = versionNumber;
            return this;
        }

        public IVersionCommand SoftwareTitle(string softwareTitle)
        {
            Version.SoftwareTitle = softwareTitle;
            return this;
        }
    }
}