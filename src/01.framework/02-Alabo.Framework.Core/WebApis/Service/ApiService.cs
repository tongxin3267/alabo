using Alabo.AutoConfigs;
using Alabo.AutoConfigs.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.Mapping;
using System;
using System.Linq;

namespace Alabo.Framework.Core.WebApis.Service
{
    /// <summary>
    ///     Api处理函数
    /// </summary>
    public class ApiService : ServiceBase, IApiService
    {
        public ApiService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        ///     Api图片地址
        /// </summary>
        /// <param name="imageUrl"></param>
        public string ApiImageUrl(string imageUrl)
        {
            //var webSite = Resolve<IAlaboConfigService>().GetValue<WebSiteConfig>();

            //if (imageUrl != null) {
            //    //var path = string.Empty;
            //    //if (imageUrl.Contains("wwwroot")) {
            //    //    path = FileHelper.RootPath + imageUrl;
            //    //} else {
            //    //    path = FileHelper.WwwRootPath + imageUrl;
            //    //}

            //    ////if (!File.Exists(path)) {
            //    ////    imageUrl = Service<IAlaboConfigService>().GetValue<WebSiteConfig>().NoPic;
            //    ////}
            //} else {
            //    imageUrl = Resolve<IAlaboConfigService>().GetValue<WebSiteConfig>().NoPic;
            //}

            if (imageUrl != null)
            {
                if (!imageUrl.Contains("http", StringComparison.CurrentCultureIgnoreCase)) {
                    imageUrl = $"{HttpWeb.ServiceHostUrl}/{imageUrl}";
                }
            }
            else
            {
                imageUrl = Resolve<IAlaboAutoConfigService>().GetValue<WebSiteConfig>().NoPic;
            }

            if (imageUrl.Contains("///")) {
                return imageUrl.Replace("//", "/");
            }

            return imageUrl;
        }

        /// <summary>
        ///     将内容转为远程图片地址
        ///     百度编辑器上传的图片可以这样处理
        /// </summary>
        /// <param name="content"></param>
        public string ConvertToApiImageUrl(string content)
        {
            if (!content.IsNullOrEmpty())
            {
                var webSite = Resolve<IAlaboAutoConfigService>().GetValue<WebSiteConfig>();
                content = content.ToLower(); //先转小写
                //双引号
                content = content.Replace("src=\"/uploads/", "src=\"" + webSite.ApiImagesUrl + "/uploads/");
                //单引号
                content = content.Replace("src='/uploads/", "src='" + webSite.ApiImagesUrl + "/uploads/");
                //双引号
                content = content.Replace("src=\"/wwwroot/uploads/",
                    "src=\"" + webSite.ApiImagesUrl + "/wwwroot/uploads/");
                //单引号
                content = content.Replace("src='/wwwroot/uploads/",
                    "src='" + webSite.ApiImagesUrl + "/wwwroot/uploads/");
                content = content.Replace("src=\"/upload/", "src=\"" + webSite.ApiImagesUrl + "/upload/");
                content = content.Replace("src=\"/wwwroot/upload/",
                    "src=\"" + webSite.ApiImagesUrl + "/wwwroot/upload/");
            }

            return content;
        }

        /// <summary>
        ///     头像地址
        /// </summary>
        /// <param name="userId">用户Id</param>
        public string ApiUserAvator(long userId)
        {
            var webSite = Resolve<IAlaboAutoConfigService>().GetValue<WebSiteConfig>();
            //var avatorUrl = Resolve<IUserDetailService>().GetSingle(u => u.UserId == userId)?.Avator;
            //if (avatorUrl.IsNullOrEmpty()) {
            //    avatorUrl = @"/wwwroot/static/images/avator/man_64.png";
            //}

            //if (!avatorUrl.Contains("http", StringComparison.CurrentCultureIgnoreCase)) {
            //    avatorUrl = $"{webSite.ApiImagesUrl}/{avatorUrl}";
            //}

            ////var path = FileHelper.WwwRootPath + avatorUrl;
            ////if (!File.Exists(path)) {
            ////    avatorUrl = $@"/wwwroot/static/images/avator/man_64.png";
            ////}

            return string.Empty;
        }

        /// <summary>
        ///     将实例图片地址替换
        /// </summary>
        /// <param name="instance"></param>
        public object InstanceToApiImageUrl(object instance)
        {
            var inputType = instance.GetType();

            var inputPropertyInfo = inputType.GetPropertiesFromCache();

            foreach (var item in inputPropertyInfo)
            {
                var property =
                    inputPropertyInfo.FirstOrDefault(r => r.Name.Equals(item.Name, StringComparison.OrdinalIgnoreCase));
                if (property != null)
                {
                    var value = property.GetValue(instance);
                    value = ConvertToApiImageUrl(value.ToStr()); // 替换图片地址
                    AutoMapping.SetPropertyInfoValue(instance, item, value);
                }
            }

            return instance;
        }
    }
}