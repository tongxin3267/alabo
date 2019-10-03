using Alabo.App.Asset.Withdraws.Domain.Enums;
using Alabo.App.Asset.Withdraws.Domain.Services;
using Alabo.App.Asset.Withdraws.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Exceptions;
using Alabo.Extensions;
using Alabo.Framework.Basic.AutoConfigs.Domain.Configs;
using Alabo.Helpers;
using Alabo.Mapping;
using Alabo.Maps;
using Alabo.UI;
using Alabo.UI.Design.AutoForms;
using Alabo.UI.Design.AutoTables;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Alabo.App.Asset.Withdraws.UI
{
    [ClassProperty(Name = "", Description = "提现")]
    public class WithdrawAutoForm : UIBase, IAutoForm, IAutoTable<WithdrawAutoForm>
    {
        public AutoForm GetView(object id, AutoBaseModel autoModel)
        {
            var result = ToAutoForm(new WithdrawAutoForm());
            result.AlertText = "【提现】将 储值 提取到自己的银行卡";

            result.ButtomHelpText = new List<string>
            {
                "提现金额为【储值】",
                "提现到账为T+3请耐心等待审核",
                "提现金额必须为100的整数倍",
                "最大提现金额为10000金额/次",
                "提现手续费为0.3%"
            };
            return result;
        }

        public ServiceResult Save(object model, AutoBaseModel autoModel)
        {
            var withDraw = (WithdrawAutoForm)model;
            withDraw.UserId = autoModel.BasicUser.Id;
            withDraw.Status = WithdrawStatus.FirstCheckSuccess;
            //如果为空则直接赋值为 储值/现金
            if (withDraw.MoneyTypeId.IsNullOrEmpty()) {
                withDraw.MoneyTypeId = Guid.Parse("e97ccd1e-1478-49bd-bfc7-e73a5d699000");
            }

            var result = Resolve<IWithdrawService>().Add(withDraw.MapTo<WithDrawInput>());
            return result;
        }

        public List<TableAction> Actions()
        {
            return null;
        }

        public PageResult<WithdrawAutoForm> PageTable(object query, AutoBaseModel autoModel)
        {
            var userInput = ToQuery<WithDrawApiInput>();

            if (autoModel.Filter == FilterType.Admin)
            {
                var dic = HttpWeb.HttpContext.ToDictionary();
                dic = dic.RemoveKey("WithdrawAdminAutoForm"); // 移除该type否则无法正常lambda

                var model = Resolve<IWithdrawService>().GetAdminPageList(dic.ToJson());
                var view = new PagedList<WithdrawAutoForm>();
                foreach (var item in model)
                {
                    var outPut = AutoMapping.SetValue<WithdrawAutoForm>(item);
                    view.Add(outPut);
                }

                return ToPageResult(view);
            }

            if (autoModel.Filter == FilterType.User)
            {
                //userInput.UserId = autoModel.BasicUser.Id;
                //userInput.LoginUserId = autoModel.BasicUser.Id;
                //var model = Resolve<IWithdrawService>().GetUserList(userInput);

                //var money = Resolve<IAutoConfigService>().GetList<Domain.CallBacks.MoneyTypeConfig>();
                //var view = new PagedList<WithdrawAutoForm>();
                //foreach (var item in model) {
                //    var outPut = new WithdrawAutoForm {
                //        //提现单号
                //        //Serial = item.Serial,
                //        //状态
                //        Status = item.Status,
                //        Amount = item.Amount,
                //        BankCardId = item.TradeExtension.WithDraw.BankCard.Id.ToString(),
                //        MoneyTypeId = item.MoneyTypeId,
                //        //MoneyTypeName = money.Find(s => s.Id == item.MoneyTypeId)?.Name,
                //        UserRemark = item.TradeExtension.TradeRemark.UserRemark,
                //        UserId = item.UserId,
                //        //BankName = $"{item.TradeExtension.WithDraw.BankCard.Type.GetDisplayName()}({item.TradeExtension.WithDraw.BankCard.Address})",
                //        //BankCardNumber = $"{item.TradeExtension.WithDraw.BankCard.Number}({item.TradeExtension.WithDraw.BankCard.Name})",
                //        CreateTime = item.CreateTime
                //    };// AutoMapping.SetValue<WithdrawAutoForm>(item);
                //    view.Add(outPut);
                //}
                //return ToPageResult(view);
            }
            else
            {
                throw new ValidException("类型权限不正确");
            }

            return null;
        }

        #region

        /// <summary>
        ///     Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     Gets or sets 会员Id
        /// </summary>
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public long UserId { get; set; }

        /// <summary>
        ///     Gets or sets the bank card identifier.
        /// </summary>
        [Display(Name = "单号")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, EditShow = false, SortOrder = 1)]
        public string Serial
        {
            get
            {
                var searSerial = $"9{Id.ToString().PadLeft(9, '0')}";
                if (Id.ToString().Length == 10) {
                    searSerial = $"{Id.ToString()}";
                }

                return searSerial;
            }
        }

        /// <summary>
        ///     Gets or sets the bank card identifier.
        /// </summary>
        [Display(Name = "状态")]
        [Field(ControlsType = ControlsType.DropdownList, DataSource = "Alabo.App.Core.Finance.Domain.Enums.TradeStatus",
            ListShow = true, EditShow = false, SortOrder = 4)]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public WithdrawStatus Status { get; set; }

        /// <summary>
        ///     Gets or sets the bank card identifier.
        /// </summary>
        [Display(Name = "银行卡")]
        [Field(ControlsType = ControlsType.DropdownList, ListShow = false, EditShow = true,
            ApiDataSource = "Api/BankCard/GetList?loginUserId=[[Id]]", SortOrder = 3)]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string BankCardId { get; set; }

        /// <summary>
        ///     Gets or sets the bank card identifier.
        /// </summary>
        //[Display(Name = "银行")]
        //[Field(ControlsType = ControlsType.TextBox, ListShow = true, EditShow = false, SortOrder = 2)]
        //public string BankName { get; set; }

        /// <summary>
        ///     Gets or sets the bank card identifier.
        /// </summary>
        //[Display(Name = "银行卡卡号")]
        //[Field(ControlsType = ControlsType.DropdownList, ListShow = true, EditShow = false, SortOrder = 2)]
        //[Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        //public string BankCardNumber { get; set; }
        /// <summary>
        ///     货币类型
        /// </summary>
        //[Display(Name = "提现账户")]
        //[Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        //[Field(ControlsType = ControlsType.TextBox, ListShow = true,
        //    IsShowAdvancedSerach = true,
        //    // DataSourceType = typeof(Domain.CallBacks.MoneyTypeConfig),
        //    IsShowBaseSerach = false,
        //    SortOrder = 200)]
        //public string MoneyTypeName { get; set; } = "提现账户";

        /// <summary>
        ///     货币类型  固定提现账户为 现金/储值账户
        /// </summary>

        [Display(Name = "提现账户")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.DropdownList, ListShow = false, EditShow = false,
            DataSourceType = typeof(MoneyTypeConfig), IsShowBaseSerach = false,
            SortOrder = 200)]
        public Guid MoneyTypeId { get; set; } = Guid.Parse("e97ccd1e-1478-49bd-bfc7-e73a5d699000");

        /// <summary>
        ///     开始金额
        /// </summary>
        [Display(Name = "申请金额")]
        [Field(ControlsType = ControlsType.TextBox, EditShow = true, ListShow = true, SortOrder = 5)]
        [Range(0.01, 10000, ErrorMessage = ErrorMessage.NameNotInRang)]
        [HelpBlock("提现金额必须为100的整数倍,最大提现额为10000金额/次")]
        public decimal Amount { get; set; }

        /// <summary>
        ///     Gets or sets the pay password.
        /// </summary>
        [Display(Name = "支付密码")]
        [Required(ErrorMessage = "请填写您的支付密码，不可以为空")]
        [Field(ControlsType = ControlsType.Password, EditShow = true, SortOrder = 6)]
        public string PayPassword { get; set; }

        /// <summary>
        ///     用户备注
        /// </summary>
        [Display(Name = "备注")]
        [Field(ControlsType = ControlsType.TextArea, EditShow = true, ListShow = true, SortOrder = 10)]
        [HelpBlock("请输入备注")]
        public string UserRemark { get; set; }

        /// <summary>
        ///     时间
        /// </summary>
        [Display(Name = "时间")]
        [Field(ControlsType = ControlsType.DateTimePicker, EditShow = false, ListShow = true, SortOrder = 11)]
        public DateTime CreateTime { get; set; }

        #endregion
    }
}