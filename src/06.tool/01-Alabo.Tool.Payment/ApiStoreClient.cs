using System;
using Alabo.Cache;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Helpers;
using ZKCloud.Open.ApiBase.Connectors;
using ZKCloud.Open.ApiBase.Formatters;
using ZKCloud.Open.DiyClient;

namespace Alabo.Tool.Payment
{
    public abstract class ApiStoreClient : RestClientBase, IApiStoreClient
    {
        protected ApiStoreClient(Uri baseUri, IConnector connector, IDataFormatter formatter) : base(baseUri, connector,
            formatter)
        {
        }

        public IObjectCache ObjectCache => Ioc.Resolve<IObjectCache>();

        public T Repository<T>() where T : IRepository
        {
            return Ioc.Resolve<T>();
        }

        public T Service<T>() where T : IService
        {
            return Ioc.Resolve<T>();
        }
    }
}