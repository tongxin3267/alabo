using Alabo.AutoConfigs;
using Alabo.Cloud.Shop.ProductQrCode.Configs;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Files;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Industry.Shop.Products.Domain.Services;
using Alabo.Industry.Shop.Products.ViewModels;
using Alabo.Runtime;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Alabo.Cloud.Shop.ProductQrCode.Servcies
{
    public class ProductQrCodeService : ServiceBase, IProductQrCodeService
    {
        /// <summary>
        ///     TODO 一品一码
        /// </summary>
        /// <param name="unitOfWork"></param>
        public ProductQrCodeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="productId"></param>
        public void CreateQrcode(long productId)
        {
            //文件夹不存在，重新生成文件夹
            FileHelper.CreateDirectory(FileHelper.WwwRootPath + "//productqrcode");
            //二维码图片地址
            var qrcodePath = $"/wwwroot/productqrcode/{productId}.jpeg";
            var qrConfig = Resolve<IAutoConfigService>().GetValue<ProductQrCodeConfig>();
            if (qrConfig != null)
            {
                if (qrConfig.BgPicture.IsNullOrEmpty()) {
                    qrConfig.BgPicture = "/wwwroot/assets/mobile/images/qrcode/01.png";
                }

                qrConfig.BgPicture.Replace('/', '\\');
            }

            //生成底部二维码
            GetImg(productId);
            var webSite = Resolve<IAutoConfigService>().GetValue<WebSiteConfig>();
            //二维码网址
            // from页面根据二维码设置，完成两种客户需求：1.跳转需求，2 展示具体的内容，比如公司介绍等等
            var url = $@"{webSite.DomainName}/product/show/{productId}";
            var product = Resolve<IProductService>().GetSingle(u => u.Id == productId);
            var productDetail = Resolve<IProductDetailService>().GetSingle(u => u.ProductId == productId);
            if (!url.Contains("http://")) {
                url = $"http://{url}";
            }

            try
            {
                var bmp = new Bitmap(520, 650);
                var g = Graphics.FromImage(bmp);
                g.Clear(Color.White);
                var productThums = productDetail.ImageJson.DeserializeJson<List<ProductThum>>();
                qrConfig.BgPicture = productThums[0].ShowCaseUrl;
                //二维码生成
                var bgPath = RuntimeContext.Current.Path.RootPath + qrConfig.BgPicture;

                var whitePath = RuntimeContext.Current.Path.RootPath + "/wwwroot/productqrcode/foot.jpeg";
                var bg = Image.FromFile(bgPath);
                var white = Image.FromFile(whitePath);

                g.DrawImage(bg, new Point(0, 0));
                g.DrawImage(white, new Point(0, 520));
                var result = new MemoryStream();
                bmp.Save(result, ImageFormat.Jpeg);
                bmp.Save(FileHelper.RootPath + qrcodePath, ImageFormat.Jpeg);
                result.Flush();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }

        /// <summary>
        ///     获取商品信息和二维码底部
        /// </summary>
        public void GetImg(long productId)
        {
            //文件夹不存在，重新生成文件夹
            FileHelper.CreateDirectory(FileHelper.WwwRootPath + "//productqrcode");
            GetWhiteFoot();
            //二维码图片地址
            var qrcodePath = "/wwwroot/productqrcode/foot.jpeg";
            var qrConfig = Resolve<IAutoConfigService>().GetValue<ProductQrCodeConfig>();
            if (qrConfig != null)
            {
                if (qrConfig.BgPicture.IsNullOrEmpty()) {
                    qrConfig.BgPicture = "/wwwroot/assets/mobile/images/qrcode/01.png";
                }

                qrConfig.BgPicture.Replace('/', '\\');
            }

            qrConfig.QrCodeBig = 120;
            qrConfig.PositionTop = 0;
            qrConfig.PositionLeft = 380;
            var webSite = Resolve<IAutoConfigService>().GetValue<WebSiteConfig>();
            //二维码网址
            var url = $@"{webSite.DomainName}/product/show/{productId}";
            var product = Resolve<IProductService>().GetSingle(u => u.Id == productId);
            if (!url.Contains("http://")) {
                url = $"http://{url}";
            }

            try
            {
                var qrGenerator = new QRCodeGenerator();
                var qrCodeData =
                    qrGenerator.CreateQrCode(url.ToEncoding(), QRCodeGenerator.ECCLevel.H); // 使用utf-8编码，解决某些浏览器不能识别问题
                var qrCode = new QRCode(qrCodeData);
                var bmp = qrCode.GetGraphic(100);
                //二维码生成
                bmp = new Bitmap(bmp, qrConfig.QrCodeBig, qrConfig.QrCodeBig);
                var bgPath = RuntimeContext.Current.Path.RootPath + "/wwwroot/productqrcode/white.jpeg";
                bmp = ImageHelper.GetThumbnail(bmp, qrConfig.QrCodeBig, qrConfig.QrCodeBig);
                var bg = Image.FromFile(bgPath);
                var g = Graphics.FromImage(bg);
                g.DrawImage(bmp, new Point(qrConfig.PositionLeft, qrConfig.PositionTop));
                qrConfig.IsDisplayUserInformation = true;
                if (qrConfig.IsDisplayUserInformation)
                {
                    var productName = product.Name;
                    //var productBn = "货号：" + product.Bn;
                    var productPrice = "￥" + product.Price;
                    var descRect = new RectangleF();
                    //使用一个框架放商品信息 控制位置
                    using (var font = new Font("微软雅黑", 12, FontStyle.Regular))
                    {
                        var length = g.MeasureString(productName, font);
                        length = g.MeasureString(productName, font);
                        descRect.Location = new Point(0, 5);
                        descRect.Size = new Size(350, 0);
                        g.DrawString(productName + "\n" + productPrice, font, Brushes.Black, descRect);
                    }
                }

                var result = new MemoryStream();
                bg.Save(result, ImageFormat.Jpeg);
                bg.Save(FileHelper.RootPath + qrcodePath, ImageFormat.Jpeg);
                result.Flush();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }

        /// <summary>
        ///     生成空白底部
        /// </summary>
        public void GetWhiteFoot()
        {
            try
            {
                //画布大小  宽*高
                var bmp = new Bitmap(520, 200);
                var g = Graphics.FromImage(bmp);
                g.Clear(Color.White);
                var result = new MemoryStream();
                bmp.Save(result, ImageFormat.Jpeg);
                bmp.Save(FileHelper.RootPath + "/wwwroot/productqrcode/white.jpeg", ImageFormat.Jpeg);
                result.Flush();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }
    }
}