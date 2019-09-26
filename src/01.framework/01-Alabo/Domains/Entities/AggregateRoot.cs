using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities.Core;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using AutoMapper;
using MongoDB.Bson.Serialization.Attributes;

namespace Alabo.Domains.Entities
{
    /// <summary>
    ///     sql聚合根
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public abstract class AggregateDefaultRoot<TEntity> : AggregateRoot<TEntity, long>
        where TEntity : IAggregateRoot
    {
        protected AggregateDefaultRoot(long id)
            : base(id)
        {
        }

        protected AggregateDefaultRoot()
            : base(0)
        {
        }
    }

    /// <summary>
    ///     sql聚合根 用户
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public abstract class AggregateDefaultUserRoot<TEntity> : AggregateUserRoot<TEntity, long>
        where TEntity : IAggregateRoot
    {
        protected AggregateDefaultUserRoot(long id)
            : base(id)
        {
        }

        protected AggregateDefaultUserRoot()
            : base(0)
        {
        }
    }

    /// <summary>
    ///     sql聚合根
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">标识类型</typeparam>
    public abstract class AggregateRoot<TEntity, TKey> : EntityBase<TEntity, TKey>, IAggregateRoot<TEntity, TKey>
        where TEntity : IAggregateRoot
    {
        /// <summary>
        ///     初始化聚合根
        /// </summary>
        /// <param name="id">标识</param>
        protected AggregateRoot(TKey id)
            : base(id)
        {
        }

        /// <summary>
        ///     版本号(乐观锁)
        /// </summary>
        [NotMapped]
        public byte[] Version { get; set; }
    }

    /// <summary>
    ///     sql聚合根,带用户Id
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">标识类型</typeparam>
    public abstract class AggregateUserRoot<TEntity, TKey> : EntityUserBase<TEntity, TKey>,
        IAggregateRoot<TEntity, TKey> where TEntity : IAggregateRoot
    {
        /// <summary>
        ///     初始化聚合根
        /// </summary>
        /// <param name="id">标识</param>
        protected AggregateUserRoot(TKey id)
            : base(id)
        {
        }

        /// <summary>
        ///     用户ID
        /// </summary>
        [Display(Name = "名称")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "100", ListShow = false, EditShow = false,
            SortOrder = 2)]
        public long UserId { get; set; }

        /// <summary>
        ///     用户
        /// </summary>
        [Display(Name = "用户名")]
        [Field(IsShowBaseSerach = true, IsShowAdvancedSerach = true, PlaceHolder = "请输入用户名",
            DataField = "UserId", Link = "/Admin/User/Edit?id=[[UserId]]", ControlsType = ControlsType.TextBox,
            GroupTabId = 1, Width = "180", ListShow = true,
            EditShow = true, SortOrder = 2)]
        [NotMapped]
        [BsonIgnore]
        [IgnoreMap]
        public string UserName { get; set; }

        /// <summary>
        ///     版本号(乐观锁)
        /// </summary>
        [IgnoreMap]
        public byte[] Version { get; set; }
    }

    /// <summary>
    ///     聚合根
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public abstract class AggregateRoot<TEntity> : AggregateRoot<TEntity, long> where TEntity : IAggregateRoot
    {
        /// <summary>
        ///     初始化聚合根
        /// </summary>
        /// <param name="id">标识</param>
        protected AggregateRoot(long id)
            : base(id)
        {
        }
    }
}