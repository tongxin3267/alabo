using Alabo.Domains.Entities;
using Alabo.UI;
using Alabo.Web.Mvc.Attributes;
using System;
using IAutoForm = Alabo.UI.AutoForms.IAutoForm;

namespace Alabo.App.Cms.Articles.UI {

    [ClassProperty(Name = "文章", Description = "文章")]
    public class ArticleAutoWorkOrder : Alabo.UI.UIBase, IAutoForm {

        public Alabo.UI.AutoForms.AutoForm GetView(object id, AutoBaseModel autoModel) {
            throw new NotImplementedException();
        }

        public ServiceResult Save(object model, AutoBaseModel autoModel) {
            throw new NotImplementedException();
        }
    }
}