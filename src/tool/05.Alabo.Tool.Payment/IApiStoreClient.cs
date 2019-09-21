using Alabo.Domains.Repositories;
using Alabo.Domains.Services;

namespace Alabo.App.Core.ApiStore {

    public interface IApiStoreClient {

        T Repository<T>() where T : IRepository;

        T Service<T>() where T : IService;
    }
}