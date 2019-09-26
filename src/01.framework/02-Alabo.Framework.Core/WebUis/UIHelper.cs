using System;
using System.Collections.Generic;
using Alabo.Extensions;

namespace Alabo.Framework.Core.WebUis
{
    /// <summary>
    ///     UI相关的函数
    /// </summary>
    public static class UIHelper
    {
        /// <summary>
        ///     随机颜色
        /// </summary>
        public static string Color
        {
            get
            {
                var list = new List<string>
                {
                    "info",
                    "success",
                    "warning",
                    "danger",
                    "primary",
                    "focus",
                    "accent",
                    "brand",
                    "metal",
                    "secondary"
                };

                var index = new Random().Next(0, list.Count - 1);
                return list[index];
            }
        }

        /// <summary>
        ///     随机图标
        /// </summary>
        public static string Icon
        {
            get
            {
                var index = new Random().Next(1, 80);
                index.IntToEnum<Flaticon>(out var flaticon);
                return flaticon.GetIcon();
            }
        }
    }
}