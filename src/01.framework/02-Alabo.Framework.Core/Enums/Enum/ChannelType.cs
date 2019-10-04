using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Framework.Core.Enums.Enum {

    /// <summary>
    ///     频道枚举
    /// </summary>
    [ClassProperty(Name = "频道枚举")]
    public enum ChannelType {

        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "文章")]
        [Field(IsDefault = true, GuidId = "E02220001110000000000000", Icon = "fa fa-copy")]
        Article = 0,

        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "评论")]
        [Field(IsDefault = false, GuidId = "E02220001110000000000001", Icon = "fa fa-comment-o")]
        Comment = 1,

        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "帮助")]
        [Field(IsDefault = true, GuidId = "E02220001110000000000003", Icon = "flaticon-support")]
        Help = 2,

        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "图片")]
        [Field(IsDefault = false, GuidId = "E02220001110000000000004", Icon = "fa fa-file-image-o")]
        Images = 4,

        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "视频")]
        [Field(IsDefault = false, GuidId = "E02220001110000000000005", Icon = "fa fa-video-camera")]
        Video = 5,

        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "下载")]
        [Field(IsDefault = false, GuidId = "E02220001110000000000006", Icon = "fa fa-cloud-download")]
        Down = 6,

        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "问答")]
        [Field(IsDefault = false, GuidId = "E02220001110000000000007", Icon = " fa fa-question-circle-o ")]
        Question = 7,

        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "招聘")]
        [Field(IsDefault = false, GuidId = "E02220001110000000000008", Icon = "fa fa-user-circle-o")]
        Job = 8,

        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "头条")]
        [Field(IsDefault = true, GuidId = "E02220001110000000000009", Icon = "fa fa-newspaper-o")]
        TopLine = 9,

        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "消息")]
        [Field(IsDefault = false, GuidId = "E02220001110000000000010", Icon = "fa fa-flickr")]
        Message = 10,

        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "会员通知")]
        [Field(IsDefault = true, GuidId = "E02220001110000000000011", Icon = "fa fa-envelope-o")]
        UserNotice = 11,

        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "产品手册")]
        [Field(IsDefault = true, GuidId = "E02220001110000000000012", Icon = "flaticon-list-3")]
        NoteBook = 12,

        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "自定义")]
        Customer = -1
    }
}