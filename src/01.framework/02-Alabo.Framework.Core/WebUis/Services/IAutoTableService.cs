﻿using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.UI;
using Alabo.UI.Design.AutoTables;
using System;

namespace Alabo.Framework.Core.WebUis.Services {

    public interface IAutoTableService : IService {

        /// <summary>
        ///     表格数据
        /// </summary>
        /// <param name="type"></param>
        /// <param name="query"></param>
        /// <param name="autoBaseModel"></param>
        /// <returns></returns>
        Tuple<ServiceResult, AutoTable> Table(string type, object query, AutoBaseModel autoBaseModel);

        /// <summary>
        ///     表格结构，无数据
        /// </summary>
        /// <param name="type"></param>
        /// <param name="query"></param>
        /// <param name="autoBaseModel"></param>
        /// <returns></returns>
        Tuple<ServiceResult, AutoTable> TableNoData(string type, object query, AutoBaseModel autoBaseModel);
    }
}