using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using ZKCloud.App.Core.ApiStore.Sms.Entities;
using ZKCloud.App.Core.ApiStore.Sms.Enums;
using ZKCloud.Datas.UnitOfWorks;
using ZKCloud.Domains.Entities;
using ZKCloud.Domains.Query;
using ZKCloud.Domains.Repositories;
using ZKCloud.Domains.Services;

namespace ZKCloud.App.Core.ApiStore.Sms.Services
{
    /// <summary>
    /// 短信服务
    /// </summary>
    public class SmsService : ServiceBase<SmsSend, ObjectId>, ISmsService
    {
       
    }
}
