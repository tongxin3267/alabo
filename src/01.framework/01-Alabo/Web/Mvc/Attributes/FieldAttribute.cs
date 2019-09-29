using Alabo.Datas.Queries.Enums;
using Alabo.Domains.Enums;
using Alabo.Web.Validations;
using System;

namespace Alabo.Web.Mvc.Attributes
{
    /// <summary>
    ///     字段属性配置
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field |
                    AttributeTargets.Parameter)]
    public class FieldAttribute : Attribute
    {
        /// <summary>
        ///     Gets the key.
        /// </summary>
        public string Key { get; }

        /// <summary>
        ///     是否为主字段
        /// </summary>
        public bool IsMain { get; set; } = false;

        /// <summary>
        ///     控件类型，输入框类型
        /// </summary>
        public ControlsType ControlsType { get; set; }

        public ControlsType ApiControlsType { get; set; }

        /// <summary>
        ///     控件的数据源
        /// </summary>
        public string DataSource { get; set; }

        /// <summary>
        ///     控件的数据源类型
        /// </summary>
        public Type DataSourceType { get; set; }

        /// <summary>
        ///     Api接口的数据源
        /// </summary>
        public string ApiDataSource { get; set; }

        /// <summary>
        ///     行数，比如TextBox
        /// </summary>
        public int Row { get; set; } = 4;

        /// <summary>
        ///     Gets or sets the selection.
        /// </summary>
        public string Selection { get; set; }

        /// <summary>
        ///     是否在列表页面上面显示
        /// </summary>
        public bool ListShow { get; set; }

        /// <summary>
        ///     是否在编辑页面上面显示
        /// </summary>
        public bool EditShow { get; set; } = true;

        /// <summary>
        ///     字段排序，从小到大排列，默认值9999
        /// </summary>
        public long SortOrder { get; set; } = 9999;

        /// <summary>
        ///     宽度，列表页显示宽度
        ///     可以是宽度也可以是100%
        /// </summary>
        public string Width { get; set; } = "110";

        /// <summary>
        ///     标识枚举是否唯一，如果是唯一，
        ///     枚举值小于零的时候，可以添加多个
        ///     枚举值大于等于零时，只能添加一个
        /// </summary>
        public bool EnumUniqu { get; set; } = false;

        /// <summary>
        ///     验证方式
        ///     远程验证方式
        /// </summary>
        public ValidType ValidType { get; set; }

        /// <summary>
        ///     是否为默认，比如枚举的时候，设置是否为默认值
        /// </summary>
        public bool IsDefault { get; set; } = false;

        /// <summary>
        ///     Gets or sets the group tab identifier.
        /// </summary>
        public long GroupTabId { get; set; }

        /// <summary>
        ///     Gets or sets the display mode.
        /// </summary>
        public DisplayMode DisplayMode { get; set; } = DisplayMode.Value;

        /// <summary>
        ///     表格显示方式
        /// </summary>
        public TableDispalyStyle TableDispalyStyle { get; set; }

        /// <summary>
        ///     系统指定Guid，防止数据冲突
        /// </summary>
        public string GuidId { get; set; }

        /// <summary>
        ///     图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        ///     其他标志
        ///     控件类型为json的时候 Mark=1 表示可以批量填充数据
        /// </summary>
        public string Mark { get; set; }

        /// <summary>
        ///     是否显示图片预览
        /// </summary>
        public bool IsImagePreview { get; set; } = false;

        /// <summary>
        ///     链接地址，如果链接地址存在时，显示链接，支持一个参数传递
        ///     参数必须和Model的File命名一样
        ///     参数格式：{{字段名}}
        ///     如：/Admin/Role/PostRoleEdit?Id=[[Id]]
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        ///     颜色
        /// </summary>
        public LabelColor LabelColor { get; set; } = 0;

        /// <summary>
        ///     是否在 基本搜索里头显示
        ///     只支持文本框搜索
        /// </summary>
        public bool IsShowBaseSerach { get; set; }

        /// <summary>
        ///     是否是高级搜索上面显示
        /// </summary>
        public bool IsShowAdvancedSerach { get; set; } = false;

        /// <summary>
        ///     是否为多标签搜索
        ///     只支持一个
        ///     通过datasource构建多标签，支持AutoConfig,枚举
        /// </summary>
        public bool IsTabSearch { get; set; } = false;

        /// <summary>
        ///     搜索操作符
        /// </summary>
        public Operator Operator { get; set; } = Operator.Equal;

        /// <summary>
        ///     提示文字
        /// </summary>
        public string PlaceHolder { get; set; }

        /// <summary>
        ///     与数据库对应的字段名称
        ///     为空是，本身即为数据库字段
        /// </summary>
        public string DataField { get; set; }

        /// <summary>
        ///     是否支持排序
        /// </summary>
        public bool SupportSearchOrder { get; set; } = false;

        /// <summary>
        ///     扩展字段属性，用来反序列json，为对应的实体名称
        ///     Gets or sets the extensions.
        /// </summary>
        public string ExtensionJson { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether [json can 添加 或 删除].
        ///     json操作表单中，可以添加新行或删除行
        /// </summary>
        public bool JsonCanAddOrDelete { get; set; } = true;
    }

    /// <summary>
    ///     Enum DisplayMode
    /// </summary>
    [ClassProperty(Name = "显示模式")]
    public enum DisplayMode
    {
        /// <summary>
        ///     The text
        /// </summary>
        Text = 1,

        /// <summary>
        ///     The value
        /// </summary>
        Value = 2,

        /// <summary>
        ///     The 所有
        /// </summary>
        All = 3,

        /// <summary>
        ///     等级
        /// </summary>
        Grade = 5
    }

    /// <summary>
    ///     表格显示方式
    /// </summary>
    [ClassProperty(Name = "表格显示方式")]
    public enum TableDispalyStyle
    {
        /// <summary>
        ///     代码的方式显示
        /// </summary>
        Code = 1
    }
}