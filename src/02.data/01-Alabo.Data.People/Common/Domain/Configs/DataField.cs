using Alabo.Core.Enums.Enum;
using Alabo.Domains.Enums;

namespace Alabo.App.Core.Common.Domain.CallBacks {

    /// <summary>
    ///     数据库字段，CSS内容管理系统，代码生成，可视化编辑都需要此字段
    ///     通过次字段动态构建视图，列表，表单等操作
    ///     配置通过Field特性配置  FieldAttribute
    /// </summary>
    public class DataField {

        /// <summary>
        ///     字段的英文名
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        ///     字段中文名称
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        ///     字段的值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        ///     默认值
        /// </summary>
        public string Default { get; set; }

        /// <summary>
        ///     控件类型，输入框类型
        /// </summary>
        public ControlsType ControlsType { get; set; } = ControlsType.TextBox;

        /// <summary>
        ///     数据类型
        /// </summary>
        public ZkCloudDateType DataType { get; set; } = ZkCloudDateType.String;

        /// <summary>
        ///     控件的数据源
        /// </summary>
        public string DataSource { get; set; }

        /// <summary>
        ///     行数，比如TextBox
        /// </summary>
        public int Row { get; set; } = 4;

        /// <summary>
        ///     是否在列表页面上面显示
        /// </summary>
        public bool ListShow { get; set; }

        /// <summary>
        ///     是否在编辑页面上面显示
        /// </summary>
        public bool EditShow { get; set; } = true;

        /// <summary>
        ///     是否是否能够发布
        /// </summary>
        public bool UserCanPublish { get; set; } = false;

        /// <summary>
        ///     字段排序，从小到大排列，默认值9999
        /// </summary>
        public long SortOrder { get; set; } = 9999;

        /// <summary>
        ///     宽度，列表页显示宽度
        ///     可以是宽度也可以是100%
        /// </summary>
        public string Width { get; set; }

        /// <summary>
        ///     多标签Id，数字越小越在前前面，与List<GroupTab>的Id对应
        /// </summary>
        public long GroupTabId { get; set; }

        /// <summary>
        ///     帮助文字信息
        /// </summary>
        public string HelpText { get; set; } = string.Empty;
    }
}