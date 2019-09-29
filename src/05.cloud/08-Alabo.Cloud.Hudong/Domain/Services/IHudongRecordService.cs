using Alabo.App.Share.HuDong.Domain.Entities;
using Alabo.App.Share.HuDong.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.App.Share.HuDong.Domain.Services
{
    public interface IHudongRecordService : IService<HudongRecord, ObjectId>
    {
        /// <summary>
        ///     ��¼�齱��¼
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        ServiceResult AddRecord(HudongRecord record);

        /// <summary>
        ///     �齱��¼
        /// </summary>
        /// <param name="viewInput"></param>
        /// <returns></returns>
        ServiceResult RecordList(HuDongViewInput viewInput);
    }
}