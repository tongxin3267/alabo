using Alabo.Datas.Stores;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Services.List;
using Alabo.Domains.UnitOfWork;
using Alabo.Validations.Aspects;
using System;
using System.Threading.Tasks;

namespace Alabo.Domains.Services.Add {

    public abstract class AddAsyncBase<TEntity, TKey> : GetListBase<TEntity, TKey>, IAddAsync<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TEntity, TKey> {

        /// <summary>
        ///     初始化服务
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="store">存储器</param>
        protected AddAsyncBase(IUnitOfWork unitOfWork, IStore<TEntity, TKey> store) : base(unitOfWork, store) {
        }

        public async Task AddAsync([Valid] TEntity entity) {
            throw new NotImplementedException();
            // await Store.AddAsync(entity);
        }

        public async Task<string> AddAsync<TCreateRequest>([Valid] TCreateRequest request)
            where TCreateRequest : IRequest, new() {
            throw new NotImplementedException();
            //return await Store.AddAsync(request);
        }

        [UnitOfWork]
        public async Task<string> CreateAsync<TRequest>([Valid] TRequest request)
            where TRequest : IRequest, new() {
            throw new NotImplementedException();
            // return await Store.CreateAsync(request);
        }
    }
}