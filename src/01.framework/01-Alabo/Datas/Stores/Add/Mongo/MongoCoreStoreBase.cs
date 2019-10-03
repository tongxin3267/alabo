using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities.Core;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Mongo.Context;
using Alabo.Extensions;
using Alabo.Linq;
using MongoDB.Driver;
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace Alabo.Datas.Stores.Add.Mongo
{
    public abstract class MongoCoreStoreBase<TEntity, TKey> : StoreBase
        where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        /// <summary>
        ///     The entity key
        /// </summary>
        private const string EntityKey = "_id";

        /// <summary>
        ///     表名
        /// </summary>
        private readonly string TableName = MongoEntityMapping.GetTableName(typeof(TEntity));

        protected MongoCoreStoreBase(IUnitOfWork unitOfWork)
        {
            UnitOfWork = (UnitOfWorkBase)unitOfWork;
        }

        /// <summary>
        ///     工作单元
        /// </summary>
        protected UnitOfWorkBase UnitOfWork { get; }

        // private static readonly string TableName = typeof(T).Name;

        /// <summary>
        ///     IMongoCollection 集合，原生支持Async
        /// </summary>
        public IMongoCollection<TEntity> Collection
        {
            get
            {
                var tableName = TableName;
                if (tableName.IsNullOrEmpty()) {
                    tableName = MongoEntityMapping.GetTableName(typeof(TEntity));
                }

                return MongoRepositoryConnection.Database.GetCollection<TEntity>(tableName);
            }
        }

        /// <summary>
        ///     兼容模式BSON集
        ///     主要用于更新子文档
        ///     例如用户表里面嵌套着地址表 那bson就是用来更新地址列表里面的某个对象 而不是把地址列表全部拿出来改完再更新回去
        /// </summary>
        public MongoCollection<TEntity> BsonCollection =>
            MongoRepositoryConnection.LegacyDatabase.GetCollection<TEntity>(TableName);

        public IRepositoryContext RepositoryContext => throw new InvalidExpressionException("mongdb数据库不支持Sql相关操作");

        /// <summary>
        ///     获取查询对象
        /// </summary>
        /// <param name="predicate">条件</param>
        public IQueryable<TEntity> ToQueryable(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null) {
                return Collection.AsQueryable();
            }

            return Collection.AsQueryable().Where(predicate);
        }

        /// <summary>
        ///     查询表达式,ID的查询
        /// </summary>
        protected virtual Expression<Func<TEntity, bool>> IdPredicate(object id)
        {
            return Lambda.Equal<TEntity>("Id", id);
        }

        /// <summary>
        ///     将表达式转换成 Filter对象
        ///     http://mongodb.github.io/mongo-csharp-driver/2.4/apidocs/html/T_MongoDB_Driver_FilterDefinitionBuilder_1.htm
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        protected virtual FilterDefinition<TEntity> ToFilter(Expression<Func<TEntity, bool>> expression)
        {
            var filter = Builders<TEntity>.Filter.Where(expression);
            return filter;
        }

        protected virtual FilterDefinition<TEntity> ToFilter(object id)
        {
            var expression = IdPredicate(id);
            var filter = Builders<TEntity>.Filter.Where(expression);
            return filter;
        }
    }
}