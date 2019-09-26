using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebUis;
using Alabo.Framework.Core.WebUis.Design.AutoForms;
using Alabo.Framework.Core.WebUis.Design.AutoTables;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.Maps;
using Alabo.UI;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Cms.Articles.Domain.Dto
{

    /// <summary>
    ///     Class ArticleInput.
    /// </summary>
    [ClassProperty(Name = "头条标签管理", Description = "头条标签管理")]
    public class TopLineTagInput : UIBase, IAutoForm, IAutoTable<TopLineTagInput>
    {
        /// <summary>
        /// key 标签健
        /// </summary>
        [BsonIgnore]
        [Display(Name = "key", Description = "")]
        [Field(ControlsType = ControlsType.Hidden, GroupTabId = 1, ListShow = true, SortOrder = 1, EditShow = true)]
        public long Key { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "值", Description = "值")]
        [Field(ControlsType = ControlsType.Hidden, GroupTabId = 1, ListShow = true, SortOrder = 1, EditShow = true)]
        public string Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "排序", Description = "排序")]
        [Field(ControlsType = ControlsType.Hidden, GroupTabId = 1, ListShow = true, SortOrder = 1, EditShow = true)]
        public int SortOrder { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "标签图标", Description = "")]
        [Field(ControlsType = ControlsType.Hidden, GroupTabId = 1, ListShow = true, SortOrder = 1, EditShow = true)]
        public string Icon { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "标签名称", Description = "")]
        [Field(ControlsType = ControlsType.Hidden, GroupTabId = 1, ListShow = true, SortOrder = 1, EditShow = true)]
        public string Name { get; set; }



        /// <summary>
        /// 页面功能方法
        /// </summary>
        /// <returns></returns>
        public List<TableAction> Actions()
        {
            var list = new List<TableAction>
            {
                ToLinkAction("编辑", "/Api/Relation/Save",TableActionType.ColumnAction),
                ToLinkAction("删除", "/Api/Relation/Delete",ActionLinkType.Delete,TableActionType.ColumnAction)
            };
            return list;
        }

        /// <summary>
        /// 获取单个详情
        /// </summary>
        /// <param name="id"></param>
        /// <param name="autoModel"></param>
        /// <returns></returns>
        public AutoForm GetView(object id, AutoBaseModel autoModel)
        {
            var result = Ioc.Resolve<IRelationService>().GetSingle(id);
            var articleForm = new TopLineTagInput();
            if (result != null)
            {
                articleForm = result.MapTo<TopLineTagInput>();
            }

            var autoForm = ToAutoForm(new TopLineTagInput());
            return autoForm;
        }

        public ServiceResult Save(object model, AutoBaseModel autoModel)
        {
            return ServiceResult.Failed;
        }

        public PageResult<TopLineTagInput> PageTable(object query, AutoBaseModel autoModel)
        {
            //var type = "ChannelTopLineTagRelation";
            //var result = Resolve<IRelationService>().GetKeyValues(type).ToList();
            //result.ForEach(u =>
            //{
            //    u.Key = u.Value;
            //    u.Value = u.Name;
            //});
            //var plList = result.MapTo<PagedList<TopLineTagInput>>();


           var list = Ioc.Resolve<IRelationService>().GetPagedList(query);
            var plList = list.MapTo<PagedList<TopLineTagInput>>();
            return ToPageResult(plList);
        }
    }
}