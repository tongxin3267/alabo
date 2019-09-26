using System;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.Framework.Core.WebUis.Domain.Services {

    public interface IUIBaseService : IService {

        /// <summary>
        /// 检查基础类型
        /// </summary>
        /// <param name="type">type类型</param>
        /// <param name="typeFind">Type 类型</param>
        /// <param name="instanceFind">Type 实列</param>
        /// <returns></returns>
        ServiceResult CheckType(string type, ref Type typeFind, ref object instanceFind);
    }
}