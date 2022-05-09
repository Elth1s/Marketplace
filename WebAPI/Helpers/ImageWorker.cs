using System.Drawing;
using System.Drawing.Drawing2D;

namespace WebAPI.Helpers
{
    public static class ImageWorker
    {
        public static Bitmap FromBase64StringToImage(this string base64String)
        {
            var cleanBase64 = base64String.Substring(22);
            byte[] byteBuffer = Convert.FromBase64String(cleanBase64);
            try
            {
                using (MemoryStream memoryStream = new MemoryStream(byteBuffer))
                {
                    memoryStream.Position = 0;
                    Image imgReturn;
                    imgReturn = Image.FromStream(memoryStream);
                    memoryStream.Close();
                    byteBuffer = null;
                    return new Bitmap(imgReturn);
                }
            }
            catch { return null; }
        }
        public static Bitmap Resize(this Image image, int width, int height)
        {

            var res = new Bitmap(width, height);
            using (var graphic = Graphics.FromImage(res))
            {
                graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphic.SmoothingMode = SmoothingMode.HighQuality;
                graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphic.CompositingQuality = CompositingQuality.HighQuality;
                graphic.DrawImage(image, 0, 0, width, height);
            }
            return res;
        }
    }
}
