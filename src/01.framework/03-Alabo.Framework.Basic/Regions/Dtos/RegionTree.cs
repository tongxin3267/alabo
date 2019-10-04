namespace Alabo.Framework.Basic.Regions.Dtos {

    public class RegionTree {

        /// <summary>
        ///     区域Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     名称
        /// </summary>

        public string Name { get; set; }

        /// <summary>
        ///     父级Id
        /// </summary>
        public long ParentId { get; set; }
    }
}