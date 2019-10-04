using System.ComponentModel.DataAnnotations;

namespace Alabo.Validations.Attributes {

    /// <summary>
    ///     验证特性常量
    /// </summary>
    public static class ValidationAttributeConst {

        /// <summary>
        ///     必填特性
        /// </summary>
        public static RequiredAttribute RequiredAttribute {
            get {
                var requiredAttribute = new RequiredAttribute {
                    ErrorMessage = ErrorMessage.NameNotAllowEmpty
                };
                return requiredAttribute;
            }
        }

        /// <summary>
        ///     不能小于等于0
        /// </summary>
        public static RangeAttribute LessThanOrEqualZero {
            get {
                var rangeAttribute = new RangeAttribute(0.01, double.MaxValue) {
                    ErrorMessage = ErrorMessage.LessThanOrEqualZero
                };
                return rangeAttribute;
            }
        }

        /// <summary>
        ///     用户不存在
        /// </summary>
        public static UserFindAttribute UserNotFind {
            get {
                var userNotFind = new UserFindAttribute {
                    ErrorMessage = "用户不存在或状态不存在"
                };
                return userNotFind;
            }
        }

        /// <summary>
        ///     对象不能空
        /// </summary>
        public static IEnumerableNotNullAttribute EnumerableNotNull {
            get {
                var userNotFind = new IEnumerableNotNullAttribute {
                    ErrorMessage = $"{0}值数量不能0"
                };
                return userNotFind;
            }
        }
    }
}