using Microsoft.EntityFrameworkCore;

namespace Alabo.Datas.UnitOfWorks.MySql
{
    /// <summary>
    ///     工作单元
    /// </summary>
    public class MySqlUnitOfWork : Ef.MySql.MySqlUnitOfWork, IUnitOfWork
    {
        /// <summary>
        ///     初始化工作单元
        /// </summary>
        /// <param name="options">配置项</param>
        /// <param name="unitOfWorkManager">工作单元服务</param>
        public MySqlUnitOfWork(DbContextOptions options, IUnitOfWorkManager unitOfWorkManager) : base(options,
            unitOfWorkManager)
        {
        }
    }
}