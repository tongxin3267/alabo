using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Mapping;
using Alabo.UI;
using Alabo.Web.Mvc.Attributes;
using Alabo.App.Agent.Circle.Domain.Services;
using Alabo.App.Agent.Circle.Domain.Entities;
using Alabo.Core.WebApis;
using Alabo.Core.WebUis;
using Alabo.Core.WebUis.Design.AutoForms;
using Alabo.Validations;

namespace Alabo.Framework.Basic.Relations.Domain.Entities {

    /// <summary>
    ///     商圈
    /// </summary>
    [BsonIgnoreExtraElements]
    [Table("Basic_Circle")]
    [ClassProperty(Name = "商圈", Icon = IconFlaticon.map_location, SideBarType = SideBarType.ControlSideBar,
        PageType = ViewPageType.List, PostApi = "Api/Circle/CircleList")]
    public class CircleForm : UIBase, IAutoForm {

        /// <summary>
        ///     商圈名称
        /// </summary>
        [Display(Name = "商圈名称")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ListShow = true, SortOrder = 1, Width = "150", LabelColor = LabelColor.Brand,
            ControlsType = ControlsType.TextBox, IsShowBaseSerach = true)]
        public string Name { get; set; }

        /// <summary>
        ///     商圈所属省份
        /// </summary>
        [Display(Name = "省份编码")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ListShow = true, SortOrder = 2, Width = "90", TableDispalyStyle = TableDispalyStyle.Code,
            ControlsType = ControlsType.TextBox, IsShowAdvancedSerach = true)]
        public long ProvinceId { get; set; }

        /// <summary>
        ///     商圈所属城市，可以等于null
        /// </summary>
        [Display(Name = "城市编码")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ListShow = true, SortOrder = 3, Width = "90", TableDispalyStyle = TableDispalyStyle.Code,
            ControlsType = ControlsType.TextBox, IsShowAdvancedSerach = true)]
        public long CityId { get; set; }

        /// <summary>
        ///     商圈所属区域
        /// </summary>
        [Display(Name = "区县编码")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ListShow = true, SortOrder = 4, Width = "90", TableDispalyStyle = TableDispalyStyle.Code,
            ControlsType = ControlsType.TextBox, IsShowAdvancedSerach = true)]
        public long CountyId { get; set; }

        /// <summary>
        ///     全称
        /// </summary>
        [Display(Name = "全称")]
        [Field(ListShow = true, SortOrder = 5, Width = "350", ControlsType = ControlsType.TextBox,
            IsShowBaseSerach = true)]
        public string FullName { get; set; }

        /// <summary>
        /// 转换成Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AutoForm GetView(object id, AutoBaseModel autoModel) {
            var circleId = ToId<long>(id);
            var circleView = Resolve<ICircleService>().GetViewById(circleId);
            var model = AutoMapping.SetValue<CircleForm>(circleView);
            return ToAutoForm(model);
        }

        public ServiceResult Save(object model, AutoBaseModel autoModel) {
            var circleView = AutoMapping.SetValue<Circle>(model);
            var result = Resolve<ICircleService>().AddOrUpdate(circleView);
            return new ServiceResult(result);
        }
    }
}