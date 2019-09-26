using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Data.People.Users.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Query;
using Alabo.Extensions;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebApis.Service;
using Alabo.Framework.Core.WebUis;
using Alabo.Framework.Core.WebUis.Design.AutoLists;
using Alabo.Framework.Core.WebUis.Design.AutoTables;
using Alabo.Helpers;
using Alabo.Industry.Offline.Merchants.Domain.Services;
using Alabo.Industry.Offline.Order.Domain.Entities;
using Alabo.Industry.Offline.Order.Domain.Entities.Extensions;
using Alabo.Industry.Offline.Order.Domain.Enums;
using Alabo.Industry.Offline.Order.Domain.Services;
using Alabo.Mapping;
using Alabo.Tenants;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Industry.Offline.Order.ViewModels
{
    /// <summary>
    ///     订单列表
    /// </summary>
    [ClassProperty(Name = "订单列表", PageType = ViewPageType.List)]
    public class MerchantOrderList : UIBase, IAutoTable<MerchantOrderList>, IAutoList
    {
        /// <summary>
        ///     店铺logo
        /// </summary>
        [Display(Name = "店铺Logo")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true)]
        public string ThumbnailUrl { get; set; }

        /// <value>
        ///     Id标识
        /// </value>
        public long Id { get; set; }

        /// <summary>
        ///     支付方式Id
        /// </summary>
        [Display(Name = "支付方式Id")]
        public long PayId { get; set; }

        /// <summary>
        ///     订单ID
        /// </summary>
        [Display(Name = "编号")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, IsShowBaseSerach = true)]
        public string Serial
        {
            get
            {
                var searSerial = $"9{Id.ToString().PadLeft(9, '0')}";
                if (Id.ToString().Length == 10) searSerial = $"{Id.ToString()}";
                return searSerial;
            }
        }

        /// <summary>
        ///     店铺id
        /// </summary>
        [Display(Name = "店铺id")]
        public string MerchantStoreId { get; set; }

        /// <summary>
        ///     店铺名称
        /// </summary>
        [Display(Name = "店铺名称")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, IsShowBaseSerach = true)]
        public string MerchantStoreName { get; set; }

        /// <summary>
        ///     订单类型
        /// </summary>
        public MerchantOrderType OrderType { get; set; }

        /// <summary>
        ///     订单交易状态
        /// </summary>
        [Display(Name = "订单状态")]
        [Field(ControlsType = ControlsType.DropdownList, ListShow = true, IsTabSearch = true)]
        public MerchantOrderStatus MerchantOrderStatus { get; set; }

        /// <summary>
        ///     订单总金额
        ///     订单总金额=商品总金额-优惠金额-（+）调整金额+税费金额+邮费金额 -其他账户支出
        /// </summary>
        [Display(Name = "应付金额")]
        [Field(ControlsType = ControlsType.Numberic, ListShow = true)]
        public decimal TotalAmount { get; set; }

        /// <summary>
        ///     订单总数量
        /// </summary>
        [Display(Name = "商品数量")]
        [Field(ControlsType = ControlsType.Numberic, ListShow = true)]
        public long TotalCount { get; set; }

        /// <summary>
        ///     订单实际支付的金额
        ///     订单实际支付的金额=商品总金额-优惠金额-（+）调整金额+邮费金额 -其他账户支出
        /// </summary>
        [Display(Name = "实付金额")]
        [Field(ControlsType = ControlsType.Numberic, ListShow = true)]
        public decimal PaymentAmount { get; set; }

        /// <summary>
        ///     创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public string CreateTime { get; set; }

        /// <summary>
        ///     支付时间
        /// </summary>
        [Display(Name = "支付时间")]
        public string PayTime { get; set; }

        /// <summary>
        ///     支付类型
        /// </summary>
        [Display(Name = "支付类型")]
        public string PaymentType { get; set; }

        /// <summary>
        ///     商品列表
        /// </summary>
        public List<MerchantCartViewModel> Products { get; set; }

        /// <summary>
        ///     Buy user
        /// </summary>
        public UserOutput BuyUser { get; set; }

        /// <summary>
        ///     Page list
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public PageResult<AutoListItem> PageList(object query, AutoBaseModel autoModel)
        {
            //Get orders
            var orders = PageTable(query, autoModel);
            var pageList = PagedList<MerchantOrderList>.Create(orders.Result, orders.RecordCount, orders.PageSize,
                orders.PageIndex);
            //Builder
            var list = new List<AutoListItem>();
            foreach (var item in pageList.Result)
            {
                var intro = new StringBuilder();
                if (item.Products?.Count > 0)
                    item.Products.ForEach(product =>
                    {
                        intro.AppendLine($"{product.ProductName}-{product.SkuName}x{product.Count}");
                    });
                var apiData = new AutoListItem
                {
                    Id = item.Id,
                    Image = item.ThumbnailUrl,
                    Title = $"{item.MerchantStoreName}-{item.MerchantOrderStatus.GetDisplayName()}",
                    Intro = $"共{item.TotalCount}件商品," + intro,
                    Value = $"实付{item.PaymentAmount}元"
                    //  Url = $"/pages/user?path=Asset_recharge_view&id={item.Id}"
                };
                list.Add(apiData);
            }

            return ToPageList(list, pageList);
        }

        public Type SearchType()
        {
            throw new NotImplementedException();
        }

        public List<TableAction> Actions()
        {
            var list = new List<TableAction>
            {
                ToLinkAction("订单详情", "Edit")
            };
            return list;
        }

        /// <summary>
        ///     Page table
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public PageResult<MerchantOrderList> PageTable(object query, AutoBaseModel autoModel)
        {
            var tenant = TenantContext.CurrentTenant;
            var dic = HttpWeb.HttpContext.ToDictionary();
            dic = dic.RemoveKey("type");
            //express
            var model = ToQuery<MerchantOrderList>();
            var expressionQuery = new ExpressionQuery<MerchantOrder>();
            if (model.MerchantOrderStatus > 0) expressionQuery.And(e => e.OrderStatus == model.MerchantOrderStatus);
            //all stores
            var merchantStores = Resolve<IMerchantStoreService>().GetList().ToList();

            //query
            var result = new List<MerchantOrderList>();
            var apiService = Resolve<IApiService>();
            var orders = Resolve<IMerchantOrderService>()
                .GetPagedList<MerchantOrder>(dic.ToJson(), expressionQuery.BuildExpression());
            orders.ForEach(item =>
            {
                var store = merchantStores.Find(s => s.Id == item.MerchantStoreId.ToObjectId());
                if (store == null) return;
                var extension = item.Extension.DeserializeJson<MerchantOrderExtension>();
                var temp = new MerchantOrderList();
                temp = AutoMapping.SetValue(item, temp);
                //user
                var user = Resolve<IUserService>().GetSingle(item.UserId);
                if (user != null) temp.BuyUser = AutoMapping.SetValue<UserOutput>(user);
                temp.MerchantOrderStatus = item.OrderStatus;
                temp.MerchantStoreName = store.Name;
                temp.ThumbnailUrl = apiService.ApiImageUrl(store.Logo);
                temp.Products = extension?.MerchantProducts;
                temp.Products?.ForEach(product =>
                {
                    product.ThumbnailUrl = apiService.ApiImageUrl(product.ThumbnailUrl);
                });
                result.Add(temp);
            });

            var pageList =
                PagedList<MerchantOrderList>.Create(result, orders.RecordCount, orders.PageSize, orders.PageIndex);

            return ToPageResult(pageList);
        }
    }
}