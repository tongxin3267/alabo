using System;
using System.ComponentModel.DataAnnotations;
using Alabo.Datas.Ef.SqlServer;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Regexs;
using Alabo.Security;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.Bson.Serialization.Attributes;

namespace Alabo.Users.Entities
{
    /// <summary>
    ///     用户
    /// </summary>
    [ClassProperty(Name = "用户")]
    public class User : AggregateDefaultRoot<User>
    {
        /// <summary>
        ///     用户名（以字母开头，包括a-z,0-9和_）：[a-zA-z][a-zA-Z0-9_]{2,15}
        /// </summary>
        [StringLength(15, MinimumLength = 3, ErrorMessage = ErrorMessage.NameNotInRang)]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [RegularExpression(RegularExpressionHelper.UserName, ErrorMessage = ErrorMessage.NotMatchFormat)]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        /// <summary>
        ///     姓名
        /// </summary>
        [Display(Name = "姓名")]
        [StringLength(20, ErrorMessage = ErrorMessage.MaxStringLength)]
        public string Name { get; set; }

        /// <summary>
        ///     邮箱
        /// </summary>
        [Display(Name = "邮箱")]
        //[Remote("verify_email", HttpMethod = "POST", ErrorMessage = ErrorMessage.IsUserd)]
        [RegularExpression(RegularExpressionHelper.Email, ErrorMessage = ErrorMessage.NotMatchFormat)]
        public string Email { get; set; }

        /// <summary>
        ///     手机号码
        /// </summary>

        [Display(Name = "手机")]
        //[Remote("verify_mobile", HttpMethod = "POST", ErrorMessage = ErrorMessage.IsUserd)]
        //[Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [RegularExpression(RegularExpressionHelper.ChinaMobile, ErrorMessage = ErrorMessage.NotMatchFormat)]
        public string Mobile { get; set; }

        /// <summary>
        ///     上级用户id http://localhost:9000/user/from?id=1
        /// </summary>
        [Display(Name = "上级用户id")]
        public long ParentId { get; set; } = 0;

        /// <summary>
        ///     用户状态
        /// </summary>
        [Display(Name = "用户状态")]
        public Status Status { get; set; } = Status.Normal;

        /// <summary>
        ///     租户
        /// </summary>
        public string Tenant { get; set; }

        /// <summary>
        ///     用户等级Id 每个类型对应一个等级
        /// </summary>
        [Display(Name = "用户等级Id")]
        public Guid GradeId { get; set; }

        /// <summary>
        ///     Gets or sets the detail.
        /// </summary>
        [BsonIgnore]
        [Display(Name = "细节")]
        public UserDetail Detail { get; set; } = new UserDetail();

        /// <summary>
        ///     Gets or sets the map.
        /// </summary>
        [BsonIgnore]
        [Display(Name = "组织架构图")]
        public UserMap Map { get; set; } = new UserMap();

        /// <summary>
        ///     获取s the name of the 会员.
        /// </summary>
        public string GetUserName()
        {
            var name = $@"{UserName}({Name})";
            return name;
        }

        /// <summary>
        ///     转换成基础用户
        /// </summary>
        /// <returns></returns>
        public BasicUser ToBasicUser()
        {
            var loginUser = new BasicUser
            {
                UserName = UserName,
                Email = Email,
                GradeId = GradeId,
                Status = Status,
                Id = Id,
                Name = Name,
                Tenant = Tenant
            };
            return loginUser;
        }
    }

    /// <summary>
    ///     应用程序映射配置
    /// </summary>
    public class UserTableMap : MsSqlAggregateRootMap<User>
    {
        /// <summary>
        ///     映射表
        /// </summary>
        protected override void MapTable(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User_User");
        }

        /// <summary>
        ///     映射属性
        /// </summary>
        protected override void MapProperties(EntityTypeBuilder<User> builder)
        {
            //应用程序编号
            builder.HasKey(t => t.Id);
         
            builder.Ignore(e => e.Tenant);
            builder.Ignore(e => e.Map);
            builder.Ignore(e => e.Detail);
            builder.Ignore(e => e.Tenant);
            builder.Ignore(e => e.Tenant);
            builder.Property(e => e.UserName).HasMaxLength(20);
        }
    }
}