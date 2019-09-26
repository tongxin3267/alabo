using System.Collections.Generic;
using _01_Alabo.Cloud.Core.SendSms.Domain.Entities;
using _01_Alabo.Cloud.Core.SendSms.Domain.Enums;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace _01_Alabo.Cloud.Core.SendSms.Domain.Services {

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