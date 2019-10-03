using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.Datas.Queries.Enums;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Framework.Core.Admins.Configs;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebUis;
using Alabo.Helpers;
using Alabo.Industry.Cms.Articles.Domain.Entities;
using Alabo.Industry.Cms.Articles.Domain.Services;
using Alabo.Mapping;
using Alabo.Maps;
using Alabo.UI;
using Alabo.UI.Design.AutoForms;
using Alabo.UI.Design.AutoTables;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson.Serialization.Attributes;

namespace Alabo.Industry.Cms.Articles.UI.AutoForm
{
    [ClassProperty(Name = "商家头条", Description = "商家头条")]
    public class ArticleAutoFrom : UIBase, IAutoForm, IAutoTable<ArticleAutoFrom>
    {
        public Alabo.UI.Design.AutoForms.AutoForm GetView(object id, AutoBaseModel autoModel)
        {
            var dic = autoModel.Query.ToObject<Dictionary<string, string>>();
            dic.TryGetValue("ChannelId", out var channelId);
            //var channel = Resolve<IChannelService>().GetSingle(r => r.Id == channelId.ToObjectId());
            //if (channel == null) {
            //    return null;
            //}
            //// 频道的分类 标签
            //var classType = Resolve<IChannelService>().GetChannelClassType(channel);
            //var tagType = Resolve<IChannelService>().GetChannelTagType(channel);

            var result = Ioc.Resolve<IArticleService>().GetSingle(id);
            var articleForm = new ArticleAutoFrom();
            if (result != null) {
                articleForm = result.MapTo<ArticleAutoFrom>();
            }

            articleForm.Id = id.ToString();
            articleForm.ChannelId = channelId; // channel.Id.ToString();

            var autoForm = ToAutoForm(articleForm);
            //autoForm.Groups.Foreach(r => {
            //    r.Items.Foreach(e => {
            //        //if (e.Field == "classes") {
            //        //    e.DataSource = $"Api/Relation/GetClassTree?Type={classType.Name}";
            //        //}

            //        if (e.Field == "tags") {
            //            e.DataSource = $"Api/Relation/GetTags?Type={tagType.Name}";
            //        }
            //    });
            //});

            autoForm.AlertText = "【头条编辑】针对于店铺商家头条信息更新";
            autoForm.ButtomHelpText = new List<string>
            {
                "缩略图：请您务必上传头条封面图片 支持jpg,jpeg,gif,png,等图片格式",
                "标题：头条新闻标题 简单 精简 扼要",
                "详细内容：主要是头条新闻的详细描述"
            };

            return autoForm;
        }

        public ServiceResult Save(object model, AutoBaseModel autoModel)
        {
            var input = (ArticleAutoFrom) model;
            var article = AutoMapping.SetValue<Article>(input);
            article.Id = input.Id.ToObjectId();
            article.ChannelId = input.ChannelId.ToObjectId();
            article.Tags = input.Tags;
            var entity = Resolve<IArticleAdminService>().GetSingle(article.Id);
            if (entity == null)
            {
                var addResult = Resolve<IArticleAdminService>().Add(article);
                if (addResult) {
                    return ServiceResult.Success;
                }

                return ServiceResult.Failed;
            }

            var result = Resolve<IArticleAdminService>().Update(article);
            if (result) {
                return ServiceResult.Success;
            }

            return ServiceResult.Failed;
        }

        public List<TableAction> Actions()
        {
            var list = new List<TableAction>
            {
                ToLinkAction("编辑", "Edit?ChannelId=e02220001110000000000009", TableActionType.ColumnAction),
                ToLinkAction("删除", "/Api/Article/Delete", ActionLinkType.Delete, TableActionType.ColumnAction)
            };
            return list;
        }

        public PageResult<ArticleAutoFrom> PageTable(object query, AutoBaseModel autoModel)
        {
            var list = Ioc.Resolve<IArticleService>().GetPagedList(query);
            var plList = list.MapTo<PagedList<ArticleAutoFrom>>();
            return ToPageResult(plList);
        }

        #region

        /// <summary>
        ///     id
        /// </summary>
        [Field(ControlsType = ControlsType.Hidden, GroupTabId = 1, ListShow = true, SortOrder = 1, EditShow = true)]
        public string Id { get; set; }

        /// <summary>
        ///     图片地址
        /// </summary>
        /// <value>The image URL.</value>
        [Display(Name = "缩略图")]
        [Field(ControlsType = ControlsType.AlbumUploder, GroupTabId = 1, ListShow = true, SortOrder = 1,
            EditShow = true)]
        [HelpBlock("请您务必上传头条封面图片 支持jpg,jpeg,gif,png,等图片格式")]
        public string ImageUrl { get; set; }

        /// <summary>
        ///     级联Id
        /// </summary>
        [Display(Name = "级联Id")]
        public long RelationId { get; set; }

        /// <summary>
        ///     频道ID
        /// </summary>
        /// <value>The channel identifier.</value>
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.Hidden)]
        [Display(Name = "频道ID")]
        public string ChannelId { get; set; }

        /// <summary>
        ///     标题
        /// </summary>
        /// <value>The title.</value>
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [StringLength(50, MinimumLength = 1, ErrorMessage = ErrorMessage.MaxStringLength)]
        [Display(Name = "标题")]
        [Field(ControlsType = ControlsType.TextBox, IsShowAdvancedSerach = true, IsShowBaseSerach = true, IsMain = true,
            Operator = Operator.Contains,
            ListShow = true, GroupTabId = 1, SortOrder = 1)]
        [HelpBlock("请您务必输入头条标题内容 标题最多100字符")]
        public string Title { get; set; }

        /// <summary>
        ///     副标题
        /// </summary>
        /// <value>The sub title.</value>
        [Display(Name = "副标题")]
        [Field(ControlsType = ControlsType.TextBox, IsShowAdvancedSerach = false, EditShow = false,
            Operator = Operator.Contains,
            ListShow = false, GroupTabId = 1, SortOrder = 2)]
        [HelpBlock("输入头条的副标题，长度不要超过100个字符")]
        public string SubTitle { get; set; }

        /// <summary>
        ///     简介
        /// </summary>
        /// <value>The intro.</value>
        [Display(Name = "简介")]
        [Field(ControlsType = ControlsType.TextArea, ListShow = false, EditShow = false)]
        [HelpBlock("输入头条的内容简介，建议在255个字符以内")]
        public string Intro { get; set; }

        /// <summary>
        ///     详细内容
        /// </summary>
        /// <value>The content.</value>
        [Display(Name = "详细内容")]
        [Field(ControlsType = ControlsType.Editor, ListShow = false, EditShow = true)]
        [HelpBlock("请您认真填写详细内容")]
        public string Content { get; set; }

        /// <summary>
        ///     CMS的附加内容，根据频道返回JSON格式的数据
        /// </summary>
        /// <value>The attach value.</value>
        [Display(Name = "附加内容")]
        public string AttachValue { get; set; }

        /// <summary>
        ///     来源
        /// </summary>
        /// <value>The source.</value>
        [Display(Name = "来源")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = false, EditShow = false)]
        [HelpBlock("请输入头条来源")]
        public string Source { get; set; }

        /// <summary>
        ///     作者
        /// </summary>
        /// <value>The author.</value>
        [Display(Name = "作者")]
        [Field(ControlsType = ControlsType.TextBox, IsShowAdvancedSerach = false, IsShowBaseSerach = false,
            ListShow = false, EditShow = false, SortOrder = 3)]
        [HelpBlock("请输入头条作者")]
        public string Author { get; set; }

        /// <summary>
        ///     外部链接
        /// </summary>
        /// <value>The link URL.</value>
        [Display(Name = "外部链接")]
        public string LinkUrl { get; set; }

        /// <summary>
        ///     浏览次数
        /// </summary>
        /// <value>The view count.</value>
        [Display(Name = "浏览次数")]
        [Field(ControlsType = ControlsType.Numberic, ListShow = false, EditShow = false, SortOrder = 4)]
        [HelpBlock("头条浏览次数必须大于等于0")]
        public int ViewCount { get; set; }

        /// <summary>
        ///     文章跳转、置顶、热门等功能
        /// </summary>
        /// <value>The state of the article.</value>
        [Display(Name = "属性")]
        public string ArticleState { get; set; }

        /// <summary>
        ///     SEO标题
        /// </summary>
        [Display(Name = "SEO标题")]
        [StringLength(200, ErrorMessage = "Seo标题长度不能超过200个字符")]
        //[Field(ListShow = false, EditShow = true, ControlsType = ControlsType.TextBox)]
        public string MetaTitle { get; set; }

        /// <summary>
        ///     SEO关键字
        /// </summary>
        [Display(Name = "SEO关键字")]
        [StringLength(300, ErrorMessage = "SEO关键字长度不能超过300个字符")]
        //[Field(ListShow = false, EditShow = true, ControlsType = ControlsType.TextBox)]
        public string MetaKeywords { get; set; }

        /// <summary>
        ///     SEO描述
        /// </summary>
        [Display(Name = "SEO描述")]
        [StringLength(400, ErrorMessage = "SEO描述长度不能超过400个字符")]
        //[Field(ListShow = false, EditShow = true, ControlsType = ControlsType.TextArea)]
        public string MetaDescription { get; set; }

        /// <summary>
        ///     状态
        /// </summary>
        [Display(Name = "状态")]
        [Field(ControlsType = ControlsType.RadioButton, DataSource = "Alabo.Domains.Enums.Status",
            EditShow = true, ListShow = true, SortOrder = 5)]
        [HelpBlock("正常：用户可正常浏览等操作。冻结和删除不能进行浏览。该删除是软删除，不是真正的删除")]
        public Status Status { get; set; } = Status.Normal;

        /// <summary>
        ///     字段列表
        /// </summary>
        /// <value>The data fields.</value>
        [BsonIgnore]
        [Display(Name = "字段列表")]
        public List<DataField> DataFields { get; set; }

        ///// <summary>
        /////     分类
        ///// </summary>
        ///// <value>The classes.</value>
        //[BsonIgnore]
        //[Display(Name = "分类")]
        //[Field(ControlsType = ControlsType.RelationClass,
        //    EditShow = true, Operator = Operator.Contains,
        //    GroupTabId = 1, SortOrder = 2)]
        //[HelpBlock("一条记录可以设置多个分类")]
        //public string Classes { get; set; }

        /// <summary>
        ///     标签
        /// </summary>
        /// <value>The tags.</value>
        [BsonIgnore]
        [Display(Name = "标签")]
        [Field(ControlsType = ControlsType.DropdownList, EditShow = false, Operator = Operator.Contains, GroupTabId = 1,
            SortOrder = 2)]
        [HelpBlock("一条记录可以设置多个标签")]
        public string Tags { get; set; }

        #endregion
    }
}