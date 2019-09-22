using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using ZKCloud.Domains.Entities;

namespace ZKCloud.Test.Samples
{
    /// <summary>
    ///     实体样例
    /// </summary>
    [DisplayName("实体样例")]
    public class EntitySample : AggregateRoot<EntitySample, Guid>
    {
        public EntitySample() : this(Guid.NewGuid())
        {
        }

        public EntitySample(Guid id) : base(id)
        {
        }

        /// <summary>
        ///     名称
        /// </summary>
        [Required(ErrorMessage = "名称不能为空")]
        public string Name { get; set; }

        /// <summary>
        ///     忽略值
        /// </summary>
        [IgnoreMap]
        public string IgnoreValue { get; set; }
    }
}