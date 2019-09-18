using System;
using System.Collections.Generic;
using System.Text;

namespace Alabo.App.Shop.Store.Domain.Dtos {

    /// <summary>
    /// 商品编辑输入模型
    /// </summary>
    public class ProductEditInput {

        /// <summary>
        /// 商品ID
        /// </summary>
        public long ProductId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 类目ID
        /// </summary>
        public Guid CategoryId { get; set; }
    }
}