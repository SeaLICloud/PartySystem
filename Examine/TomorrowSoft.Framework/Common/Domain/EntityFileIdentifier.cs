using TomorrowSoft.Framework.Domain.Bases;
using TomorrowSoft.Framework.Domain.Exceptions;

namespace TomorrowSoft.Framework.Common.Domain
{
    public struct EntityFileIdentifier : IBusinessIdentifier
    {
        private const string Key = "File";
        public string FileId { get; private set; }
        public string EntityId { get; private set; }

        public EntityFileIdentifier(string entityId, string fileId)
            : this()
        {
            EntityId = entityId;
            FileId = fileId;
        }

        public static EntityFileIdentifier of(string entityId, string fileId)
        {
            return new EntityFileIdentifier(entityId, fileId);
        }

        public override string ToString()
        {
            return string.Format("{0}/{1}", EntityId, FileId);
        }

        public static implicit operator string(EntityFileIdentifier id)
        {
            return id.ToString();
        }

        public static implicit operator EntityFileIdentifier(string id)
        {
            var subs = id.Split(new[] { '/' });
            if (subs.Length<2 || subs[subs.Length-2] != Key)
                throw new DomainErrorException("不是文件的Id");
            return EntityFileIdentifier.of(
                id.Substring(0, id.Length-Key.Length-subs[subs.Length-1].Length-2), 
                FileIdentifier.of(subs[subs.Length-1]));
        }
    }
}