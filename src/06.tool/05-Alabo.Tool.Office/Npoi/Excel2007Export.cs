﻿using Alabo.Tool.Office.Core;

namespace Alabo.Tool.Office.Npoi
{
    /// <summary>
    ///     Npoi Excel2007 导出操作
    /// </summary>
    public class Excel2007Export : ExcelExportBase
    {
        /// <summary>
        ///     初始化Npoi Excel2003 导出操作
        /// </summary>
        public Excel2007Export()
            : base(ExportFormat.Xlsx, new Excel2007())
        {
        }
    }
}