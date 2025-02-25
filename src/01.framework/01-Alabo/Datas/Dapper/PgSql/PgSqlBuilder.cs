﻿using Alabo.Datas.Matedatas;
using Alabo.Datas.Sql.Queries.Builders.Abstractions;
using Alabo.Datas.Sql.Queries.Builders.Core;
using System.Text;

namespace Alabo.Datas.Dapper.PgSql {

    /// <summary>
    ///     PgSql Sql生成器
    /// </summary>
    public class PgSqlBuilder : SqlBuilderBase {

        /// <summary>
        ///     初始化Sql生成器
        /// </summary>
        /// <param name="matedata">实体元数据解析器</param>
        /// <param name="parameterManager">参数管理器</param>
        public PgSqlBuilder(IEntityMatedata matedata = null, IParameterManager parameterManager = null) : base(matedata,
            parameterManager) {
        }

        /// <summary>
        ///     获取Sql方言
        /// </summary>
        protected override IDialect GetDialect() {
            return new PgSqlDialect();
        }

        /// <summary>
        ///     创建Sql生成器
        /// </summary>
        public override ISqlBuilder New() {
            return new PgSqlBuilder(EntityMatedata, ParameterManager);
        }

        /// <summary>
        ///     创建分页Sql
        /// </summary>
        protected override void CreatePagerSql(StringBuilder result) {
            AppendSql(result, GetSelect());
            AppendSql(result, GetFrom());
            AppendSql(result, GetJoin());
            AppendSql(result, GetWhere());
            AppendSql(result, GetGroupBy());
            AppendSql(result, GetOrderBy());
            result.Append($"Limit {GetPager().PageSize} OFFSET {GetPager().GetSkipCount()}");
        }
    }
}