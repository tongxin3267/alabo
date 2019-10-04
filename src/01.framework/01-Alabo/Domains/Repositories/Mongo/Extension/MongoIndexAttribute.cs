using System;

namespace Alabo.Domains.Repositories.Mongo.Extension {

    /// <summary>
    ///     Class MongoIndexAttribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MongoIndexAttribute : Attribute {

        public MongoIndexAttribute() {
            Unique = false;
            Ascending = true;
        }

        /// <summary>
        ///     是否是唯一的  默认flase
        /// </summary>
        public bool Unique { get; set; }

        /// <summary>
        ///     是否是升序 默认true
        /// </summary>
        public bool Ascending { get; set; }

        /// <summary>
        ///     是否是组合索引名称
        /// </summary>
        public string GroupField { get; set; }
    }
}