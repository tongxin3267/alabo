using System;
using Alabo.Domains.Entities;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebUis;
using Alabo.UI;
using Alabo.UI.Design.AutoForms;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Industry.Cms.Articles.UI
{
    [ClassProperty(Name = "文章", Description = "文章")]
    public class ArticleAutoWorkOrder : UIBase, IAutoForm
    {
        public Alabo.UI.Design.AutoForms.AutoForm GetView(object id, AutoBaseModel autoModel)
        {
            throw new NotImplementedException();
        }

        public ServiceResult Save(object model, AutoBaseModel autoModel)
        {
            throw new NotImplementedException();
        }
    }
}