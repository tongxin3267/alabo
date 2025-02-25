﻿using Alabo.Framework.Tasks.Queues.Models;
using System.Collections.Generic;

namespace Alabo.App.Share.RewardRuless.Dtos
{
    /// <summary>
    /// </summary>
    public class RewardModulesOutput
    {
        /// <summary>
        ///     数量
        /// </summary>
        public long Count { get; set; }

        /// <summary>
        /// </summary>
        public List<TaskModuleAttribute> ShareModules { get; set; }
    }
}