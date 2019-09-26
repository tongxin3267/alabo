using System.ComponentModel.DataAnnotations;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Query.Dto;
using Alabo.UI;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.Finance.Dtos {

    [ClassProperty(Name = "银行卡管理", SideBarType = SideBarType.BankCardSideBar, PostApi = "Api/BankCard/AddBankCard", Icon = IconFontawesome.bank)]
    public class ApiBankCardInput : EntityDto {
        public string Id { get; set; }

        public long UserId { get; set; }

        /// <summary>
        /// 银行卡号
        /// </summary>
        [Display(Name = "银行卡号")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, Width = "150", ListShow = true, SortOrder = 1)]
        public string BankCardId { get; set; }

        /// <summary>
        ///     持卡人姓名
        /// </summary>
        [Display(Name = "持卡人姓名")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, IsShowBaseSerach = true, Width = "120",
            ListShow = true, SortOrder = 2)]
        public string Name { get; set; }

        /// <summary>
        ///     银行类型
        /// </summary>
        [Display(Name = "银行类型")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.DropdownList, DataSource = "Alabo.Framework.Core.Enums.Enum.BankType", Width = "120", EditShow = true, ListShow = true, SortOrder = 3)]
        public BankType Type { get; set; }

        /// <summary>
        ///     开户行
        /// </summary>
        [Display(Name = "开户行地址")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, Width = "150", ListShow = true, SortOrder = 4)]
        public string Address { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public long LoginUserId { get; set; }
    }
}