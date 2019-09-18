﻿using System;
using System.Data;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Enums;
using Alabo.Domains.Repositories;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Extensions;
using Alabo.Extensions;

namespace Alabo.App.Open.Kpi.Domain.Repositories {

    using Kpi = Entities.Kpi;

    /// <summary>
    ///     Class KpiRepository.
    /// </summary>
    /// <seealso cref="IKpiRepository" />
    internal class KpiRepository : RepositoryEfCore<Kpi, long>, IKpiRepository {

        /// <summary>
        /// 获取最后一条记录
        /// </summary>
        /// <param name="kpi"></param>
        public Kpi GetLastSingle(Kpi kpi) {
            var sqlWhere = $"  UserId={kpi.UserId} and ModuleId='{kpi.ModuleId}' and Type={Convert.ToInt16(kpi.Type)}";
            var timeTypeCondition = DataRecordExtension.GetTimeTypeCondition(kpi.Type, kpi.CreateTime);
            if (!timeTypeCondition.IsNullOrEmpty()) {
                sqlWhere += $" And {timeTypeCondition}";
            }

            var sql = $"SELECT top 1 * FROM kpi_kpi WHERE {sqlWhere} order by id desc ";
            using (var reader = RepositoryContext.ExecuteDataReader(sql)) {
                Kpi readKpi = null;
                if (reader.Read()) {
                    readKpi = ReadKpi(reader);
                }

                return readKpi;
            }
        }

        private Kpi ReadKpi(IDataReader reader) {
            var kpi = new Kpi {
                Id = reader["Id"].ConvertToLong(0),
                UserId = reader["UserId"].ConvertToLong(),

                CreateTime = reader["CreateTime"].ConvertToDateTime(),
                Type = (TimeType)reader["Type"].ConvertToInt(0),
                EntityId = reader["EntityId"].ConvertToLong(),

                TotalValue = reader["TotalValue"].ToDecimal(),
                Value = reader["Value"].ToDecimal()
            };
            return kpi;
        }

        public KpiRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}