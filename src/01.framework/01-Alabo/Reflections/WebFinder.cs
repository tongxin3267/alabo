﻿using Microsoft.Extensions.PlatformAbstractions;
using System.Collections.Generic;
using System.Reflection;

namespace Alabo.Reflections {

    /// <summary>
    ///     Web类型查找器
    /// </summary>
    public class WebFinder : Finder {

        /// <summary>
        ///     获取程序集列表
        /// </summary>
        public override List<Assembly> GetAssemblies() {
            LoadAssemblies(PlatformServices.Default.Application.ApplicationBasePath);
            return base.GetAssemblies();
        }
    }
}