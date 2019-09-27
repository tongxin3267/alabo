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
        ///     HTMl文本
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "HtmlEditBox")]
        HtmlEditBox = 8,

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
        ///     可编辑的下拉列表
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "EditableDropdownList")]
        EditableDropdownList = 14,

        /// <summary>
        ///     样式参考：http://ui.5ug.com/metronic_v4.5.4/theme/admin_4/components_bootstrap_switch.html
        /// </summary>
        [LabelCssClass("m-badge--success", IsDataField = true)]
        [Display(Name = "Switch切换")]
        Switch = 15,

        ListBox = 16,

        DropDownListMutil = 17,

        /// <summary>
        ///     数字框，浮点数
        /// </summary>
        [LabelCssClass("m-badge--success", IsDataField = true)]
        [Display(Name = "浮点数")]
        Decimal = 18,

        /// <summary>
        ///     可编辑的下拉列表
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "EditableDropdownList")]
        ListBoxMultiple = 19,

        /// <summary>
        ///     时间范围
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "DateTimePickerRank")]
        DateTimePickerRank = 20,

        /// <summary>
        ///     复训多选
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "CheckBoxList")]
        CheckBoxMultipl = 21,

        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "RadioButtonMultipl")]
        RadioButtonMultipl = 30,

        /// <summary>
        ///     商品分类
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "商品分类")]
        ProductCate = 31,

        /// <summary>
        ///     商品标签
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "商品标签")]
        ProductTag = 32,

        /// <summary>
        ///     地区
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "地区")]
        City = 33,

        /// <summary>
        ///     邮箱
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "邮箱")]
        Email = 34,

        /// <summary>
        ///     用户类型与等级
        ///     显示两个联动下拉框，第一个显示用户类型，第二个显示类型等级
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "用户类型与等级")]
        UserTypeGrade = 35,

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

        Color = 51,

        /// <summary>
        ///     布尔值
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "布尔值")]
        Bool = 52,

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
        [Display(Name = "用户名")]
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
        ///     链接选择
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "链接选择")]
        Link = 1007,

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
        DataSelect = 1200
    }
}