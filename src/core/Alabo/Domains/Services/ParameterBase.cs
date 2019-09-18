using System.Linq;
using Alabo.Exceptions;
using Alabo.Validations;

namespace Alabo.Domains.Services
{
    /// <summary>
    ///     参数
    /// </summary>
    public abstract class ParameterBase : IValidation
    {
        /// <summary>
        ///     验证
        /// </summary>
        public virtual ValidationResultCollection Validate()
        {
            var result = DataAnnotationValidation.Validate(this);
            if (result.IsValid) {
                return ValidationResultCollection.Success;
            }

            throw new ValidException(result.First().ErrorMessage);
        }
    }
}