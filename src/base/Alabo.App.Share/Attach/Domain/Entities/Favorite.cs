using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.App.Share.Attach.Domain.Enums;
using Alabo.App.Share.Attach.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.UI;
using Alabo.UI.AutoTables;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Share.Attach.Domain.Entities {

    /// <summary>
    ///     通用收藏
    /// </summary>
    [BsonIgnoreExtraElements]
    [Table("Attach_Favorite")]
    [AutoDelete(IsAuto = true)]
    [ClassProperty(Name = "收藏", SideBarType = SideBarType.FullScreen, PostApi = "Api/Favorite/List", ListApi = "Api/Favorite/List")]
    public class Favorite : AggregateMongodbUserRoot<Favorite>, IAutoTable<Favorite> {

        public List<TableAction> Actions() {
            var list = new List<TableAction>
            {
                //ToLinkAction("编辑", "Edit",TableActionType.ColumnAction),//管理员编辑
                ToLinkAction("删除", "/Api/Favorite/QueryDelete",ActionLinkType.Delete,TableActionType.ColumnAction)//管理员删除
            };
            return list;
        }

        public PageResult<Favorite> PageTable(object query, AutoBaseModel autoModel) {
            var model = Resolve<IFavoriteService>().GetPagedList(query);
            return ToPageResult(model);
        }

        /// <summary>
        ///     收藏类型
        ///     比如订单、商品、文章等评论
        /// </summary>
        [Display(Name = "收藏类型")]
        public FavoriteType Type { get; set; }

        /// <summary>
        ///     对应的实体Id
        /// </summary>
        [Display(Name = "对应的实体Id")]
        public string EntityId { get; set; }

        /// <summary>
        /// 收藏名称
        /// </summary>
        [Display(Name = "商品")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, IsShowBaseSerach = true, SortOrder = 2, IsShowAdvancedSerach = true)]
        public string Name { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        [Display(Name = "图片")]
        [Field(ControlsType = ControlsType.AlbumUploder, ListShow = true, SortOrder = 1, IsImagePreview = true)]
        public string Image { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        public string Url { get; set; }
    }
}