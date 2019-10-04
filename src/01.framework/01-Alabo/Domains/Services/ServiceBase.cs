using Alabo.Cache;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services.View;
using Alabo.Helpers;
using Alabo.Security.Sessions;

namespace Alabo.Domains.Services {

    public abstract class ServiceBase : IService {

        /// <summary>
        ///     初始化应用服务
        /// </summary>
        protected ServiceBase(IUnitOfWork unitOfWork) {
            Session = Security.Sessions.Session.Instance;
            UnitOfWork = unitOfWork;
        }

        /// <summary>
        ///     工作单元
        /// </summary>
        protected IUnitOfWork UnitOfWork { get; }

        /// <summary>
        ///     用户会话
        /// </summary>
        public ISession Session { get; set; }

        /// <summary>
        ///     获取数据操作对象服务
        /// </summary>
        public T Repository<T>() where T : IRepository {
            return Ioc.Resolve<T>();
        }

        /// <summary>
        ///     获取实体的数据库操作服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public T Resolve<T>() where T : IService {
            return Ioc.Resolve<T>();
        }

        /// <summary>
        ///     缓存
        /// </summary>
        public IObjectCache ObjectCache => Ioc.Resolve<IObjectCache>();
    }

    /// <summary>
    ///     实体服务
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TKey">主键类型</typeparam>
    public abstract class ServiceBase<TEntity, TKey> : ViewBaseService<TEntity, TKey>, IService<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TEntity, TKey> {

        /// <summary>
        ///     初始化增删改查服务
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="repository">仓储</param>
        protected ServiceBase(IUnitOfWork unitOfWork, IRepository<TEntity, TKey> repository) : base(unitOfWork,
            repository) {
        }

        ///// <summary>
        /////     转换为实体
        ///// </summary>
        ///// <param name="request">参数</param>
        //protected virtual TEntity ToEntity<TRequest>(TRequest request) {
        //    return request.MapTo<TEntity>();
        //}

        ///// <summary>
        /////     创建参数转换为实体
        ///// </summary>
        ///// <param name="request">创建参数</param>
        //protected virtual TEntity ToEntityFromCreateRequest<TCreateRequest, TRequest>(TCreateRequest request) {
        //    if (typeof(TCreateRequest) == typeof(TRequest)) {
        //        return ToEntity(Helpers.Convert.To<TRequest>(request));
        //    }

        //    return request.MapTo<TEntity>();
        //}

        ///// <summary>
        /////     修改参数转换为实体
        ///// </summary>
        ///// <param name="request">修改参数</param>
        //protected virtual TEntity ToEntityFromUpdateRequest<TUpdateRequest, TRequest>(TUpdateRequest request) {
        //    if (typeof(TUpdateRequest) == typeof(TRequest)) {
        //        return ToEntity(Helpers.Convert.To<TRequest>(request));
        //    }

        //    return request.MapTo<TEntity>();
        //}
    }
}