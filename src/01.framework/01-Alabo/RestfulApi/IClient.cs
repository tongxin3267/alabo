﻿using System;
using System.Collections.Generic;
using System.Text;
using Alabo.Dependency;
using ZKCloud.Open.ApiBase.Connectors;
using ZKCloud.Open.ApiBase.Formatters;
using ZKCloud.Open.DiyClient;

namespace Alabo.RestfulApi {

    public interface IClient : IScopeDependency {
    }

    public abstract class ClientBase : RestClientBase {
        private static readonly Func<IConnector> _connectorCreator = () => new HttpClientConnector();

        private static readonly Func<IDataFormatter> _formmaterCreator = () => new JsonFormatter();

        private static readonly string _baseUrl = Runtime.RuntimeContext.Current.WebsiteConfig.OpenApiSetting.DiyUrl;

        public ClientBase(Uri baseUri)
            : base(baseUri, _connectorCreator(), _formmaterCreator()) {
        }

        public ClientBase()
            : this(new Uri(_baseUrl)) {
        }
    }
}