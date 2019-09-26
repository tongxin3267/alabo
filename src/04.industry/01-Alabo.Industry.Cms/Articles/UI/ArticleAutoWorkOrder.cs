using System;
using Alabo.Domains.Entities;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebUis;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Industry.Cms.Articles.UI {

    [ClassProperty(Name = "文章", Description = "文章")]
    public class ArticleAutoWorkOrder : UIBase, Alabo.Framework.Core.WebUis.Design.AutoForms.IAutoForm {

        public Alabo.Framework.Core.WebUis.Design.AutoForms.AutoForm GetView(object id, AutoBaseModel autoModel) {
            throw new NotImplementedException();
        }

        public ServiceResult Save(object model, AutoBaseModel autoModel) {
            throw new NotImplementedException();
        }
    }
}