using System.Collections.Generic;

namespace TomorrowSoft.Framework.Common.Domain
{
    public interface IContainFiles
    {
        IEnumerable<File> Files { get; }
        File File(FileIdentifier id);
        void RemoveFile(FileIdentifier id);
        void AddFile(File file);
        IEnumerable<File> GetFiles(string title);
    }
}