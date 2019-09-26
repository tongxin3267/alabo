using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Base.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Framework.Core.Admins.Repositories;
using Alabo.Framework.Core.Admins.Services;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebUis;
using Alabo.Framework.Core.WebUis.Design.AutoForms;
using Alabo.Helpers;
using Alabo.Maps;
using Alabo.Runtime;
using Alabo.Users.Services;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;

namespace _01_Alabo.Cloud.Core.Truncate {

    /// <summary>
    /// 清空数据
    /// </summary>
    [ClassProperty(Name = "清空数据", Icon = IconFlaticon.exclamation)]
    public class TruncateForm : UIBase, IAutoForm {

        /// <summary>
        /// 激活Key
        /// </summary>
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Display(Name = "激活Key")]
        [HelpBlock("请联系您的服务商获取，格式示列: b0000000-2600-4000-bc3f-e00000000000<br/><code>该操作风险极大，请确保已备份Mongodb和Sql数据</code><br/><code>若您非运维人员请勿操作</code>")]
        [Field(ControlsType = ControlsType.TextBox, SortOrder = 1)]
        public string ProjectId { get; set; }

        /// <summary>
        /// 激活秘钥
        /// </summary>
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Display(Name = "激活秘钥")]
        [HelpBlock(" 请联系您的服务商获取，格式示列: NZBZNQIYDT********WVA4HSDNUW6FPMFO<br/><code>该操作风险极大，请确保已备份Mongodb和Sql数据</code><br/><code>若您非运维人员请勿操作</code>")]
        [Field(ControlsType = ControlsType.TextArea, SortOrder = 2)]
        public string Key { get; set; }

        /// <summary>
        /// </summary>
        [Display(Name = "管理员支付密码")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.Password, SortOrder = 300)]
        [HelpBlock("输入当前管理员密码，必须是超级管理员Admin才可以操作该功能<br/><code>该操作风险极大，请确保已备份Mongodb和Sql数据</code><br/><code>若您非运维人员请勿操作</code>")]
        public string PayPassword { get; set; }

        /// <summary>
        /// 平台预留手机
        /// </summary>
        [Display(Name = "平台预留手机")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, SortOrder = 4)]
        [HelpBlock("请联系您的服务商获取<br/><code>该操作风险极大，请确保已备份Mongodb和Sql数据</code><br/><code>若您非运维人员请勿操作</code>")]
        public string Mobile { get; set; }

        /// <summary>
        /// 用户表的名称
        /// </summary>
        [Display(Name = "用户表的名称")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, SortOrder = 5)]
        [HelpBlock("请正确填写确保您是专业的运维人员<br/><code>该操作风险极大，请确保已备份Mongodb和Sql数据</code><br/><code>若您非运维人员请勿操作</code>")]
        public string UserTable { get; set; }

        /// <summary>
        /// 用户表的名称
        /// </summary>
        [Display(Name = "调度中心表名")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, SortOrder = 6)]
        [HelpBlock("调度中心表名,请正确填写确保您是专业的运维人员<br/><code>该操作风险极大，请确保已备份Mongodb和Sql数据</code><br/><code>若您非运维人员请勿操作</code>")]
        public string ScheduleTable { get; set; }

        ///// <summary>
        ///// 平台预留手机
        ///// </summary>
        //[Display(Name = "Sql数据库名")]
        //[Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        //[Field(ControlsType = ControlsType.TextBox, SortOrder = 7)]
        //[HelpBlock("<br/><code>该操作风险极大，请确保已备份Mongodb和Sql数据</code><br/><code>若您非运维人员请勿操作</code>请正确填写确保您是专业的运维人员")]
        //public string SqlTableName { get; set; }

        /// <summary>
        /// Mongodb数据库名称
        /// </summary>
        [Display(Name = "Mongodb数据库名称")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, SortOrder = 8)]
        [HelpBlock("请正确填写确保您是专业的运维人员<br/><code>该操作风险极大，请确保已备份Mongodb和Sql数据</code><br/><code>若您非运维人员请勿操作</code>")]
        public string MongoTableName { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        [Display(Name = "公司名称")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, SortOrder = 8)]
        [HelpBlock("请填写当前项目正确的公司名称<br/><code>该操作风险极大，请确保已备份Mongodb和Sql数据</code><br/><code>若您非运维人员请勿操作</code>")]
        public string CompanyName { get; set; }

        /// <summary>
        ///     手机验证码
        /// </summary>
        [Display(Name = "手机验证码")]
        // [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        // [MinLength(6, ErrorMessage = ErrorMessage.MinStringLength)]
        public string MobileVerifiyCode { get; set; }

        public AutoForm GetView(object id, AutoBaseModel autoModel) {
            var view = new TruncateForm();
            if (id.ConvertToLong() == 10000) {
                view = new TruncateForm {
                    UserTable = "User_User",
                    ScheduleTable = "Task_Schedule",
                    Key = RuntimeContext.Current.WebsiteConfig.OpenApiSetting.Key,
                };
            }

            var autoForm = ToAutoForm(view);
            return autoForm;
        }

        public ServiceResult Save(object model, AutoBaseModel autoModel) {
            var truncateInput = model.MapTo<TruncateForm>();
            if (!HttpWeb.UserName.Equal("admin")) {
                return ServiceResult.FailedWithMessage("当前操作用户名非admin,不能进行该操作");
            }

            var user = Resolve<IAlaboUserService>().GetSingle(HttpWeb.UserId);
            if (user.Status != Status.Normal) {
                return ServiceResult.FailedWithMessage("用户状态不正常,不能进行该操作");
            }
            if (!user.UserName.Equal("admin")) {
                return ServiceResult.FailedWithMessage("当前操作用户名非admin,不能进行该操作");
            }

            if (!truncateInput.UserTable.Equals("User_User")) {
                return ServiceResult.FailedWithMessage("用户数据表填写出错,不能进行该操作");
            }
            if (!truncateInput.MongoTableName.Equals(RuntimeContext.Current.WebsiteConfig.MongoDbConnection.Database)) {
                return ServiceResult.FailedWithMessage("Mongodb数据库表填写出错,不能进行该操作");
            }
            if (!truncateInput.ScheduleTable.Equals("Task_Schedule")) {
                return ServiceResult.FailedWithMessage("调度作业表填写错误,不能进行该操作");
            }
            //if (!truncateInput.ProjectId.Equals(HttpWeb.Token.ProjectId.ToString())) {
            //    return ServiceResult.FailedWithMessage("项目Id填写错误,不能进行该操作");
            //}

            if (!truncateInput.Key.Equals(RuntimeContext.Current.WebsiteConfig.OpenApiSetting.Key)) {
                return ServiceResult.FailedWithMessage("秘钥填写错误,不能进行该操作");
            }

            if (!truncateInput.Mobile.Equals(Resolve<IOpenService>().OpenMobile)) {
                return ServiceResult.FailedWithMessage("平台预留手机号码填写错误,不能进行该操作");
            }

            //if (!truncateInput.CompanyName.Equals(Resolve<IOpenService>().Project.CompanyName)) {
            //    return ServiceResult.FailedWithMessage("公司名称填写错误,不能进行该操作");
            //}

            Repository<ICatalogRepository>().TruncateTable();

            // 先更新数据库脚本
            Resolve<ICatalogService>().UpdateDatabase();

            // 清空缓存
            Resolve<IAdminService>().ClearCache();
            Resolve<ITableService>().Log("清空数据");
            Resolve<IOpenService>().SendRaw(Resolve<IOpenService>().OpenMobile, "您的系统数据已清空，请悉知。");
            return ServiceResult.Success;
        }
    }
}