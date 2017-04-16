using System;
using System.IO;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.Container;

namespace TomorrowSoft.Framework.Infrastructure.Crosscutting.Files
{
    [RegisterToContainer]
    public class FileHelper : IFileHelper
    {
        public byte[] Read(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
                throw new Exception("文件读取失败");
            var fs = new FileStream(filePath, FileMode.Open);
            var br = new BinaryReader(fs);
            var content = br.ReadBytes((int)fs.Length);
            fs.Close();
            return content;
        }

        public void Delete(string filePath)
        {
            System.IO.File.Delete(filePath);
        }
    }
}