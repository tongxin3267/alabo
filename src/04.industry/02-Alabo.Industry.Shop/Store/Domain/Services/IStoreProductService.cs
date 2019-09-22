using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Alabo.App.Shop.Store.Domain.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Shop.Store.Domain.Services {

    public interface IStoreProductService : IService {

        /// <summary>
        /// 根据用户和商品ID获取前台商品添加视图
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        ProductEditOuput GetProductView(ProductEditInput parameter);

        /// <summary>
        /// 商品保存
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        ServiceResult EditProduct(ProductEditOuput parameter);
    }
}