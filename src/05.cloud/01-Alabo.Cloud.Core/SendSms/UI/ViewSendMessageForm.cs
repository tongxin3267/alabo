using Alabo.App.Core.Common.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Framework.Basic.Relations.Domain.Entities;
using Alabo.Mapping;
using Alabo.Regexs;
using Alabo.UI;
using Alabo.UI.AutoForms;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Alabo.App.Share.Messages.ViewsModels
{

    /// <summary>
    /// Class ViewSendMessage.
    /// </summary>
    [ClassProperty(Name = "短信发送", Icon = "fa fa-file", Description = "短信发送")]
    public class ViewSendMessageForm : UIBase , IAutoForm
    {

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        [Display(Name = "手机号码")]
        [RegularExpression(RegularExpressionHelper.ChinaMobile, ErrorMessage = ErrorMessage.NotMatchFormat)]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "110", ListShow = true, EditShow = true, SortOrder = 1004)]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        [Display(Name = "消息内容")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextArea, GroupTabId = 1, Width = "110", ListShow = true,EditShow =true, SortOrder = 1004)]
        [StringLength(500, MinimumLength = 2, ErrorMessage = ErrorMessage.MaxStringLength)]
        public string Message { get; set; }

        public AutoForm GetView(object id, AutoBaseModel autoModel)
        {
            var viewSendMessageId = ToId<long>(id);
            var viewSendMessage = Resolve<IMessageQueueService>().GetViewById(viewSendMessageId);
            var model = AutoMapping.SetValue<ViewSendMessageForm>(viewSendMessage);
            return ToAutoForm(model);
        }

        public ServiceResult Save(object model, AutoBaseModel autoModel)
        {
            var viewSendMessage = AutoMapping.SetValue<MessageQueue>(model);
            var result = Resolve<IMessageQueueService>().AddOrUpdate(viewSendMessage);
            return new ServiceResult(result);
        }
    }
}