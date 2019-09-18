using MongoDB.Bson;
using Alabo.App.Open.HuDong.Domain.Entities;
using Alabo.App.Open.HuDong.Dtos;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Extensions;

namespace Alabo.App.Open.HuDong.Domain.Services {

    /// <summary>
    ///
    /// </summary>
    public class HudongRecordService : ServiceBase<HudongRecord, ObjectId>, IHudongRecordService {

        public HudongRecordService(IUnitOfWork unitOfWork, IRepository<HudongRecord, ObjectId> repository) : base(unitOfWork, repository) {
        }

        /// <summary>
        /// Ìí¼Ó³é½±¼ÇÂ¼
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public ServiceResult AddRecord(HudongRecord record) {
            if (record != null) {
                if (!Add(record)) {
                    return ServiceResult.FailedWithMessage("»î¶¯Ìí¼ÓÊ§°Ü");
                }
            }

            return ServiceResult.Success;
        }

        /// <summary>
        /// ³é½±¼ÇÂ¼
        /// </summary>
        /// <param name="viewInput"></param>
        /// <returns></returns>
        public ServiceResult RecordList(HuDongViewInput viewInput) {
            var result = Resolve<IHudongRecordService>().GetList(p => p.UserId == viewInput.userId.ToLong());

            return ServiceResult.SuccessWithObject(result);
        }
    }
}