using System.Collections.Generic;
using Alabo.Extensions;

namespace Alabo.App.Core.User {

    /// <summary>
    ///     用户扩展
    /// </summary>
    public static class UserExtension {

        /// <summary>
        ///     To the user object.
        ///     获取Url参数的中的，当前登录用户
        /// </summary>
        /// <param name="obj">The object.</param>
        public static Users.Entities.User ToUserObject(this object obj) {
            var dic = obj.ToObject<Dictionary<string, string>>();
            Users.Entities.User user = null;
            dic.TryGetValue("BasicUser", out var value);
            if (!value.IsNullOrEmpty()) {
                user = value.ToObject<Users.Entities.User>();
            }

            return user;
        }
    }
}