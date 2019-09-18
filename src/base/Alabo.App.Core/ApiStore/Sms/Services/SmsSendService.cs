using MongoDB.Bson;
using System.Collections.Generic;
using Alabo.App.Core.ApiStore.Sms.Entities;
using Alabo.App.Core.ApiStore.Sms.Enums;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Query;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;

namespace Alabo.App.Core.ApiStore.Sms.Services {

    public class SmsSendService : ServiceBase<SmsSend, ObjectId>, ISmsSendService {

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="repository"></param>
        public SmsSendService(IUnitOfWork unitOfWork, IRepository<SmsSend, ObjectId> repository) : base(unitOfWork, repository) {
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ServiceResult Add(IList<SmsSend> input) {
            return Add(input);
        }

        /// <summary>
        /// 根据状态获取
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public IList<SmsSend> GetAll(SendState input) {
            var query = new ExpressionQuery<SmsSend>();
            if (input != SendState.All) {
                query.And(r => r.State == input);
            }

            var list = GetList(query);
            return list;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ServiceResult Update(SmsSend input) {
            return Update(input);
        }
    }
}