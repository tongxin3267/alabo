using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Repositories.Mongo.Extension;
using Alabo.Extensions;
using Alabo.Framework.Basic.Address.Domain.Entities;
using Alabo.Framework.Basic.Address.Domain.Services;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebUis;
using Alabo.Framework.Core.WebUis.Design.AutoForms;
using Alabo.Framework.Core.WebUis.Design.AutoTables;
using Alabo.Maps;
using Alabo.Regexs;
using Alabo.UI;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace Alabo.Data.People.Users.UI.AutoFrom {

    /// <summary>
    ///     用户地址
    ///     用于管理员 查看所有用户的地址
    /// </summary>
    [ClassProperty(Name = "用户地址", PageType = ViewPageType.List, Icon = IconFontawesome.address_book,
        SideBarType = SideBarType.UserAddressSideBar)]
    public class UserAddressAdminAutoFrom : UIBase, IAutoTable<UserAddressAdminAutoFrom>, IAutoForm {

        #region 属性

        /// <summary>
        /// id
        /// </summary>
        [Required(ErrorMessage = "Id不能为空")]
        [Key]
        [Display(Name = "ID", Order = 1)]
        [Field(ControlsType = ControlsType.Hidden, ListShow = false, EditShow = true, SortOrder = 1, Width = "50")]
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId Id { get; set; }

        /// <summary>
        ///     收货人名称
        /// </summary>
        [Display(Name = "收货姓名")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [HelpBlock("请您务必输入收货人姓名")]
        [Field(ControlsType = ControlsType.TextBox, IsShowBaseSerach = true, Width = "100", ListShow = true, EditShow = true,
            SortOrder = 2)]
        public string Name { get; set; }

        /// <summary>
        ///     手机号码
        /// </summary>
        [Display(Name = "手机号码")]
        [RegularExpression(RegularExpressionHelper.ChinaMobile, ErrorMessage = ErrorMessage.NotMatchFormat)]
        [Field(ControlsType = ControlsType.Numberic, IsShowBaseSerach = true, IsShowAdvancedSerach = true, EditShow = true, Width = "90",
            ListShow = true, SortOrder = 3)]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [HelpBlock("请您务必输入收货人手机号码")]
        public string Mobile { get; set; }

        /// <summary>
        ///     是否默认地址
        /// </summary>
        [Display(Name = "是否默认")]
        [Field(ControlsType = ControlsType.Switch, ListShow = true, EditShow = true, SortOrder = 4)]
        public bool IsDefault { get; set; } = false;

        /// <summary>
        ///     区域Id
        /// </summary>
        [Display(Name = "所在区域")]
        [Field(ControlsType = ControlsType.CityDropList, ListShow = false, EditShow = true, SortOrder = 5)]
        [HelpBlock("请您务必选择所在区域")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public long RegionId { get; set; }

        /// <summary>
        ///     地址方式
        /// </summary>
        [Display(Name = "地址类型")]
        //[Field(EditShow = false, Width = "100", ListShow = true, SortOrder = 5)]
        [HelpBlock("请选择您的地址类型")]
        public AddressLockType Type { get; set; }

        /// <summary>
        ///     详细街道地址，不需要重复填写省/市/区
        /// </summary>
        [Display(Name = "详细地址")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [StringLength(40, ErrorMessage = ErrorMessage.MaxStringLength)]
        [Field(ControlsType = ControlsType.TextArea, IsShowAdvancedSerach = true, EditShow = true, ListShow = true, Width = "150", SortOrder = 7)]
        [HelpBlock("请您务必详细地址")]
        public string Address { get; set; }

        /// <summary>
        ///     邮政编码
        /// </summary>
        [Display(Name = "邮政编码")]
        [RegularExpression(@"[0-9]{6}$", ErrorMessage = ErrorMessage.NotMatchFormat)]
        //[Field(ControlsType = ControlsType.Numberic, IsShowBaseSerach = false, IsShowAdvancedSerach = false, Width = "100",
        //    ListShow = false, EditShow = false)]
        [HelpBlock("请输入所在地邮政编码")]
        public string ZipCode { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        [Display(Name = "用户Id")]
        [Field(ControlsType = ControlsType.Hidden, EditShow = true)]
        public long UserId { get; set; }

        /// <summary>
        ///    所属账号
        /// </summary>
        [Display(Name = "用户名")]
        [Field(ControlsType = ControlsType.TextBox, IsShowBaseSerach = true, Width = "100", ListShow = true,
            SortOrder = 2)]
        public string UserName { get; set; }

        /// <summary>
        /// 所属用户id
        /// </summary>
        [Display(Name = "所属用户Id")]
        [Field(ControlsType = ControlsType.Hidden, EditShow = true)]
        public long RootUserId { get; set; }

        /// <summary>
        ///     详细地址描述，比如广东东莞南城太沙路121号
        /// </summary>
        [Display(Name = "区域")]
        [Field(ControlsType = ControlsType.TextArea, TableDispalyStyle = TableDispalyStyle.Code, Width = "250",
            EditShow = false,
            ListShow = true, SortOrder = 6)]
        //[HelpBlock("请输入具体地址信息，如：广东东莞南城太沙路121号")]
        public string AddressDescription { get; set; }

        /// <summary>
        ///     省份编码
        /// </summary>
        public long Province { get; set; }

        /// <summary>
        ///     城市编码
        /// </summary>
        public long City { get; set; }

        #endregion 属性

        public List<TableAction> Actions() {
            return new List<TableAction>
            {
                ToLinkAction("编辑", "Edit",TableActionType.ColumnAction),
                ToLinkAction("删除", "/Api/UserAddress/QueryDelete",ActionLinkType.Delete,TableActionType.ColumnAction)
            };
        }

        public AutoForm GetView(object id, AutoBaseModel autoModel) {
            var result = new AutoForm();

            if (id.ToString().ToObjectId() != ObjectId.Empty) {
                var model = Resolve<IUserAddressService>().GetSingle(id);
                if (model == null) {
                    result = ToAutoForm(new UserAddressAdminAutoFrom());
                }

                var resultModel = model.MapTo<UserAddressAdminAutoFrom>();
                resultModel.RootUserId = model.UserId;
                result = ToAutoForm(resultModel);
            } else {
                result = ToAutoForm(new UserAddressAdminAutoFrom());
            }
            result.AlertText = "【编辑地址】请您认真填写收货人姓名、手机及其详细地址，便于确认收货的地址";

            result.ButtomHelpText = new List<string> {
                "建议您务必输入收货人姓名、手机号码",
                "建议您务必选择正确的区域及输入详细地址",
            };

            return result;
        }

        /// <summary>
        /// PageTable
        /// </summary>
        /// <param name="query"></param>
        /// <param name="autoModel"></param>
        /// <returns></returns>
        public PageResult<UserAddressAdminAutoFrom> PageTable(object query, AutoBaseModel autoModel) {
            //var dic = HttpWeb.HttpContext.ToDictionary();
            //dic = dic.RemoveKey("type");// 移除该type否则无法正常lambda

            var result = Resolve<IUserAddressService>().GetPagedList(query);
            var region = Resolve<IRegionService>();//.GetList(s => result.Select(r => r.UserId).Contains(s.Id));
            //var page = result.MapTo<PagedList<UserAddressAdminAutoFrom>>();
            var page = new PageResult<UserAddressAdminAutoFrom>() {
                CurrentSize = result.CurrentSize,
                PageCount = result.PageCount,
                PageIndex = result.PageIndex,
                PageSize = result.PageSize,
                RecordCount = result.RecordCount,
                Result = result.Result.MapTo<PagedList<UserAddressAdminAutoFrom>>()
            };

            page.Result.ForEach(s => {
                var regionStr = region.GetFullName(s.RegionId);
                s.AddressDescription = regionStr ?? string.Empty;
            });
            return page;
        }

        public ServiceResult Save(object model, AutoBaseModel autoModel) {
            var input = (UserAddressAdminAutoFrom)model;
            var inputModel = input.MapTo<UserAddress>();
            inputModel.UserId = input.RootUserId;
            //详细地址传入的是null 给跪
            inputModel.AddressDescription = inputModel.Address;
            var result = Resolve<IUserAddressService>().AddOrUpdate(inputModel);

            return result ? ServiceResult.Success : ServiceResult.Failed;
        }
    }
}