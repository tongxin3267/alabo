using Alabo.Cloud.People.Identities.Domain.Entities;
using Alabo.Data.People.Users.ViewModels;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Mapping;
using Alabo.Users.Enum;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Alabo.Cloud.People.Identities.Domain.Services
{
    /// <summary>
    ///     Class IdentityService.
    /// </summary>
    /// <seealso cref="Alabo.Domains.Services.ServiceBase" />
    /// <seealso cref="IIdentityService" />
    public class IdentityService : ServiceBase<Identity, ObjectId>, IIdentityService
    {
        public IdentityService(IUnitOfWork unitOfWork, IRepository<Identity, ObjectId> repository) : base(unitOfWork,
            repository)
        {
        }

        /// <summary>
        ///     Adds the or update.
        ///     添加实名认证
        /// </summary>
        /// <returns>ServiceResult.</returns>
        //public ServiceResult AddOrUpdate(IdentityView identityView) {
        //    if (identityView == null) {
        //        throw new ValidException("输入不能为空");
        //    }

        //    Identity input = AutoMapping.SetValue<Identity>(identityView);

        //    input.UserId = input.LoginUserId;

        //    var userdetail = Resolve<IUserDetailService>().GetSingle(e => e.UserId == input.UserId);
        //    if (userdetail == null) {
        //        return ServiceResult.FailedWithMessage("当前用户不存在！");
        //    }

        //    var find = GetSingle(r => r.UserId == input.UserId);
        //    if (find == null) {
        //        find = input;
        //    }

        //    find.Status = identityView.Status;
        //    find.UserId = userdetail.UserId;
        //    //find.CheckTime = DateTime.Now;
        //    // 添加或新增实名认证。一个用户只能有一个
        //    if (AddOrUpdate(find, find.Id != ObjectId.Empty)) {
        //        //userdetail.Sex = input.Sex;
        //        var result = Resolve<IUserDetailService>().Update(userdetail);
        //        if (result) {
        //            // 实名认证成功，修该用户的姓名和性别
        //            var user = Resolve<IUserService>().GetSingle(r => r.Id == userdetail.UserId);
        //            if (input.Status == IdentityStatus.Succeed) {
        //                user.Name = input.RealName;
        //                Resolve<IUserService>().Update(user);
        //            }
        //            //修改成功，清空缓存

        //            Resolve<IUserService>().DeleteUserCache(userdetail.UserId, user.UserName);

        //            return ServiceResult.Success;
        //        }
        //    }

        //    return ServiceResult.FailedWithMessage("实名认证失败");
        //}
        public IdentityView GetView(string id)
        {
            var view = new IdentityView();
            if (!id.IsNullOrEmpty())
            {
                var identity = Resolve<IIdentityService>().GetSingle(u => u.Id == id.ToObjectId());
                view = AutoMapping.SetValue<IdentityView>(identity);
            }

            return view;
        }

        public bool IsIdentity(long userId)
        {
            var model = GetSingle(r => r.UserId == userId);
            if (model == null) {
                return false;
            }

            if (model.Status == IdentityStatus.Succeed) {
                return true;
            }

            return false;
        }

        public ServiceResult Identity(Identity view)
        {
            var url =
                $"http://apistore.5ug.com/Api/ApiStore/Identity?IdentifyNum={view.CardNo}&UserName={view.RealName}";
            var httpRequest = (HttpWebRequest)WebRequest.CreateDefault(new Uri(url));
            HttpWebResponse httpResponse = null;

            try
            {
                httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            }
            catch (WebException ex)
            {
                httpResponse = (HttpWebResponse)ex.Response;
            }

            var st = httpResponse.GetResponseStream();
            var reader = new StreamReader(st, Encoding.GetEncoding("utf-8"));
            var temp = reader.ReadToEnd();
            var result = temp.DeserializeJson<IdentityApiResult>();
            var model = Resolve<IIdentityService>().GetSingle(u => u.UserId == view.UserId);
            if (result.Status == "1")
            {
                if (model == null)
                {
                    view.Status = IdentityStatus.Succeed;
                    view.UserId = view.UserId;
                    Resolve<IIdentityService>().Add(view);
                }

                return ServiceResult.Success;
            }

            view.Status = IdentityStatus.Failed;
            if (model == null) {
                Resolve<IIdentityService>().Add(view);
            }

            return ServiceResult.FailedWithMessage(result.Message);

            return ServiceResult.FailedWithMessage("实名认证失败");
        }

        public ServiceResult FaceIdentity(Identity view)
        {
            var url = new Uri("http://apistore.5ug.com/Api/ApiStore/FaceIdentity");
            var httpClient = new HttpClient();
            var body = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"IdCardNum", view.CardNo},
                {"RealName", view.RealName}
                // { "Image", view.FaceImage}
            });

            var response = httpClient.PostAsync(url, body).Result;
            var data = response.Content.ReadAsStringAsync().Result;
            return null; //接口调用成功数据
        }
    }
}