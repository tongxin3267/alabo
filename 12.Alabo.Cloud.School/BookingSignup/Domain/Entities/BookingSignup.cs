using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.App.Market.BookingSignup.Domain.Enums;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.UI;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Market.BookingSignup.Domain.Entities {

    [BsonIgnoreExtraElements]
    [Table("BookingSignup_BookingSignup")]
    [ClassProperty(Name = "预约课程", Description = "预约课程", Icon = IconFlaticon.route, SideBarType = SideBarType.SchoolSideBar)]
    [AutoDelete(IsAuto = true)]
    public class BookingSignup : AggregateMongodbRoot<BookingSignup> {

        /// <summary>
        ///     课程名称
        /// </summary>
        [Display(Name = "名称")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, IsMain = true, ListShow = true, IsShowBaseSerach = true, EditShow = true, Link = "/Admin/Course/Edit?id=[[Id]]", SortOrder = 0)]
        public string Name { get; set; }

        /// <summary>
        /// 课程图片
        /// </summary>
        [Field(ControlsType = ControlsType.AlbumUploder, SortOrder = 0, IsImagePreview = true, ListShow = true, Width = "15%", EditShow = true)]
        [Display(Name = "图片")]
        public string Image { get; set; }

        /// <summary>
        ///     课程介绍
        /// </summary>
        [Display(Name = "内容")]
        //  [Required(ErrorMessage = "课程介绍不能为空")]
        [Field(ControlsType = ControlsType.Editor, EditShow = true)]
        [HelpBlock("编辑课程内容")]
        public string Intro { get; set; }

        /// <summary>
        /// 显示价格文字
        /// </summary>
        [Display(Name = "规格")]
        [Required(ErrorMessage = "规格不能为空")]
        [Field(ControlsType = ControlsType.TextBox, EditShow = true)]
        [HelpBlock("编辑课程简介")]
        public string Brief { get; set; }

        /// <summary>
        /// 课程价格
        /// 0的时候表示免费
        /// </summary>
        [Display(Name = "价格")]
        [Required(ErrorMessage = "价格不能为空")]
        [Field(ControlsType = ControlsType.TextBox, EditShow = true, ListShow = true)]
        [HelpBlock("设置每节课门票价格")]
        public decimal Price { get; set; } = 0m;

        /// <summary>
        /// 课程开始时间
        /// </summary>
        [Display(Name = "开始时间")]
        [Field(ListShow = true, EditShow = true, ControlsType = ControlsType.TextBox)]
        public string StartTime { get; set; }

        /// <summary>
        /// 课程结束时间
        /// 课程开始时间+课程时间（TotalTime）
        /// 自动计算
        /// </summary>
        [Display(Name = "结束时间")]
        [Field(ListShow = true, EditShow = true, ControlsType = ControlsType.TextBox)]
        public string EndTime { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [Display(Name = "地址")]
        [Field(ControlsType = ControlsType.TextBox, EditShow = true)]
        public string Address { get; set; }

        /// <summary>
        /// 状态 默认结束
        /// </summary>
        [Display(Name = "状态")]
        [Field(ControlsType = ControlsType.RadioButton, DataSourceType = typeof(BookingSignupState), EditShow = true, ListShow = true)]
        public BookingSignupState State { get; set; } = BookingSignupState.End;
    }
}