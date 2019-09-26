using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.Core.Enums.Enum;
using Alabo.Core.WebUis;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Repositories.Mongo.Extension;
using Alabo.UI;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.Framework.Basic.Relations.Domain.Entities {

    /// <summary>
    ///     地区
    ///     这个对象中的值生成后不应该修改
    /// </summary>
    [ClassProperty(Name = "国家区域", Icon = IconFlaticon.map_location, SideBarType = SideBarType.ControlSideBar, PageType = ViewPageType.List, PostApi = "Api/Region/RegionList")]
    [BsonIgnoreExtraElements]
    [Table("Basic_Region")]
    public class Region : AggregateMongodbRoot<Region> {

        public Region() : base(ObjectId.Empty) {
        }

        public Region(long regionId, long parentId) {
            RegionId = regionId;
            ParentId = parentId;
            // this.Id = (regionId.ToString() + parentId.ToString()).ConvertToObjectId();
        }

        /// <summary>
        ///     区域Id
        ///     区域编码
        ///     唯一
        /// </summary>
        [MongoIndex(Unique = true)]
        [Field(ListShow = true, SortOrder = 1, Width = "150", TableDispalyStyle = TableDispalyStyle.Code,
            ControlsType = ControlsType.TextBox, IsShowAdvancedSerach = true)]
        [Display(Name = "区域编码")]
        public long RegionId { get; set; }

        /// <summary>
        ///     地区名称
        /// </summary>
        [Field(ListShow = true, ControlsType = ControlsType.TextBox, SortOrder = 2, Width = "150", IsMain = true,
            Link = "/Admin/Region/List?ParentId=[[RegionId]]&Method=GetPagedList", IsShowBaseSerach = true)]
        [Display(Name = "地区名称")]
        public string Name { get; set; }

        /// <summary>
        ///     所属国家
        /// </summary>
        [Display(Name = "所属国家")]
        public Country Country { get; set; } = Country.China;

        /// <summary>
        ///     上级地区的Id，没有时等于0
        /// </summary>
        [Display(Name = "上级地区的Id，没有时等于0")]
        public long ParentId { get; set; }

        /// <summary>
        ///     城市Id
        /// </summary>
        public long CityId { get; set; }

        /// <summary>
        ///     省份Id
        /// </summary>
        public long ProvinceId { get; set; }

        /// <summary>
        ///     名字全称：比如:广东省东莞市南城区
        /// </summary>
        [Display(Name = "全称")]
        [Field(ListShow = true, SortOrder = 10, Width = "250", IsShowAdvancedSerach = true)]
        public string FullName { get; set; }

        /// <summary>
        ///     城市中心点
        /// </summary>
        public string Center { get; set; }

        /// <summary>
        ///     首字母
        /// </summary>
        [Display(Name = "首字母")]
        public string FirstLetter { get; set; }

        /// <summary>
        ///     获取或设置区域的级别
        /// </summary>
        [Display(Name = "类型")]
        [Field(ListShow = true, SortOrder = 20, Width = "100")]
        public RegionLevel Level { get; set; }
    }
}