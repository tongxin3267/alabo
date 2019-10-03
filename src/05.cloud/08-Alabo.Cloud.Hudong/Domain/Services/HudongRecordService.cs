using Alabo.App.Share.HuDong.Domain.Entities;
using Alabo.App.Share.HuDong.Dtos;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Extensions;
using MongoDB.Bson;

namespace Alabo.App.Share.HuDong.Domain.Services
{
    /// <summary>
    /// </summary>
    public class HudongRecordService : ServiceBase<HudongRecord, ObjectId>, IHudongRecordService
    {
        public HudongRecordService(IUnitOfWork unitOfWork, IRepository<HudongRecord, ObjectId> repository) : base(
            unitOfWork, repository)
        {
        }

        /// <summary>
        ///     ��ӳ齱��¼
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public ServiceResult AddRecord(HudongRecord record)
        {
            if (record != null) {
                if (!Add(record)) {
                    return ServiceResult.FailedWithMessage("����ʧ��");
                }
            }

            return ServiceResult.Success;
        }

        /// <summary>
        ///     �齱��¼
        /// </summary>
        /// <param name="viewInput"></param>
        /// <returns></returns>
        public ServiceResult RecordList(HuDongViewInput viewInput)
        {
            var result = Resolve<IHudongRecordService>().GetList(p => p.UserId == viewInput.userId.ToLong());

            return ServiceResult.SuccessWithObject(result);
        }
    }
}