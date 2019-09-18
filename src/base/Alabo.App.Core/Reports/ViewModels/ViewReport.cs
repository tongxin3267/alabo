using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Core.Reports.ViewModels {

    /// <summary>
    ///     统计服务
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ViewReport<T> : BaseViewModel {

        /// <summary>
        ///     Key值
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        ///     显示名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     统计数据
        /// </summary>

        public T Value { get; set; }

        /// <summary>
        ///     比例
        /// </summary>
        public decimal Raido { get; set; } = 0.00000m;
    }
}