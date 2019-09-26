using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Alabo.AutoConfigs;
using Alabo.Domains.Enums;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Framework.Basic.Grades.Domain.Configs;
using Alabo.Framework.Basic.Grades.Domain.Services;
using Alabo.Helpers;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.Industry.Shop.Activitys.Modules.MemberDiscount.Callbacks
{


    /// <summary>
    /// 会员等级前台显示配置
    /// </summary>
    [NotMapped]
    [ClassProperty(Name = "会员等级前台显示配置", Icon = "fa fa-cny", Description = "",
        PageType = ViewPageType.Edit, SortOrder = 20,
        SideBarType = SideBarType.UserRightsSideBar)]
    public class MemberDiscountConfig : BaseViewModel, IAutoConfig
    {
        /// <summary>
        /// GradeIds
        /// </summary>
        [Field(ControlsType = ControlsType.Switch,EditShow =true)]
        [Display(Name = "显示默认会员等级价")]
        [HelpBlock("不选择时，商品需要设置活动才显示会员等级价。")]
        public bool IsShowDefaultGradePrice { get; set; }

        /// <summary>
        /// GradeIds
        /// </summary>
        [Field(ControlsType = ControlsType.CheckBoxMultipl, SortOrder = 2, DataSourceType = typeof(UserGradeConfig))]
        [Display(Name = "显示会员等级价")]
        [HelpBlock("不选择时，前台不显示该等级的价格。")]
        public string GradeIds { get; set; }

        /// <summary>
        /// 免费会员前台是否显示价格
        /// </summary>
        [Field(ControlsType = ControlsType.Switch)]
        [Display(Name = "免费会员前台是否显示价格")]
        [HelpBlock("不选择时，前台不显示该等级的价格。")]
        public bool IsFrontShowPrice { get; set; }

        /// <summary>
        /// 不显示价格的替代文本
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox)]
        [Display(Name = "前台价格替代文本")]
        [HelpBlock("不选择时，前台不显示该等级的价格。")]
        public string PriceAlterText { get; set; } = "升级后显示价格";

        ///// <summary>
        ///// GradeIds
        ///// </summary>
        //[Field(ControlsType = ControlsType.Switch, SortOrder = 2, DataSourceType = typeof(UserGradeConfig))]
        //[Display(Name = "前台是否显示价格")]
        //[HelpBlock("ControlsType")]
        //public string ShowGradeIds { get; set; }

        ///// <summary>
        ///// GradeIds
        ///// </summary>
        //[Field(ControlsType = ControlsType.TextBox, EditShow = true)]
        //[Display(Name = "显示默认会员等级价")]
        //[HelpBlock("不选择时，商品需要设置活动才显示会员等级价。")]

        //public string ShowText { get; set; } = "升级后显示价格";


        public void SetDefault()
        {
            var userGrades = Ioc.Resolve<IGradeService>().GetUserGradeList().ToList();
            var gradeIds = userGrades.Select(u => u.Id).ToList();
            if (gradeIds.Count > 0)
            {
                var memberDiscountConfig = new MemberDiscountConfig
                {
                    IsShowDefaultGradePrice = true,
                    GradeIds = string.Join(",", gradeIds)
                };
                Ioc.Resolve<IAutoConfigService>().AddOrUpdate<MemberDiscountConfig>(memberDiscountConfig);
            }
        }
    }
}
