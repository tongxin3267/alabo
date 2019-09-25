using Alabo.AutoConfigs.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.AutoConfigs.Repositories {

    public interface IAutoConfigRepository : IRepository<AutoConfig, long> {
    }
}