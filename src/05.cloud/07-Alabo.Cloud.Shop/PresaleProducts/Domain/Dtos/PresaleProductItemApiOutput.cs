using System;
using System.Collections.Generic;
using System.Text;
using Alabo.App.Market.PresaleProducts.Domain.ViewModels;

namespace Alabo.App.Market.PresaleProducts.Domain.Dtos
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
