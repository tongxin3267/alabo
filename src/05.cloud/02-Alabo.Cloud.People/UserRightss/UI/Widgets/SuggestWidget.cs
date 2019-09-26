using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Alabo.App.Market.UserRightss.Domain.Dtos;
using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Market.UserRightss.Domain.Services;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Framework.Core.WebUis.Design.Widgets;

namespace Alabo.App.Market.UI.Widgets
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
