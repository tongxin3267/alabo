using System.Collections.Generic;
using Alabo.Cloud.People.UserRightss.Domain.Services;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.UI.Design.Widgets;

namespace Alabo.Cloud.People.UserRightss.UI.Widgets
{
    public class SuggestWidget : IWidget
    {
        public object Get(string json)
        {
            var jsonObj = json.ToObject<Dictionary<string, long>>();

            var para = json.ToObject<UserInfoMap>();


            var userId = jsonObj.GetValue("userId");
            var isAdmin = Ioc.Resolve<IUserService>().IsAdmin(userId);
            var rightList = Ioc.Resolve<IUserRightsService>().GetView(userId);
            return rightList;
            //if (isAdmin)
            //{
            //    return rightList;
            //}
            //else
            //{
            //    return rightList.Take(3);
            //}
        }
    }

    public class UserInfoMap
    {
        public long LoginUserId { get; set; }
    }
}