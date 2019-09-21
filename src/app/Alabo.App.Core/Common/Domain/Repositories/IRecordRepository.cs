﻿using Alabo.App.Core.Common.Domain.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Core.Common.Domain.Repositories {

    public interface IRecordRepository : IRepository<Record, long> {
    }
}