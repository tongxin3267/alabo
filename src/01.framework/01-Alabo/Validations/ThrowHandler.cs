﻿using Alabo.Exceptions;
using System.Linq;

namespace Alabo.Validations {

    /// <summary>
    ///     验证失败，抛出异常 - 默认验证处理器
    /// </summary>
    public class ThrowHandler : IValidationHandler {

        /// <summary>
        ///     处理验证错误
        /// </summary>
        /// <param name="results">验证结果集合</param>
        public void Handle(ValidationResultCollection results) {
            if (results.IsValid) {
                return;
            }

            throw new ValidException(results.First().ErrorMessage);
        }
    }
}