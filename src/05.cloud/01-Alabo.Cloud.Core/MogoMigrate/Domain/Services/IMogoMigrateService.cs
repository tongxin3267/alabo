using _01_Alabo.Cloud.Core.MogoMigrate.ViewModels;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace _01_Alabo.Cloud.Core.MogoMigrate.Domain.Services
{
    public interface IMogoMigrateService : IService
    {
        /// <summary>
        ///     获取迁移数据库视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MogoMigrateView GetMogoMigrateView(long id);

        /// <summary>
        ///     数据迁移
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        ServiceResult Migrate(MogoMigrateView view);
    }
}