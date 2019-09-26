using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebUis;
using Alabo.Framework.Core.WebUis.Design.AutoForms;
using Alabo.Framework.Core.WebUis.Design.AutoTables;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Mapping;
using Alabo.Maps;
using Alabo.UI;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using ICircleService = Alabo.App.Agent.Circle.Domain.Services.ICircleService;

namespace Alabo.App.Agent.Circle.Domain.Dtos {

    public class CircleView : UIBase, IAutoForm, IAutoTable {
        public string Id { get; set; }

        /// <summary>
        ///     商圈名称
        /// </summary>
        [Display(Name = "商圈名称")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ListShow = true, SortOrder = 1, Width = "150", LabelColor = LabelColor.Brand,
            ControlsType = ControlsType.TextBox, IsShowBaseSerach = true)]
        public string Name { get; set; }

        /// <summary>
        ///     所属区域
        /// </summary>
        [Display(Name = "所属区域")]
        public long RegionId { get; set; }

        /// <summary>
        ///     商圈所属省份
        /// </summary>
        [Display(Name = "省份编码")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public long ProvinceId { get; set; }

        /// <summary>
        ///     商圈所属城市，可以等于null
        /// </summary>
        [Display(Name = "城市编码")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public long CityId { get; set; }

        /// <summary>
        ///     商圈所属区域
        /// </summary>
        [Display(Name = "区县编码")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public long CountyId { get; set; }

        /// <summary>
        ///     区域名称
        /// </summary>
        [Display(Name = "所属区域")]
        [Field(ListShow = true, SortOrder = 5, Width = "350", ControlsType = ControlsType.TextBox,
            IsShowBaseSerach = true)]
        public string RegionName { get; set; }

        /// <summary>
        ///     全称
        /// </summary>
        [Display(Name = "全称")]
        [Field(ListShow = true, SortOrder = 5, Width = "350", ControlsType = ControlsType.TextBox,
            IsShowBaseSerach = true)]
        public string FullName { get; set; }

        public List<TableAction> Actions() {
            return new List<TableAction>();
        }

        public AutoForm GetView(object id, AutoBaseModel autoModel) {
            var str = id.ToString();
            var model = Resolve<ICircleService>().GetSingle(u => u.Id == str.ToObjectId());
            if (model != null) {
                return ToAutoForm(model);
            }

            return ToAutoForm(new Entities.Circle());
        }

        public PageResult<CircleView> PageTable(object query, AutoBaseModel autoModel) {
            var model = Resolve<ICircleService>().GetPagedList(query);
            var circles = new List<CircleView>();

            foreach (var item in model) {
                var cityView = AutoMapping.SetValue<CircleView>(item);
                cityView.RegionName = Resolve<IRegionService>().GetRegionNameById(item.RegionId);
                circles.Add(cityView);
            }

            var result = new PageResult<CircleView>();
            result = AutoMapping.SetValue<PageResult<CircleView>>(model);
            result.Result = circles;

            return result;
        }

        public ServiceResult Save(object model, AutoBaseModel autoModel) {
            var circleView = (CircleView)model;
            var view = circleView.MapTo<Entities.Circle>();
            view.Id = circleView.Id.ToObjectId();
            view.RegionName = Resolve<IRegionService>().GetRegionNameById(view.RegionId);
            var result = Resolve<ICircleService>().AddOrUpdate(view);
            if (result) {
                return ServiceResult.Success;
            }
            return ServiceResult.FailedWithMessage("操作失败，请重试");
        }
    }
}