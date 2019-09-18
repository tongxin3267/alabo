using MongoDB.Bson;
using Alabo.App.Open.HuDong.Domain.Entities;
using Alabo.App.Open.HuDong.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Open.HuDong.Domain.Services {

    public interface IHudongRecordService : IService<HudongRecord, ObjectId> {

        /// <summary>
        /// ��¼�齱��¼
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        ServiceResult AddRecord(HudongRecord record);

        /// <summary>
        /// �齱��¼
        /// </summary>
        /// <param name="viewInput"></param>
        /// <returns></returns>
        ServiceResult RecordList(HuDongViewInput viewInput);
    }
}