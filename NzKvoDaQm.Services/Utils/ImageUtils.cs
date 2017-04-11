namespace NzKvoDaQm.Services.Utils
{

    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Text;

    public class ImageUtils
    {
        ImageUtils()
        {   
        }

        public static Image ConvertBase64ToImage(string base64)
        {
            var bytes = Convert.FromBase64String(base64);
            var stream = new MemoryStream(bytes);
            return Bitmap.FromStream(stream);
        }
    }
}