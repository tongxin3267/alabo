using Alabo.Domains.Services;

namespace Alabo.App.Core.Api.Domain.Service {

    /// <summary>
    ///     Api处理函数
    /// </summary>
    public interface IApiService : IService {

        /// <summary>
        ///     Api 图片地址
        /// </summary>
        /// <param name="imageUrl"></param>
        string ApiImageUrl(string imageUrl);

        /// <summary>
        ///     将内容当中的图片地址
        ///     等转入远程Api地址
        /// </summary>
        /// <param name="content"></param>
        string ConvertToApiImageUrl(string content);

        /// <summary>
        ///     将内容当中的图片地址
        ///     等转入远程Api地址
        /// </summary>
        /// <param name="instance">对象实例</param>
        object InstanceToApiImageUrl(object instance);

        /// <summary>
        ///     Api 用户头像
        /// </summary>
        /// <param name="userId">用户Id</param>
        string ApiUserAvator(long userId);
    }
}