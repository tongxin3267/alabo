﻿using Alabo.Data.Things.Goodss.Domain.Entities;
using System.Collections.Generic;

namespace Alabo.Data.Things.Goodss.Dtos
{
    public class GoodsLineOutput : GoodsLine
    {
        /// <summary>
        ///     产品线捆绑的产品列表
        /// </summary>
        public IList<Goods> GoodsList { get; set; }
    }
}