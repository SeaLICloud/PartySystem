namespace TomorrowSoft.Framework.Infrastructure.Crosscutting.Files
{
    public interface IFileHelper
    {
        byte[] Read(string filePath);
        void Delete(string filePath);
    }
}