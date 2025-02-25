﻿namespace Alabo.Tool.Office
{
    /// <summary>
    ///     文件导出工厂
    /// </summary>
    public interface IExportFactory
    {
        /// <summary>
        ///     创建文件导出
        /// </summary>
        /// <param name="format">导出格式</param>
        IExport Create(ExportFormat format = ExportFormat.Xlsx);
    }
}