using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Alabo.Datas.Stores;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Linq;
using Alabo.Mapping;

namespace Alabo.Domains.Services.Dictionary
{
    public abstract class DictionaryBaseService<TEntity, TKey> : DictionaryAsyncBaseService<TEntity, TKey>,
        IDictionaryService<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        /// <summary>
        ///     服务构造函数
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="store">仓储</param>
        protected DictionaryBaseService(IUnitOfWork unitOfWork, IStore<TEntity, TKey> store) : base(unitOfWork, store)
        {
        }

        public TEntity Find(Dictionary<string, string> dictionary)
        {
            var predicate = LinqHelper.DictionaryToLinq<TEntity>(dictionary);
            return GetSingle(predicate);
        }

        public Dictionary<string, string> GetDictionary(object id)
        {
            var find = GetSingle(id);
            var dic = AutoMapping.ConverDictionary(find);
            return dic;
        }

        public Dictionary<string, string> GetDictionary(Expression<Func<TEntity, bool>> predicate)
        {
            var find = GetSingle(predicate);
            var dic = AutoMapping.ConverDictionary(find);
            return dic;
        }

        public IList<KeyValue> GetKeyValue(Expression<Func<TEntity, bool>> predicate)
        {
            var list = GetList(predicate);
            var result = new List<KeyValue>();
            if (list != null)
                foreach (var item in list)
                {
                    var keyValue = new KeyValue
                    {
                        Key = item.Id
                    };
                    var dynamicItem = (dynamic) item;
                    keyValue.Name = dynamicItem.Name;
                    keyValue.Value = dynamicItem.Name;
                    result.Add(keyValue);
                }

            return result;
        }
    }
}