using Alabo.App.Core.Common;
using Alabo.Core.Enums.Enum;
using Alabo.Core.Reflections.Interfaces;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Offline.Product.Domain.CallBacks {

    /// <summary>
    /// 线下商品分类
    /// </summary>
    [ClassProperty(Name = "本店出品分类", Icon = "fa fa-question", Description = "本店出品分类", SortOrder = 40, PageType = ViewPageType.List, SideBarType = SideBarType.NewsSideBar)]
    [RelationProperty(RelationType = RelationType.ClassRelation, IsOnlyRoot = true)]
    public class MerchantProductClassRelation : IRelation {
    }
}