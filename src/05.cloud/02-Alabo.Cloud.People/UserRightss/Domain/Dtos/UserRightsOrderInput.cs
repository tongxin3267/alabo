using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Core.User;
using Alabo.App.Core.User.Domain.Callbacks;
using Alabo.App.Core.User.Domain.Dtos;
using Alabo.App.Core.User.Domain.Entities;
using Alabo.App.Market.UserRightss.Domain.Entities;
using Alabo.App.Market.UserRightss.Domain.Enums;
using Alabo.Domains.Entities;
using Alabo.Framework.Basic.Grades.Domain.Configs;
using Alabo.Users.Entities;
using Alabo.Validations;

namespace Alabo.App.Market.UserRightss.Domain.Dtos {

    public class UserRightsOrderInput {

        [Display(Name = "用户Id")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public long UserId { get; set; }

        /// <summary>
        ///     手机号码
        /// </summary>
        [Display(Name = "手机号码")]
        //[Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string Mobile { get; set; }

        /// <summary>
        ///     支付金额
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        ///     姓名,公司名字
        /// </summary>
        [Display(Name = "姓名或公司名称")]
        //[Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string Name { get; set; }

        /// <summary>
        ///     所属区域
        /// </summary>
        // [Display(Name = "所属区域")]
        public string RegionId { get; set; }

        /// <summary>
        /// 推荐人
        /// </summary>
        public string Parent { get; set; }

        /// <summary>
        /// 推荐人
        /// </summary>

        public User ParnetUser { get; set; }

        /// <summary>
        ///     开通方式
        /// </summary>
        public UserRightOpenType OpenType { get; set; }

        /// <summary>
        ///     等级Id
        /// </summary>
        [Display(Name = "等级Id不能为空")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public Guid GradeId { get; set; }

        /// <summary>
        ///     登录用户
        /// </summary>
        [JsonIgnore]
        public User User { get; set; }

        /// <summary>
        /// 被购买的用户
        /// 如果是立即购买或升级则是当前登录用户
        /// 如果是帮别人开通则是其他用户
        /// </summary>
        public User BuyUser { get; set; }

        /// <summary>
        ///     购买的等级
        /// </summary>
        [JsonIgnore]
        public UserGradeConfig BuyGrade { get; set; }

        /// <summary>
        ///     目前会员等级
        /// </summary>
        [JsonIgnore]
        public UserGradeConfig CurrentGrade { get; set; }

        /// <summary>
        ///     登录用户当前权益
        /// </summary>
        [JsonIgnore]
        public IList<UserRights> UserRightList { get; set; } = new List<UserRights>();

        /// <summary>
        /// 注册用户信息
        /// </summary>
        public RegInput RegInfo { get; set; }
    }
}