using Alabo.Reflections;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alabo.Domains.Repositories.Mongo.Context
{
    /// <summary>
    ///     表映射
    /// </summary>
    public static class MongoEntityMapping
    {
        private static readonly IDictionary<Type, string> TableMappingCache = new Dictionary<Type, string>();

        /// <summary>
        ///     获取表名
        /// </summary>
        /// <param name="type"></param>
        public static string GetTableName(Type type)
        {
            if (!TableMappingCache.TryGetValue(type, out var tableName))
            {
                var attribute = type.GetAttribute<TableAttribute>();
                if (attribute != null)
                {
                    tableName = attribute.Name;
                    if (!TableMappingCache.ContainsKey(type)) {
                        try
                        {
                            TableMappingCache.TryAdd(type, tableName);
                        }
                        catch (System.Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }

            return tableName;
        }
    }
}