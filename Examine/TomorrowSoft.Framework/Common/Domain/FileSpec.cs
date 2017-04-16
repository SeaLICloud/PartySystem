using Machine.Specifications;

namespace TomorrowSoft.Framework.Common.Domain
{
    public class FileSpec
    {
        Establish context = () => file = new File();
        Because of = () => result = file.Size();
        protected static File file;
        protected static string result;
        protected const string EntityId = "EntityId";
        protected const string FileId = "FileId";
    }

    [Subject(typeof(File), "Size")]
    public class when_file_content_is_null : FileSpec
    {
        It should_equal_0_KB = () => result.ShouldEqual("0 KB");
    }

    [Subject(typeof(File), "Size")]
    public class when_file_content_is_not_null : FileSpec
    {
        Establish context = 
            () =>
                {
                    file.Length = 2048000;
                    file.Content = new byte[file.Length];
                };
        It should_equal_correct_value = () => result.ShouldEqual("2,000 KB");
    }
}