using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.Tasks.Domain.Enums {

    /// <summary>
    ///     分润所依赖的关系图
    /// </summary>
    [ClassProperty(Name = "分润依赖关系图")]
    public enum RelationshipType {

        /// <summary>
        ///     未依赖
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "未依赖")]
        NoRelationship = 1,

        /// <summary>
        ///     会员推荐关系(组织架构图）
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "会员推荐关系(组织架构图）")]
        UserRecommendedRelationship = 2,

        /// <summary>
        ///     用户类型推荐关系(用户类型组织架构图）
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "用户类型推荐关系(用户类型组织架构图）")]
        UserTypeRelationship = 3,

        /// <summary>
        ///     客户责任关系(CRM客户图）
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "客户责任关系(CRM客户图）")]
        CRMRelationship = 4,

        /// <summary>
        ///     会员安置关系(安置关系图)
        ///     会员注册时候，手动安置会员之间的关系，常见的双轨，多轨之间的关系
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "会员安置关系(安置关系图)")]
        PlacementRelationship = 5,

        /// <summary>
        ///     商品推荐关系(商品推荐图)
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "商品推荐关系(商品推荐图)")]
        GoodsRelationship = 6,

        /// <summary>
        ///     订单推荐关系(订单推荐图)
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "订单推荐关系(订单推荐图)")]
        OrderRelationship = 7,

        /// <summary>
        ///     区域推荐关系(省市区/县关系)
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "区域推荐关系(省市区/县关系)")]
        RegionalRelationship = 8,

        /// <summary>
        ///     城市商圈关系
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "城市商圈关系")]
        BusinessCircle = 9,

        /// <summary>
        ///     公司组织架构(部门、员工关系)
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = " 公司组织架构(部门、员工关系)")]
        Department = 10,

        /// <summary>
        ///     门店图(组织架构图上的服务网点)
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = " 门店图(组织架构图上的服务网点)")]
        ServiceCenterRelationship = 101,

        /// <summary>
        ///     工单责任图(工单责任图)
        ///     根据工单表里头的对用的用户ID字段
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = " 工单责任图(工单责任图)")]
        WorkOrderRelationship = 102
    }
}