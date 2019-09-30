using Alabo.Cloud.Shop.PresaleProducts.Domain.ViewModels;
using System.Collections.Generic;

namespace Alabo.Cloud.Shop.PresaleProducts.Domain.Dtos
{
    public class PresaleProductItemApiOutput
    {
        /// <summary>
        ///     返回数据源的总页数
        /// </summary>
        public long TotalSize { get; set; }

        /// <summary>
        /// </summary>
        public IList<PresaleProductItem> ProductItems { get; set; }
    }
}