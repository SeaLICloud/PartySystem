using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace TomorrowSoft.Framework.Infrastructure.Crosscutting.Extensions
{
    public static class ImageExtensions
    {
         public static byte[] ToThumbnail(this byte[] imageContent, int width)
         {
             if (imageContent.Length == 0)
                 return imageContent;
             MemoryStream stream = new MemoryStream(imageContent);
             Image image = Image.FromStream(stream);
             var height = width*image.Height/image.Width;

             MemoryStream mstream = new MemoryStream();
             var thumbnail = image.GetThumbnailImage(width, height, null, IntPtr.Zero);
             thumbnail.Save(mstream, ImageFormat.Jpeg);
             byte[] byData = new Byte[mstream.Length];
             mstream.Position = 0;
             mstream.Read(byData, 0, byData.Length);
             mstream.Close();
             return byData;
         }
    }
}