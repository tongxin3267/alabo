using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Alabo.App.Core.Finance.Domain.Entities;
using Alabo.App.Core.Finance.Domain.Services;
using Alabo.App.Core.Finance.Dtos;
using Alabo.Core.Enums.Enum;
using Alabo.Core.WebApis;
using Alabo.Core.WebUis;
using Alabo.Core.WebUis.Design.AutoForms;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Maps;
using Alabo.UI;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.Finance.UI.AutoForm {

    [ClassProperty(Name = "银行卡", Description = "银行卡")]
    public class BankCardAutoForm : UIBase, IAutoForm {

        #region 银行卡

        public string Id { get; set; }

        public long UserId { get; set; }

        /// <summary>
        /// 银行卡号
        /// </summary>
        [Display(Name = "银行卡号")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [HelpBlock("请输入正确的银行卡号")]
        [Field(ControlsType = ControlsType.TextBox, Width = "150", ListShow = true, GroupTabId = 1, SortOrder = 1)]
        public string BankCardId { get; set; }

        /// <summary>
        ///     持卡人姓名
        /// </summary>
        [Display(Name = "持卡人姓名")]
        [HelpBlock("请输入银行卡对应的持卡人姓名")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, Width = "120", GroupTabId = 1,
            ListShow = true, SortOrder = 2)]
        public string Name { get; set; }

        /// <summary>
        ///     银行类型
        /// </summary>
        [Display(Name = "银行类型")]
        [HelpBlock("请选择银行卡对应的银行类型")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.DropdownList, DataSource = "Alabo.Core.Enums.Enum.BankType", Width = "120",
            EditShow = true, GroupTabId = 1, ListShow = true, SortOrder = 3)]
        public BankType Type { get; set; }

        /// <summary>
        ///     开户行
        /// </summary>
        [Display(Name = "开户行地址")]
        [HelpBlock("请输入银行卡的开户行地址")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, Width = "150", ListShow = true, GroupTabId = 1, SortOrder = 4)]
        public string Address { get; set; }

        #endregion 银行卡

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="autoModel"></param>
        /// <returns></returns>
        public Alabo.Core.WebUis.Design.AutoForms.AutoForm GetView(object id, AutoBaseModel autoModel) {
            //不允许更改
            //return ToAutoForm(new BankCardAutoForm());

            var result = ToAutoForm(new BankCardAutoForm());
            result.AlertText = "【添加银行卡】请正确的输入您的银行卡信息，输入后请确认您的输入无误";

            result.ButtomHelpText = new List<string> {
                "银行卡号只能为纯数字，其他字符无法提交",
                "银行卡添加后无法编辑，只可解除绑定",
                "请认真核对您的银行卡信息输入是否有误",
            };
            return result;

            //if (id == null)
            //{
            //    return ToAutoForm(new BankCardAutoForm());
            //}

            //var idStr = id.ToString();
            //var model = Resolve<IBankCardService>().GetSingle(u => u.Id == idStr.ToObjectId());

            //if (model == null)
            //{
            //    return ToAutoForm(new BankCardAutoForm());
            //}

            //var result = AutoMapping.SetValue<ApiBankCardInput>(model);
            //result.BankCardId = model.Number;
            //return ToAutoForm(result.MapTo<BankCardAutoForm>());
        }

        public ServiceResult Save(object model, AutoBaseModel autoModel) {
            var temp = ((BankCardAutoForm)model).MapTo<ApiBankCardInput>();
            temp.LoginUserId = autoModel.BasicUser.Id;

            //银行卡必须为数字不能以0开头
            var r = new Regex("^([1-9][0-9]*)$");
            if (!r.IsMatch(temp.BankCardId)) {
                return ServiceResult.FailedWithMessage("请正确输入银行卡号");
            }

            if (temp.BankCardId.Length < 10) {
                return ServiceResult.FailedWithMessage("请正确输入银行卡号");
            }

            if (temp.Type == 0) {
                return ServiceResult.FailedWithMessage("请选择银行类型");
            }

            var bankCardList = Resolve<IBankCardService>().GetList(u => u.UserId == temp.LoginUserId);
            if (bankCardList.Count >= 10) {
                return ServiceResult.FailedWithMessage("银行卡最多只可绑定十张");
            }

            var res = Resolve<IBankCardService>().GetSingle(u => u.Number == temp.BankCardId);
            BankCard bankCard = new BankCard {
                Number = temp.BankCardId,
                Address = temp.Address,
                Name = temp.Name,
                Type = temp.Type,
                UserId = temp.LoginUserId,
                Id = temp.Id.ToObjectId()
            };

            if (res == null) {
                var result = Resolve<IBankCardService>().Add(bankCard);
                if (!result) {
                    return ServiceResult.Failed;
                }

                return ServiceResult.Success;
            }
            return ServiceResult.FailedWithMessage("该银行卡已经绑定");
        }
    }
}