using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZKCloud.Domain.Services;
using ZKCloud.Core.AutoConfig.Domain.Entities;
using ZKCloud.Core.AutoConfig.Domain.Repositories;

namespace ZKCloud.Core.AutoConfig.Domain.Services {
    public class GenericConfigService : ServiceBase, IGenericConfigService {
        public void AddOrUpdate(IEnumerable<GenericConfig> configSource) {
            if (configSource == null)
                throw new ArgumentNullException(nameof(configSource));
            foreach(var item in configSource) {
                AddOrUpdate(item);
            }
        }

        public void AddOrUpdate(GenericConfig config) {
            if (config == null)
                throw new ArgumentNullException(nameof(config));
            GenericConfig find = null;
            if (config.Id > 0)
                find = Repository<GenericConfigRepository>().ReadSingle(e => e.Id == config.Id);
            if (find == null)
                find = Repository<GenericConfigRepository>().ReadSingle(e => e.AppName == config.AppName && e.Key == config.Key);
            if (find == null) {
                find = new GenericConfig() {
                    AppName = config.AppName,
                    Key = config.Key,
                    Value = config.Value,
                    LastUpdated = DateTime.Now
                };
                Repository<GenericConfigRepository>().AddSingle(find);
            } else {
                find.AppName = config.AppName;
                find.Key = config.Key;
                find.Value = config.Value;
                find.LastUpdated = DateTime.Now;
                Repository<GenericConfigRepository>().UpdateSingle(find);
            }
        }

        public void Delete(string appName) {
            if (string.IsNullOrWhiteSpace(appName))
                throw new ArgumentNullException(nameof(appName));
            Repository<GenericConfigRepository>().Delete(e => e.AppName == appName);
        }

        public IEnumerable<GenericConfig> GetList(string appName) {
            if (string.IsNullOrWhiteSpace(appName))
                throw new ArgumentNullException(nameof(appName));
            return Repository<GenericConfigRepository>().ReadMany(e => e.AppName == appName);
        }
    }
}
