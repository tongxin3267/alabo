using System;
using System.Collections.Generic;
using System.Text;
using Alabo.App.Shop.Product.ViewModels;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Shop.Product.Domain.Services {

    public interface IProductApiService : IService {

        /// <summary>
        /// 生成商品图片
        /// </summary>
        /// <param name="product"></param>
        /// <param name="images"></param>
        /// <returns></returns>
        string CreateImage(Entities.Product product, List<string> images);
    }
}