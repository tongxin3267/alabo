using Microsoft.AspNetCore.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Validations.Attributes
{
    /// <summary>
    ///     数组、List、IEnumerable类型验证：1.不能为空 2.必须要有值
    /// </summary>
    public class ListAttribute : ValidationProviderAttribute
    {
        public override IEnumerable<ValidationAttribute> GetValidationAttributes()
        {
            return new List<ValidationAttribute>
            {
                ValidationAttributeConst.RequiredAttribute,
                ValidationAttributeConst.EnumerableNotNull
            };
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class IEnumerableNotNullAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) return false;

            return true;
        }
    }
}