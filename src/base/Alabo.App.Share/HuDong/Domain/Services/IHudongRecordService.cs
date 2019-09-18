using MongoDB.Bson;
using Alabo.App.Open.HuDong.Domain.Entities;
using Alabo.App.Open.HuDong.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Open.HuDong.Domain.Services {

    public interface IHudongRecordService : IService<HudongRecord, ObjectId> {

        /// <summary>
        /// ¼ÇÂ¼³é½±¼ÇÂ¼
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        ServiceResult AddRecord(HudongRecord record);

        /// <summary>
        /// ³é½±¼ÇÂ¼
        /// </summary>
        /// <param name="viewInput"></param>
        /// <returns></returns>
        ServiceResult RecordList(HuDongViewInput viewInput);
    }
}