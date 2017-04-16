using TomorrowSoft.Framework.Authorize.Domain;
using TomorrowSoft.Framework.Domain.Exceptions;

namespace TomorrowSoft.Framework.Authorize.Application.Impl
{
    public partial class SecurityService
    {
        public IVersionCommand CreateVersion(string type)
        {
            var version = new Version();
            version.Type = type;
            repository.Save(version);
            return new VersionCommand(version);
        }

        public IVersionCommand EditVersion(string type)
        {
            var version = GetVersionByType(type);
            return new VersionCommand(version);
        }

        public Version GetVersionByType(string type)
        {
            if(!repository.IsExisted(new Version.ByType(type)))
                throw new DomainErrorException(string.Format("没有找到【{0}】的版本的信息", type));
            return repository.FindOne(new Version.ByType(type));
        }
    }
}