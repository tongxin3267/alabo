using System.Collections.Generic;
using Alabo.Domains.Services;

namespace Alabo.Industry.Shop.Products.Domain.Services {

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