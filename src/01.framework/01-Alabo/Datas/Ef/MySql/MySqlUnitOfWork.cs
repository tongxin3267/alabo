using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Alabo.Datas.Ef.Internal;
using Alabo.Datas.Ef.Map;
using Alabo.Datas.UnitOfWorks;
using Alabo.Reflections;

namespace Alabo.Datas.Ef.MySql
{
    /// <summary>
    ///     MySql工作单元
    /// </summary>
    public abstract class MySqlUnitOfWork : UnitOfWorkBase
    {
        /// <summary>
        ///     初始化MySql工作单元
        /// </summary>
        /// <param name="options">配置</param>
        /// <param name="manager">工作单元服务</param>
        protected MySqlUnitOfWork(DbContextOptions options, IUnitOfWorkManager manager)
            : base(options, manager)
        {
        }

        /// <summary>
        ///     获取映射类型列表
        /// </summary>
        /// <param name="assembly">程序集</param>
        protected override IEnumerable<IMap> GetMapTypes()
        {
            return Reflection.GetInstancesByInterface<IMySqlMap>();
        }

        /// <summary>
        ///     拦截添加操作
        /// </summary>
        protected override void InterceptAddedOperation(EntityEntry entry)
        {
            base.InterceptAddedOperation(entry);
            Helper.InitVersion(entry);
        }

        /// <summary>
        ///     拦截修改操作
        /// </summary>
        protected override void InterceptModifiedOperation(EntityEntry entry)
        {
            base.InterceptModifiedOperation(entry);
            Helper.InitVersion(entry);
        }
    }
}