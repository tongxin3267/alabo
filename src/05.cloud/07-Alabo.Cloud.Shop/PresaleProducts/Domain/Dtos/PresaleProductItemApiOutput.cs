using System.Collections.Generic;
using Alabo.Cloud.Shop.PresaleProducts.Domain.ViewModels;

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
