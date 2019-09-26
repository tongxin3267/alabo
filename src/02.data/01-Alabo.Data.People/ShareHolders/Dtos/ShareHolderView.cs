using System;
using System.Collections.Generic;
using System.Text;
using Alabo.App.Agent.ShareHolders.Domain.Entities;
using Alabo.App.Agent.ShareHolders.Domain.Services;
using Alabo.App.Core.User.Domain.Services;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebUis;
using Alabo.Framework.Core.WebUis.Design.AutoForms;
using Alabo.Domains.Entities;
using Alabo.Extensions;
using Alabo.Maps;
using Alabo.UI;

namespace Alabo.App.Agent.ShareHolders.Domain.Dtos {

    public class ShareHolderView : UIBase, IAutoForm {
        public string Id { get; set; }

        /// <summary>
        ///     股东名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     股东等级
        /// </summary>
        public Guid GradeId { get; set; }

        /// <summary>
        ///     用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        ///     所属用户名
        /// </summary>
        public string UserName { get; set; }

        public AutoForm GetView(object id, AutoBaseModel autoModel) {
            var str = id.ToString();
            var model = Resolve<IShareHolderService>().GetSingle(u => u.Id == str.ToObjectId());
            if (model != null) {
                return ToAutoForm(model);
            }

            return ToAutoForm(new ShareHolder());
        }

        public ServiceResult Save(object model, AutoBaseModel autoModel) {
            var shareHolderView = (ShareHolderView)model;
            var user = Resolve<IUserService>().GetSingle(u => u.UserName == shareHolderView.UserName);

            var view = shareHolderView.MapTo<ShareHolder>();
            if (user == null) {
                return ServiceResult.FailedWithMessage("所属用户名不存在");
            }

            var shareHolder = Resolve<IShareHolderService>().GetSingle(u => u.UserName == view.UserName);
            if (shareHolder != null && shareHolderView.Id.IsNullOrEmpty()) {
                return ServiceResult.FailedWithMessage("该用户已是股东了");
            }

            view.Id = shareHolderView.Id.ToObjectId();
            view.UserId = user.Id;
            var result = Resolve<IShareHolderService>().AddOrUpdate(view);

            if (result) {
                return ServiceResult.Success;
            }
            return ServiceResult.FailedWithMessage("操作失败，请重试");
        }
    }
}