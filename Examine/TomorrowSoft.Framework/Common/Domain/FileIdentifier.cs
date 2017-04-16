using TomorrowSoft.Framework.Domain.Bases;
using TomorrowSoft.Framework.Domain.Exceptions;

namespace TomorrowSoft.Framework.Common.Domain
{
    public struct FileIdentifier : IBusinessIdentifier
    {
        private const string Key = "File";
        public string DBID { get; private set; }

        public FileIdentifier(string dbid)
            : this()
        {
            DBID = dbid;
        }

        public static FileIdentifier of(string dbid)
        {
            return new FileIdentifier(dbid);
        }

        public override string ToString()
        {
            return string.Format("{0}/{1}", Key, DBID);
        }

        public static implicit operator string(FileIdentifier id)
        {
            return id.ToString();
        }

        public static implicit operator FileIdentifier(string id)
        {
            var subs = id.Split(new[] { '/' }, 2);
            if (subs[0] != Key)
                throw new DomainErrorException("不是文件的Id");
            return FileIdentifier.of(subs[1]);
        }
    }
}