using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Helpers;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;
using Alabo.Web.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Cloud.Shop.ShopSaleReport.Dtos
{
    /// <summary>
    ///     直推会员等级报表
    /// </summary>
    [ClassProperty(Name = "会员等级报表", Icon = "fa fa-puzzle-piece", Description = "会员等级报表")]
    public class ViewPriceStyleSale : BaseViewModel
    {
        [Field(ListShow = true)] [Key] public long Id { get; set; }

        /// <summary>
        ///     用户Id
        /// </summary>
        [Display(Name = "用户名称")]
        public long UserId { get; set; }

        /// <summary>
        ///     用户名
        /// </summary>
        [Display(Name = "用户名")]
        [HelpBlock("请输入合伙人所属用户名")]
        [Field(IsShowBaseSerach = true, PlaceHolder = "请输入用户名", DataField = "UserId",
            ValidType = ValidType.UserName, IsMain = true, ControlsType = ControlsType.TextBox, GroupTabId = 1,
            Width = "120", ListShow = true, EditShow = true, SortOrder = 2)]
        public string UserName { get; set; }

        /// <summary>
        ///     累计订单数量
        /// </summary>
        [Display(Name = "订单总数")]
        [Field(LabelColor = LabelColor.Default, Width = "100", ListShow = true, SortOrder = 3)]
        public long OrderCount { get; set; }

        /// <summary>
        ///     累计购买商品数量
        ///     与订单表Count对应
        /// </summary>
        [Display(Name = "已购商品总数")]
        [Field(LabelColor = LabelColor.Default, Width = "100", ListShow = true, SortOrder = 4)]
        public long ProductCount { get; set; }

        /// <summary>
        ///     商品销售价
        ///     累计商品销售价格
        ///     与订单表Amount对应
        /// </summary>
        [Display(Name = "订单总金额")]
        [Field(LabelColor = LabelColor.Default, Width = "100", ListShow = true, SortOrder = 5)]
        public decimal PriceAmount { get; set; }

        /// <summary>
        ///     累计分润价格
        /// </summary>
        [Display(Name = "分润总金额")]
        [Field(LabelColor = LabelColor.Default, Width = "100", ListShow = true, SortOrder = 6)]
        public decimal FenRunAmount { get; set; }

        /// <summary>
        ///     订单实际支付的金额
        /// </summary>
        [Display(Name = "现金总支付")]
        [Field(LabelColor = LabelColor.Default, Width = "100", ListShow = true, SortOrder = 7)]
        public decimal PaymentAmount { get; set; }

        /// <summary>
        ///     总服务费
        /// </summary>
        [Display(Name = "总服务费")]
        [Field(LabelColor = LabelColor.Default, Width = "100", ListShow = true, SortOrder = 8)]
        public decimal FeeAmount { get; set; }

        /// <summary>
        ///     最后更新时间
        /// </summary>
        [Display(Name = "更新时间")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, EditShow = false, SortOrder = 102, Width = "150")]
        public DateTime ModifiedTime { get; set; }

        /// <summary>
        ///     获取链接
        /// </summary>
        public IEnumerable<ViewLink> ViewLinks()
        {
            var quickLinks = new List<ViewLink>
            {
                //    new ViewLink("数据更新","/Admin/Basic/Run?Service=IOrderAdminService&Method=UpdateUserTeamGrade&query=[[Id]]",Icons.Settings,LinkType.ColumnLink),
                new ViewLink("Ta的推荐", "/Admin/User/ParentUser?UserId=[[Id]]", Icons.Run, LinkType.ColumnLink),
                new ViewLink("会员详情", "/Admin/User/Edit?Id=[[Id]]", Icons.List, LinkType.ColumnLink),
                new ViewLink("财务详情", "/Admin/Account/Edit?Id=[[Id]]", Icons.Coins, LinkType.ColumnLink)
            };

            var priceStyleConfinJson = Ioc.Resolve<IAutoConfigService>()
                .GetList("Alabo.App.Shop.Product.Domain.CallBacks.PriceStyleConfig");
            var i = 1;
            foreach (var item in priceStyleConfinJson)
            {
                var name = item.ToStr().SubstringBetween("\"Name\": \"", "\",");
                var id = item.ToStr().SubstringBetween("\"Id\": \"", "\",");
                long planfromId = 997100 + i++;
                quickLinks.Add(new ViewLink(name + "<span  class='m-badge  m-badge--danger '>会员</span>销售表",
                    $"/Admin/Basic/List?Service=IOrderAdminService&Method=GetViewPriceStyleSalePagedList&type=user&PlatformId={planfromId}&priceStyle={id}",
                    "flaticon-map", LinkType.TableQuickLink));
                planfromId++;
                //quickLinks.Add(new ViewLink(name + "<span  class='m-badge  m-badge--info '>直推</span>销售表", $"/Admin/Basic/List?Service=IOrderAdminService&Method=GetViewPriceStyleSalePagedList&type=recomend&PlatformId={planfromId}&priceStyle={id}", "flaticon-map", LinkType.TableQuickLink));
                planfromId++;
                //quickLinks.Add(new ViewLink(name + "<span  class='m-badge  m-badge--sucess'>间推</span>销售表", $"/Admin/Basic/List?Service=IOrderAdminService&Method=GetViewPriceStyleSalePagedList&type=second&PlatformId={planfromId}&priceStyle={id}", "flaticon-map", LinkType.TableQuickLink));
                planfromId++;
                quickLinks.Add(new ViewLink(name + "<span  class='m-badge  m-badge--primary '>团队</span>销售表",
                    $"/Admin/Basic/List?Service=IOrderAdminService&Method=GetViewPriceStyleSalePagedList&type=team&PlatformId={planfromId}&priceStyle={id}",
                    "flaticon-map", LinkType.TableQuickLink));
            }

            return quickLinks;
        }
    }
}