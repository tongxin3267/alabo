using Alabo.AutoConfigs.Repositories;
using Alabo.AutoConfigs.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Framework.Basic.Grades.Domain.Configs;
using Alabo.Framework.Basic.Grades.Dtos;
using Alabo.Framework.Core.Enums.Enum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alabo.Framework.Basic.Grades.Domain.Services {

    public class GradeService : ServiceBase, IGradeService {

        public GradeService(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        public UserGradeConfig DefaultUserGrade {
            get {
                var grades = GetUserGradeList();
                if (grades != null) {
                    return grades.FirstOrDefault(r => r.IsDefault);
                }

                var gradeConfig = new UserGradeConfig();
                gradeConfig.SetDefault();
                return new UserGradeConfig();
            }
        }

        public UserGradeConfig GetGrade(Guid gradeId) {
            var userGrades = GetUserGradeList();
            var grade = userGrades.FirstOrDefault(e => e.Id == gradeId);
            if (grade == null) {
                grade = new UserGradeConfig();
            }

            return grade;
        }

        public IList<UserGradeConfig> GetUserGradeList() {
            var userGrades = Resolve<IAlaboAutoConfigService>()
                .GetList<UserGradeConfig>(r => r.Status == Status.Normal);
            if (userGrades.Count == 0) {
                var userType = Resolve<IAlaboAutoConfigService>().GetList<UserTypeConfig>()
                    .FirstOrDefault(r => r.TypeClass == UserTypeEnum.Member);
                var gradeConfig = new UserGradeConfig();
                if (userType != null) {
                    gradeConfig.Id = userType.Id;
                }

                gradeConfig.SetDefault();
                userGrades = Resolve<IAlaboAutoConfigService>()
                    .GetList<UserGradeConfig>(r => r.Status == Status.Normal);
            }

            return userGrades;
        }

        /// <summary>
        ///     获取所有等级Key
        /// </summary>
        /// <param name="key"></param>
        public List<BaseGradeConfig> GetGradeListByKey(string key) {
            var config = Resolve<IAlaboAutoConfigService>().GetConfig(key);
            var t = new BaseGradeConfig();
            var configlist = new List<BaseGradeConfig>();
            if (config != null) {
                if (config.Value != null) {
                    configlist = config.Value.Deserialize(t);
                }
            }

            return configlist;
        }

        /// <summary>
        ///     根据用户类型Id，获取用户类型所有的等级
        /// </summary>
        /// <param name="guid"></param>
        public List<BaseGradeConfig> GetGradeListByGuid(Guid guid) {
            var configList = Repository<IAutoConfigRepository>().GetList(e => e.Type.Contains("GradeConfig"));
            var baseGradeConfigs = new List<BaseGradeConfig>();
            configList.Foreach(r => { baseGradeConfigs.AddRange(r.Value.Deserialize(new BaseGradeConfig())); });
            var userTypeGrades = baseGradeConfigs.Where(r => r.UserTypeId == guid);
            return userTypeGrades.ToList();
        }

        /// <summary>
        ///     根据用户类型Id和等级Id获取用户类型等级
        /// </summary>
        /// <param name="userTypeId">用户类型Id></param>
        /// <param name="gradeId">等级Id</param>
        public BaseGradeConfig GetGradeByUserTypeIdAndGradeId(Guid userTypeId, Guid gradeId) {
            var grades = GetGradeListByGuid(userTypeId);
            var grade = grades?.FirstOrDefault(r => r.Id == gradeId);
            return grade;
        }

        public ServiceResult UpdateUserGrade(UserGradeInput userGradeInput) {
            //TODO 2019年9月22日 重构注释
            //return Resolve<IUpgradeRecordService>()
            //   .ChangeUserGrade(userGradeInput.UserId, userGradeInput.GradeId, userGradeInput.Type);
            return null;
        }

        public dynamic GetGrade(Guid userTypeId, Guid gradeId) {
            throw new NotImplementedException();
        }
    }
}