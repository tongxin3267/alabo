using System;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Core.UserType.ViewModels {

    public class ViewCityGrade : BaseViewModel {
        public Guid Id { get; set; }

        public Guid UserTypeId { get; set; } = Guid.Parse("71BE65E6-3A64-414D-972E-1A3D4A365102");

        public string Name { get; set; }

        public decimal Contribute { get; set; }
        public decimal Price { get; set; }

        public long Radix { get; set; }

        public string GradePrivileges { get; set; }

        public bool IsDefault { get; set; }

        public string Icon { get; set; }

        public string Remark { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime ModifiedTime { get; set; }
    }
}