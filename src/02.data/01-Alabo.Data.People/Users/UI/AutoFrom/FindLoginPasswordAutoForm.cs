﻿using Alabo.Data.People.Users.Domain.Services;
using Alabo.Data.People.Users.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Mapping;
using Alabo.Regexs;
using Alabo.Tables.Domain.Services;
using Alabo.UI;
using Alabo.UI.Design.AutoForms;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Alabo.Data.People.Users.UI.AutoFrom
{
    [ClassProperty(Name = "找回登录密码", Description = "根据手机号码，手机验证码，找回登录密码")]
    public class FindLoginPasswordAutoForm : UIBase, IAutoForm
    {
        /// <summary>
        ///     手机号码
        /// </summary>
        [Display(Name = "手机号码")]
        [Field(ControlsType = ControlsType.TextBox, Width = "80", EditShow = true, SortOrder = 2)]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [RegularExpression(RegularExpressionHelper.ChinaMobile, ErrorMessage = ErrorMessage.NotMatchFormat)]
        public string Mobile { get; set; }

        /// <summary>
        ///     手机验证码
        /// </summary>
        [Display(Name = "手机验证码")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [MinLength(6, ErrorMessage = "验证码格式不正确")]
        [Field(ControlsType = ControlsType.PhoneVerifiy, GroupTabId = 2, Mark = "Mobile", EditShow = true)]
        public string MobileVerifiyCode { get; set; }

        /// <summary>
        ///     新密码
        /// </summary>
        [Display(Name = "新登录密码", Description = "6-20个字符。")]
        [Field(ControlsType = ControlsType.Password, Width = "80", SortOrder = 4)]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [StringLength(255, MinimumLength = 6, ErrorMessage = "密码长度不能少于6位")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        ///     确认新密码
        /// </summary>
        [Display(Name = "确认新登录密码")]
        [DataType(DataType.Password)]
        [Field(ControlsType = ControlsType.Password, Width = "80", SortOrder = 5)]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Compare("Password", ErrorMessage = "密码与确认密码不相同")]
        public string ConfirmPassword { get; set; }

        public long UserId { get; set; }

        public AutoForm GetView(object id, AutoBaseModel autoModel)
        {
            var result = new AutoForm();
            result = ToAutoForm(new FindLoginPasswordAutoForm());
            result.AlertText = "【找回登录密码】为了更好的保护你的帐号安全，避免您和您的好友受到损失，建议您设置密码";

            result.ButtomHelpText = new List<string>
            {
                "建议确保登录密码与支付密码不同！",
                "建议密码采用字母和数字混合，并且不短于6位。"
            };
            return result;
        }

        public ServiceResult Save(object model, AutoBaseModel autoModel)
        {
            var parameter = (FindLoginPasswordAutoForm)model;

            if (!Resolve<IOpenService>().CheckVerifiyCode(parameter.Mobile, parameter.MobileVerifiyCode.ConvertToLong())
            ) {
                return ServiceResult.FailedWithMessage("验证码错误");
            }

            var view = AutoMapping.SetValue<FindPasswordInput>(parameter);
            var result = Resolve<IUserDetailService>().FindPassword(view);
            if (result.Succeeded) {
                return ServiceResult.Success;
            }

            var resList = result.ErrorMessages.ToList();
            return ServiceResult.FailedWithMessage(resList[0]);
        }
    }
}