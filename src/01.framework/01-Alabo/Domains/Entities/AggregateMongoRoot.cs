using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities.Core;
using Alabo.Domains.Enums;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Alabo.Domains.Entities
{
    /// <summary>
    ///     Mongodb聚合根
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public abstract class AggregateMongodbRoot<TEntity> : EntityBase<TEntity, ObjectId>,
        IAggregateMongoRoot<TEntity> where TEntity : IAggregateRoot
    {
        /// <summary>
        ///     初始化聚合根
        /// </summary>
        /// <param name="id">标识</param>
        protected AggregateMongodbRoot(ObjectId id)
            : base(id)
        {
        }

        protected AggregateMongodbRoot()
            : this(ObjectId.GenerateNewId())
        {
        }

        /// <summary>
        ///     版本号(乐观锁)
        /// </summary>
        [BsonIgnore]
        public byte[] Version { get; set; }

        public bool IsObjectIdEmpty()
        {
            if (Id == ObjectId.Empty) return true;

            return false;
        }
    }

    /// <summary>
    ///     Mongodb聚合根
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public abstract class AggregateMongodbUserRoot<TEntity> : EntityUserBase<TEntity, ObjectId>,
        IAggregateRoot<TEntity, ObjectId> where TEntity : IAggregateRoot
    {
        /// <summary>
        ///     初始化聚合根
        /// </summary>
        /// <param name="id">标识</param>
        protected AggregateMongodbUserRoot(ObjectId id)
            : base(id)
        {
        }

        protected AggregateMongodbUserRoot()
            : this(ObjectId.Empty)
        {
        }

        /// <summary>
        ///     下单用户ID
        /// </summary>
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
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
            EditShow = false, SortOrder = 2)]
        [NotMapped]
        [BsonIgnore]
        public string UserName { get; set; }
    }
}