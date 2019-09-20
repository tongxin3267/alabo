using Alabo.Domains.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Entities.Core;
using Alabo.Extensions;
using Alabo.UI.AutoReports;
using Alabo.UI.AutoReports.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace Alabo.Datas.Stores.Report {

    public class ReportStoreCommons<TEntity, TKey> where TEntity : class, IKey<TKey>, IVersion, IEntity {

        public static List<AutoReport> GetCountReport(IEnumerable<TEntity> queryList, CountReportInput inputParas) {
            var type = typeof(TEntity);
            var rsList = new List<AutoReport>();
            var gpListByDate = queryList.GroupBy(x => x.CreateTime.Date).OrderBy(x => x.Key);

            var colList = new List<string> { "日期", "全部" };
            var rowList = new List<object>();

            var dicEnumNameValue = new Dictionary<string, string>();
            if (inputParas.Field.IsNotNullOrEmpty()) {
                var objEnum = type.GetProperty(inputParas.Field);

                // IsEnum == true
                if (objEnum != null && objEnum.PropertyType.IsEnum) {
                    var enumMembers = objEnum.PropertyType.GetFields().Where(x => x.IsLiteral);
                    foreach (var item in enumMembers) {
                        var enumItem = item.GetValue(null);

                        dicEnumNameValue.Add(enumItem.GetDisplayName(), enumItem.ToString());
                    }
                }
            }

            foreach (var gpDataDate in gpListByDate) {
                var rowDic = new Dictionary<string, string> {
                    ["日期"] = gpDataDate.Key.ToString("yyyy-MM-dd"),
                    ["全部"] = gpDataDate.Count().ToString()
                };

                foreach (var enumItem in dicEnumNameValue) {
                    var enumDispayName = enumItem.Key;
                    if (!colList.Contains(enumDispayName)) {
                        colList.Add(enumDispayName);
                    }

                    var gListByEnum = WhereQuery(gpDataDate, inputParas.Field, enumItem.Value).ToList();
                    rowDic[enumDispayName] = gListByEnum.Count.ToString();
                }

                rowList.Add(rowDic);
            }

            rsList.Add(new AutoReport {
                Name = $"{inputParas.EntityType}统计",
                AutoReportChart = new AutoReportChart {
                    Type = ReportChartType.Line,
                    Columns = colList,
                    Rows = rowList
                }
            });

            return rsList;
        }

        public static PagedList<CountReportTable> GetCountTable(IEnumerable<TEntity> queryList,
            CountReportInput inputParas) {
            var type = typeof(TEntity);
            var rsList = new PagedList<CountReportTable>();
            var gpListByDate = queryList.GroupBy(x => x.CreateTime.Date).OrderBy(x => x.Key);

            var coluList = new List<Columns>
            {
                new Columns {name = "日期", type = "Date"},
                new Columns {name = "全部", type = "Count"}
            };

            var rowList = new List<object>();

            var dicEnumNameValue = new Dictionary<string, string>();
            if (inputParas.Field.IsNotNullOrEmpty()) {
                var objEnum = type.GetProperty(inputParas.Field);

                // IsEnum == true
                if (objEnum != null && objEnum.PropertyType.IsEnum) {
                    var enumMembers = objEnum.PropertyType.GetFields().Where(x => x.IsLiteral);
                    foreach (var item in enumMembers) {
                        var enumItem = item.GetValue(null);

                        dicEnumNameValue.Add(enumItem.GetDisplayName(), enumItem.ToString());
                    }
                }
            }

            foreach (var gpDataDate in gpListByDate) {
                var rowDic = new Dictionary<string, string> {
                    ["Date"] = gpDataDate.Key.ToString("yyyy-MM-dd"),
                    ["Count"] = gpDataDate.Count().ToString()
                };

                foreach (var enumItem in dicEnumNameValue) {
                    var enumDispayName = enumItem.Key;
                    if (coluList.Where(x => x.type == enumItem.Key).Count() < 1) {
                        coluList.Add(new Columns { name = enumDispayName, type = enumItem.Key });
                    }

                    var gListByEnum = WhereQuery(gpDataDate, inputParas.Field, enumItem.Value).ToList();
                    rowDic[enumItem.Key] = gListByEnum.Count.ToString();
                }

                rowList.Add(rowDic);
            }

            var rsRowList = rowList.Skip((inputParas.PageIndex - 1) * inputParas.PageSize).Take(inputParas.PageSize)
                .ToList();

            rsList.Add(new CountReportTable {
                Name = $"{inputParas.EntityType}报表数据",
                AutoReportChart = new CountReportItem {
                    CurrentSize = inputParas.PageSize,
                    PageIndex = inputParas.PageIndex,
                    TotalCount = rowList.Count,
                    PageCount = rowList.Count % inputParas.PageSize == 0
                        ? rowList.Count / inputParas.PageSize
                        : rowList.Count / inputParas.PageSize + 1,
                    PageSize = inputParas.PageSize,
                    Columns = coluList,
                    Rows = rsRowList
                }
            });

            return rsList;
        }

        public static List<AutoReport> GetSumReport(IEnumerable<TEntity> queryList, SumTableInput inputParas) {
            var rsList = new List<AutoReport>();
            var type = typeof(TEntity);

            var gpListByDate = queryList.GroupBy(x => x.CreateTime.Date).OrderBy(x => x.Key);

            var colList = new List<string> { "日期" };
            var rowList = new List<object>();

            var dicEnumNameValue = new Dictionary<string, string>();
            if (inputParas.SpecialField.IsNotNullOrEmpty()) {
                var objEnum = type.GetProperty(inputParas.SpecialField);

                // IsEnum == true
                if (objEnum != null && objEnum.PropertyType.IsEnum) {
                    var enumMembers = objEnum.PropertyType.GetFields().Where(x => x.IsLiteral);
                    foreach (var item in enumMembers) {
                        var enumItem = item.GetValue(null);

                        dicEnumNameValue.Add(enumItem.GetDisplayName(), enumItem.ToString());
                    }
                }
            }

            if (inputParas.Fields.Count > 0) {
                foreach (var gItemList in gpListByDate) {
                    var dic = new Dictionary<string, string>
                    {
                        {"日期", gItemList.Key.ToString("yyyy-MM-dd")}
                    };

                    foreach (var fieldName in inputParas.Fields) {
                        var fieldDispName = type.FullName.GetFiledDisplayName(fieldName);

                        if (dicEnumNameValue.Count > 0) {
                            foreach (var enumItem in dicEnumNameValue) {
                                var keyName = $"{fieldDispName}[{enumItem.Key}]";
                                if (colList.Where(x => x == keyName).Count() < 1) {
                                    colList.Add(keyName);
                                }

                                var gListByEnum = WhereQuery(gItemList, inputParas.SpecialField, enumItem.Value)
                                    .ToList();
                                var rsSum = 0M;
                                var prop = type.GetProperty(fieldName);
                                foreach (var row in gListByEnum) {
                                    rsSum += prop.GetValue(row).ToDecimal();
                                }

                                dic.Add(keyName, rsSum.ToString());
                            }
                        } else {
                            if (!colList.Contains(fieldName)) {
                                colList.Add(fieldName);
                            }

                            var prop = type.GetProperty(fieldName);

                            var rsSum = 0M;
                            foreach (var row in gItemList) {
                                rsSum += prop.GetValue(row).ToDecimal();
                            }

                            dic.Add(fieldName, rsSum.ToString());
                        }

                        rowList.Add(dic);
                    }
                }
            }

            rsList.Add(new AutoReport {
                Name = $"{inputParas.Type}SumReport统计",
                AutoReportChart = new AutoReportChart {
                    Type = ReportChartType.Line,
                    Columns = colList,
                    Rows = rowList
                }
            });

            return rsList;
        }

        public static PagedList<SumReportTable> GetSumReportTable(IEnumerable<TEntity> queryList,
            SumTableInput inputParas) {
            var rsList = new PagedList<SumReportTable>();
            var type = typeof(TEntity);

            var gpListByDate = queryList.GroupBy(x => x.CreateTime.Date).OrderBy(x => x.Key);

            var colList = new List<SumColumns>
            {
                new SumColumns {name = "日期", type = "日期"}
            };
            var rowList = new List<object>();

            var dicEnumNameValue = new Dictionary<string, string>();
            if (inputParas.SpecialField.IsNotNullOrEmpty()) {
                var objEnum = type.GetProperty(inputParas.SpecialField);

                // IsEnum == true
                if (objEnum != null && objEnum.PropertyType.IsEnum) {
                    var enumMembers = objEnum.PropertyType.GetFields().Where(x => x.IsLiteral);
                    foreach (var item in enumMembers) {
                        var enumItem = item.GetValue(null);

                        dicEnumNameValue.Add(enumItem.GetDisplayName(), enumItem.ToString());
                    }
                }
            }

            if (inputParas.Fields.Count > 0) {
                foreach (var gItemList in gpListByDate) {
                    var dic = new Dictionary<string, string>
                    {
                        {"日期", gItemList.Key.ToString("yyyy-MM-dd")}
                    };

                    foreach (var fieldName in inputParas.Fields) {
                        var fieldDispName = type.FullName.GetFiledDisplayName(fieldName);

                        if (dicEnumNameValue.Count > 0) {
                            foreach (var enumItem in dicEnumNameValue) {
                                var keyName = $"{fieldDispName}[{enumItem.Key}]";
                                if (colList.Where(x => x.type == keyName).Count() < 1) {
                                    colList.Add(new SumColumns { name = keyName, type = keyName });
                                }

                                var gListByEnum = WhereQuery(gItemList, inputParas.SpecialField, enumItem.Value)
                                    .ToList();
                                var rsSum = 0M;
                                var prop = type.GetProperty(fieldName);
                                foreach (var row in gListByEnum) {
                                    rsSum += prop.GetValue(row).ToDecimal();
                                }

                                dic.Add(keyName, rsSum.ToString());
                            }
                        } else {
                            if (colList.Where(x => x.type == fieldDispName).Count() < 1) {
                                colList.Add(new SumColumns { name = fieldDispName, type = fieldDispName });
                            }

                            var prop = type.GetProperty(fieldName);
                            var rsSum = 0M;
                            foreach (var row in gItemList) {
                                rsSum += prop.GetValue(row).ToDecimal();
                            }

                            dic.Add(fieldName, rsSum.ToString());
                        }
                    }

                    rowList.Add(dic);
                }
            }

            var rsRowList = rowList.Skip((int)((inputParas.PageIndex - 1) * inputParas.PageSize))
                .Take((int)inputParas.PageSize).ToList();
            rsList.Add(new SumReportTable {
                Name = $"{inputParas.Type} SumTable 统计",
                SumReportTableItem = new SumReportTableItems {
                    CurrentSize = inputParas.PageSize,
                    PageIndex = inputParas.PageIndex,
                    TotalCount = rowList.Count,
                    PageCount = rowList.Count % inputParas.PageSize == 0
                        ? rowList.Count / inputParas.PageSize
                        : rowList.Count / inputParas.PageSize + 1,
                    PageSize = inputParas.PageSize,
                    Columns = colList,
                    Rows = rsRowList
                }
            });

            return rsList;
        }

        public static IEnumerable<TEntity> WhereQuery(IEnumerable<TEntity> queryList, string columnName,
            string propertyValue) {
            return queryList.Where(m => {
                return m.GetType().GetProperty(columnName).GetValue(m, null).ToString().StartsWith(propertyValue);
            });
        }
    }
}