namespace Alabo.Industry.Offline.Product.ViewModels
{
    /// <summary>
    ///     RelationViewModel
    /// </summary>
    public class RelationViewModel
    {
        /// <summary>
        ///     Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Icon
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        ///     Check
        /// </summary>
        public bool Check { get; set; }

        /// <summary>
        ///     Sort
        /// </summary>
        public long SortOrder { get; set; }
    }
}