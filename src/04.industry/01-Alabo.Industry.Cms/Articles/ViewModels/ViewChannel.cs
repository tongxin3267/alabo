using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Enums;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.Industry.Cms.Articles.ViewModels
{
    /// <summary>
    ///     频道模型
    /// </summary>
    public class ViewChannel : BaseViewModel
    {
        /// <summary>
        ///     频道名称
        /// </summary>
        [Display(Name = "频道名称")]
        [Field(ListShow = true, EditShow = true, ControlsType = ControlsType.TextBox)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        ///     频道类型
        /// </summary>
        [Display(Name = "频道类型")]
        [Field(ListShow = true, EditShow = true, ControlsType = ControlsType.DropdownList,
            DataSource = "Alabo.App.Cms.Articles.Domain.Enums.ChannelType")]
        [Required]
        public ChannelType ChannelType { get; set; }

        /// <summary>
        ///     启用评论
        /// </summary>
        [Display(Name = "启用评论")]
        [Field(EditShow = true, ControlsType = ControlsType.Switch)]
        public bool IsComment { get; set; } = false;

        /// <summary>
        ///     SEO标题
        /// </summary>
        [Display(Name = "SEO标题")]
        [StringLength(200, ErrorMessage = "Seo标题长度不能超过200个字符")]
        [Field(ListShow = false, EditShow = true, ControlsType = ControlsType.TextBox)]
        public string MetaTitle { get; set; }

        /// <summary>
        ///     SEO关键字
        /// </summary>
        [Display(Name = "SEO关键字")]
        [StringLength(300, ErrorMessage = "SEO关键字长度不能超过300个字符")]
        [Field(ListShow = false, EditShow = true, ControlsType = ControlsType.TextBox)]
        public string MetaKeywords { get; set; }

        /// <summary>
        ///     SEO描述
        /// </summary>
        [Display(Name = "SEO描述")]
        [StringLength(400, ErrorMessage = "SEO描述长度不能超过400个字符")]
        [Field(ListShow = false, EditShow = true, ControlsType = ControlsType.TextArea)]
        public string MetaDescription { get; set; }
    }
}