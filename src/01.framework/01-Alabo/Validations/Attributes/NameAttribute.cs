using Microsoft.AspNetCore.Mvc.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Validations.Attributes {

    public class NameAttribute : ValidationProviderAttribute {

        /// <summary>
        ///     获取所有的长度
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<ValidationAttribute> GetValidationAttributes() {
            return new List<ValidationAttribute>
            {
                ValidationAttributeConst.RequiredAttribute
                //  new RegularExpressionAttribute(pattern: "[A-Za-z]*"),
            };
        }
    }

    /// <summary>
    ///     名称统一验证，名称长度不能超过8
    /// </summary>
    public class Name8Attribute : ValidationProviderAttribute {

        /// <summary>
        ///     获取所有的长度
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<ValidationAttribute> GetValidationAttributes() {
            return new List<ValidationAttribute>
            {
                ValidationAttributeConst.RequiredAttribute,
                //  new RegularExpressionAttribute(pattern: "[A-Za-z]*"),
                new StringLengthAttribute(8)
            };
        }
    }

    /// <summary>
    ///     名称统一验证，名称长度不能超过20
    /// </summary>
    public class Name20Attribute : ValidationProviderAttribute {

        /// <summary>
        ///     获取所有的长度
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<ValidationAttribute> GetValidationAttributes() {
            return new List<ValidationAttribute>
            {
                ValidationAttributeConst.RequiredAttribute,
                //  new RegularExpressionAttribute(pattern: "[A-Za-z]*"),
                new StringLengthAttribute(20)
            };
        }
    }

    /// <summary>
    ///     名称统一验证，名称长度不能超过60
    /// </summary>
    public class Name60Attribute : ValidationProviderAttribute {

        /// <summary>
        ///     获取所有的长度
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<ValidationAttribute> GetValidationAttributes() {
            return new List<ValidationAttribute>
            {
                ValidationAttributeConst.RequiredAttribute,
                //  new RegularExpressionAttribute(pattern: "[A-Za-z]*"),
                new StringLengthAttribute(60)
            };
        }
    }
}