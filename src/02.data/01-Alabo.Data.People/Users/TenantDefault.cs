using System;
using Alabo.Data.People.Employes.Domain.Services;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Domains.Base.Services;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Framework.Basic.Address.Domain.Services;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Framework.Basic.Grades.Domain.Services;
using Alabo.Framework.Core.Reflections.Interfaces;
using Alabo.Helpers;
using Alabo.Users.Entities;

namespace Alabo.Data.People.Users
{
    /// <summary>
    ///     租户初始数据
    ///     生成管理员、导入员工等
    /// </summary>
    [DefaultInitSort(SortIndex = 1)]
    public class TenantDefault : IDefaultInit
    {
        /// <summary>
        ///     是否租户初始开始
        /// </summary>
        public bool IsTenant => true;

        /// <summary>
        ///     init
        /// </summary>
        public void Init()
        {
            //导入地址数据 ,从js文件导入,如果从高德地图导入会导致地址库不同步的问题
            Ioc.Resolve<IRegionService>().InitRegion();

            //// 导入商圈数据
            //Ioc.Resolve<ICircleService>().Init();

            //// 导入Schedule数据
            //Ioc.Resolve<IScheduleService>().Init();

            //// 初始化所有没有资产账号的会员
            //Ioc.Resolve<IAccountService>().InitAllUserIdsWidthOutAccount();

            // 自动配置相关数据
            Ioc.Resolve<IAutoConfigService>().InitDefaultData();

            //add admin user
            var openService = Ioc.Resolve<IOpenService>();

            var defaultPwd = "111111";
            var defaultUser = new User
            {
                Email = "admin@5ug.com",
                Name = "老板",
                Status = Status.Normal,
                UserName = "admin",
                GradeId = Ioc.Resolve<IGradeService>().DefaultUserGrade.Id,
                Mobile = HttpWeb.Site.Phone
            };
            var passWord = defaultPwd;
            var payPassWord = defaultPwd;
            defaultUser.Detail = new UserDetail
            {
                Password = passWord.ToMd5HashString(),
                PayPassword = payPassWord.ToMd5HashString()
            };
            defaultUser.Map = new UserMap();
            //save admin user
            var userService = Ioc.Resolve<IUserService>();
            if (!userService.ExistsUserName(defaultUser.UserName)) defaultUser = userService.AddUser(defaultUser);

            //add platform user
            var planformUser = new User
            {
                Email = "planform@5ug.com",
                Name = "平台",
                Status = Status.Normal,
                UserName = "planform",
                GradeId = Ioc.Resolve<IGradeService>().DefaultUserGrade.Id,
                Mobile = HttpWeb.Site.Phone
            };
            var planformpassWord = defaultPwd;
            var planformpayPassWord = defaultPwd;
            planformUser.Detail = new UserDetail
            {
                Password = planformpassWord.ToMd5HashString(),
                PayPassword = planformpassWord.ToMd5HashString()
            };

            planformUser.Map = new UserMap();

            //save platform user
            if (!userService.ExistsUserName(planformUser.UserName)) planformUser = userService.AddUser(planformUser);
            userService.Log("初始化化系统管理员成功，请尽快修改您的登录信息");
            userService.Log("初始化化系统平台用户成功，请尽快修改您的登录信息");
            try
            {
                openService.SendRaw(defaultUser.Mobile,
                    $@"恭喜您系统管理员初始成功，用户名为{defaultUser.UserName},登录密码为{passWord},支付密码为{payPassWord},请尽快修改您的登录信息");
                openService.SendRaw(planformUser.Mobile,
                    $@"恭喜您系统平台初始成功，用户名为{planformUser.UserName},登录密码为{planformpassWord},支付密码为{planformpayPassWord},请尽快修改您的登录信息");
            }
            catch (Exception ex)
            {
                userService.Log($"初始化数据-短信发送失败，{ex.Message}");
            }

            // 初始化岗位权限以及员工
            Ioc.Resolve<IPostRoleService>().Init();
        }
    }
}