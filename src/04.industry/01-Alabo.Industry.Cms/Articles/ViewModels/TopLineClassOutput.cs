using Alabo.Web.Mvc.ViewModel;

namespace Alabo.Industry.Cms.Articles.ViewModels
{
    /// <summary>
    /// </summary>
    public class TopLineClassOutput : BaseViewModel
    {
        /// <summary>
        ///     Gets or sets the name of the class.
        /// </summary>
        /// <value>
        ///     The name of the class.
        /// </value>
        public string ClassName { get; set; }

        /// <summary>
        ///     Gets or sets the entity identifier.
        /// </summary>
        /// <value>
        ///     The entity identifier.
        /// </value>
        public long EntityId { get; set; }

        /// <summary>
        ///     Gets or sets the relation identifier.
        /// </summary>
        /// <value>
        ///     The relation identifier.
        /// </value>
        public long RelationId { get; set; }
    }
}