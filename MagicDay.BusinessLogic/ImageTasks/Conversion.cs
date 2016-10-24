using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace MagicDay.BusinessLogic.ImageTasks
{
    public class Conversion
    {
        public byte[] ReSizeOriginalImage(byte[] imageData, int sizeToScale, Int64 quality)
        {
            using (MemoryStream imageStream = new MemoryStream(imageData))
            {
                Bitmap bitMap = new Bitmap(imageStream);
                ImageCodecInfo imageCodecInfo = GetEncoder(ImageFormat.Jpeg);
                Encoder imageEncoder = Encoder.Quality;
                EncoderParameters imageEncoderParameters = new EncoderParameters(1);
                EncoderParameter imageEncoderParameter = new EncoderParameter(imageEncoder, quality);
                imageEncoderParameters.Param[0] = imageEncoderParameter;
                int width = bitMap.Width;
                int height = bitMap.Height;
                ImageConverter converter = new ImageConverter();
                byte[] resizedImage;

                if (width > height || width == height)
                {
                    //landscape
                    if (height > sizeToScale)
                    {
                        double scale = (double)bitMap.Width / sizeToScale;
                        Bitmap resizedBitmap = (new Bitmap(bitMap, new Size(sizeToScale, System.Convert.ToInt32(bitMap.Height / scale))));
                        resizedBitmap.Save(imageStream, imageCodecInfo, imageEncoderParameters);
                        resizedBitmap.SetResolution(200f, 200f);
                        resizedImage = (byte[])converter.ConvertTo(resizedBitmap, typeof(byte[]));
                        imageData = resizedImage;
                    }
                }
                //portrait
                else if (width < height)
                {
                    if (width > sizeToScale)
                    {
                        double scale = (double)bitMap.Height / sizeToScale;
                        Bitmap resizedBitmap = (new Bitmap(bitMap, new Size(System.Convert.ToInt32(bitMap.Width / scale), sizeToScale)));
                        resizedBitmap.Save(imageStream, imageCodecInfo, imageEncoderParameters);
                        resizedBitmap.SetResolution(200f, 200f);
                        resizedImage = (byte[])converter.ConvertTo(resizedBitmap, typeof(byte[]));
                        imageData = resizedImage;
                    }
                }
                return imageData;
            }
        }
        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
    }
}
