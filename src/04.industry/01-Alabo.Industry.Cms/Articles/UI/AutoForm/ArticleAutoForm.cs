using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Framework.Basic.Relations.Domain.Services;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebUis;
using Alabo.Framework.Core.WebUis.Design.AutoForms;
using Alabo.Industry.Cms.Articles.Domain.CallBacks;
using Alabo.Industry.Cms.Articles.Domain.Entities;
using Alabo.Industry.Cms.Articles.Domain.Services;
using Alabo.Mapping;
using Alabo.Maps;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson.Serialization.Attributes;

namespace Alabo.Industry.Cms.Articles.UI.AutoForm
{
    [ClassProperty(Name = "文章", GroupName = "基本设置,详细内容,搜索引擎优化,其他选项", Icon = "fa fa-puzzle-piece", SideBarType = SideBarType.ArticleSideBarSideBar)]
    public class ArticleAutoForm : UIBase, IAutoForm
    {
        #region
        /// <summary>
        /// id
        /// </summary>
        [Field(ControlsType = ControlsType.Hidden, GroupTabId = 1, ListShow = true, SortOrder = 1, EditShow = true)]
        public string Id { get; set; }

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
            ListShow = true, SortOrder = 2, GroupTabId = 1)]
        public string Title { get; set; }

        /// <summary>
        ///     副标题
        /// </summary>
        /// <value>The sub title.</value>
        [Display(Name = "副标题")]
        [Field(ControlsType = ControlsType.TextBox, IsShowAdvancedSerach = true, IsShowBaseSerach = true,
            ListShow = true, SortOrder = 2, GroupTabId = 4)]
        public string SubTitle { get; set; }

        /// <summary>
        ///     简介
        /// </summary>
        /// <value>The intro.</value>
        [Display(Name = "简介")]
        [Field(ControlsType = ControlsType.TextArea, IsShowAdvancedSerach = false, IsShowBaseSerach = false,
            ListShow = true, SortOrder = 2, GroupTabId = 4)]
        public string Intro { get; set; }

        /// <summary>
        ///     详细内容
        /// </summary>
        /// <value>The content.</value>
        [Display(Name = "详细内容")]
        [Field(ControlsType = ControlsType.Editor, IsShowAdvancedSerach = false, IsShowBaseSerach = false, GroupTabId = 2,
            ListShow = false, SortOrder = 2)]
        public string Content { get; set; }

        /// <summary>
        ///     CMS的附加内容，根据频道返回JSON格式的数据
        /// </summary>
        /// <value>The attach value.</value>
        [Display(Name = "附加内容")]
        [Field(ControlsType = ControlsType.TextBox, IsShowAdvancedSerach = false, IsShowBaseSerach = false,
            ListShow = false, SortOrder = 2, GroupTabId = 4)]
        public string AttachValue { get; set; }

        /// <summary>
        ///     外部链接
        /// </summary>
        /// <value>The link URL.</value>
        [Display(Name = "外部链接")]
        public string LinkUrl { get; set; }

        /// <summary>
        ///     图片地址
        /// </summary>
        /// <value>The image URL.</value>
        [Display(Name = "缩略图")]
        [Field(ControlsType = ControlsType.ImagePreview, IsShowAdvancedSerach = false, IsShowBaseSerach = false,
            ListShow = true, SortOrder = 1, GroupTabId = 1)]
        public string ImageUrl { get; set; }

        /// <summary>
        ///     浏览次数
        /// </summary>
        /// <value>The view count.</value>
        [Display(Name = "浏览次数")]
        [Field(ControlsType = ControlsType.NumberRang, IsShowAdvancedSerach = false, IsShowBaseSerach = false,
            ListShow = true, EditShow = false, SortOrder = 4)]
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
        [Field(ListShow = false, EditShow = true, ControlsType = ControlsType.TextBox, GroupTabId = 3)]
        public string MetaTitle { get; set; }

        /// <summary>
        ///     SEO关键字
        /// </summary>
        [Display(Name = "SEO关键字")]
        [StringLength(300, ErrorMessage = "SEO关键字长度不能超过300个字符")]
        [Field(ListShow = false, EditShow = true, ControlsType = ControlsType.TextBox, GroupTabId = 3)]
        public string MetaKeywords { get; set; }

        /// <summary>
        ///     SEO描述
        /// </summary>
        [Display(Name = "SEO描述")]
        [StringLength(400, ErrorMessage = "SEO描述长度不能超过400个字符")]
        [Field(ListShow = false, EditShow = true, ControlsType = ControlsType.TextArea, GroupTabId = 3)]
        public string MetaDescription { get; set; }

        /// <summary>
        ///     状态
        /// </summary>
        [Display(Name = "状态")]
        [Field(ControlsType = ControlsType.DropdownList, DataSource = " Alabo.Domains.Enums.Status", Width = "100",
            IsShowAdvancedSerach = true, IsShowBaseSerach = true, ListShow = true, SortOrder = 5)]
        public Status Status { get; set; } = Status.Normal;

        /// <summary>
        ///     分类
        /// </summary>
        /// <value>The classes.</value>
        [BsonIgnore]
        [Field(ControlsType = ControlsType.RelationClass, DataSourceType = typeof(ChannelArticleClassRelation), Width = "100", GroupTabId = 1,
            IsShowAdvancedSerach = true, IsShowBaseSerach = true, ListShow = true, SortOrder = 5)]
        [Display(Name = "分类")]
        public List<long> Classes { get; set; }

        /// <summary>
        ///     标签
        /// </summary>
        /// <value>The tags.</value>
        [BsonIgnore]
        [Display(Name = "标签")]
        [Field(ControlsType = ControlsType.RelationTags, DataSourceType = typeof(ChannelArticleTagRelation), Width = "100", GroupTabId = 1,
            IsShowAdvancedSerach = true, IsShowBaseSerach = true, ListShow = true, SortOrder = 5)]
        public List<long> Tags { get; set; }

        /// <summary>
        ///     来源
        /// </summary>
        /// <value>The source.</value>
        [Display(Name = "来源")]
        [Field(ControlsType = ControlsType.TextBox, IsShowAdvancedSerach = false, IsShowBaseSerach = false,
            ListShow = false, SortOrder = 2, GroupTabId = 4)]

        public string Source { get; set; }

        /// <summary>
        ///     作者
        /// </summary>
        /// <value>The author.</value>
        [Display(Name = "作者")]
        [Field(ControlsType = ControlsType.TextBox, IsShowAdvancedSerach = true, IsShowBaseSerach = true,
            ListShow = true, SortOrder = 3, GroupTabId = 4)]
        public string Author { get; set; }

        #endregion
        /// <summary>
        /// 获取view
        /// </summary>
        /// <param name="id"></param>
        /// <param name="autoModel"></param>
        /// <returns></returns>
        public Alabo.Framework.Core.WebUis.Design.AutoForms.AutoForm GetView(object id, AutoBaseModel autoModel)
        {
            var dic = autoModel.Query.ToObject<Dictionary<string, string>>();
            dic.TryGetValue("ChannelId", out var channelId);
            var result = Resolve<IArticleService>().GetSingle(id);
            var articleForm = new ArticleAutoForm();
            if (result != null)
            {
                articleForm = result.MapTo<ArticleAutoForm>();
            }

            articleForm.Id = id.ToString();
            if (channelId == null)
            {
                articleForm.ChannelId = result.ChannelId.ToString();
            }
            else
            {
                articleForm.ChannelId = channelId.ToString();
            }
            if (result != null)
            {
                var channel = Resolve<IChannelService>().GetSingle(r => r.Id == articleForm.ChannelId.ToObjectId());
                if (channel != null)
                {
                    var classType = Resolve<IChannelService>().GetChannelClassType(channel);
                    var tagType = Resolve<IChannelService>().GetChannelTagType(channel);

                    var classlist = Resolve<IRelationIndexService>().GetRelationIds(classType.FullName, result.RelationId); //文章分类
                    if (!string.IsNullOrEmpty(classlist))
                    {
                        var clas = classlist.ToSplitList(",");
                        if (clas.Count > 0)
                        {
                            clas.ForEach(p =>
                            {
                                articleForm.Classes.Add(Convert.ToInt16(p));
                            });
                        }
                    }

                    var relationList = Resolve<IRelationIndexService>().GetRelationIds(tagType.FullName, result.RelationId); //文章标签

                    if (!string.IsNullOrEmpty(relationList))
                    {
                        articleForm.Tags.Clear();
                        var tags = relationList.ToSplitList(",");
                        if (tags.Count > 0)
                        {
                            tags.ForEach(p =>
                            {
                                articleForm.Tags.Add(Convert.ToInt16(p));
                            });
                        }
                    }
                }
            }

            var autoForm = ToAutoForm(articleForm);

            return autoForm;
        }


        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <param name="autoModel"></param>
        /// <returns></returns>
        public ServiceResult Save(object model, AutoBaseModel autoModel)
        {
            if (model == null)
            {
                return ServiceResult.FailedMessage("保存的内容不能为空");
            }
            var aform = model.MapTo<ArticleAutoForm>();

            var input = model.MapTo<Article>();
            if (aform.Classes != null)
            {
                input.Classes = aform.Classes.Join("").ToString();
            }
            if (aform.Tags != null)
            {
                input.Tags = aform.Tags.Join("").ToString();
            }
            input.ChannelId = aform.ChannelId.ToObjectId();
           

            var article = AutoMapping.SetValue<Article>(input);
            var channel = Resolve<IChannelService>().GetSingle(r => r.Id == article.ChannelId);
            if (channel == null)
            {
                Tuple.Create(ServiceResult.FailedWithMessage("频道不存在"), new Article());
            }
            var SerResult = ServiceResult.Success;
            article.RelationId = GetMaxRelationId();
            article.Id = aform.Id.ToObjectId();
            article.Tags = input.Tags;
            var entity = Resolve<IArticleAdminService>().GetSingle(article.Id);
            var result = false;
            if (entity == null)
            {
                result = Resolve<IArticleAdminService>().Add(article);
                if (result)
                {
                    SerResult = ServiceResult.Success;
                }
                else
                {
                    SerResult = ServiceResult.Failed;
                }
            }
            else
            {
                result = Resolve<IArticleAdminService>().Update(article);

            }
            if (result)
            {
                if (!string.IsNullOrEmpty(input.Classes))
                {
                    // 添加标签和分类
                    var classType = Resolve<IChannelService>().GetChannelClassType(channel);
                    var classIds = input.Classes;
                    Resolve<IRelationIndexService>()
                          .AddUpdateOrDelete(classType.FullName, article.RelationId, classIds);
                }
                if (!string.IsNullOrEmpty(input.Tags))
                {
                    var tagType = Resolve<IChannelService>().GetChannelTagType(channel);
                    var tagIds = input.Tags;

                    Resolve<IRelationIndexService>()
                        .AddUpdateOrDelete(tagType.FullName, article.RelationId, tagIds);
                }

                DeleteCache();

                SerResult = ServiceResult.Success;
            }
            else
            {
                SerResult = ServiceResult.Failed;
            }

            return SerResult;
        }

        private void DeleteCache()
        {
            ObjectCache.Remove("GetHelpNav");
        }

        private long GetMaxRelationId()
        {
            var articles = Resolve<IRelationIndexService>().GetList();
            var article = articles.OrderByDescending(r => r.RelationId).FirstOrDefault();
            if (article != null)
            {
                return article.RelationId + 1;
            }
            else
            {
                return 1;
            }
        }
    }
}

