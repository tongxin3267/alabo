using Alabo.Data.People.Circles.Domain.Entities;
using Alabo.Data.People.Circles.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Mapping;
using Alabo.UI;
using Alabo.UI.Design.AutoForms;
using Alabo.UI.Design.AutoTables;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Data.People.Circles.UI
{
    /// <summary>
    ///     商圈
    /// </summary>
    [ClassProperty(Name = "商圈")]
    public class CircleForm : Circle, IAutoForm, IAutoTable<CircleForm>
    {
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
        ///     转换成Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AutoForm GetView(object id, AutoBaseModel autoModel)
        {
            var circleId = ToId<long>(id);
            var circleView = Resolve<ICircleService>().GetViewById(circleId);
            var model = AutoMapping.SetValue<CircleForm>(circleView);
            return ToAutoForm(model);
        }

        public ServiceResult Save(object model, AutoBaseModel autoModel)
        {
            var circleView = AutoMapping.SetValue<Circle>(model);
            var result = Resolve<ICircleService>().AddOrUpdate(circleView);
            return new ServiceResult(result);
        }

        public PageResult<CircleForm> PageTable(object query, AutoBaseModel autoModel)
        {
            throw new System.NotImplementedException();
        }

        public List<TableAction> Actions()
        {
            throw new System.NotImplementedException();
        }
    }
}