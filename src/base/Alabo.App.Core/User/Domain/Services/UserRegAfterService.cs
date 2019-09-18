﻿using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.App.Core.Admin.Domain.Services;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.Tasks.Domain.Services;
using Alabo.App.Core.UserType.Domain.Services;
using Alabo.App.Core.UserType.Modules.Partner;
using Alabo.Core.Enums.Enum;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.Schedules;

namespace Alabo.App.Core.User.Domain.Services {

    public class UserRegAfterService : ServiceBase, IUserRegAfterService {

        public UserRegAfterService(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        /// <summary>
        ///     注册后操作
        /// </summary>
        /// <param name="user">用户</param>
        public void AddBackJob(Entities.User user) {
            var backJobParameter = new BackJobParameter {
                ModuleId = TaskQueueModuleId.AfterUserReg,
                CheckLastOne = true,
                ServiceName = typeof(IUserRegAfterService).Name,
                Parameter = user.Id,
                UserId = user.Id,
                Method = "AfterUserRegTask"
            };
            Resolve<ITaskQueueService>().AddBackJob(backJobParameter);
        }

        /// <summary>
        /// 注册后事件，通过队列来处理
        /// </summary>
        /// <param name="userId"></param>
        public void AfterUserRegTask(long userId) {
            var user = Resolve<IUserService>().GetUserDetail(userId);
            if (user != null) {
                // 生成用户二维码
                Resolve<IUserQrCodeService>().CreateCode(user);

                // 添加内部合伙人关系
                AddParnter(user);

                //更新：User_Map表：更新会员本身的团队信息 LevelNumber,ChildNode,TeamNumber 字段
                Ioc.Resolve<IUserMapService>().UpdateTeamInfo(user.Id);

                //团队等级自动更新模块
                var backJobParameter = new BackJobParameter {
                    ModuleId = TaskQueueModuleId.TeamUserGradeAutoUpdate,
                    UserId = user.Id,
                    ServiceName = typeof(IGradeInfoService).Name,
                    Method = "TeamUserGradeAutoUpdate",
                    Parameter = user.Id,
                };
                // Resolve<ITaskQueueService>().AddBackJob(backJobParameter);

                Resolve<IGradeInfoService>().TeamUserGradeAutoUpdate(userId);

                // 继承IUserRegAfter的方法
                ExecuteUserAfter(user);
            }
        }

        #region 继承IUserRegAfter的方法

        private void ExecuteUserAfter(Entities.User user) {
            // 继承IUserRegAfter的方法
            var userAfterMethods = new List<IUserRegAfter>();
            var types = Resolve<ITypeService>().GetAllTypeByInterface(typeof(IUserRegAfter));
            foreach (var item in types.ToList()) {
                var config = Activator.CreateInstance(item);
                if (config is IUserRegAfter set) {
                    userAfterMethods.Add(set);
                }
            }

            foreach (var userRegItem in userAfterMethods.OrderBy(r => r.SortOrder)) {
                userRegItem.Excecute(user);
            }
        }

        #endregion 继承IUserRegAfter的方法

        #region 与内部合伙人关联

        /// <summary>
        ///     与内部合伙人关联
        /// </summary>
        /// <param name="user">用户</param>
        private void AddParnter(Entities.User user) {
            var config = Resolve<IAutoConfigService>().GetValue<PartnerConfig>();
            if (config.AfterUserRegAddPanter) {
                //会员注册后自动添加合伙人
                var parnterUserTypeId = UserTypeEnum.Partner.GetFieldId();
                Resolve<ITypeUserService>().SetTypeUser(user.Id, parnterUserTypeId);
            }
        }

        #endregion 与内部合伙人关联
    }
}