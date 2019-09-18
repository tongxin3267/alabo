using MongoDB.Bson;
using System.Collections.Generic;
using Alabo.App.Core.ApiStore.Sms.Entities;
using Alabo.App.Core.ApiStore.Sms.Enums;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Core.ApiStore.Sms.Services {

    public interface ISmsSendService : IService<SmsSend, ObjectId> {
        /// <summary>
        /// 写入mongo
        /// </summary>
        /// <param name="input">把电话号码和状态</param>

        ServiceResult Add(IList<SmsSend> input);

        /// <summary>
        /// 从mongo读取
        /// </summary>

        IList<SmsSend> GetAll(SendState input);
    }
}