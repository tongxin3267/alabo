using System;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Asset.Withdraws.Domain.Services;
using Alabo.App.Core.Finance.Domain.Enums;
using Alabo.App.Core.Finance.Domain.Services;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebUis;
using Alabo.Framework.Core.WebUis.Design.AutoForms;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Framework.Basic.AutoConfigs.Domain.Configs;
using Alabo.UI;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.Finance.UI.AutoForm {

    [ClassProperty(Name = "提现审核", Description = "提现")]
    public class WithdrawReviewAutoForm : UIBase, IAutoForm {
        #region

        /// <summary>
        ///     Id
        /// </summary>
        [Display(Name = "Id")]
        [Field(ControlsType = ControlsType.Hidden, EditShow = true)]
        public long Id { get; set; }

        /// <summary>
        ///     Gets or sets 会员Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        ///     Gets or sets the bank card identifier.
        /// </summary>
        [Display(Name = "单号")]
        [Field(ControlsType = ControlsType.Label, EditShow = true, SortOrder = 1)]
        public string Serial {
            get {
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
        [Display(Name = "当前状态")]
        [Field(ControlsType = ControlsType.Label, EditShow = true, SortOrder = 4)]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string Status { get; set; }

        /// <summary>
        ///     Gets or sets the bank card identifier.
        /// </summary>
        [Display(Name = "银行卡")]
        [Field(ControlsType = ControlsType.Label, EditShow = true, SortOrder = 3)]
        public string BankCardId { get; set; }

        /// <summary>
        ///     货币类型  固定提现账户为 现金/储值账户
        /// </summary>

        [Display(Name = "提现账户")]
        [Field(ControlsType = ControlsType.DropdownList, EditShow = false,
            DataSourceType = typeof(MoneyTypeConfig),
            SortOrder = 200)]
        public Guid? MoneyTypeId { get; set; } = Guid.Parse("e97ccd1e-1478-49bd-bfc7-e73a5d699000");

        /// <summary>
        ///     开始金额
        /// </summary>
        [Display(Name = "申请金额")]
        [Field(ControlsType = ControlsType.Label, EditShow = true, SortOrder = 5)]
        [HelpBlock("提现金额必须为100的整数倍,最大提现额为10000金额/次")]
        public decimal Amount { get; set; }

        /// <summary>
        ///     手续费
        /// </summary>
        [Display(Name = "手续费")]
        [Field(ControlsType = ControlsType.Label, TableDispalyStyle = TableDispalyStyle.Code,
            EditShow = true, SortOrder = 5)]
        public decimal Fee { get; set; }

        /// <summary>
        ///     手续费
        /// </summary>
        [Display(Name = "应付金额")]
        [Field(ControlsType = ControlsType.Label, TableDispalyStyle = TableDispalyStyle.Code,
            EditShow = true, Width = "80",
            SortOrder = 5)]
        public decimal PaymentAmount { get; set; }

        /// <summary>
        ///     用户备注
        /// </summary>
        [Display(Name = "备注")]
        [Field(ControlsType = ControlsType.Label, EditShow = true, SortOrder = 10)]
        public string UserRemark { get; set; }

        /// <summary>
        ///    时间
        /// </summary>
        [Display(Name = "时间")]
        [Field(ControlsType = ControlsType.Label, EditShow = false, SortOrder = 11)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        [Display(Name = "审核状态")]
        [Field(ControlsType = ControlsType.RadioButton, DataSourceType = typeof(ReviewState), EditShow = true, SortOrder = 12)]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public ReviewState ReviewState { get; set; }

        /// <summary>
        /// 审核信息
        /// </summary>
        [Display(Name = "审核信息")]
        [Field(ControlsType = ControlsType.TextArea, EditShow = true, SortOrder = 12)]
        //[Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [HelpBlock("请输入审核信息")]
        public string ReviewRemark { get; set; }

        /// <summary>
        /// 审核拒绝理由
        /// </summary>
        [Display(Name = "原因")]
        [Field(ControlsType = ControlsType.TextArea, EditShow = true, SortOrder = 12)]
        //[Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [HelpBlock("请输入原因")]
        public string FailuredReason { get; set; }

        #endregion

        public Alabo.Framework.Core.WebUis.Design.AutoForms.AutoForm GetView(object id, AutoBaseModel autoModel) {
            var withDraw = Resolve<IWithdrawService>().GetAdminWithDraw(id.ConvertToLong());
            var form = new WithdrawReviewAutoForm();
            if (withDraw == null) {
                form = new WithdrawReviewAutoForm();
            } else {
                //form = new WithdrawReviewAutoForm() {
                //    Status = withDraw.Status.GetDisplayName(),
                //    Amount = withDraw.Amount,
                //    Fee = withDraw.Fee,
                //    PaymentAmount = withDraw.Amount - withDraw.Fee,

                //    BankCardId = withDraw.BankCard?.Number,
                //    CreateTime = withDraw.CreateTime,
                //    FailuredReason = withDraw.FailuredReason,
                //    Id = withDraw.Id,
                //    MoneyTypeId = withDraw.MoneyTypeConfig?.Id,

                //    ReviewRemark = withDraw.Remark,//汇款备注提示

                //    UserRemark = withDraw.UserRemark,
                //    UserId = withDraw.UserId
                //};
                // withDraw.MapTo<WithdrawReviewAutoForm>();
            }
            var result = ToAutoForm(form);
            result.AlertText = "【审核提现】";

            //result.ButtomHelpText = new List<string> {
            //    "提现金额为【储值】",
            //    "提现到账为T+3请耐心等待审核",
            //    "提现金额必须为100的整数倍",
            //    "最大提现金额为10000金额/次",
            //    "提现手续费为0.3%"
            //};
            return result;
        }

        public ServiceResult Save(object model, AutoBaseModel autoModel) {
            //TODO 提现审核注释
            var withDraw = (WithdrawReviewAutoForm)model;
            //withDraw.UserId = autoModel.BasicUser.Id;

            //var adminWithDraw = Resolve<IWithdrawService>().GetAdminWithDraw(withDraw.Id);

            //if (withDraw.ReviewState == ReviewState.Pass && adminWithDraw.Status == TradeStatus.Pending) {
            //    //通过初审
            //   // adminWithDraw.Status = TradeStatus.FirstCheckSuccess;
            //} else if (withDraw.ReviewState == ReviewState.Reject && adminWithDraw.Status == TradeStatus.Pending) {
            //    //初审拒绝
            //    adminWithDraw.Status = TradeStatus.Failured;
            //} else if (withDraw.ReviewState == ReviewState.Pass && adminWithDraw.Status == TradeStatus.FirstCheckSuccess) {
            //    //通过二审
            //    adminWithDraw.Status = TradeStatus.Success;
            //} else if (withDraw.ReviewState == ReviewState.Reject && adminWithDraw.Status == TradeStatus.FirstCheckSuccess) {
            //    //拒绝二审
            //    adminWithDraw.Status = TradeStatus.Failured;
            //} else {
            //    //其他状态 返回异常
            //    return ServiceResult.FailedMessage("状态异常");
            //}

            ////如果为空则直接赋值为 储值/现金
            //if (withDraw.MoneyTypeId.IsNullOrEmpty()) {
            //    withDraw.MoneyTypeId = Guid.Parse("e97ccd1e-1478-49bd-bfc7-e73a5d699000");
            //}

            //var result = Resolve<IWithdrawService>().WithDrawCheck(new ViewModels.WithDraw.ViewWithDrawCheck() {
            //    Amount = adminWithDraw.Amount,
            //    FailuredReason = withDraw.FailuredReason,
            //    Status = adminWithDraw.Status,
            //    TradeId = withDraw.Id,
            //    Remark = withDraw.ReviewRemark
            //});
            //return result;
            return null;
        }
    }
}