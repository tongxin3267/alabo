using Alabo.Domains.Entities;
using Alabo.UI;
using Alabo.Web.Mvc.Attributes;
using System;
using IAutoForm = Alabo.Core.WebUis.Design.AutoForms.IAutoForm;

namespace Alabo.App.Cms.Articles.UI {

    [ClassProperty(Name = "文章", Description = "文章")]
    public class ArticleAutoWorkOrder : Alabo.UI.UIBase, Alabo.Core.WebUis.Design.AutoForms.IAutoForm {

        public Alabo.Core.WebUis.Design.AutoForms.AutoForm GetView(object id, AutoBaseModel autoModel) {
            throw new NotImplementedException();
        }

        public ServiceResult Save(object model, AutoBaseModel autoModel) {
            throw new NotImplementedException();
        }
    }
}