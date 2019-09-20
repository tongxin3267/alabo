using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using MongoDB.Bson;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Base.Entities;
using Alabo.Domains.Entities;
using Alabo.Domains.Entities.Core;
using Alabo.Domains.Repositories;
using Alabo.Domains.Repositories.Model;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Linq.Dynamic;
using Alabo.Reflections;
using Alabo.Runtime;
using Alabo.Web.ViewFeatures;

namespace Alabo.Domains.Base.Services {

    /// <summary>
    ///     表相关服务
    /// </summary>
    public class TableService : ServiceBase<Table, ObjectId>, ITableService {

        public TableService(IUnitOfWork unitOfWork, IRepository<Table, ObjectId> repository) : base(unitOfWork,
            repository) {
        }

        /// <summary>
        ///     初始化所有的表服务
        /// </summary>
        public void Init() {
            Resolve<ITableService>().DeleteAll();
            if (!Exists()) {
                var tables = new List<Table>();

                #region AutoConfig配置

                // AutoConfig配置
                var types = EntityDynamicService.GetAllAutoConfig();

                foreach (var type in types) {
                    var table = new Table {
                        Key = type.Name,
                        Type = type.FullName,
                        TableType = TableType.AutoConfig,
                        TableName = "Common_AutoConfig"
                    };
                    if (type.FullName.Contains("ViewModels")) {
                        continue;
                    }

                    if (type.FullName.Contains("Entities")) {
                        continue;
                    }

                    if (!(type.FullName.Contains("Config") && type.Name != "AutoConfig")) {
                        continue;
                    }

                    var classDescription = new ClassDescription(type);
                    table.Name = classDescription?.ClassPropertyAttribute?.Name;

                    foreach (var item in classDescription.Propertys.Where(r => r.Name == "Id")) {
                        table.Columns.Add(GetColumn(item));
                    }

                    var propertys = classDescription.Propertys.Where(r =>
                        r.Name != "Id" && r.Name != "HttpContext" && r.Name != "CreateTime");

                    foreach (var item in propertys) {
                        table.Columns.Add(GetColumn(item));
                    }

                    foreach (var item in classDescription.Propertys.Where(r => r.Name == "CreateTime")) {
                        table.Columns.Add(GetColumn(item));
                    }

                    tables.Add(table);
                }

                #endregion AutoConfig配置

                #region 枚举

                // AutoConfig配置
                types = EntityDynamicService.GetAllEnum();

                foreach (var type in types) {
                    var table = new Table {
                        Key = type.Name,
                        Type = type.FullName,
                        TableType = TableType.Enum,
                        TableName = string.Empty
                    };

                    foreach (var item in Enum.GetValues(type)) {
                        var column = new TableColumn();
                        column.Key = Enum.GetName(type, item);
                        column.Name = item.GetDisplayName();

                        table.Columns.Add(column);
                    }

                    tables.Add(table);
                }

                #endregion 枚举

                #region 级联分类与标签

                types = EntityDynamicService.GetAllRelationTypes();

                foreach (var type in types) {
                    var table = new Table {
                        Key = type.Name,
                        Type = type.FullName,
                        TableName = "Common_Relation"
                    };
                    table.Columns = new List<TableColumn>();
                    var classDescription = new ClassDescription(type);
                    table.Name = classDescription?.ClassPropertyAttribute?.Name;

                    if (type.FullName.Contains("Class")) {
                        table.TableType = TableType.ClassRelation;
                        tables.Add(table);
                    }

                    if (type.FullName.Contains("Tag")) {
                        table.TableType = TableType.TagRelation;
                        tables.Add(table);
                    }
                }

                #endregion 级联分类与标签

                #region 实体类

                types = RuntimeContext.Current.GetPlatformRuntimeAssemblies().SelectMany(a => a.GetTypes()
                    .Where(t => t.GetInterfaces().Contains(typeof(IEntity)) ||
                                t.GetInterfaces().Contains(typeof(IMongoEntity))));
                types = types.Where(r => r.IsGenericType == false);
                types = types.Where(r => r.IsAbstract == false);
                types = types.Where(r => r.IsInterface == false);
                types = types.Where(r => !r.FullName.Contains("Test."));
                types = types.Where(r => !r.FullName.Contains("Tests."));

                var sqlTables = EntityDynamicService.GetSqlTable();
                foreach (var type in types) {
                    var table = new Table { Key = type.Name, Type = type.FullName };
                    if (type.FullName.Contains("Config") && type.Name != "AutoConfig") {
                        continue;
                    }

                    if (type.FullName.Contains("ViewModels")) {
                        continue;
                    }

                    if (!type.FullName.Contains("Entities")) {
                        continue;
                    }

                    if (type.BaseType.Name.Contains("Mongo")) {
                        var tableAttribute = type.GetAttribute<TableAttribute>();
                        if (tableAttribute == null) {
                            continue;
                        }

                        table.TableName = tableAttribute.Name;
                        table.TableType = TableType.Mongodb;
                    } else {
                        table.TableType = TableType.SqlServer;
                        var find = sqlTables.FirstOrDefault(r => r.EndsWith(table.Key));
                        if (find != null) {
                            table.TableName = find;
                        }
                    }

                    var classDescription = new ClassDescription(type);
                    table.Name = classDescription?.ClassPropertyAttribute?.Name;

                    foreach (var item in classDescription.Propertys.Where(r => r.Name == "Id")) {
                        table.Columns.Add(GetColumn(item));
                    }

                    var propertys = classDescription.Propertys.Where(r =>
                        r.Name != "Id" && r.Name != "HttpContext" && r.Name != "CreateTime");

                    foreach (var item in propertys) {
                        table.Columns.Add(GetColumn(item));
                    }

                    foreach (var item in classDescription.Propertys.Where(r => r.Name == "CreateTime")) {
                        table.Columns.Add(GetColumn(item));
                    }

                    tables.Add(table);
                }

                #endregion 实体类

                AddMany(tables);
            }
        }

        /// <summary>
        ///     获取mongo的表
        /// </summary>
        /// <returns></returns>
        public List<KeyValue> MongodbCatalogEntityKeyValues() {
            return GetKeyValues(table => {
                if (table.TableType == TableType.Mongodb) {
                    return table;
                }

                return null;
            });
        }

        /// <summary>
        ///     获取sqlserver的表
        /// </summary>
        /// <returns></returns>
        public List<KeyValue> SqlServcieCatalogEntityKeyValues() {
            return GetKeyValues(table => {
                if (table.TableType == TableType.SqlServer) {
                    return table;
                }

                return null;
            });
            //return new List<KeyValue>();
        }

        /// <summary>
        ///     获取所有表咯
        /// </summary>
        /// <returns></returns>
        public List<KeyValue> CatalogEntityKeyValues() {
            return GetKeyValues(table => {
                if (table.TableType == TableType.SqlServer || table.TableType == TableType.Mongodb) {
                    return table;
                }

                return null;
            });
        }

        private TableColumn GetColumn(PropertyDescription propertyDescription) {
            var column = new TableColumn();

            column.Key = propertyDescription.Property.Name;
            column.Type = propertyDescription.Property.PropertyType.Name;

            column.Name = column.Key;
            if (propertyDescription.FieldAttribute != null) {
                // column.Name = propertyDescription.FieldAttribute.FieldName;
            }

            if (propertyDescription.DisplayAttribute != null) {
                column.Name = propertyDescription.DisplayAttribute.Name;
            }

            return column;
        }

        private List<KeyValue> GetKeyValues(Func<Table, Table> func) {
            var resultList = new List<KeyValue>();
            var tables = Resolve<ITableService>().GetList();
            foreach (var item in tables) {
                var result = func.Invoke(item);
                if (result != null) //避免加null
{
                    resultList.Add(new KeyValue {
                        Name = $"{result.Name}[{result.Type}]",
                        Value = item.Key,
                        Key = result.Key,
                        Icon = null
                        //SortOrder=
                    });
                }
            }

            return resultList;
        }
    }
}