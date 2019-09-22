using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Alabo.Core.Files
{
    public static class ImageHelper
    {
        /// <summary>
        ///     图片压缩
        /// </summary>
        /// <param name="imageUrl">原始图片地址</param>
        /// <param name="savePath">压缩后的图片保存地址</param>
        /// <param name="width">图片宽度</param>
        /// <param name="height">图片高度</param>
        public static void ImageCompression(string imageUrl, string savePath, int width, int height)
        {
            try
            {
                var originalImage = Image.FromFile(imageUrl);
                var towidth = width;
                var toheight = height;
                var x = 0;
                var y = 0;
                var ow = originalImage.Width;
                var oh = originalImage.Height;
                //裁剪同等比例大小,不变形
                if (originalImage.Width / (double) originalImage.Height > towidth / (double) toheight)
                {
                    oh = originalImage.Height;
                    ow = originalImage.Height * towidth / toheight;
                    y = 0;
                    x = (originalImage.Width - ow) / 2;
                }
                else
                {
                    ow = originalImage.Width;
                    oh = originalImage.Width * height / towidth;
                    x = 0;
                    y = (originalImage.Height - oh) / 2;
                }

                //新建一个bmp图片
                var b = new Bitmap(towidth, toheight);
                try
                {
                    //新建一个画板
                    var g = Graphics.FromImage(b);
                    //设置高质量插值法
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    //设置高质量,低速度呈现平滑程度
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    //清空画布并以透明背景色填充
                    g.Clear(Color.Transparent);
                    //在指定位置并且按指定大小绘制原图片的指定部分
                    g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight), new Rectangle(x, y, ow, oh),
                        GraphicsUnit.Pixel);
                    //获取图像编码解码器的所有相关信息
                    var ici = GetCodecInfoByFileName(savePath);
                    //保存图片
                    SaveImage(b, savePath, ici);
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    originalImage.Dispose();
                    b.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        ///     保存图片
        /// </summary>
        /// <param name="image">Image 对象</param>
        /// <param name="savePath">保存路径</param>
        /// <param name="ici">指定格式的编解码参数</param>
        private static void SaveImage(Image image, string savePath, ImageCodecInfo ici)
        {
            //设置 原图片 对象的 EncoderParameters 对象
            var parameters = new EncoderParameters(1);
            parameters.Param[0] = new EncoderParameter(Encoder.Quality, (long) 100);
            image.Save(savePath, ici, parameters);
            parameters.Dispose();
        }

        /// <summary>
        ///     获取图像编码解码器的所有相关信息
        /// </summary>
        /// <param name="mimeType">包含编码解码器的多用途网际邮件扩充协议 (MIME) 类型的字符串</param>
        /// <returns>返回图像编码解码器的所有相关信息</returns>
        private static ImageCodecInfo GetCodecInfo(string mimeType)
        {
            var codecInfo = ImageCodecInfo.GetImageEncoders();
            foreach (var ici in codecInfo) {
                if (ici.MimeType == mimeType) {
                    return ici;
                }
            }

            return null;
        }

        /// <summary>
        ///     根据文件名获取图片编码信息
        /// </summary>
        /// <param name="fileName"></param>
        private static ImageCodecInfo GetCodecInfoByFileName(string fileName)
        {
            var fileExt = fileName.Substring(fileName.LastIndexOf(".") + 1).ToUpper();
            var codecInfo = ImageCodecInfo.GetImageEncoders();
            foreach (var ici in codecInfo) {
                if (ici.FilenameExtension.Contains(fileExt)) {
                    return ici;
                }
            }

            return null;
        }

        /// <summary>
        ///     得到图片格式
        /// </summary>
        /// <param name="name">文件名称</param>
        public static ImageFormat GetFormat(string name)
        {
            var ext = name.Substring(name.LastIndexOf(".") + 1);
            switch (ext.ToLower())
            {
                case "jpg":
                case "jpeg":
                    return ImageFormat.Jpeg;

                case "bmp":
                    return ImageFormat.Bmp;

                case "png":
                    return ImageFormat.Png;

                case "gif":
                    return ImageFormat.Gif;

                default:
                    return ImageFormat.Jpeg;
            }
        }

        /// <summary>
        ///     图片缩放
        /// </summary>
        /// <param name="b"></param>
        /// <param name="destHeight"></param>
        /// <param name="destWidth"></param>
        public static Bitmap GetThumbnail(Bitmap b, int destHeight, int destWidth)
        {
            Image imgSource = b;
            var thisFormat = imgSource.RawFormat;
            int sW = 0, sH = 0;
            // 按比例缩放
            var sWidth = imgSource.Width;
            var sHeight = imgSource.Height;
            if (sHeight > destHeight || sWidth > destWidth)
            {
                if (sWidth * destHeight > sHeight * destWidth)
                {
                    sW = destWidth;
                    sH = destWidth * sHeight / sWidth;
                }
                else
                {
                    sH = destHeight;
                    sW = sWidth * destHeight / sHeight;
                }
            }
            else
            {
                sW = sWidth;
                sH = sHeight;
            }

            var outBmp = new Bitmap(destWidth, destHeight);
            var g = Graphics.FromImage(outBmp);
            g.Clear(Color.Transparent);
            // 设置画布的描绘质量
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(imgSource, new Rectangle((destWidth - sW) / 2, (destHeight - sH) / 2, sW, sH), 0, 0,
                imgSource.Width, imgSource.Height, GraphicsUnit.Pixel);
            g.Dispose();
            // 以下代码为保存图片时，设置压缩质量
            var encoderParams = new EncoderParameters();
            var quality = new long[1];
            quality[0] = 100;
            var encoderParam = new EncoderParameter(Encoder.Quality, quality);
            encoderParams.Param[0] = encoderParam;
            imgSource.Dispose();
            return outBmp;
        }
    }
}