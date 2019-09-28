using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.Extensions;
using System;

namespace Alabo.Framework.Core.WebUis.Services
{
    public class UIBaseService : ServiceBase, IUIBaseService
    {
        public UIBaseService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        ///     检查基础类型
        /// </summary>
        /// <param name="type"></param>
        /// <param name="typeFind"></param>
        /// <param name="instanceFind"></param>
        /// <returns></returns>
        public ServiceResult CheckType(string type, ref Type typeFind, ref object instanceFind)
        {
            if (type.IsNullOrEmpty() || type == "undefined") return ServiceResult.FailedWithMessage("类型不能为空");
            typeFind = type.GetTypeByName();
            instanceFind = type.GetInstanceByName();
            if (typeFind == null || instanceFind == null)
                return ServiceResult.FailedWithMessage($"类型不存在，请确认{type}输入是否正确");

            return ServiceResult.Success;
        }
    }
}