﻿using System.Threading.Tasks;

namespace Alabo.Initialize {

    /// <summary>
    ///     默认数据初始
    /// </summary>
    public interface IDe人faultInit {

        /// <summary>
        /// 是否租户初始化时初始
        /// </summary>
        bool IsTenant { get; }

        /// <summary>
        ///     初始化默认数据
        /// </summary>
        void Init();
    }
}