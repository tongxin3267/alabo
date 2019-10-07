using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Domains.Enums
{
    /// <summary>
    ///     控件类型
    /// </summary>
    [ClassProperty(Name = "控件类型")]
    public enum ControlsType
    {
        /// <summary>
        ///     Label类型
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "Label")]
        Label = 1,

        /// <summary>
        ///     文本框
        /// </summary>
        [LabelCssClass("m-badge--success", IsDataField = true)]
        [Display(Name = "文本框")]
        TextBox = 2,

        /// <summary>
        ///     多行文本
        /// </summary>
        [Display(Name = "多行文本")]
        [LabelCssClass(BadgeColorCalss.Success)]
        TextArea = 3,

        /// <summary>
        ///     多选框
        /// </summary>
        [LabelCssClass("m-badge--success", IsDataField = true)]
        [Display(Name = "多选框")]
        CheckBox = 4,

        /// <summary>
        ///     单选框
        /// </summary>
        [LabelCssClass("m-badge--success", IsDataField = true)]
        [Display(Name = "单选框")]
        RadioButton = 5,

        /// <summary>
        ///     下拉列表
        /// </summary>
        [LabelCssClass("m-badge--success", IsDataField = true)]
        [Display(Name = "下拉列表")]
        DropdownList = 6,

        /// <summary>
        ///     数字框
        /// </summary>
        [LabelCssClass("m-badge--success", IsDataField = true)]
        [Display(Name = "数字框")]
        Numberic = 7,

        /// <summary>
        ///     时间框
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "时间框")]
        DateTimePicker = 9,

        /// <summary>
        ///     图片类型
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "图片")]
        AlbumUploder = 10,

        /// <summary>
        ///     附件类型
        /// </summary>
        [Display(Name = "附件")]
        [LabelCssClass(BadgeColorCalss.Success)]
        FileUploder = 11,

        /// <summary>
        ///     密码类型
        /// </summary>
        [Display(Name = "密码")]
        [LabelCssClass(BadgeColorCalss.Success)]
        Password = 12,

        /// <summary>
        ///     隐藏类型
        /// </summary>
        [Display(Name = "隐藏")]
        [LabelCssClass(BadgeColorCalss.Success)]
        Hidden = 13,

        /// <summary>
        ///     Swith开关,用户bool类型
        /// </summary>
        [LabelCssClass("m-badge--success", IsDataField = true)]
        [Display(Name = "Switch切换")]
        Switch = 15,

        /// <summary>
        ///    浮点数
        /// </summary>
        [LabelCssClass("m-badge--success", IsDataField = true)]
        [Display(Name = "浮点数")]
        Decimal = 18,

        /// <summary>
        ///     级联标签
        ///     recursive   mode="Tag"
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "EditableDropdownList")]
        RelationTags = 40,

        /// <summary>
        ///     级联分类
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "EditableDropdownList")]
        RelationClass = 41,

        /// <summary>
        ///     省份选择
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "省份选择")]
        ProvinceDropList = 42,

        /// <summary>
        ///     城市选择
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "城市选择")]
        CityDropList = 43,

        /// <summary>
        ///     区县选择
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "区县选择")]
        CountyDropList = 44,

        /// <summary>
        ///     百度编辑器
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "EditableDropdownList")]
        Editor = 45,

        /// <summary>
        /// 颜色
        /// </summary>
        Color = 51,

        /// <summary>
        ///     表单提交时的确认框，比如同意注册协议等
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "布尔值")]
        Agree = 53,

        /// <summary>
        ///     手机验证号
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "手机号")]
        Phone = 54,

        /// <summary>
        ///     手机验证号
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "手机验证码")]
        PhoneVerifiy = 55,

        /// <summary>
        ///     用户名
        ///     在列表页面时输入用户名
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "用户名")]
        UserName = 1001,

        /// <summary>
        ///     数字区间
        ///     支持浮点，整数等类型
        ///     用于高级搜索
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "数字区间")]
        NumberRang = 1002,

        /// <summary>
        ///     时间范围
        ///     用于高级搜索
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "时间范围")]
        DateTimeRang = 1003,

        /// <summary>
        ///     图片预览
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "图片预览")]
        ImagePreview = 1004,

        /// <summary>
        ///     Json方式显示
        ///     需要设置ExtensionJson
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "Json")]
        Json = 1005,

        /// <summary>
        ///     图标选择
        ///     需要设置ExtensionJson
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "图标选择")]
        Icon = 1006,

        /// <summary>
        ///     字列表
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "字列表")]
        ChildList = 1008,

        /// <summary>
        ///     Json列表方式显示
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "JsonList")]
        JsonList = 1009,

        /// <summary>
        ///     表格列操作按钮
        /// </summary>
        ColumnButton = 1100,

        /// <summary>
        ///     数据选择控件，对应前台x-select-data
        /// </summary>
        DataSelect = 1200,

        /// <summary>
        /// Markdown
        /// </summary>
        [Display(Name = "Markdown")]
        Markdown = 1280,
    }
}