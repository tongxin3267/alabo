using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Linq.Dynamic;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Validations.Attributes
{
    /// <summary>
    ///     用户ID验证:不能为空，Id>0,能够查找到用户
    /// </summary>
    public class UserIdAttribute : ValidationProviderAttribute
    {
        /// <summary>
        ///     验证特性
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<ValidationAttribute> GetValidationAttributes()
        {
            return new List<ValidationAttribute>
            {
                ValidationAttributeConst.RequiredAttribute,
                ValidationAttributeConst.LessThanOrEqualZero,
                ValidationAttributeConst.UserNotFind
            };
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class UserFindAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) {
                return false;
            }

            var userId = value.ConvertToLong();
            if (userId <= 0) {
                return false;
            }

            var find = EntityDynamicService.GetSingleUser(userId);
            if (find == null) {
                return false;
            }

            if (find.Status != Status.Normal) {
                return false;
            }

            return true;
        }
    }
}