using MongoDB.Bson;
using System;
using System.Collections.Generic;
using Alabo.App.Share.HuDong.Domain.Entities;
using Alabo.App.Share.HuDong.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Share.HuDong.Domain.Services {

    /// <summary>
    /// 活动服务
    /// </summary>
    public interface IHudongService : IService<Hudong, ObjectId> {

        /// <summary>
        /// 获取互动类型下拉
        /// </summary>
        /// <returns></returns>
        List<EnumKeyValue> GetAwardType();

        /// <summary>
        /// 获取活动视图
        /// </summary>
        /// <param name="viewInput"></param>
        /// <returns></returns>
        Tuple<ServiceResult, Hudong> GetView(HuDongViewInput viewInput);

        /// <summary>
        /// 转盘获取抽奖信息
        /// </summary>
        /// <param name="viewInput"></param>
        /// <returns></returns>
        Tuple<ServiceResult, Hudong> GetAwards(HuDongViewInput viewInput);

        /// <summary>
        /// 活动编辑或添加
        /// </summary>
        /// <param name="huDong"></param>
        /// <returns></returns>
        ServiceResult Edit(Hudong huDong);

        /// <summary>
        /// 根据活动ID和类型获取记录
        /// </summary>
        /// <param name="id"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        Hudong GetSingle(ObjectId id, string key);

        /// <summary>
        /// 抽奖所中
        /// </summary>
        /// <param name="drawInput"></param>
        /// <returns></returns>
        ServiceResult Draw(DrawInput drawInput);
    }
}