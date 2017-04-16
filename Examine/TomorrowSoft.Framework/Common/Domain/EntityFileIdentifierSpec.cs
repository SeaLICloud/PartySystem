using Machine.Specifications;

namespace TomorrowSoft.Framework.Common.Domain
{

    public class EntityFileIdentifierSpec
    {
        protected const string EntityId = "Entity/Id";
        protected const string FileId = "File/Id";
        protected static File file;
        protected static EntityFileIdentifier result;
    }

    public class when_construct_a_file_identifier : EntityFileIdentifierSpec
    {
        Because of = () => result = EntityFileIdentifier.of(EntityId, FileId);
        It should_to_correct_string =
            () => result.ToString().ShouldEqual(string.Format("{0}/{1}", EntityId, FileId));
    }

    public class when_convert_string_to_file_identifier : EntityFileIdentifierSpec
    {
        Because of = () => result = string.Format("{0}/{1}", EntityId, FileId);
        It should_be_correct_entity_id = () => result.EntityId.ShouldEqual(EntityId);
        It should_be_correct_file_id = () => result.FileId.ShouldEqual(FileId);
    }
}