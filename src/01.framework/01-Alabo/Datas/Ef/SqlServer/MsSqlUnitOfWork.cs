using Alabo.Datas.Ef.Map;
using Alabo.Datas.UnitOfWorks;
using Alabo.Reflections;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Alabo.Datas.Ef.SqlServer
{
    /// <summary>
    ///     SqlServer工作单元
    /// </summary>
    public abstract class MsSqlUnitOfWork : UnitOfWorkBase
    {
        /// <summary>
        ///     初始化SqlServer工作单元
        /// </summary>
        /// <param name="options">配置</param>
        /// <param name="manager">工作单元服务</param>
        protected MsSqlUnitOfWork(DbContextOptions options, IUnitOfWorkManager manager)
            : base(options, manager)
        {
        }

        /// <summary>
        ///     获取映射类型列表
        /// </summary>
        /// <param name="assembly">程序集</param>
        protected override IEnumerable<IMap> GetMapTypes()
        {
            return Reflection.GetInstancesByInterface<IMsSqlMap>();
        }
    }
}