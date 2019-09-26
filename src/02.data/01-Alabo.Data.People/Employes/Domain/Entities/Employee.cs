using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Repositories.Mongo.Extension;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Alabo.Data.People.Employes.Domain.Entities {

    [BsonIgnoreExtraElements]
    [Table("Admin_Employee")]
    [ClassProperty(Name = "员工")]
    [AutoDelete(IsAuto = true)]
    public class Employee : AggregateMongodbUserRoot<Employee> {

        /// <summary>
        /// 员工名称
        /// </summary>
        [Display(Name = "员工名称")]
        [Required]
        [HelpBlock("填写员工的名称，长度不能超过10个字")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "150", IsShowBaseSerach = true,
            EditShow = true,
            IsShowAdvancedSerach = true, ListShow = true, SortOrder = 1)]
        public string Name { get; set; }

        /// <summary>
        /// 岗位、角色，部门ID
        /// </summary>
        [Display(Name = "岗位")]
        [Required]
        [HelpBlock("请选择岗位")]
        [Field(ControlsType = ControlsType.DropdownList, ApiDataSource = "Api/PostRole/GetKeyValues", GroupTabId = 1, Width = "150", IsShowBaseSerach = true,
           EditShow = true,
          ListShow = true, SortOrder = 1)]
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId PostRoleId { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Display(Name = "是否有效")]
        [Required]
        [HelpBlock("该员工是否有效")]
        [Field(ControlsType = ControlsType.Switch, GroupTabId = 1, Width = "150", IsShowBaseSerach = true,
         EditShow = true,
         ListShow = true, SortOrder = 1)]
        public bool IsEnable { get; set; }

        /// <summary>
        /// 是否为超级管理员
        /// </summary>
        public bool IsSuperAdmin { get; set; }

        /// <summary>
        /// 员工详细说明
        /// </summary>
        [Display(Name = "详细说明")]
        [HelpBlock("输入员工的详细介绍")]
        [Field(ControlsType = ControlsType.TextArea, GroupTabId = 1, Width = "150",
            EditShow = true, SortOrder = 99)]
        public string Intro { get; set; }
    }
}