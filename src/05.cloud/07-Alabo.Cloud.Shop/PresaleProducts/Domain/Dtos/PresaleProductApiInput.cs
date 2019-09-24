using System;
using System.Collections.Generic;
using System.Text;
using Alabo.Domains.Query.Dto;

namespace Alabo.App.Market.PresaleProducts.Domain.Dtos
{
    public class PresaleProductApiInput : ApiInputDto
    {
        /// <summary>
        /// 商品模式
        /// </summary>
        public Guid? PriceStyleId { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 商品分类
        /// </summary>
        public string CategoryId { get; set; }

        /// <summary>
        /// 起始价格
        /// </summary>
        public uint StartPrice { get; set; }

        /// <summary>
        /// 结束价格
        /// </summary>
        public uint EndPrice { get; set; }
    }
}
