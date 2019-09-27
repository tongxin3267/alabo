using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Reflection;

namespace Alabo.Extensions
{
    public static class ControllerExtensions
    {
        /// <summary>
        ///     TryUpdateModel的函数对象
        /// </summary>

        private static Lazy<MethodInfo> TryUpdateModelMethodInfo { get; }
            = new Lazy<MethodInfo>(() => typeof(Controller).GetMethods(
                BindingFlags.NonPublic | BindingFlags.Instance).First(
                m => m.Name == "TryUpdateModel" && m.GetParameters().Length == 1));

        /// <summary>
        ///     判断表单是否有效
        ///     同时检查AntiForgeryToken和ModelState.IsValid
        /// </summary>
        public static bool IsFormValid(this Controller controller)
        {
            return controller.ModelState.IsValid;
        }

        /// <summary>
        ///     返回检查表单失败的原因
        ///     表单有效时返回null
        /// </summary>
        public static string FormInvalidReason(this Controller controller)
        {
            foreach (var value in controller.ModelState.Values)
            {
                var error = value.Errors.FirstOrDefault();
                if (error != null) return error.ErrorMessage;
            }

            return null;
        }

        /// <summary>
        ///     TryUpdateModel的object版本
        ///     可以在外面调用，调用后会自动验证对象中的所有字段
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="obj"></param>
        public static bool TryUpdateModelForObject(
            this Controller controller, object obj)
        {
            return (bool)TryUpdateModelMethodInfo.Value.MakeGenericMethod(
                obj.GetType()).Invoke(controller, new[] { obj });
        }
    }
}