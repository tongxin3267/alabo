using System;
using System.Collections.Generic;
using System.Text;
using Alabo.App.Shop.Product.Domain.Entities;

namespace Alabo.App.Shop.Product.Domain.Dtos
{
    /// <summary>
    /// 产品线ViewModel
    /// </summary>
    public class ProductLineViewOut : ProductLine
    {
        /// <summary>
        /// 产品线捆绑的产品列表
        /// </summary>
        public IList<Domain.Entities.Product> ProductList { get; set; }
    }
}
