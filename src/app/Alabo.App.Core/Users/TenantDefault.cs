using System;
using Alabo.App.Core.Admin.Extensions;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.Employes.Domain.Services;
using Alabo.App.Core.User.Domain.Entities;
using Alabo.App.Core.User.Domain.Services;
using Alabo.Core.Extensions;
using Alabo.Domains.Base.Services;
using Alabo.Domains.Enums;
using Alabo.Helpers;
using Alabo.Initialize;

namespace Alabo.App.Core.User {

    /// <summary>
    /// 租户初始数据
    /// 生成管理员、导入员工等
    /// </summary>
    [DefaultInitSort(SortIndex = 1)]
    public class TenantDefault : IDefaultInit {

        /// <summary>
        /// 是否租户初始开始
        /// </summary>
        public bool IsTenant => true;

        /// <summary>
        /// init
        /// </summary>
        public void Init() {
            // 自动配置相关数据
            Ioc.Resolve<IAutoConfigService>().InitDefaultData();

            //add admin user
            var openService = Ioc.Resolve<IOpenService>();

            var defaultPwd = "111111";
            var defaultUser = new Domain.Entities.User {
                Email = "admin@5ug.com",
                Name = "老板",
                Status = Status.Normal,
                UserName = "admin",
                GradeId = Ioc.Resolve<IGradeService>().DefaultUserGrade.Id,
                Mobile = HttpWeb.Site.Phone,
            };
            var passWord = defaultPwd;
            var payPassWord = defaultPwd;
            defaultUser.Detail = new UserDetail {
                Password = passWord.ToMd5HashString(),
                PayPassword = payPassWord.ToMd5HashString()
            };
            defaultUser.Map = new UserMap();
            //save admin user
            var userService = Ioc.Resolve<IUserService>();
            if (!userService.ExistsUserName(defaultUser.UserName)) {
                defaultUser = userService.AddUser(defaultUser);
            }

            //add platform user
            var planformUser = new Domain.Entities.User {
                Email = "planform@5ug.com",
                Name = "平台",
                Status = Status.Normal,
                UserName = "planform",
                GradeId = Ioc.Resolve<IGradeService>().DefaultUserGrade.Id,
                Mobile = HttpWeb.Site.Phone
            };
            var planformpassWord = defaultPwd;
            var planformpayPassWord = defaultPwd;
            planformUser.Detail = new UserDetail {
                Password = planformpassWord.ToMd5HashString(),
                PayPassword = planformpassWord.ToMd5HashString()
            };

            planformUser.Map = new UserMap();

            //save platform user
            if (!userService.ExistsUserName(planformUser.UserName)) {
                planformUser = userService.AddUser(planformUser);
            }
            userService.Log("初始化化系统管理员成功，请尽快修改您的登录信息");
            userService.Log("初始化化系统平台用户成功，请尽快修改您的登录信息");
            try {
                openService.SendRaw(defaultUser.Mobile, $@"恭喜您系统管理员初始成功，用户名为{defaultUser.UserName},登录密码为{passWord},支付密码为{payPassWord},请尽快修改您的登录信息");
                openService.SendRaw(planformUser.Mobile, $@"恭喜您系统平台初始成功，用户名为{planformUser.UserName},登录密码为{planformpassWord},支付密码为{ planformpayPassWord },请尽快修改您的登录信息");
            } catch (Exception ex) {
                userService.Log($"初始化数据-短信发送失败，{ex.Message}");
            }

            // 初始化岗位权限以及员工
            Ioc.Resolve<IPostRoleService>().Init();
        }
    }
}