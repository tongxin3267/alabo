﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Alabo.Domains.Repositories.SqlServer {

    public sealed partial class SqlHelper {

        #region Schema 数据库结构

        /// <summary>
        ///     执行指定数据库连接字符串的命令,返回服务器数据库名称数组
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        public string[] GetDatabases(string connectionString) {
            var sql = "select name from sys.databases where name not in ('master','model','msdb','tempdb')";
            var dt = ExecuteDataTable(connectionString, sql);
            return dt.Rows.Cast<DataRow>().Select(row => row["name"].ToString()).ToArray();
        }

        /// <summary>
        ///     执行指定数据库连接字符串的命令,返回指定数据库的表信息
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="database"></param>
        public List<DbTable> GetDbTables(string connectionString, string database) {

            #region SQL

            var sql = string.Format(@"SELECT
                                        obj.name tablename,
                                        schem.name schemname,
                                        idx.rows,
                                        CAST
                                        (
	                                        CASE
		                                        WHEN (SELECT COUNT(1) FROM sys.indexes WHERE object_id= obj.OBJECT_ID AND is_primary_key=1) >=1 THEN 1
		                                        ELSE 0
	                                        END
                                        AS BIT) HasPrimaryKey
                                        from {0}.sys.objects obj
                                        inner join {0}.dbo.sysindexes idx on obj.object_id=idx.id and idx.indid<=1
                                        INNER JOIN {0}.sys.schemas schem ON obj.schema_id=schem.schema_id
                                        where type='U'
                                        order by obj.name", database);

            #endregion SQL

            var dt = ExecuteDataTable(connectionString.Replace("master", database), sql);
            return dt.Rows.Cast<DataRow>().Select(row => new DbTable()).ToList();
        }

        /// <summary>
        ///     执行指定数据库连接字符串的命令,返回指定数据库、表的字段信息
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="database"></param>
        /// <param name="tableName"></param>
        /// <param name="schema"></param>
        public List<DbColumn> GetDbColumns(string connectionString, string database, string tableName,
            string schema = "dbo") {

            #region SQL

            var sql = string.Format(@"
                                        WITH indexCTE AS
                                        (
	                                        SELECT
                                            ic.column_id,
                                            ic.index_column_id,
                                            ic.object_id
                                            FROM {0}.sys.indexes idx
                                            INNER JOIN {0}.sys.index_columns ic ON idx.index_id = ic.index_id AND idx.object_id = ic.object_id
                                            WHERE  idx.object_id =OBJECT_ID(@tableName) AND idx.is_primary_key=1
                                        )
                                        select
                                        colm.column_id ColumnID,
                                        CAST(CASE WHEN indexCTE.column_id IS NULL THEN 0 ELSE 1 END AS BIT) IsPrimaryKey,
                                        colm.name ColumnName,
                                        systype.name ColumnType,
                                        colm.is_identity IsIdentity,
                                        colm.is_nullable IsNullable,
                                        cast(colm.max_length as int) ByteLength,
                                        (
                                            case
                                                when systype.name='nvarchar' and colm.max_length>0 then colm.max_length/2
                                                when systype.name='nchar' and colm.max_length>0 then colm.max_length/2
                                                when systype.name='ntext' and colm.max_length>0 then colm.max_length/2
                                                else colm.max_length
                                            end
                                        ) CharLength,
                                        cast(colm.precision as int) Precision,
                                        cast(colm.scale as int) Scale,
                                        prop.value Remark
                                        from {0}.sys.columns colm
                                        inner join {0}.sys.types systype on colm.system_type_id=systype.system_type_id and colm.user_type_id=systype.user_type_id
                                        left join {0}.sys.extended_properties prop on colm.object_id=prop.major_id and colm.column_id=prop.minor_id
                                        LEFT JOIN indexCTE ON colm.column_id=indexCTE.column_id AND colm.object_id=indexCTE.object_id
                                        where colm.object_id=OBJECT_ID(@tableName)
                                        order by colm.column_id", database);

            #endregion SQL

            var param = new SqlParameter("@tableName", SqlDbType.NVarChar, 100) { Value = string.Format("{0}.{1}.{2}", database, schema, tableName) };
            var dt = ExecuteDataTable(connectionString, sql, param);
            return dt.Rows.Cast<DataRow>().Select(row => new DbColumn {
                //ColumnID = row.Field<int>("ColumnID"),
                //IsPrimaryKey = row.Field<bool>("IsPrimaryKey"),
                //ColumnName = row.Field<string>("ColumnName"),
                //ColumnType = row.Field<string>("ColumnType"),
                //IsIdentity = row.Field<bool>("IsIdentity"),
                //IsNullable = row.Field<bool>("IsNullable"),
                //ByteLength = row.Field<int>("ByteLength"),
                //CharLength = row.Field<int>("CharLength"),
                //Scale = row.Field<int>("Scale"),
                Remark = row["Remark"].ToString()
            }).ToList();
        }

        /// <summary>
        ///     执行指定数据库连接字符串的命令,返回指定数据库、表的索引信息
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="database"></param>
        /// <param name="tableName"></param>
        /// <param name="schema"></param>
        public List<DbIndex> GetDbIndexs(string connectionString, string database, string tableName,
            string schema = "dbo") {

            #region SQL

            var sql = string.Format(@"
                                        select
                                        idx.name IndexName
                                        ,idx.type_desc IndexType
                                        ,idx.is_primary_key IsPrimaryKey
                                        ,idx.is_unique IsUnique
                                        ,idx.is_unique_constraint IsUniqueConstraint
                                        ,STUFF(
                                        (
	                                        SELECT  ','+c.name from {0}.sys.index_columns ic
	                                        inner join {0}.sys.columns c on ic.column_id=c.column_id and ic.object_id=c.object_id
	                                        WHERE ic.is_included_column = 0 and ic.index_id=idx.index_id AND ic.object_id=idx.object_id
	                                        ORDER BY ic.key_ordinal
	                                        FOR XML PATH('')
                                        ),1,1,'') IndexColumns
                                        ,STUFF(
                                        (
	                                        SELECT  ','+c.name from {0}.sys.index_columns ic
	                                        inner join {0}.sys.columns c on ic.column_id=c.column_id and ic.object_id=c.object_id
	                                        WHERE ic.is_included_column = 1 and ic.index_id=idx.index_id AND ic.object_id=idx.object_id
	                                        ORDER BY ic.key_ordinal
	                                        FOR XML PATH('')
                                        ),1,1,'') IncludeColumns
                                        from {0}.sys.indexes idx
                                        where object_id =OBJECT_ID(@tableName)", database);

            #endregion SQL

            var param = new SqlParameter("@tableName", SqlDbType.NVarChar, 100) { Value = string.Format("{0}.{1}.{2}", database, schema, tableName) };
            var dt = ExecuteDataTable(connectionString, sql, param);
            return dt.Rows.Cast<DataRow>().Select(row => new DbIndex()).ToList();
        }

        #endregion Schema 数据库结构
    }

    /// <summary>
    ///     表索引结构
    /// </summary>
    public sealed class DbIndex {

        /// <summary>
        ///     索引名称
        /// </summary>
        public string IndexName { get; set; }

        /// <summary>
        ///     索引类型
        /// </summary>
        public string IndexType { get; set; }

        /// <summary>
        ///     是否为主键
        /// </summary>
        public bool IsPrimaryKey { get; set; }

        /// <summary>
        ///     是否唯一索引
        /// </summary>
        public bool IsUnique { get; set; }

        /// <summary>
        ///     是否唯一约束
        /// </summary>
        public bool IsUniqueConstraint { get; set; }

        /// <summary>
        ///     索引列
        /// </summary>
        public string IndexColumns { get; set; }

        /// <summary>
        ///     覆盖索引列
        /// </summary>
        public string IncludeColumns { get; set; }
    }

    /// <summary>
    ///     表结构
    /// </summary>
    public sealed class DbTable {

        /// <summary>
        ///     表名称
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        ///     表的架构
        /// </summary>
        public string SchemaName { get; set; }

        /// <summary>
        ///     表的记录数
        /// </summary>
        public int Rows { get; set; }

        /// <summary>
        ///     是否含有主键
        /// </summary>
        public bool HasPrimaryKey { get; set; }
    }

    /// <summary>
    ///     表字段结构
    /// </summary>
    public sealed class DbColumn {

        /// <summary>
        ///     字段ID
        /// </summary>
        public int ColumnID { get; set; }

        /// <summary>
        ///     是否主键
        /// </summary>
        public bool IsPrimaryKey { get; set; }

        /// <summary>
        ///     字段名称
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        ///     字段类型
        /// </summary>
        public string ColumnType { get; set; }

        /// <summary>
        ///     数据库类型对应的C#类型
        /// </summary>
        public string CSharpType => SqlMap.MapCsharpType(ColumnType);

        /// <summary>
        /// </summary>
        public Type CommonType => SqlMap.MapCommonType(ColumnType);

        /// <summary>
        ///     字节长度
        /// </summary>
        public int ByteLength { get; set; }

        /// <summary>
        ///     字符长度
        /// </summary>
        public int CharLength { get; set; }

        /// <summary>
        ///     小数位
        /// </summary>
        public int Scale { get; set; }

        /// <summary>
        ///     是否自增列
        /// </summary>
        public bool IsIdentity { get; set; }

        /// <summary>
        ///     是否允许空
        /// </summary>
        public bool IsNullable { get; set; }

        /// <summary>
        ///     描述
        /// </summary>
        public string Remark { get; set; }
    }

    public class SqlMap {

        public static string MapCsharpType(string dbtype) {
            if (string.IsNullOrEmpty(dbtype)) {
                return dbtype;
            }

            dbtype = dbtype.ToLower();
            var csharpType = "object";
            switch (dbtype) {
                case "bigint":
                    csharpType = "long";
                    break;

                case "binary":
                    csharpType = "byte[]";
                    break;

                case "bit":
                    csharpType = "bool";
                    break;

                case "char":
                    csharpType = "string";
                    break;

                case "date":
                    csharpType = "DateTime";
                    break;

                case "datetime":
                    csharpType = "DateTime";
                    break;

                case "datetime2":
                    csharpType = "DateTime";
                    break;

                case "datetimeoffset":
                    csharpType = "DateTimeOffset";
                    break;

                case "decimal":
                    csharpType = "decimal";
                    break;

                case "float":
                    csharpType = "double";
                    break;

                case "image":
                    csharpType = "byte[]";
                    break;

                case "int":
                    csharpType = "int";
                    break;

                case "money":
                    csharpType = "decimal";
                    break;

                case "nchar":
                    csharpType = "string";
                    break;

                case "ntext":
                    csharpType = "string";
                    break;

                case "numeric":
                    csharpType = "decimal";
                    break;

                case "nvarchar":
                    csharpType = "string";
                    break;

                case "real":
                    csharpType = "Single";
                    break;

                case "smalldatetime":
                    csharpType = "DateTime";
                    break;

                case "smallint":
                    csharpType = "short";
                    break;

                case "smallmoney":
                    csharpType = "decimal";
                    break;

                case "sql_variant":
                    csharpType = "object";
                    break;

                case "sysname":
                    csharpType = "object";
                    break;

                case "text":
                    csharpType = "string";
                    break;

                case "time":
                    csharpType = "TimeSpan";
                    break;

                case "timestamp":
                    csharpType = "byte[]";
                    break;

                case "tinyint":
                    csharpType = "byte";
                    break;

                case "uniqueidentifier":
                    csharpType = "Guid";
                    break;

                case "varbinary":
                    csharpType = "byte[]";
                    break;

                case "varchar":
                    csharpType = "string";
                    break;

                case "xml":
                    csharpType = "string";
                    break;

                default:
                    csharpType = "object";
                    break;
            }

            return csharpType;
        }

        public static Type MapCommonType(string dbtype) {
            if (string.IsNullOrEmpty(dbtype)) {
                return Type.Missing.GetType();
            }

            dbtype = dbtype.ToLower();
            var commonType = typeof(object);
            switch (dbtype) {
                case "bigint":
                    commonType = typeof(long);
                    break;

                case "binary":
                    commonType = typeof(byte[]);
                    break;

                case "bit":
                    commonType = typeof(bool);
                    break;

                case "char":
                    commonType = typeof(string);
                    break;

                case "date":
                    commonType = typeof(DateTime);
                    break;

                case "datetime":
                    commonType = typeof(DateTime);
                    break;

                case "datetime2":
                    commonType = typeof(DateTime);
                    break;

                case "datetimeoffset":
                    commonType = typeof(DateTimeOffset);
                    break;

                case "decimal":
                    commonType = typeof(decimal);
                    break;

                case "float":
                    commonType = typeof(double);
                    break;

                case "image":
                    commonType = typeof(byte[]);
                    break;

                case "int":
                    commonType = typeof(int);
                    break;

                case "money":
                    commonType = typeof(decimal);
                    break;

                case "nchar":
                    commonType = typeof(string);
                    break;

                case "ntext":
                    commonType = typeof(string);
                    break;

                case "numeric":
                    commonType = typeof(decimal);
                    break;

                case "nvarchar":
                    commonType = typeof(string);
                    break;

                case "real":
                    commonType = typeof(float);
                    break;

                case "smalldatetime":
                    commonType = typeof(DateTime);
                    break;

                case "smallint":
                    commonType = typeof(short);
                    break;

                case "smallmoney":
                    commonType = typeof(decimal);
                    break;

                case "sql_variant":
                    commonType = typeof(object);
                    break;

                case "sysname":
                    commonType = typeof(object);
                    break;

                case "text":
                    commonType = typeof(string);
                    break;

                case "time":
                    commonType = typeof(TimeSpan);
                    break;

                case "timestamp":
                    commonType = typeof(byte[]);
                    break;

                case "tinyint":
                    commonType = typeof(byte);
                    break;

                case "uniqueidentifier":
                    commonType = typeof(Guid);
                    break;

                case "varbinary":
                    commonType = typeof(byte[]);
                    break;

                case "varchar":
                    commonType = typeof(string);
                    break;

                case "xml":
                    commonType = typeof(string);
                    break;

                default:
                    commonType = typeof(object);
                    break;
            }

            return commonType;
        }
    }
}