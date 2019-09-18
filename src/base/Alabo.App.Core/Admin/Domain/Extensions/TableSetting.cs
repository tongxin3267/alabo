using System.Collections.Generic;
using Alabo.Domains.Entities.Extensions;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.Admin.Domain.Extensions {

    public class TableSetting : EntityExtension {

        /// <summary>
        ///     表格的ViewModel类型
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        ///     当前页数量
        /// </summary>
        public long PageSize { get; set; } = 20;

        /// <summary>
        ///     表头列属性，动态构建用户的表格
        /// </summary>
        public IEnumerable<FieldAttribute> FieldAttributes { get; set; } = new List<FieldAttribute>();
    }
}