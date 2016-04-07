using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZKCloud.Domain.Services;
using ZKCloud.Core.AutoConfig.Domain.Entities;

namespace ZKCloud.Core.AutoConfig.Domain.Services {
    public interface IGenericConfigService : IService {
        void AddOrUpdate(GenericConfig config);

        void AddOrUpdate(IEnumerable<GenericConfig> configSource);

        void Delete(string appName);

        IEnumerable<GenericConfig> GetList(string appName);
    }
}
