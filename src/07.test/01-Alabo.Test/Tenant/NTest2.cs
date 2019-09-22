using PetaPoco;
using System;
using System.Collections.Generic;
using Xunit;
using Alabo.App.Core.Reports.Domain.Services;
using Alabo.App.Core.UI.Domain.Services;
using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Shop.Order.Domain.Entities;
using Alabo.App.Shop.Order.Domain.Services;
using Alabo.Domains.Base.Services;
using Alabo.Domains.Entities.Core;
using Alabo.Domains.Enums;
using Alabo.Domains.Repositories.Model;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.Linq;
using Alabo.Reflections;
using Alabo.Test.Base.Core.Model;
using Alabo.UI.AutoReports;
using Alabo.UI.AutoReports.Dtos;
using Alabo.UI.AutoReports.Enums;

namespace Alabo.Test.Tenant {

    public class NTest2 : CoreTest {

        [Fact]
        public void T5() {
            var sql = $@"SELECT * FROM ims_hlmms_member";
            var rs = Db.Instance.Fetch<dynamic>(sql);
        }

        [Fact]
        public void T4() {
            var input = new SingleReportInput {
                EntityType = "Order",
                Field = "TotalAmount",
                Style = ReportStyle.Sum,
            };

            input.Condition = new EntityQueryCondition {
                EntityType = input.EntityType,
                Field = input.Field,
                TimeType = TimeType.Day,
                ReferTime = DateTime.Now.Date.AddDays(-1),
            };

            var rs1 = Resolve<IAutoReportService>().GetSingleData(input);
        }

        [Fact]
        public void T3() {
            var input = new SingleReportInput {
                EntityType = "Order",
                Field = "Id",
                Style = ReportStyle.Count,
            };

            input.Condition = new EntityQueryCondition {
                EntityType = input.EntityType,
                Field = input.Field,
                TimeType = TimeType.Day,
                ReferTime = "2019-07-11".ToDateTime(),
            };

            var rs1 = Resolve<IAutoReportService>().GetSingleData(input);
        }

        [Fact]
        public void T2() {
            var condition = new EntityQueryCondition {
                EntityType = "Order",
                Field = "TotalAmount",
                Value = "330",
                TimeType = TimeType.Day,
                Operator = Datas.Queries.Enums.OperatorCompare.GreaterEqual,
                ReferTime = "2019-07-11".ToDateTime(),
            };

            var sql = condition.ToSqlWhere();
            var lin = condition.ToLinq<Order, long>();

            var oList = Resolve<IOrderService>().GetList(lin);
        }

        [Fact]
        public void T1() {
            //var count = Resolve<ILightAppService>().Count("Base_Logs");
            //var rs2 = Ioc.Resolve<ILogsService>().GetCountReport(x => x.CreateTime >= "2019-06-22".ToDateTime() && x.Level == Domains.Enums.LogsLevel.Warning);
            //Ioc.Resolve<ILogsService>().GetList(x => x.CreateTime >= "2019-06-22".ToDateTime() && x.Level == Domains.Enums.LogsLevel.Warning);

            //var inputParas = new Domains.Dtos.CountReportInput
            //{
            //    EntityType = "Logs",
            //    StartTime = "2019-06-20".ToDateTime(),
            //    EndTime = "2019-07-03".ToDateTime(),
            //    PageIndex = 1,
            //    PageSize = 8,
            //    Field = "Level",
            //};

            //var rs1 = Ioc.Resolve<ILogsService>().GetCountReport(inputParas);

            //var rss = ReportQuery(inputParas);
            //var result = Resolve<IAutoReportService>().GetDayCountReport(reportParas);
            //Assert.NotNull(result);
        }

        public List<AutoReport> ReportQuery(Domains.Dtos.CountReportInput inputParas) {
            var rsList = new List<AutoReport>();
            Type typeFind = null;
            object instanceFind = null;
            var checkType = Resolve<IUIBaseService>().CheckType(inputParas.EntityType, ref typeFind, ref instanceFind);
            if (!checkType.Succeeded) {
                return rsList;
            }

            if (!(instanceFind is IEntity)) {
                // 非实体类型不能完成数据统计
                return rsList;
            }

            // 特殊字段处理
            if (inputParas.Field.IsNotNullOrEmpty()) {
                var property = instanceFind.GetType().GetProperty(inputParas.Field);
                var filedType = property.GetType();
                var filed = Reflection.GetPropertyValue(inputParas.Field, instanceFind);
                //IsSpec = 1;
            }

            //var serviceInterfaceName = $"I{inputParas.Type}Service";

            //var siInstance = Ioc.Resolve(serviceInterfaceName);
            ////Ioc.Resolve<ILogsService>().GetCountReport();
            //var logService = Ioc.Resolve<ILogsService>();

            //var list1 = siInstance.GetList(x => x.CreateTime > inputParas.StartTime);
            //var list2 = logService.GetList(x => x.CreateTime > inputParas.StartTime);

            var table = Resolve<ITableService>().GetSingle(r => r.Key == typeFind.Name);
            if (table.TableType == TableType.Mongodb) {
                var rs = Linq.Dynamic.DynamicService.ResolveMethod(typeFind.Name, "GetCountReport", inputParas);

                //var single = Resolve<ILogsService>().FirstOrDefault();
                //var list = Ioc.Resolve("ILogsService");

                // var serviceBase= DynamicService
                //var tlist = Resolve<ITableService>().GetList();
                //var result = from list in tlist
                //             group list by list.CreateTime into g
                //             select new { g.Key, Total = g.Sum(n => n.ToObjectId().ToInt64()) };
            }

            return null;
        }
    }
}