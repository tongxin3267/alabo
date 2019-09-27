using Alabo.Datas.Stores;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Repositories.Model;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.Linq;
using Alabo.Maps;
using Alabo.Reflections;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Alabo.Logging.Logs.Entities;
using Alabo.Logging.Logs.Services;
using Convert = Alabo.Helpers.Convert;

namespace Alabo.Domains.Services.Add
{
    public abstract class CoreBaseService<TEntity, TKey> : ServiceBase
        where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        /// <summary>
        ///     初始化服务
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="store">存储器</param>
        protected CoreBaseService(IUnitOfWork unitOfWork, IStore<TEntity, TKey> store) : base(unitOfWork)
        {
            Store = store;
            EntityDescription = Reflection.GetDisplayNameOrDescription<TEntity>();
        }

        /// <summary>
        ///     存储器
        /// </summary>
        protected IStore<TEntity, TKey> Store { get; }

        /// <summary>
        ///     实体描述
        /// </summary>
        protected string EntityDescription { get; }

        /// <summary>
        ///     数据类型
        /// </summary>
        protected TableType TableType
        {
            get
            {
                if (typeof(TEntity).BaseType.Name.Contains("Mongo")) return TableType.Mongodb;

                return TableType.SqlServer;
            }
        }

        /// <summary>
        ///     将ID转换成列表
        /// </summary>
        /// <param name="ids">Id标识列表</param>
        protected IEnumerable<TKey> IdsToList(string ids)
        {
            return Convert.ToList<TKey>(ids);
        }

        /// <summary>
        ///     查询表达式,ID的查询
        /// </summary>
        protected virtual Expression<Func<TEntity, bool>> IdPredicate(object id)
        {
            return Lambda.Equal<TEntity>("Id", id);
        }

        /// <summary>
        ///     转换为数据传输对象
        /// </summary>
        /// <param name="entity">实体</param>
        protected TDto ToDto<TDto>(TEntity entity)
            where TDto : IResponse, new()
        {
            return entity.MapTo<TDto>();
        }

        /// <summary>
        ///     转换为数据传输对象
        /// </summary>
        /// <param name="entities">实体</param>
        protected IEnumerable<TDto> ToDto<TDto>(IEnumerable<TEntity> entities)
            where TDto : IResponse, new()
        {
            if (entities == null) return null;

            IList<TDto> resultList = new List<TDto>();
            entities.Foreach(r => { resultList.Add(ToDto<TDto>(r)); });
            return resultList;
        }

        /// <summary>
        ///     将Id转换成主键类型字段
        /// </summary>
        /// <param name="id">主键ID</param>
        protected TKey ToKey(object id)
        {
            var key = Convert.To<TKey>(id);
            return key;
        }

        /// <summary>
        ///     日志记录
        /// </summary>
        /// <param name="content">日志内容，内容不能为空</param>
        /// <param name="level">日志级别</param>
        public void Log(string content, LogsLevel level = LogsLevel.Information)
        {
            if (!content.IsNullOrEmpty())
            {
                var log = new Logs
                {
                    Content = content.Replace("{{Table}}", ""),
                    Level = level,
                    Type = typeof(TEntity).Name
                };
                if (HttpWeb.HttpContext != null)
                    try
                    {
                        log.Url = HttpWeb.Url;
                        log.Browser = HttpWeb.Browser;
                        log.UserId = HttpWeb.UserId;
                        log.IpAddress = HttpWeb.Ip;
                    }
                    catch
                    {
                        // ignored
                    }

                if (log.Type != "Logs") Ioc.Resolve<ILogsService>().Add(log);
            }
        }
    }
}