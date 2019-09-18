using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Alabo.App.Core.User.Domain.Repositories;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Helpers;
using Convert = System.Convert;

namespace Alabo.App.Core.Reports {

    public abstract class ReportConfigRuleBase : IReportRule {
        private ReportRuleConfig _reportRuleConfig;

        public ReportConfigRuleBase(Guid id, ReportContext context) {
            Id = id;
            Context = context;
        }

        public Guid Id { get; }

        public ReportRuleConfig Config {
            get {
                if (_reportRuleConfig == null) {
                    _reportRuleConfig = ReadRuleConfig(Id);
                }

                return _reportRuleConfig;
            }
        }

        public ReportContext Context { get; }

        public virtual IReportRuleResult Execute(ReportRuleParameter parameter) {
            var repositoryContext = Ioc.Resolve<IUserRepository>().RepositoryContext;
            IList<SqlParameter> queryParameters = new List<SqlParameter>();
            foreach (var item in Config.QueryParameters) {
                var sqlParameter = new SqlParameter {
                    ParameterName = item.Name
                };
                if (!parameter.TryGetValue(item.Name, out var value)) {
                    value = item.DefaultValue;
                }

                sqlParameter.Value = Convert.ChangeType(value, item.Type);
                queryParameters.Add(sqlParameter);
            }

            IList<ReportTableRow> rowList = new List<ReportTableRow>();
            using (var dr = repositoryContext.ExecuteDataReader(Config.QueryString, queryParameters.ToArray())) {
                var columnDictionary = Config.Columns.ToDictionary(e => e.Name, e => e);
                while (dr.Read()) {
                    var row = new ReportTableRow();
                    for (var i = 0; i < dr.FieldCount; i++) {
                        var filedName = dr.GetName(i);
                        if (!columnDictionary.ContainsKey(filedName)) {
                            continue;
                        }

                        var column = columnDictionary[filedName];
                        row.AddValue(column.Name, dr.GetValue(i));
                    }

                    rowList.Add(row);
                }
            }

            return RowResult(Id, rowList);
        }

        protected IReportRuleResult RowResult(Guid id, IEnumerable<ReportTableRow> source) {
            if (source == null) {
                throw new ArgumentNullException(nameof(source));
            }

            var ruleFactory = Context.HttpContextAccessor.HttpContext.RequestServices.GetService<IReportRuleFactory>();
            if (ruleFactory == null) {
                throw new ReportRuleFactoryNotFoundException(
                    "get IReportRuleFactory from ServiceProvider return null.");
            }

            return new ReportConfigRuleResult(source.ToArray(), ruleFactory.GetRuleScheme(id));
        }

        public virtual ReportScheme GetScheme() {
            if (Config == null) {
                throw new ArgumentException("report rule config is null.");
            }

            var columns = Config.Columns.Select(e => new ReportColumn {
                Name = e.Name,
                Text = e.Text,
                Format = e.Format,
                Summary = e.Summary,
                Order = e.Order
            }).ToArray();
            return new ReportScheme(Config.Id, Config.Name, "", columns);
        }

        public abstract ReportRuleConfig ReadRuleConfig(Guid id);
    }
}