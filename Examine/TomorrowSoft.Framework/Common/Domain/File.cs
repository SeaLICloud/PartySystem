using System;
using System.IO;
using TomorrowSoft.Framework.Domain.Bases;

namespace TomorrowSoft.Framework.Common.Domain
{
    public partial class File : Entity<File>
    {
        public static string Root = "";

        public virtual FileIdentifier Id
        {
            get { return FileIdentifier.of(DBID.ToString()); }
        }

        /// <summary>
        /// 文件标题
        /// </summary>
        public virtual string Title { get; set; }
        /// <summary>
        /// 文件名
        /// </summary> 
        public virtual string Name { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>
        public virtual string ContentType { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public virtual int Length { get; set; }
        /// <summary>
        /// 是否以内容的方式存在数据库
        /// </summary>
        public virtual bool SaveAsContent { get; set; }
        /// <summary>
        /// 文件内容
        /// </summary>
        public virtual byte[] Content { get; set; }
        /// <summary>
        /// 存储文件名称
        /// </summary>
        public virtual string StorageFolder { get; set; }

        public virtual string Size()
        {
            return string.Format("{0:N0} KB", Length / 1024);
        }

        public virtual byte[] GetContent()
        {
            if (SaveAsContent)
                return Content;

            var fullName = Path.Combine(Root, this.StorageFolder, this.Name);
            var fs = new FileStream(fullName, FileMode.Open);
            var contentFromFile = new byte[fs.Length];
            fs.Read(contentFromFile, 0, contentFromFile.Length);
            fs.Close();
            return contentFromFile;
        }

        /// <summary>
        /// 保存为文件
        /// </summary>
        /// <param name="content"></param>
        public virtual void SaveAs(byte[] content)
        {
            this.SaveAsContent = false;
            this.StorageFolder = DateTime.Today.ToString("yyyy-MM-dd");
            
            //文件夹如果不存在，则创建
            if (!Directory.Exists(Path.Combine(Root, this.StorageFolder)))
                Directory.CreateDirectory(Path.Combine(Root, this.StorageFolder));

            //保存文件 
            this.Name = GetUniqueFileName(Path.Combine(Root, this.StorageFolder, this.Name));
            var fs = new FileStream(Path.Combine(Root, this.StorageFolder, this.Name), FileMode.Create);
            fs.Write(content, 0, content.Length);
            fs.Close();
        }

        /// <summary>
        /// 得到唯一文件名，如：如果存在file.doc，则返回file (1).doc；如果存在file (1).doc，则返回file (2).doc
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns></returns>
        private string GetUniqueFileName(string fullName)
        {
            var directory = Path.GetDirectoryName(fullName);
            var filename = Path.GetFileNameWithoutExtension(fullName);
            var extension = Path.GetExtension(fullName);
            int index = 0;
            while (System.IO.File.Exists(fullName))
            {
                index++;
                fullName = string.Format("{0}\\{1} ({2}){3}", directory, filename, index, extension);
            }
            return Path.GetFileName(fullName);
        }

        /// <summary>
        /// 删除物理文件
        /// </summary>
        public virtual void Delete()
        {
            if (SaveAsContent)
                return;

            var fullName = Path.Combine(Root, this.StorageFolder, this.Name);
            if(System.IO.File.Exists(fullName))
                System.IO.File.Delete(fullName);
        }
    }
}