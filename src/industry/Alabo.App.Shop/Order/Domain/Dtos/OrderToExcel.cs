using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Shop.Order.Domain.Dtos {

    public class OrderToExcel {

        [Display(Name = "订单号")]
        [Field(ListShow = true, SortOrder = 1)]
        public string OrderId { get; set; }

        [Display(Name = "用户名")]
        [Field(ListShow = true, SortOrder = 1)]
        public string UserName { get; set; }

        [Display(Name = "电话")]
        [Field(ListShow = true, SortOrder = 1)]
        public string Mobile { get; set; }

        [Display(Name = "品名")]
        [Field(ListShow = true, SortOrder = 1)]
        public string ProductName { get; set; }

        [Display(Name = "规格")]
        [Field(ListShow = true, SortOrder = 1)]
        public string ProductSku { get; set; }

        [Display(Name = "单价")]
        [Field(ListShow = true, SortOrder = 1)]
        public string Price { get; set; }

        [Display(Name = "数量")]
        [Field(ListShow = true, SortOrder = 1)]
        public string Count { get; set; }

        [Display(Name = "总价")]
        [Field(ListShow = true, SortOrder = 1)]
        public string TotalPrice { get; set; }

        [Display(Name = "已付金额")]
        [Field(ListShow = true, SortOrder = 1)]
        public string PayPrice { get; set; }

        [Display(Name = "下单时间")]
        [Field(ListShow = true, SortOrder = 1)]
        public string CreateTime { get; set; }

        [Display(Name = "支付时间")]
        [Field(ListShow = true, SortOrder = 1)]
        public string PayTime { get; set; }

        [Display(Name = "支付方式")]
        [Field(ListShow = true, SortOrder = 1)]
        public string PayType { get; set; }

        [Display(Name = "支付状态")]
        [Field(ListShow = true, SortOrder = 1)]
        public string PayStatus { get; set; }

        [Display(Name = "订单状态")]
        [Field(ListShow = true, SortOrder = 1)]
        public string OrderStatus { get; set; }
    }
}