﻿using Alabo.Domains.Repositories;
using Alabo.Domains.Services;

namespace Alabo.Tool.Payment
{
    public interface IApiStoreClient
    {
        T Repository<T>() where T : IRepository;

        T Service<T>() where T : IService;
    }
}