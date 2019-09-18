using System;
using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.UI;
using Alabo.UI.AutoForms;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.UserType.Domain.Dtos {

    /// </summary>
    [ClassProperty(Name = "测试区域", Icon = "fa fa-puzzle-piece", Description = "测试区域", ListApi = "Api/UserAdmin/UserList",
        PageType = ViewPageType.List, PostApi = "Api/User/AddUser")]
    public class ViewRegion : UIBase, IAutoForm {

        /// <summary>
        ///     用户名（以字母开头，包括a-z,0-9和_）：[a-zA-z][a-zA-Z0-9_]{2,15}
        /// </summary>
        [Display(Name = "区县选择")]
        [Field(ControlsType = ControlsType.CountyDropList, EditShow = true)]
        public string CountyId { get; set; }

        [Display(Name = "城市选择")]
        [Field(ControlsType = ControlsType.CityDropList, EditShow = true)]
        public string CityId { get; set; }

        [Display(Name = "省份选择")]
        [Field(ControlsType = ControlsType.ProvinceDropList, EditShow = true)]
        public string ProvinceId { get; set; }

        public AutoForm GetView(object id, AutoBaseModel autoModel) {
            var view = new ViewRegion();
            return ToAutoForm(view);
        }

        public ServiceResult Save(object model, AutoBaseModel autoModel) {
            throw new NotImplementedException();
        }
    }
}