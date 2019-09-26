using Alabo.Exceptions;
using Alabo.Validations;
using System.Linq;
using System.Runtime.Serialization;

namespace Alabo.Web.Views
{
    /// <summary>
    ///     视图模型
    /// </summary>
    [DataContract]
    public abstract class ViewModelBase : IValidation
    {
        /// <summary>
        ///     验证
        /// </summary>
        public virtual ValidationResultCollection Validate()
        {
            var result = DataAnnotationValidation.Validate(this);
            if (result.IsValid) return ValidationResultCollection.Success;

            throw new ValidException(result.First().ErrorMessage);
        }
    }
}