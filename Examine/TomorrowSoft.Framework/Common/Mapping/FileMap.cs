using TomorrowSoft.Framework.Common.Domain;
using TomorrowSoft.Framework.Infrastructure.Data.Repositories;

namespace TomorrowSoft.Framework.Common.Mapping
{
    public class FileMap:BaseClassMap<File>
    {
        public FileMap()
        {
            Map(x => x.Title);
            Map(x => x.Name);
            Map(x => x.ContentType);
            Map(x => x.Length);
            Map(x => x.SaveAsContent);
            Map(x => x.Content).Length(500 * 1024 * 1024);
            Map(x => x.StorageFolder);
        }
    }
}