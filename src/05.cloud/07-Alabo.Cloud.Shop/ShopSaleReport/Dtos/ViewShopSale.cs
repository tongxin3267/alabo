using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;
using Alabo.Web.Validations;

namespace Alabo.Cloud.Shop.ShopSaleReport.Dtos
{
    /// <summary>
    ///     直推会员等级报表
    /// </summary>
    [ClassProperty(Name = "销售统计表", Icon = "fa fa-puzzle-piece", Description = "会员等级报表",
        SideBarType = SideBarType.FullScreen,
        GroupName = "基本信息,高级选项")]
    public class ViewShopSale : BaseViewModel
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
        public long TotalOrderCount { get; set; }

        /// <summary>
        ///     累计购买商品数量
        ///     与订单表Count对应
        /// </summary>
        [Display(Name = "已购商品总数")]
        [Field(LabelColor = LabelColor.Default, Width = "100", ListShow = true, SortOrder = 4)]
        public long TotalProductCount { get; set; }

        /// <summary>
        ///     商品销售价
        ///     累计商品销售价格
        ///     与订单表Amount对应
        /// </summary>
        [Display(Name = "订单总金额")]
        [Field(LabelColor = LabelColor.Default, Width = "100", ListShow = true, SortOrder = 5)]
        public decimal TotalPriceAmount { get; set; }

        /// <summary>
        ///     累计分润价格
        /// </summary>
        [Display(Name = "总分润金额")]
        [Field(LabelColor = LabelColor.Default, Width = "100", ListShow = true, SortOrder = 6)]
        public decimal TotalFenRunAmount { get; set; }

        /// <summary>
        ///     订单实际支付的金额
        /// </summary>
        [Display(Name = "现金总支付")]
        [Field(LabelColor = LabelColor.Default, Width = "100", ListShow = true, SortOrder = 7)]
        public decimal TotalPaymentAmount { get; set; }

        /// <summary>
        ///     总服务费
        /// </summary>
        [Display(Name = "总服务费")]
        [Field(LabelColor = LabelColor.Default, Width = "100", ListShow = true, SortOrder = 8)]
        public decimal TotalFeeAmount { get; set; }

        /// <summary>
        ///     总邮费
        /// </summary>
        [Display(Name = "总邮费")]
        [Field(LabelColor = LabelColor.Default, Width = "100", ListShow = true, SortOrder = 8)]
        public decimal TotalExpressAmount { get; set; }

        ///// <summary>
        ///// 推荐等级信息
        ///// </summary>
        [Display(Name = "等级信息")]
        [Field(Width = "500", ListShow = true, DataSource = "PriceStyleConfig", SortOrder = 9)]
        public Dictionary<Guid, string> PriceStyleSales { get; set; }

        ///// <summary>
        ///// 推荐等级信息
        ///// </summary>
        //[Display(Name = "等级信息")]
        //[Field("等级信息", ValidType = ValidType.UserName, ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "400", ListShow = false, SortOrder = 5)]
        //public string GradeInfoString { get; set; }

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
                new ViewLink("会员销售统计表",
                    "/Admin/Basic/List?Service=IOrderAdminService&Method=GetShopSalePagedList&type=user&PlatformId=997090",
                    Icons.List, LinkType.TableQuickLink),
                new ViewLink("直推会员销售统计表",
                    "/Admin/Basic/List?Service=IOrderAdminService&Method=GetShopSalePagedList&type=recomend&PlatformId=997091",
                    Icons.List, LinkType.TableQuickLink),
                new ViewLink("间推会员销售统计表",
                    "/Admin/Basic/List?Service=IOrderAdminService&Method=GetShopSalePagedList&type=second&PlatformId=997092",
                    Icons.List, LinkType.TableQuickLink),
                new ViewLink("团队销售统计表",
                    "/Admin/Basic/List?Service=IOrderAdminService&Method=GetShopSalePagedList&type=team&PlatformId=9970903",
                    Icons.List, LinkType.TableQuickLink),
                //    new ViewLink("数据更新","/Admin/Basic/Run?Service=IOrderAdminService&Method=UpdateUserTeamGrade&query=[[Id]]",Icons.Settings,LinkType.ColumnLink),
                new ViewLink("Ta的推荐", "/Admin/User/ParentUser?UserId=[[Id]]", Icons.Run, LinkType.ColumnLink),
                new ViewLink("会员详情", "/Admin/User/Edit?Id=[[Id]]", Icons.List, LinkType.ColumnLink),
                new ViewLink("财务详情", "/Admin/Account/Edit?Id=[[Id]]", Icons.Coins, LinkType.ColumnLink)
            };

            return quickLinks;
        }
    }
}