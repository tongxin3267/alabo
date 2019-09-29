using Alabo.Domains.Enums;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Domains.Entities.Core
{
    public abstract class EntityCommonWithSeo<TEntity> : EntityCommonWithSeo<TEntity, long>
        where TEntity : IAggregateRoot
    {
        protected EntityCommonWithSeo(long id) : base(id)
        {
        }

        protected EntityCommonWithSeo() : this(0)
        {
        }
    }

    /// <summary>
    ///     包含Seo关键词的基类
    ///     基本继承类 T为主键类型
    ///     比如Long Int Guid 等
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class EntityCommonWithSeo<TEntity, TKey> : EntityCommon<TEntity, TKey>
        where TEntity : IAggregateRoot
    {
        protected EntityCommonWithSeo(TKey id)
            : base(id)
        {
        }

        /// <summary>
        ///     SEO标题
        /// </summary>
        [Display(Name = "SEO标题")]
        [StringLength(200, ErrorMessage = "Seo标题长度不能超过200个字符")]
        [Field(ListShow = false, EditShow = true, ControlsType = ControlsType.TextBox)]
        public string MetaTitle { get; set; }

        /// <summary>
        ///     SEO关键字
        /// </summary>
        [Display(Name = "SEO关键字")]
        [StringLength(300, ErrorMessage = "SEO关键字长度不能超过300个字符")]
        [Field(ListShow = false, EditShow = true, ControlsType = ControlsType.TextBox)]
        public string MetaKeywords { get; set; }

        /// <summary>
        ///     SEO描述
        /// </summary>
        [Display(Name = "SEO描述")]
        [StringLength(400, ErrorMessage = "SEO描述长度不能超过400个字符")]
        [Field(ListShow = false, EditShow = true, ControlsType = ControlsType.TextArea)]
        public string MetaDescription { get; set; }
    }

    /// <summary>
    ///     基类继承
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class EntityCommon<TEntity, TKey> : AggregateRoot<TEntity, TKey> where TEntity : IAggregateRoot
    {
        protected EntityCommon(TKey id)
            : base(id)
        {
        }

        /// <summary>
        ///     排序,越小排在越前面
        /// </summary>
        [Display(Name = "排序", Order = 1000)]
        [Field(ControlsType = ControlsType.Numberic, ListShow = true, EditShow = true, SortOrder = 10000,
            Width = "110")]
        [Range(0, 99999, ErrorMessage = "请输入0-99999之间的数字")]
        [HelpBlock("排序,越小排在越前面，请输入0-99999之间的数字")]
        public long SortOrder { get; set; } = 1000;

        /// <summary>
        ///     最后更新时间
        /// </summary>
        [Display(Name = "更新时间")]
        [Field(ControlsType = ControlsType.DateTimePicker, ListShow = true, EditShow = false, SortOrder = 10002,
            Width = "160")]
        public DateTime ModifiedTime { get; set; } = DateTime.Now;

        /// <summary>
        ///     通用状态 状态：0正常,1冻结,2删除
        ///     实体的软删除通过此字段来实现
        ///     软删除：指的是将实体标记为删除状态，不是真正的删除，可以通过回收站找回来
        /// </summary>
        [Display(Name = "状态")]
        [Field(ControlsType = ControlsType.RadioButton, IsTabSearch = true, ListShow = true, EditShow = true,
            SortOrder = 10003, Width = "110", DataSourceType = typeof(Status))]
        public virtual Status Status { get; set; } = Status.Normal;

        /// <summary>
        ///     备注，此备注一般表示管理员备注，前台会员不可以修改
        /// </summary>
        [Display(Name = "备注")]
        [Field(ControlsType = ControlsType.TextArea, ListShow = false, EditShow = true, Row = 5, SortOrder = 10004)]
        public string Remark { get; set; }

        public void Init()
        {
            throw new NotImplementedException();
        }

        public ValidationResultCollection Validate()
        {
            throw new NotImplementedException();
        }
    }
}