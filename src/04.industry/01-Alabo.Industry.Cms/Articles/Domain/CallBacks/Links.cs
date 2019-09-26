using Alabo.Domains.Enums;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Framework.Core.Reflections.Interfaces;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Industry.Cms.Articles.Domain.CallBacks {

    /// <summary>
    ///     控制面板中 头条分类
    /// </summary>
    [ClassProperty(Name = "头条分类", Icon = "fa fa-question", Description = "头条分类", SortOrder = 40,
        PageType = ViewPageType.List,
        SideBarType = SideBarType.NewsSideBar)]
    [RelationProperty(RelationType = RelationType.ClassRelation)]
    public class ChannelTopLineClassRelation : IRelation {
    }

    /// <summary>
    ///     控制面板中 头条标签
    /// </summary>
    [ClassProperty(Name = "头条标签", Icon = "fa fa-question-circle", Description = "头条标签", SortOrder = 41,
        PageType = ViewPageType.List, SideBarType = SideBarType.NewsSideBar)]
    [RelationProperty(RelationType = RelationType.TagRelation)]
    public class ChannelTopLineTagRelation : IRelation {
    }

    /// <summary>
    ///     控制面板中 文章分类
    /// </summary>
    [ClassProperty(Name = "文章分类", Icon = "fa fa-archive", Description = "文章分类", SortOrder = 42,
        PageType = ViewPageType.List,
        SideBarType = SideBarType.ArticleSideBarSideBar)]
    [RelationProperty(RelationType = RelationType.ClassRelation)]
    public class ChannelArticleClassRelation : IRelation {
    }

    /// <summary>
    ///     控制面板中 文章标签
    /// </summary>
    [ClassProperty(Name = "文章标签", Icon = "fa fa-book", Description = "文章标签", SortOrder = 43,
        PageType = ViewPageType.List,
        SideBarType = SideBarType.ArticleSideBarSideBar)]
    [RelationProperty(RelationType = RelationType.TagRelation)]
    public class ChannelArticleTagRelation : IRelation {
    }

    /// <summary>
    ///     控制面板中 帮助分类
    /// </summary>
    [ClassProperty(Name = "帮助分类", Icon = "fa fa-heart", Description = "帮助分类", SortOrder = 44,
        PageType = ViewPageType.List,
        SideBarType = SideBarType.HelpSideBar)]
    [RelationProperty(RelationType = RelationType.ClassRelation)]
    public class ChannelHelpClassRelation : IRelation {
    }

    /// <summary>
    ///     控制面板中 帮助标签
    /// </summary>
    [ClassProperty(Name = "帮助标签", Icon = "fa fa-heart-o", Description = "帮助标签", SortOrder = 45,
        PageType = ViewPageType.List,
        SideBarType = SideBarType.HelpSideBar)]
    [RelationProperty(RelationType = RelationType.TagRelation)]
    public class ChannelHelpTagRelation : IRelation {
    }

    /// <summary>
    ///     控制面板中 评论分类
    /// </summary>
    [ClassProperty(Name = "评论分类", Icon = "fa fa-comments", Description = "评论分类", SortOrder = 46,
        PageType = ViewPageType.List,
        SideBarType = SideBarType.CommentSideBar)]
    [RelationProperty(RelationType = RelationType.ClassRelation)]
    public class ChannelCommentClassRelation : IRelation {
    }

    /// <summary>
    ///     控制面板中 评论标签
    /// </summary>
    [ClassProperty(Name = "评论标签", Icon = "fa fa-comments-o", Description = "评论标签", SortOrder = 47,
        PageType = ViewPageType.List, SideBarType = SideBarType.CommentSideBar)]
    [RelationProperty(RelationType = RelationType.TagRelation)]
    public class ChannelCommentTagRelation : IRelation {
    }

    /// <summary>
    ///     控制面板中 图片分类
    /// </summary>
    [ClassProperty(Name = "图片分类", Icon = "fa fa-file-image-o", Description = "图片分类", SortOrder = 48,
        PageType = ViewPageType.List, SideBarType = SideBarType.ImagesSideBar)]
    [RelationProperty(RelationType = RelationType.ClassRelation)]
    public class ChannelImagesClassRelation : IRelation {
    }

    /// <summary>
    ///     控制面板中 图片标签
    /// </summary>
    [ClassProperty(Name = "图片标签", Icon = "fa fa-picture-o", Description = "图片标签", SortOrder = 49,
        PageType = ViewPageType.List,
        SideBarType = SideBarType.ImagesSideBar)]
    [RelationProperty(RelationType = RelationType.TagRelation)]
    public class ChannelImagesTagRelation : IRelation {
    }

    /// <summary>
    ///     控制面板中 视频分类
    /// </summary>
    [ClassProperty(Name = "视频分类", Icon = "fa fa-video-camera", Description = "视频分类", SortOrder = 50,
        PageType = ViewPageType.List, SideBarType = SideBarType.VideoSideBar)]
    [RelationProperty(RelationType = RelationType.ClassRelation)]
    public class ChannelVideoClassRelation : IRelation {
    }

    /// <summary>
    ///     控制面板中 视频标签
    /// </summary>
    [ClassProperty(Name = "视频标签", Icon = "fa fa-file-video-o", Description = "视频标签", SortOrder = 51,
        PageType = ViewPageType.List, SideBarType = SideBarType.VideoSideBar)]
    [RelationProperty(RelationType = RelationType.TagRelation)]
    public class ChannelVideoTagRelation : IRelation {
    }

    /// <summary>
    ///     控制面板中 下载分类
    /// </summary>
    [ClassProperty(Name = "下载分类", Icon = "fa fa-download", Description = "下载分类", SortOrder = 52,
        PageType = ViewPageType.List,
        SideBarType = SideBarType.DownSideBar)]
    [RelationProperty(RelationType = RelationType.ClassRelation)]
    public class ChannelDownClassRelation : IRelation {
    }

    /// <summary>
    ///     控制面板中 下载标签
    /// </summary>
    [ClassProperty(Name = "下载标签", Icon = "fa fa-cloud-download", Description = "下载标签", SortOrder = 53,
        PageType = ViewPageType.List, SideBarType = SideBarType.DownSideBar)]
    [RelationProperty(RelationType = RelationType.TagRelation)]
    public class ChannelDownTagRelation : IRelation {
    }

    /// <summary>
    ///     控制面板中 问答分类
    /// </summary>
    [ClassProperty(Name = "问答分类", Icon = "fa fa-question", Description = "问答分类", SortOrder = 54,
        PageType = ViewPageType.List,
        SideBarType = SideBarType.QuestionSideBar)]
    [RelationProperty(RelationType = RelationType.ClassRelation)]
    public class ChannelQuestionClassRelation : IRelation {
    }

    /// <summary>
    ///     控制面板中 问答标签
    /// </summary>
    [ClassProperty(Name = "问答标签", Icon = "fa fa-question-circle", Description = "问答标签", SortOrder = 55,
        PageType = ViewPageType.List, SideBarType = SideBarType.QuestionSideBar)]
    [RelationProperty(RelationType = RelationType.TagRelation)]
    public class ChannelQuestionTagRelation : IRelation {
    }

    /// <summary>
    ///     控制面板中 招聘分类
    /// </summary>
    [ClassProperty(Name = "产品手册分类", Icon = "fa fa-cubes", Description = "产品手册分类", SortOrder = 56,
        PageType = ViewPageType.List,
        SideBarType = SideBarType.NoteBookSideBar)]
    [RelationProperty(RelationType = RelationType.ClassRelation)]
    public class ChannelNoteBookClassRelation : IRelation {
    }

    /// <summary>
    ///     控制面板中 招聘标签
    /// </summary>
    [ClassProperty(Name = "产品手册标签", Icon = "fa fa-cube", Description = "产品手册标签", SortOrder = 57,
        PageType = ViewPageType.List,
        SideBarType = SideBarType.NoteBookSideBar)]
    [RelationProperty(RelationType = RelationType.TagRelation)]
    public class ChannelNoteBookTagRelation : IRelation {
    }

    /// <summary>
    ///     控制面板中 招聘分类
    /// </summary>
    [ClassProperty(Name = "招聘分类", Icon = "fa fa-cubes", Description = "招聘分类", SortOrder = 56,
        PageType = ViewPageType.List,
        SideBarType = SideBarType.JobSideBar)]
    [RelationProperty(RelationType = RelationType.ClassRelation)]
    public class ChannelJobClassRelation : IRelation {
    }

    /// <summary>
    ///     控制面板中 招聘标签
    /// </summary>
    [ClassProperty(Name = "招聘标签", Icon = "fa fa-cube", Description = "招聘标签", SortOrder = 57,
        PageType = ViewPageType.List,
        SideBarType = SideBarType.JobSideBar)]
    [RelationProperty(RelationType = RelationType.TagRelation)]
    public class ChannelJobTagRelation : IRelation {
    }

    /// <summary>
    ///     控制面板中 消息分类
    /// </summary>
    [ClassProperty(Name = "消息分类", Icon = "fa fa-cube", Description = "消息分类", SortOrder = 59,
        PageType = ViewPageType.List,
        SideBarType = SideBarType.SinglePageSideBar)]
    [RelationProperty(RelationType = RelationType.TagRelation)]
    public class ChannelMessageClassRelation : IRelation {
    }

    /// <summary>
    ///     控制面板中 用户通知分类
    /// </summary>
    [ClassProperty(Name = "用户通知分类", Icon = "fa fa-cube", Description = "用户通知分类", SortOrder = 57,
        PageType = ViewPageType.List,
        SideBarType = SideBarType.JobSideBar)]
    [RelationProperty(RelationType = RelationType.TagRelation)]
    public class ChannelUserNoticeClassRelation : IRelation {
    }

    /// <summary>
    ///     控制面板中 用户通知标签
    /// </summary>
    [ClassProperty(Name = "用户通知标签", Icon = "fa fa-cube", Description = "用户通知标签", SortOrder = 59,
        PageType = ViewPageType.List,
        SideBarType = SideBarType.SinglePageSideBar)]
    [RelationProperty(RelationType = RelationType.TagRelation)]
    public class ChannelUserNoticeTagRelation : IRelation {
    }

    /// <summary>
    ///     控制面板中 招聘标签
    /// </summary>
    [ClassProperty(Name = "消息标签", Icon = "fa fa-cube", Description = "消息标签", SortOrder = 57,
        PageType = ViewPageType.List,
        SideBarType = SideBarType.JobSideBar)]
    [RelationProperty(RelationType = RelationType.TagRelation)]
    public class ChannelMessageTagRelation : IRelation {
    }

    /// <summary>
    ///     控制面板中 单页面分类.
    /// </summary>
    [ClassProperty(Name = "单页面分类", Icon = "fa fa-cubes", Description = "单页面分类", SortOrder = 58,
        PageType = ViewPageType.List,
        SideBarType = SideBarType.SinglePageSideBar)]
    [RelationProperty(RelationType = RelationType.ClassRelation)]
    public class SingelClassRelation : IRelation {
    }

    /// <summary>
    ///     控制面板中 单页面标签
    /// </summary>
    [ClassProperty(Name = "单页面标签", Icon = "fa fa-cube", Description = "单页面标签", SortOrder = 59,
        PageType = ViewPageType.List,
        SideBarType = SideBarType.SinglePageSideBar)]
    [RelationProperty(RelationType = RelationType.TagRelation)]
    public class SingleTagRelation : IRelation {
    }

    /// <summary>
    ///     控制面板中 工单分类
    /// </summary>
    [ClassProperty(Name = "工单分类", Icon = "fa fa-file-image-o", Description = "工单分类", SortOrder = 60,
        PageType = ViewPageType.List, SideBarType = SideBarType.CustomerServiceSideBar)]
    [RelationProperty(RelationType = RelationType.ClassRelation)]
    public class WorkOrderClassRelation : IRelation {
    }
}