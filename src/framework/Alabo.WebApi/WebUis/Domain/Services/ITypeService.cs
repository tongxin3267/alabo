using System;
using System.Collections.Generic;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Core.Admin.Domain.Services {

    /// <summary>
    ///     类型相关操作服务
    ///     动态类型操作
    /// </summary>
    public interface ITypeService : IService {

        /// <summary>
        /// 获取所有枚举列表，用于前端展示
        /// </summary>
        /// <returns></returns>
        IEnumerable<EnumList> GetEnumList();

        /// <summary>
        ///  所有的类型接口，包括枚举、AutoConfig(List对象的)、分类、标签
        /// </summary>
        /// <returns></returns>
        IEnumerable<EnumList> AllKeyValues();

        /// <summary>
        ///     获取所有的AutoCofing类型
        /// </summary>
        IEnumerable<Type> GetAllConfigType(string confinName);

        /// <summary>
        /// 根据接口类型返回所有相关的接口
        /// </summary>

        IEnumerable<Type> GetAllTypeByInterface(Type interfaceType);

        /// <summary>
        /// 根据接口和名称获取类型
        /// </summary>
        /// <param name="interfaceType"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        Type GetAllTypeByInterfaceAndName(Type interfaceType, string name);

        /// <summary>
        /// 获取所有的控制器Api
        /// </summary>

        IEnumerable<Type> GetAllApiController();

        /// <summary>
        ///     Gets the type of the automatic configuration.
        ///     根据名取AutoCofing类型
        /// </summary>
        /// <param name="configName">Name of the configuration. 可以是配置名如：UserConfig ，也可以是AutoConfig的完整命名空间</param>
        /// <returns>Type.</returns>
        Type GetAutoConfigType(string configName);

        /// <summary>
        ///     获取所有的IService类型
        /// </summary>
        IEnumerable<Type> GetAllServiceType();

        /// <summary>
        /// 获取所有实体相关的服务
        /// </summary>

        IEnumerable<Type> GetAllEntityService();

        /// <summary>
        ///     获取所有的IService类型
        /// </summary>
        /// <param name="serviceName">可以是配置名如：IAlaboUserService ，也可以是IService的完整命名空间</param>
        Type GetServiceType(string serviceName);

        /// <summary>
        ///     根据实体类型的名称获取IService名称
        /// </summary>
        /// <param name="entityName">比如：输入User 获取IAlaboUserService</param>
        string GetServiceTypeByEntity(string entityName);

        /// <summary>
        ///     获取所有的IService类型
        /// </summary>
        Type GetServiceTypeFromEntity(string entityName);

        /// <summary>
        ///     获取所有的IService类型
        /// </summary>
        IEnumerable<Type> GetAllEntityType();

        /// <summary>
        ///     获取d单个的IService类型
        /// </summary>
        /// <param name="entityName">可以是配置名如：IAlaboUserService ，也可以是IService的完整命名空间</param>
        Type GetEntityType(string entityName);

        /// <summary>
        ///     获取所有的Enum类型
        /// </summary>
        IEnumerable<Type> GetAllEnumType();

        /// <summary>
        ///     获取的单个的Enum类型
        /// </summary>
        /// <param name="enumName">可以是配置名如：UserTypeEnum ，也可以是Enum的完整命名空间</param>
        Type GetEnumType(string enumName);

        /// <summary>
        ///     获取AutoConfig 的字典类型，可用于构建下拉菜单，复选框，表格等
        ///     其中AutoConfig中，必须要指定Main字段特性，才可以识别值object
        /// </summary>
        /// <param name="configName"></param>
        /// <param name="isAllSelect"></param>
        ///
        Dictionary<string, object> GetAutoConfigDictionary(string configName, bool isAllSelect = false);

        /// <summary>
        ///     获取枚举 的字典类型
        ///     可用于构建下拉菜单，复选框，表格等
        ///     其中enum为display的值
        /// </summary>
        /// <param name="enumName"></param>
        Dictionary<string, object> GetEnumDictionary(string enumName);

        object GetEntityType(object entityName);

        /// <summary>
        ///     获取枚举 的字典类型
        ///     可用于构建下拉菜单，复选框，表格等
        ///     其中enum为display的值
        /// </summary>
        /// <param name="type"></param>
        Dictionary<string, object> GetEnumDictionary(Type type);

        /// <summary>
        ///     获取枚举 的字典类型
        ///     可用于构建下拉菜单，复选框，表格等
        ///     其中enum为display的值
        /// </summary>
        /// <param name="type">The 类型.</param>
        Dictionary<string, object> GetEnumSelectItem(Type type);

        IEnumerable<Type> GetAllConfigType();

        /// <summary>
        /// 获取App名称
        /// </summary>
        /// <param name="type"></param>

        string GetAppName(Type type);

        /// <summary>
        /// 获取分组名称
        /// </summary>
        /// <param name="type"></param>

        string GetGroupName(Type type);

        /// <summary>
        /// 获取App名称
        /// </summary>
        /// <param name="fullName"></param>

        string GetAppName(string fullName);

        /// <summary>
        /// 获取分组名称
        /// </summary>
        /// <param name="fullName"></param>

        string GetGroupName(string fullName);
    }
}