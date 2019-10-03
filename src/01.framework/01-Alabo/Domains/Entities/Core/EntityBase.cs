using Alabo.Domains.Enums;
using Alabo.Domains.Repositories.Mongo.Extension;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using Convert = Alabo.Helpers.Convert;

namespace Alabo.Domains.Entities.Core
{
    /// <summary>
    ///     领域实体
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public abstract class EntityBase<TEntity> : EntityBase<TEntity, Guid> where TEntity : IEntity
    {
        /// <summary>
        ///     初始化领域实体
        /// </summary>
        /// <param name="id">标识</param>
        protected EntityBase(Guid id) : base(id)
        {
        }
    }

    /// <summary>
    ///     领域实体
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public abstract class EntityUserBase<TEntity, TKey> : EntityBase<TEntity, TKey>, IUserId where TEntity : IEntity
    {
        /// <summary>
        ///     初始化领域实体
        /// </summary>
        /// <param name="id">标识</param>
        protected EntityUserBase(TKey id) : base(id)
        {
        }
    }

    /// <summary>
    ///     领域实体
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">标识类型</typeparam>
    public abstract class EntityBase<TEntity, TKey> : DomainBase<TEntity>, IEntity<TEntity, TKey>
        where TEntity : IEntity
    {
        /// <summary>
        ///     初始化领域实体
        /// </summary>
        /// <param name="id">标识</param>
        protected EntityBase(TKey id)
        {
            Id = id;
        }

        /// <summary>
        ///     标识
        /// </summary>
        [Required(ErrorMessage = "Id不能为空")]
        [Key]
        [Display(Name = "ID", Order = 1)]
        [Field(ControlsType = ControlsType.Hidden, ListShow = false, EditShow = true, SortOrder = 1, Width = "50")]
        [JsonConverter(typeof(ObjectIdConverter))]
        public TKey Id { get; set; }

        /// <summary>
        /// 租户
        /// </summary>
        //  public string Tenant { get; set; }

        /// <summary>
        ///     创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        [Field(ControlsType = ControlsType.DateTimePicker, ListShow = true, EditShow = false, SortOrder = 10001,
            Width = "160")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        ///     初始化
        /// </summary>
        public virtual void Init()
        {
            if (string.IsNullOrWhiteSpace(Alabo.Extensions.Extensions.SafeString(Id)) || Id.Equals(default(TKey))) {
                Id = CreateId();
            }
        }

        /// <summary>
        ///     相等运算
        /// </summary>
        public override bool Equals(object other)
        {
            return this == other as EntityBase<TEntity, TKey>;
        }

        /// <summary>
        ///     获取哈希
        /// </summary>
        public override int GetHashCode()
        {
            return ReferenceEquals(Id, null) ? 0 : Id.GetHashCode();
        }

        /// <summary>
        ///     相等比较
        /// </summary>
        public static bool operator ==(EntityBase<TEntity, TKey> left, EntityBase<TEntity, TKey> right)
        {
            if ((object)left == null && (object)right == null) {
                return true;
            }

            if (!(left is TEntity) || !(right is TEntity)) {
                return false;
            }

            if (Equals(left.Id, null)) {
                return false;
            }

            if (left.Id.Equals(default(TKey))) {
                return false;
            }

            return left.Id.Equals(right.Id);
        }

        /// <summary>
        ///     不相等比较
        /// </summary>
        public static bool operator !=(EntityBase<TEntity, TKey> left, EntityBase<TEntity, TKey> right)
        {
            return !(left == right);
        }

        /// <summary>
        ///     创建标识
        /// </summary>
        protected virtual TKey CreateId()
        {
            return Convert.To<TKey>(Guid.NewGuid());
        }

        /// <summary>
        ///     验证
        /// </summary>
        protected override void Validate(ValidationResultCollection results)
        {
            //if (Id == null || Id.Equals(default(TKey))) {
            //    results.Add(new ValidationResult("Id不能为空"));
            //}

            if (Id == null) {
                results.Add(new ValidationResult("Id不能为空"));
            }
        }
    }
}