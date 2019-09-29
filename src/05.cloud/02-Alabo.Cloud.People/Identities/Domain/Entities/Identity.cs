using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Users.Enum;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;
using MongoDB.Bson.Serialization.Attributes;

namespace Alabo.Cloud.People.Identities.Domain.Entities
{
    /// <summary>
    ///     用户身份认证
    /// </summary>
    [ClassProperty(Name = "个人认证", Icon = "fa fa-puzzle-piece", Description = "个人认证",
        SideBarType = SideBarType.IdentitySideBar, PostApi = "/Api/Identity/Identity")]
    [BsonIgnoreExtraElements]
    [Table("Cloud_People_Identity")]
    [AutoDelete(IsAuto = true)] //  /Api/Identity/QueryDelete
    public class Identity : AggregateMongodbUserRoot<Identity>
    {
        /// <summary>
        ///     真实姓名
        /// </summary>
        [Display(Name = "真实姓名")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, IsMain = true, EditShow = true, GroupTabId = 1, Width = "110",
            ListShow = true,
            SortOrder = 2)]
        [HelpBlock("请务必填写【真实姓名】")]
        public string RealName { get; set; }

        /// <summary>
        ///     证件号
        /// </summary>
        [Display(Name = "身份证号码")]
        [Field(ControlsType = ControlsType.TextBox, EditShow = true, GroupTabId = 1, Width = "110", ListShow = true,
            SortOrder = 5)]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [HelpBlock("请务必填写【身份证号码】")]
        public string CardNo { get; set; }

        /// <summary>
        ///     审核状态（0：已提交，1：审核中(暂时不用)，2：审核通过，3：审核不通过）
        /// </summary>
        [Display(Name = "审核状态")]
        [Field(ControlsType = ControlsType.RadioButton, DataSource = "Alabo.Framework.Core.Enums.Enum.IdentityStatus",
            Width = "110", ListShow = true, EditShow = false, IsShowAdvancedSerach = false, IsShowBaseSerach = false,
            SortOrder = 11)]
        public IdentityStatus Status { get; set; }

        /// <summary>
        ///     性别
        /// </summary>
        //[Display(Name = "性别")]
        //[NotMapped]
        //[Field(ControlsType = ControlsType.Label, DataSourceType = typeof(Sex), EditShow = false,
        //    Width = "200", SortOrder = 7)]
        //public Sex Sex { get; set; } = Sex.UnKnown;

        /// <summary>
        ///     证件类型0：身份证 1：护照 2：营业执照 3：驾照
        /// </summary>
        //[Display(Name = "证件类型")]
        //[Field(ControlsType = ControlsType.DropdownList, Width = "200", EditShow = false, DataSourceType = typeof(CardType), ListShow = true, SortOrder = 3)]
        //[Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        //public CardType CardType { get; set; }

        ///// <summary>
        ///// 正脸照
        ///// </summary>
        //[Display(Name = "个人头像")]
        //[Field(ControlsType = ControlsType.AlbumUploder, EditShow = true, Width = "200", SortOrder = 7)]
        //[HelpBlock("请上传一张半身正面照")]
        //public string FaceImage { get; set; }

        ///// <summary>
        /////     证件图片1url，证件正面
        ///// </summary>
        //[Display(Name = "证件正面照")]
        //[Field(ControlsType = ControlsType.ImagePreview, EditShow = false, SortOrder = 8)]
        //public string FrontImage { get; set; }

        ///// <summary>
        /////     证件图片2url，证件反面
        ///// </summary>
        //[Display(Name = "证件反面照")]
        //[Field(ControlsType = ControlsType.ImagePreview, EditShow = false, SortOrder = 9)]
        //public string AntiImage { get; set; }

        ///// <summary>
        /////     证件图小样
        ///// </summary>
        //[Display(Name = "手持证件正面照")]
        //[Field(ControlsType = ControlsType.ImagePreview, EditShow = false, SortOrder = 10)]
        //public string SmallImage { get; set; }

        /// <summary>
        ///     审核备注，不通过时备注信息
        /// </summary>
        //[Display(Name = "审核备注")]
        // public string CheckRemark { get; set; }

        ///// <summary>
        /////     审核人
        ///// </summary>
        //[Display(Name = "审核人")]
        //[Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "110", EditShow = false, SortOrder = 13)]
        //public string CheckUserName { get; set; }

        ///// <summary>
        /////     审核时间
        ///// </summary>
        //[Display(Name = "审核时间")]
        //[Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "110", ListShow = true, EditShow = false,
        //    SortOrder = 14)]
        //public DateTime CheckTime { get; set; }

        /// <summary>
        ///     获取链接
        /// </summary>
        public IEnumerable<ViewLink> ViewLinks()
        {
            var quickLinks = new List<ViewLink>
            {
                new ViewLink("实名审核", "/Admin/Identity/Edit?Id=[[Id]]", Icons.Edit, LinkType.ColumnLink)
            };
            return quickLinks;
        }
    }
}