using Alabo.Datas.Stores;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Services.Attach;
using Alabo.Extensions;
using Alabo.Validations.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Alabo.Domains.Services.Bulk
{
    public abstract class BulkAsyncBase<TEntity, TKey> : AttachBase<TEntity, TKey>, IBulkAsync<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        /// <summary>
        ///     服务构造函数
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="store">仓储</param>
        protected BulkAsyncBase(IUnitOfWork unitOfWork, IStore<TEntity, TKey> store) : base(unitOfWork, store)
        {
        }

        public async Task AddManyAsync(IEnumerable<TEntity> soucre)
        {
            if (soucre == null || !soucre.Any()) {
                return;
            }

            var addList = new List<TEntity>();
            soucre.Foreach(r =>
            {
                if (r != null)
                {
                    r.CreateTime = DateTime.Now;
                    addList.Add(r);
                }
            });
            if (!addList.Any()) {
                return;
            }

            await Store.AddManyAsync(addList);
            Log($"成功新增{{Table}}记录{addList.Count}条,新增记录Id:{addList.Select(r => r.Id).SplitString()}");
        }

        //public ServiceResult AddMany(IEnumerable<TEntity> soucre)
        //{
        //    if (soucre == null || !soucre.Any()) return ServiceResult.Failed;

        //    var addList = new List<TEntity>();
        //    soucre.Foreach(r =>
        //    {
        //        if (r != null)
        //        {
        //            r.CreateTime = DateTime.Now;
        //            addList.Add(r);
        //        }
        //    });
        //    if (!addList.Any()) return ServiceResult.Failed;

        //     Store.AddMany(addList);
        //    Log($"成功新增{{Table}}记录{addList.Count}条,新增记录Id:{addList.Select(r => r.Id).SplitString()}");
        //    return ServiceResult.Success;
        //}
        public async Task AddManyAsyncAsync([Valid] IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public async void AddUpdateOrDeleteAsync(IEnumerable<TEntity> entities,
            Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TDto>> SaveAsync<TDto, TRequest>(List<TRequest> addList, List<TRequest> updateList,
            List<TRequest> deleteList)
            where TDto : IResponse, new()
            where TRequest : IRequest, IKey, new()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync([Valid] IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public async void UpdateManyAsync(Action<TEntity> updateAction,
            Expression<Func<TEntity, bool>> predicate = null)
        {
            throw new NotImplementedException();
        }
    }
}