using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Alabo.Datas.Queries.Enums;
using Alabo.Domains.Repositories.Mongo.Context;
using Alabo.Exceptions;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Alabo.Industry.Cms.LightApps.Domain
{
    /// <summary>
    ///     支持租户
    /// </summary>
    public static class LightAppStore
    {
        public static Dictionary<string, Operator> DicComparison = new Dictionary<string, Operator>
        {
            {"==", Operator.Equal},
            {"<<", Operator.Less},
            {"<=", Operator.LessEqual},
            {">>", Operator.Greater},
            {">=", Operator.GreaterEqual},
            {"!=", Operator.NotEqual},
            {"s%", Operator.Starts},
            {"e%", Operator.Ends},
            {"c%", Operator.Contains}
        };

        /// <summary>
        ///     IMongoCollection 集合，原生支持Async
        /// </summary>
        public static IMongoCollection<dynamic> GetCollection(string tableName)
        {
            return MongoRepositoryConnection.Database.GetCollection<dynamic>(tableName);
        }

        /// <summary>
        ///     CountTable
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static long Count(string tableName)
        {
            return GetCollection(tableName).AsQueryable().Count();
        }

        public static (string Field, Operator op, string Value) ComparisonParse(KeyValuePair<string, string> kvItem)
        {
            foreach (var item in DicComparison)
                if (kvItem.Value.StartsWith(item.Key))
                    return (kvItem.Key, DicComparison[item.Key], kvItem.Value.Replace(item.Key, ""));

            return (kvItem.Key, Operator.Equal, kvItem.Value);
        }

        /// <summary>
        ///     Json转换为DynamicObject
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static dynamic JsonToDynamicObject(string json)
        {
            try
            {
                // dynamic converter
                var convert = new ExpandoObjectConverter();
                // dynamic object
                var model = JsonConvert.DeserializeObject<ExpandoObject>(json, convert);

                return model;
            }
            catch (Exception exc)
            {
                throw new ValidException("DataJson 错误!");
            }
        }

        /// <summary>
        ///     按条件获取一条数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <param name="fieldValue"></param>
        /// <returns></returns>
        public static dynamic GetSingle(string tableName, string fieldName, string fieldValue)
        {
            var dic = new Dictionary<string, string>
            {
                {fieldName, fieldValue}
            };

            var listOp = new List<(string Field, Operator op, string Value)>();
            foreach (var item in dic)
            {
                var rs = ComparisonParse(item);
                listOp.Add(rs);
            }

            return QuerySingle(tableName, listOp);
        }

        /// <summary>
        ///     按Id删除一条数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="id"></param>
        public static bool DeleteOne(string tableName, ObjectId id)
        {
            var rs = GetCollection(tableName).DeleteOne(Builders<dynamic>.Filter.Eq("_id", id));
            return rs.DeletedCount > 0;
        }

        /// <summary>
        ///     获取列表, 返回所有条件相符的数据
        /// </summary>
        /// <returns></returns>
        public static dynamic QuerySingle(string tableName, List<(string Field, Operator op, string Value)> listOp)
        {
            var filter = FilterParser(listOp);
            var rs = GetCollection(tableName).Find(filter).FirstOrDefault();

            return rs;
        }

        /// <summary>
        ///     获取列表, 返回所有条件相符的数据
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<dynamic> QueryList(string tableName,
            List<(string Field, Operator op, string Value)> listOp)
        {
            var filter = FilterParser(listOp);
            var rs = GetCollection(tableName).Find(filter).ToList();

            return rs;
        }

        public static dynamic ValueParse(string value)
        {
            if (value.Length == 24 && ObjectId.TryParse(value, out var rsObjectId)) return rsObjectId;

            if (value.Contains('.') && decimal.TryParse(value, out var rsDecimal)) return rsDecimal;

            //if (DateTime.TryParse(value, out DateTime rsDateTime))
            //{
            //    return rsDateTime;
            //}

            if (long.TryParse(value, out var rsLong)) return rsLong;

            if (bool.TryParse(value, out var rsBool)) return rsBool;

            return value;
        }

        /// <summary>
        ///     按条件建立数据查询 Filter
        /// </summary>
        /// <param name="opList"></param>
        /// <returns></returns>
        public static FilterDefinition<dynamic> FilterParser(List<(string Field, Operator op, string Value)> opList)
        {
            var filter = Builders<dynamic>.Filter.Empty;
            foreach (var item in opList)
            {
                var value = ValueParse(item.Value);

                switch (item.op)
                {
                    case Operator.Equal:
                        filter = filter & Builders<dynamic>.Filter.Eq(item.Field, value);
                        break;

                    case Operator.NotEqual:
                        filter = filter & Builders<dynamic>.Filter.Ne(item.Field, value);
                        break;

                    case Operator.Greater:
                        filter = filter & Builders<dynamic>.Filter.Gt(item.Field, value);
                        break;

                    case Operator.GreaterEqual:
                        filter = filter & Builders<dynamic>.Filter.Gte(item.Field, value);
                        break;

                    case Operator.Less:
                        filter = filter & Builders<dynamic>.Filter.Lt(item.Field, value);
                        break;

                    case Operator.LessEqual:
                        filter = filter & Builders<dynamic>.Filter.Lte(item.Field, value);
                        break;
                    //case Operator.Starts:
                    //    filter = filter & Builders<dynamic>.Filter.Eq(item.Field, value);
                    //    break;
                    //case Operator.Ends:
                    //    filter = filter & Builders<dynamic>.Filter.Eq(item.Field, value);
                    //    break;
                    //case Operator.Contains:
                    //    filter = filter & Builders<dynamic>.Filter.Eq(item.Field, value);
                    //    break;
                    default:
                        filter = filter & Builders<dynamic>.Filter.Eq(item.Field, value);
                        break;
                }
            }

            return filter;
        }
    }
}