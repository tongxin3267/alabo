using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Alabo.App.Share.Rewards.Domain.Entities;
using Alabo.App.Share.Rewards.Domain.Services;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Data.People.Users.Dtos;
using Alabo.Domains.Entities;
using Alabo.Extensions;
using Alabo.Framework.Basic.AutoConfigs.Domain.Configs;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebUis;
using Alabo.UI;
using Alabo.UI.Design.AutoPreviews;
using Alabo.UI.Design.AutoTables;
using Alabo.Web.Mvc.Attributes;
using ZKCloud.Open.Share.Models;

namespace Alabo.App.Share.Rewards.Dtos
{
    /// <summary>
    ///     Class RewardApiOutput.
    /// </summary>
    public class RewardApiOutput
    {
        /// <summary>
        ///     Gets or sets the reward.
        /// </summary>
        /// <value>The reward.</value>
        [Display(Name = "奖赏")]
        public Reward Reward { get; set; }

        /// <summary>
        ///     Gets or sets the share user.
        /// </summary>
        /// <value>The share user.</value>
        [Display(Name = "用户分享")]
        public UserOutput ShareUser { get; set; }

        /// <summary>
        ///     Gets or sets the order user.
        /// </summary>
        /// <value>The order user.</value>
        [Display(Name = "订购用户")]
        public UserOutput OrderUser { get; set; }

        /// <summary>
        ///     Gets or sets the name of the money type.
        /// </summary>
        /// <value>The name of the money type.</value>
        [Display(Name = "货币类型")]
        public string MoneyTypeName { get; set; }

        /// <summary>
        ///     Gets or sets the share module.
        /// </summary>
        /// <value>The share module.</value>
        [Display(Name = "共享模块")]
        public ShareModule ShareModule { get; set; }

        public PageResult<RewardApiOutput> PageTable(object query, AutoBaseModel autoModel)
        {
            throw new NotImplementedException();
        }
    }

    public class RewardPriviewOutput : UIBase, IAutoTable<RewardApiOutput>, IAutoPreview
    {
        public long Id { get; set; }

        /// <summary>
        ///     分润订单Id,对应ShareOrder表，不是商城的Order_Order 表
        /// </summary>
        [Display(Name = "分润订单Id")]
        public long OrderId { get; set; }

        /// <summary>
        ///     触发分润用户的Id
        /// </summary>
        [Required]
        [Display(Name = "触发用户")]
        public long OrderUserId { get; set; }

        /// <summary>
        ///     触发分润用户的Id
        /// </summary>
        [Required]
        [Display(Name = "触发用户")]
        [Field(ListShow = true, EditShow = true, SortOrder = 3)]
        public string OrderUserName { get; set; }

        /// <summary>
        ///     分润货币ID
        /// </summary>
        public Guid MoneyTypeId { get; set; }

        /// <summary>
        ///     分润货币ID
        /// </summary>
        [Required]
        [Display(Name = "货币类型")]
        [Field(ListShow = true, EditShow = true, SortOrder = 4)]
        public string MoneyTypeName { get; set; }

        /// <summary>
        ///     分润金额
        /// </summary>
        [Required]
        [Display(Name = "金额")]
        [Field(ListShow = true, EditShow = true, SortOrder = 1)]
        public decimal Amount { get; set; }

        /// <summary>
        ///     分润后改账户金额
        /// </summary>
        [Required]
        [Display(Name = "账后金额")]
        [Field(ListShow = true, EditShow = true, SortOrder = 5)]
        public decimal AfterAmount { get; set; }

        ///// <summary>
        /////     分润状态
        ///// </summary>
        //[Required]
        //[Display(Name = "分润状态")]
        //[Field(ListShow = true, EditShow = true,  DataSource = "Alabo.App.Share.Share.Domain.Enums.FenRunStatus", SortOrder = 3)]
        //public FenRunStatus Status { get; set; }

        ///// <summary>
        /////     分润状态
        ///// </summary>
        //[Required]
        //[Display(Name = "分润状态")]
        //[Field(ListShow = true, EditShow = true, SortOrder = 3)]
        //public string StatusName { get; set; }

        /// <summary>
        ///     分润类型Id：在分润维度设计的配置模块
        /// </summary>
        [Required]
        [Display(Name = "分润类型Id")]
        public Guid ModuleId { get; set; }

        /// <summary>
        ///     分润配置Id
        /// </summary>
        [Display(Name = "分润配置Id")]
        public long ModuleConfigId { get; set; }

        /// <summary>
        ///     分润简要介绍
        ///     有分润维度的日志模板生成
        /// </summary>
        [Required]
        [Display(Name = "简介")]
        [Field(ListShow = true, EditShow = true)]
        public string Intro { get; set; }

        /// <summary>
        ///     根据Id自动生成12位序列号
        /// </summary>
        [Display(Name = "编号")]
        [Field(ListShow = true, EditShow = true, SortOrder = 4)]
        public string Serial
        {
            get
            {
                var searSerial = $"9{Id.ToString().PadLeft(9, '0')}";
                if (Id.ToString().Length == 10) searSerial = $"{Id.ToString()}";

                return searSerial;
            }
        }

        public AutoPreview GetPreview(string id, AutoBaseModel autoModel)
        {
            var model = Resolve<IRewardService>().GetSingle(u => u.Id == id.ToInt64());
            var monetypes = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();

            var temp = new AutoPreview();

            //var moneyType = temp.KeyValues.FirstOrDefault(s => s.Key.ToString() == "MoneyTypeId");
            //?.Value = "";//

            var result = new RewardPriviewOutput
            {
                Id = model.Id,
                OrderId = model.OrderId,
                MoneyTypeId = model.MoneyTypeId,
                AfterAmount = model.AfterAmount,
                Amount = model.Amount,
                ModuleConfigId = model.ModuleConfigId,
                ModuleId = model.ModuleId,
                OrderUserId = model.OrderUserId,
                OrderUserName = Resolve<IUserService>().GetSingle(model.OrderUserId)?.Name,
                Intro = model.Intro,
                MoneyTypeName = monetypes.FirstOrDefault(s => s.Id == model.MoneyTypeId)?.Name
            };
            temp.KeyValues = result.ToKeyValues();
            return temp;
        }

        public List<TableAction> Actions()
        {
            var list = new List<TableAction>
            {
                ToLinkAction("分润", "/Reward/list")
            };
            return list;
        }

        PageResult<RewardApiOutput> IAutoTable<RewardApiOutput>.PageTable(object query, AutoBaseModel autoModel)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     构建自动表单
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public PageResult<RewardPriviewOutput> PageTable(object query, AutoBaseModel autoModel)
        {
            throw new NotImplementedException();
        }
    }
}