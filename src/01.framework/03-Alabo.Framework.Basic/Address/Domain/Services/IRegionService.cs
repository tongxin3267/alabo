using MongoDB.Bson;
using System.Collections.Generic;
using Alabo.App.Core.Common.Domain.Dtos;
using Alabo.Framework.Basic.Relations.Domain.Entities;
using Alabo.Core.Enums.Enum;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Core.Common.Domain.Services {

    public interface IRegionService : IService<Region, ObjectId> {

        /// <summary>
        ///     导入初始数据
        /// </summary>
        void Init();

        void InitRegion();

        string GetRegionNameById(long id);

        string GetRegionData(RegionLevel level);

        /// <summary>
        ///     获取省份
        /// </summary>
        /// <param name="query"></param>

        PagedList<Region> GetProvincePagedList(object query);

        /// <summary>
        ///     根据区域Id获取区域名称
        ///     比如：输入虎门区域Id 441900121，则返回 广东省东莞市虎门镇
        ///     比如：输入东莞区域Id 441900 ，则返回 广东省东莞市
        ///     比如：输入广东区域Id 440000 ，则返回 广东省
        ///     ///
        /// </summary>
        /// <param name="areaId"></param>
        string GetFullName(long areaId);

        /// <summary>
        ///     根据区域Id获取城市Id
        /// </summary>
        /// <param name="id">主键ID</param>
        long GetCityId(long id);

        /// <summary>
        /// </summary>
        long GetCountyId(long id);

        /// <summary>
        ///     根据区域Id获取省份ID
        /// </summary>
        /// <param name="id">主键ID</param>
        long GetProvinceId(long id);

        /// <summary>
        ///     区域树形结构
        ///     用于构建前台地址组件
        /// </summary>
        IEnumerable<RegionTree> RegionTrees();
    }
}