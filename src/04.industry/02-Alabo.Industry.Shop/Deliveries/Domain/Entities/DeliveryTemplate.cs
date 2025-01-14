﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Alabo.Data.People.Stores.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Repositories.Mongo.Extension;
using Alabo.Exceptions;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebApis.Service;
using Alabo.Industry.Shop.Deliveries.Domain.Enums;
using Alabo.Industry.Shop.Deliveries.Domain.Services;
using Alabo.UI;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace Alabo.Industry.Shop.Deliveries.Domain.Entities
{
    /// <summary>
    /// 店铺运费模板
    /// </summary>
    [Table("Shop_DeliveryTemplate")]
    public class DeliveryTemplate : AggregateMongodbUserRoot<DeliveryTemplate>
    {
        #region 属性

        [JsonConverter(typeof(ObjectIdConverter))] public ObjectId StoreId { get; set; }

        /// <summary>
        ///     运费模板方式
        /// </summary>
        [Display(Name = "运费模板名称")]
        [Required]
        [Field(ControlsType = ControlsType.TextBox, IsShowBaseSerach = true, IsShowAdvancedSerach = true, Width = "180", ListShow = true,
            Operator = Datas.Queries.Enums.Operator.Contains,
            EditShow = false, SortOrder = 1)]
        public string TemplateName { get; set; }

        /// <summary>
        ///     运费模板方式
        /// </summary>
        [Display(Name = "运费模板方式")]
        public DeliveryTemplateType TemplateType { get; set; }

        /// <summary>
        ///     运费计算方式
        /// </summary>
        [Display(Name = "运费计算方式")]
        public DeliveryFreightCalculateType CalculateType { get; set; }

        /// <summary>
        ///     发货时间
        /// </summary>
        [Display(Name = "发货时间")]
        public DeliveryTimeType DeliveryTime { get; set; } = DeliveryTimeType.DeliveryTimeTypef;

        public Guid ExpressId { get; set; }

        /// <summary>
        ///     ExpressConfig的配置ID
        /// </summary>
        public ExpressType ExpressType { get; set; }

        /// <summary>
        ///     默认首重
        /// </summary>
        public decimal StartStandard { get; set; } = 1;

        /// <summary>
        ///     默认首费：输入0.01-999.99（最多包含两位小数） 不能为空也不能为0
        /// </summary>
        [Display(Name = "默认首费")]
        [Field(IsShowBaseSerach = true, IsShowAdvancedSerach = true, Width = "180", ListShow = true,
           EditShow = false, SortOrder = 3)]
        public decimal StartFee { get; set; } = 8;

        /// <summary>
        ///     默认续费标准：输入1-9999范围内的整数
        /// </summary>
        public decimal EndStandard { get; set; } = 1;

        /// <summary>
        ///     默认续费：输入0.01-999.99（最多包含两位小数） 不能为空也不能为0
        /// </summary>
        public decimal EndFee { get; set; } = 6;

        /// <summary>
        ///     运费模板费用
        ///     区域模板费用
        /// </summary>
        public IList<RegionTemplateFee> TemplateFees { get; set; } = new List<RegionTemplateFee>();

        public bool Enabled { get; set; }

        #endregion 属性

        public PageResult<DeliveryTemplate> PageTable(object query, AutoBaseModel autoModel)
        {
            var model = new PagedList<DeliveryTemplate>();
            if (autoModel.Filter == FilterType.Admin)
            {
                model = Resolve<IDeliveryTemplateService>().GetPagedList(query);
            }
            else if (autoModel.Filter == FilterType.User)
            {
                var store = Resolve<IStoreService>().GetUserStore(autoModel.BasicUser.Id);
                if (store == null)
                {
                    throw new ValidException("您不是供应商");
                }
                //  model = Resolve<IDeliveryTemplateService>().GetPagedList(query, r => r.StoreId == store.Id);
            }
            else
            {
                throw new ValidException("方式不对");
            }
            model.Result = model.Result.Select(s =>
            {
                //ConvertToApiImageUrl
                s.UserName = Resolve<IApiService>().ConvertToApiImageUrl(s.UserName);
                return s;
            }).ToList();
            return null;
        }
    }

    /// <summary>
    ///     运费模板费用
    ///     区域模板费用
    /// </summary>
    public class RegionTemplateFee
    {
        /// <summary>
        ///     区域ID可以是城市ID，也可以是区域ID，也可以是省份ID
        /// </summary>
        public IList<long> RegionList { get; set; } = new List<long>();

        //public string RegionName { get; set; }

        /// <summary>
        ///     首费标准：输入1-9999范围内的整数
        /// </summary>
        public decimal StartStandard { get; set; } = 1;

        /// <summary>
        ///     首费：输入0.01-999.99（最多包含两位小数） 不能为空也不能为0
        /// </summary>
        public decimal StartFee { get; set; } = 10;

        /// <summary>
        ///     续费标准：输入1-9999范围内的整数
        /// </summary>
        public decimal EndStandard { get; set; } = 1;

        /// <summary>
        ///     续费：输入0.01-999.99（最多包含两位小数） 不能为空也不能为0
        /// </summary>
        public decimal EndFee { get; set; } = 8;
    }
}