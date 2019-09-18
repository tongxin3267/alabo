using System;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Entities.Core;
using Alabo.Domains.Services;
using Alabo.Linq.Dynamic;
using ZKCloud.Open.DynamicExpression;
using Alabo.UI;
using Alabo.UI.AutoTables;

namespace Alabo.App.Core.UI.Domain.Services {

    public class AutoTableServcie : ServiceBase, IAutoTableService {

        public AutoTableServcie(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        public Tuple<ServiceResult, AutoTable> Table(string type, object query, AutoBaseModel autoBaseModel) {
            Type typeFind = null;
            object instanceFind = null;
            AutoTable autoTable = null;
            var checkType = Resolve<IUIBaseService>().CheckType(type, ref typeFind, ref instanceFind);
            if (!checkType.Succeeded) {
                return new Tuple<ServiceResult, AutoTable>(checkType, new AutoTable());
            }

            autoTable = AutoTableMapping.Convert(typeFind.FullName);

            #region IAutoTable类型

            // 如果是IAutoTable类型，则数据从AutoTable中获取,否则从Api接口中获取
            if (instanceFind is IAutoTable) {
                var target = new Interpreter().SetVariable("autoTable", instanceFind);
                var parameters = new[]
                {
                    new Parameter("query", query),
                    new Parameter("baseModel",autoBaseModel)
                };
                try {
                    autoTable.Result = target.Eval("autoTable.PageTable(query,baseModel)", parameters);
                } catch (Exception ex) {
                    return new Tuple<ServiceResult, AutoTable>(ServiceResult.FailedWithMessage(ex.Message), new AutoTable());
                }
            }

            #endregion IAutoTable类型

            #region autoConfig类型

            else if (instanceFind is IAutoConfig) {
                var data = Resolve<IAutoConfigService>().GetObjectList(typeFind);
                var result = PagedList<object>.Create(data, data.Count, 100, 1);

                PageResult<object> apiRusult = new PageResult<object> {
                    PageCount = result.PageCount,
                    Result = result,
                    RecordCount = result.RecordCount,
                    CurrentSize = result.CurrentSize,
                    PageIndex = result.PageIndex,
                    PageSize = result.PageSize,
                };
                autoTable.Result = apiRusult;
            } else if (instanceFind is IEntity) {
                var result = DynamicService.ResolveMethod(typeFind.Name, "GetApiPagedList", query);
                if (!result.Item1.Succeeded) {
                    return new Tuple<ServiceResult, AutoTable>(result.Item1, new AutoTable());
                }

                autoTable.Result = result.Item2;
            }

            #endregion autoConfig类型

            return new Tuple<ServiceResult, AutoTable>(ServiceResult.Success, autoTable);
        }

        public Tuple<ServiceResult, AutoTable> TableNoData(string type, object query, AutoBaseModel autoBaseModel) {
            Type typeFind = null;
            object instanceFind = null;
            AutoTable autoTable = null;
            var checkType = Resolve<IUIBaseService>().CheckType(type, ref typeFind, ref instanceFind);
            if (!checkType.Succeeded) {
                return new Tuple<ServiceResult, AutoTable>(checkType, new AutoTable());
            }

            autoTable = AutoTableMapping.Convert(typeFind.FullName);

            return new Tuple<ServiceResult, AutoTable>(ServiceResult.Success, autoTable);
        }
    }
}