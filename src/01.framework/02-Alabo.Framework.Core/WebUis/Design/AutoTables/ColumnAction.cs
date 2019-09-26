namespace Alabo.Framework.Core.WebUis.Design.AutoTables
{
    /// <summary>
    ///     表格列上操作，通常用来做审核功能
    ///     比如提现初审，二审
    /// </summary>
    public class ColumnAction
    {
        /// <summary>
        ///     操作名称
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        ///     弹窗表格的Form对象
        ///     必须继承IAutoForm
        /// </summary>
        public string Type { get; set; }
    }
}