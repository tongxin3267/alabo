using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Alabo.App.Core.User.Domain.Repositories;
using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Core.UserType.Domain.Entities.Extensions;
using Alabo.App.Core.UserType.Domain.Enums;
using Alabo.App.Core.UserType.Domain.Repositories;
using Alabo.Core.Enums.Enum;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Enums;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Exceptions;
using Alabo.Extensions;
using Alabo.Reflections;
using Alabo.Runtime;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.UserType.Domain.Services {

    public class UserTypeService : ServiceBase<Entities.UserType, long>, IUserTypeService {
        private static readonly string _singleUserTypeCacheKey = "UserTypeCacheKey";
        private readonly IUserRepository _userRepository;

        private readonly IUserTypeRepository _userTypeRepository;

        public UserTypeService(IUnitOfWork unitOfWork, IRepository<Entities.UserType, long> repository) : base(
            unitOfWork, repository) {
            _userTypeRepository = Repository<IUserTypeRepository>();
            _userRepository = Repository<IUserRepository>();
        }

        public Entities.UserType GetSingle(long id) {
            var cacheKey = _singleUserTypeCacheKey + "_Id_" + id;
            if (!ObjectCache.TryGet(cacheKey, out Entities.UserType userType)) {
                userType = _userTypeRepository.GetSingle(id);
                userType.UserTypeExtensions = userType.Extensions.DeserializeJson<UserTypeExtensions>();
                if (userType != null) {
                    ObjectCache.Set(cacheKey, userType);
                }
            }

            return userType;
        }

        public Entities.UserType GetSingle(long userId, UserTypeEnum userTypeEnum) {
            var userTypeId = userTypeEnum.GetFieldId();
            return GetSingle(userId, userTypeId);
        }

        public Entities.UserType GetSingle(long userId, Guid userTypeId) {
            var cacheKey = $"{_singleUserTypeCacheKey}_{userId}_{userTypeId}";
            ;
            if (!ObjectCache.TryGet(cacheKey, out Entities.UserType userType)) {
                userType = _userTypeRepository.GetSingle(userId, userTypeId);
                if (userType != null) {
                    userType.UserTypeExtensions = userType.Extensions.DeserializeJson<UserTypeExtensions>();
                    ObjectCache.Set(cacheKey, userType);
                }
            }

            return userType;
        }

        public Entities.UserType GetSingle(Guid userTypeId, long entityId) {
            var cacheKey = $"{_singleUserTypeCacheKey}_{entityId}_{userTypeId}";
            ;
            if (!ObjectCache.TryGet(cacheKey, out Entities.UserType userType)) {
                userType = _userTypeRepository.GetSingle(userTypeId, entityId);
                userType.UserTypeExtensions = userType.Extensions.DeserializeJson<UserTypeExtensions>();
                if (userType != null) {
                    ObjectCache.Set(cacheKey, userType);
                }
            }

            return userType;
        }

        public Entities.UserType GetSingleDetail(long id) {
            var userType = GetSingle(id);
            if (userType != null) {
                // userType.Detail = Service<IUserTypeAdminService>().GetSingle(r => r.UserTypeId == userType.Id);
            }

            return userType;
        }

        /// <summary>
        ///     用户所包含的所有等级，分润检查时会启用
        /// </summary>
        /// <param name="userId">用户Id</param>
        public IList<Guid> UserAllGradeId(long userId) {
            var cacheKey = "UserAllGradeId" + userId;
            if (!ObjectCache.TryGet(cacheKey, out List<Guid> result)) {
                var user = Resolve<IUserService>().GetSingle(userId);
                if (user != null) {
                    result.Add(user.GradeId);
                    var typeGradeIds = _userTypeRepository.UserAllGradeId(userId);
                    result.AddRange(typeGradeIds);
                    ObjectCache.Set(cacheKey, result);
                }
            }

            return result;
        }

        public User.Domain.Entities.User GetUserTypeUser(Guid userTypeId, long eneityId) {
            User.Domain.Entities.User user = null;
            ;
            var userType = GetSingle(userTypeId, eneityId);
            if (userType != null) {
                user = Resolve<IUserService>().GetSingle(userType.UserId);
            }

            return user;
        }

        public IEnumerable<Type> GetAllTypes() {
            var cacheKey = _singleUserTypeCacheKey + "_AllTypes_";
            if (!ObjectCache.TryGet(cacheKey, out IEnumerable<Type> types)) {
                types = RuntimeContext.Current.GetPlatformRuntimeAssemblies().SelectMany(a => a.GetTypes()
                    .Where(t => t.GetInterfaces().Contains(typeof(IUserType)) && t.FullName.EndsWith("UserType")));
                //排序，根据SordOrder从小到大排列
                types = types.OrderBy(r =>
                    r.GetTypeInfo().GetAttribute<UserTypeModulesAttribute>() != null
                        ? r.GetTypeInfo().GetAttribute<UserTypeModulesAttribute>().SortOrder
                        : 1);
                ObjectCache.Set(cacheKey, types);
            }

            return types;
        }

        public string GetUserTypeKey(Guid userTypeId) {
            var cacheKey = _singleUserTypeCacheKey + "UserTypeKeys";
            //缓存处理
            if (!ObjectCache.TryGet(cacheKey, out IDictionary<Guid, string> dictionary)) {
                dictionary = new Dictionary<Guid, string>();
                foreach (UserTypeEnum item in Enum.GetValues(typeof(UserTypeEnum))) {
                    var guid = item.GetCustomAttr<FieldAttribute>().GuidId.ToGuid();
                    var key = $"Alabo.App.Core.UserType.Modules.{item.ToString()}.{item.ToString()}UserType";
                    dictionary.Add(guid, key);
                }

                if (dictionary != null) {
                    ObjectCache.Set(cacheKey, dictionary);
                }
            }

            var userTypeKey = string.Empty;
            dictionary?.TryGetValue(userTypeId, out userTypeKey);
            return userTypeKey;
        }

        /// <summary>
        ///     根据用户类型等级获取用户类型的TypeId
        /// </summary>
        /// <param name="gradeKey"></param>
        public Guid GetUserTypeIdByGradeKey(string gradeKey) {
            var cacheKey = _singleUserTypeCacheKey + "UserTypeIds";
            //缓存处理
            if (!ObjectCache.TryGet(cacheKey, out IDictionary<string, Guid> dictionary)) {
                dictionary = new Dictionary<string, Guid>();
                var userType = gradeKey.Replace("GradeConfig", "UserType");
                foreach (UserTypeEnum item in Enum.GetValues(typeof(UserTypeEnum))) {
                    var guid = item.GetCustomAttr<FieldAttribute>().GuidId.ToGuid();
                    var key = $"Alabo.App.Core.UserType.Modules.{item.ToString()}.{item.ToString()}GradeConfig";
                    dictionary.Add(key, guid);
                }

                if (dictionary != null) {
                    ObjectCache.Set(cacheKey, dictionary);
                }
            }

            var userTypeId = Guid.Empty;
            dictionary?.TryGetValue(gradeKey, out userTypeId);
            return userTypeId;
        }

        public void Update(Entities.UserType userType) {
            _userTypeRepository.UpdateSingle(userType);
        }

        /// <summary>
        ///     根据用户类型Id获取SideBar
        /// </summary>
        /// <param name="userTypeId">用户类型Id</param>
        public SideBarType GetSideBarByKey(Guid userTypeId) {
            var cacheKey = _singleUserTypeCacheKey + "UserTypeEnum";
            //缓存处理
            if (!ObjectCache.TryGet(cacheKey, out IDictionary<Guid, UserTypeEnum> dictionary)) {
                dictionary = new Dictionary<Guid, UserTypeEnum>();
                foreach (UserTypeEnum item in Enum.GetValues(typeof(UserTypeEnum))) {
                    var guid = item.GetCustomAttr<FieldAttribute>().GuidId.ToGuid();
                    dictionary.Add(guid, item);
                }

                if (dictionary != null) {
                    ObjectCache.Set(cacheKey, dictionary);
                }
            }

            var userTypeKey = UserTypeEnum.Customer;
            dictionary?.TryGetValue(userTypeId, out userTypeKey);
            var sideBarType = GetSideBarType(userTypeKey);
            return sideBarType;
        }

        /// <summary>
        ///     根据用户Id，获取所有的用户类型
        /// </summary>
        /// <param name="userId">用户Id</param>
        public IList<Entities.UserType> GetAllUserType(long userId) {
            return GetList(r => r.UserId == userId && r.Status == UserTypeStatus.Success).ToList();
        }

        /// <summary>
        ///     检查用户是否包含所属的用户类型Id
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="userTypeIds"></param>
        public bool HasUserType(long userId, List<string> userTypeIds) {
            var listGuid = new List<Guid>();
            userTypeIds.ForEach(r => { listGuid.Add(r.ToGuid()); });
            return HasUserType(userId, listGuid);
        }

        /// <summary>
        ///     检查用户是否包含所属的用户类型Id
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="userTypeIds"></param>
        public bool HasUserType(long userId, List<Guid> userTypeIds) {
            // 如果包括会员类型Id，则为正确
            if (userTypeIds.Contains(UserTypeEnum.Member.GetFieldId())) {
                return true;
            }

            var count = Count(r => r.UserId == userId && userTypeIds.Contains(r.UserTypeId));
            if (count == 0) {
                return false;
            }

            return true;
        }

        public SideBarType GetSideBarType(UserTypeEnum typeEnum) {
            switch (typeEnum) {
                case UserTypeEnum.Member:
                    return SideBarType.UserSideBar;

                case UserTypeEnum.ServiceCenter:
                    return SideBarType.ServiceCenterSideBar;

                case UserTypeEnum.ChuangKe:
                    return SideBarType.ChuangKeSideBar;

                case UserTypeEnum.Partner:
                    return SideBarType.PartnerSideBar;

                case UserTypeEnum.Province:
                    return SideBarType.ProvinceSideBar;

                case UserTypeEnum.Circle:
                    return SideBarType.CircleSideBar;

                case UserTypeEnum.City:
                    return SideBarType.CitySideBar;

                case UserTypeEnum.County:
                    return SideBarType.CountySideBar;

                case UserTypeEnum.BranchCompnay:
                    return SideBarType.BranchCompnaySideBar;

                case UserTypeEnum.Employees:
                    return SideBarType.EmployeesSideBar;

                case UserTypeEnum.ShareHolders:
                    return SideBarType.ShareHoldersSideBar;

                default:
                    throw new ValidException("未设置菜单");
            }
        }
    }
}