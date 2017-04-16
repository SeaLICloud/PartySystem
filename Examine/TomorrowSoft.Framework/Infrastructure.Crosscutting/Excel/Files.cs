using System;
using System.IO;
using System.Text;

namespace TomorrowSoft.Framework.Infrastructure.Crosscutting.Excel
{
    public static class Files
    {
        /// <summary>  
        /// 写入文件   
        /// </summary>  
        /// <param name="Content"></param>  
        /// <param name="FileSavePath"></param>  
        public static void Write(string Content, string FileSavePath)
        {
            if (File.Exists(FileSavePath))
            {
                File.Delete(FileSavePath);
            }
            FileStream fs = File.Create(FileSavePath);
            Byte[] bContent = System.Text.Encoding.Default.GetBytes(Content);
            fs.Write(bContent, 0, bContent.Length);
            fs.Close();
            fs.Dispose();
        }

        /// <summary>  
        /// 获取文件的内容  
        /// </summary>  
        /// <param name="file"></param>  
        /// <returns></returns>  
        public static string Read(string file)
        {
            FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            StringBuilder output = new StringBuilder();
            string rl;
            while ((rl = sr.ReadLine()) != null)
            {
                output.Append(rl + "\r\n");
            }
            sr.Close();
            fs.Close();
            return output.ToString();
        }  
    }
}