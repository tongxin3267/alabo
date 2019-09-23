using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Shop.Category.Domain.Entities;
using Alabo.App.Shop.Category.Domain.Services;
using Alabo.App.Shop.Product.Domain.CallBacks;
using Alabo.App.Shop.Product.Domain.Entities;
using Alabo.App.Shop.Product.Domain.Repositories;
using Alabo.App.Shop.Product.ViewModels;
using Alabo.App.Shop.Store.Domain.Services;
using Alabo.Cache;
using Alabo.Core.Files;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Mapping;

namespace Alabo.App.Shop.Product.Domain.Services {

    public class ProductApiService : ServiceBase, IProductApiService {

        public ProductApiService(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        /// <summary> 根据图片原始地址获取List<ProductThum>，同时自动生成多张图片 </summary>
        /// <param name="images">原始图片地址，多个用,隔开</param>

        public string CreateImage(Entities.Product product, List<string> images) {
            var list = new List<ProductThum>();
            if (images.IsNullOrEmpty()) {
                return list.ToJson();
            }

            var config = Resolve<IAutoConfigService>().GetValue<ProductConfig>();
            if (config.WidthThanHeight <= 0) {
                throw new InvalidOperationException("缩略图高宽比例必须>0.");
            }

            if (config.ThumbnailWidth <= 0) {
                throw new InvalidOperationException("列表页缩略图宽度必须>0.");
            }

            if (config.ShowCaseWidth <= 0) {
                throw new InvalidOperationException("详情页橱窗图宽度必须>0.");
            }

            var i = 0;
            foreach (var item in images) {
                if (!item.IsNullOrEmpty()) {
                    var savePath = "";
                    var thum = new ProductThum();
                    var originalPath = "";
                    var suffixIndex = item.LastIndexOf(".", StringComparison.Ordinal);
                    if (suffixIndex < 0) {
                        continue;
                    }
                    var suffix = item.Substring(suffixIndex, item.Length - suffixIndex); //后缀名
                    if (item.Contains("X") || item.StartsWith("http")) //如果不是新增，上送的图片会带有X,处理为原图片
                    {
                        thum.OriginalUrl = item.Split('_')[0];
                        int width = decimal.ToInt16(config.ThumbnailWidth); //宽度
                        int height = decimal.ToInt16(config.ThumbnailWidth * config.WidthThanHeight); //高度

                        thum.ThumbnailUrl = $@"{thum.OriginalUrl}_{width}X{height}{suffix}";
                        width = decimal.ToInt16(config.ShowCaseWidth); //宽度
                        height = decimal.ToInt16(config.ShowCaseWidth * config.WidthThanHeight); //高度
                        thum.ShowCaseUrl = $@"{thum.OriginalUrl}_{width}X{height}{suffix}";
                        originalPath = Path.Combine(FileHelper.RootPath, thum.OriginalUrl.TrimStart('/'));
                    } else {
                        thum.OriginalUrl = item;
                        //缩略图片处理
                        int width = decimal.ToInt16(config.ThumbnailWidth); //宽度
                        int height = decimal.ToInt16(config.ThumbnailWidth * config.WidthThanHeight); //高度

                        thum.ThumbnailUrl = $@"{thum.OriginalUrl}_{width}X{height}{suffix}";
                        originalPath = Path.Combine(FileHelper.RootPath, thum.OriginalUrl.TrimStart('/'));
                        //替换第二个路径前的/，避免生成相对路径
                        savePath = Path.Combine(FileHelper.RootPath, thum.ThumbnailUrl.TrimStart('/'));
                        //替换第二个路径前的/，避免生成相对路径
                        if (!File.Exists(savePath)) //图片不存在才处理
                        {
                            ImageHelper.ImageCompression(originalPath, savePath, width, height); //改成绝对路径
                        }

                        //详情页图片处理
                        width = decimal.ToInt16(config.ShowCaseWidth); //宽度
                        height = decimal.ToInt16(config.ShowCaseWidth * config.WidthThanHeight); //高度
                        thum.ShowCaseUrl = $@"{thum.OriginalUrl}_{width}X{height}{suffix}";
                        savePath = Path.Combine(FileHelper.RootPath, thum.ShowCaseUrl.TrimStart('/'));
                        //替换第二个路径前的/，避免生成相对路径
                        if (!File.Exists(savePath)) //图片不存在才处理
                        {
                            ImageHelper.ImageCompression(originalPath, savePath, width, height); //改成绝对路径
                        }
                    }

                    //生成小图片
                    if (i == 0 && !item.StartsWith("http")) {
                        product.ThumbnailUrl = thum.ThumbnailUrl;
                        product.SmallUrl = $@"{thum.OriginalUrl}_50X50{suffix}";
                        savePath = Path.Combine(FileHelper.RootPath,
                            product.SmallUrl.TrimStart('/')); //替换第二个路径前的/，避免生成相对路径
                        if (!File.Exists(savePath)) //图片不存在才处理
                        {
                            ImageHelper.ImageCompression(originalPath, savePath, 50, 50); //改成绝对路径
                        }

                        i++;
                    }

                    list.Add(thum);
                }
            }

            return list.ToJson();
        }
    }
}