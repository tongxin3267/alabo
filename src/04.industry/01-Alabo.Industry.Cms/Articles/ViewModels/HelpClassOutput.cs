using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Cms.Articles.ViewModels {

    /// <summary>
    /// </summary>
    public class HelpClassOutput : BaseViewModel {

        /// <summary>
        ///     Gets or sets the name of the class.
        /// </summary>
        /// <value>
        ///     The name of the class.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the relation identifier.
        /// </summary>
        /// <value>
        ///     The relation identifier.
        /// </value>
        public string Url { get; set; }

        /// <summary>
        ///     Gets or sets the Icon
        /// </summary>
        /// <value>
        ///     The relation Icon
        /// </value>
        public string Image { get; set; }
    }
}