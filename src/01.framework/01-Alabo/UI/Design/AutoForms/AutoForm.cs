﻿using Alabo.Domains.Enums;
using Alabo.Web.Mvc.ViewModel;
using Alabo.Web.Validations;
using System.Collections.Generic;
using Alabo.Extensions;

namespace Alabo.UI.Design.AutoForms
{
    public class AutoForm
    {
        /// <summary>
        ///     如果为空的时候，渲染表单
        ///     如果不为空：渲染信息提示，不渲染表单
        /// </summary>
        public FromMessage FromMessage { get; set; } = null;

        /// <summary>
        ///     Id
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        ///     表单名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     布局方式
        /// </summary>
        public FormLayout Layout { get; set; } = new FormLayout();

        /// <summary>
        ///     字段以及分组
        /// </summary>
        public IList<FieldGroup> Groups { get; set; } = new List<FieldGroup>();

        /// <summary>
        ///     底部按钮文字
        /// </summary>
        public string BottonText { get; set; } = "提交";

        /// <summary>
        ///     表单标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     底部链接
        /// </summary>
        public IEnumerable<ViewLink> ViewLinks { get; set; }

        /// <summary>
        ///     表单上的提示文字
        /// </summary>
        public string AlertText { get; set; } = string.Empty;

        /// <summary>
        ///     表单底部提示文字
        /// </summary>
        public List<string> ButtomHelpText { get; set; } = new List<string>();
    }

    /// <summary>
    ///     表单布局方式
    /// </summary>
    public class FormLayout
    {
        /// <summary>
        ///     表单布局方式
        /// </summary>
        public FormLayoutType Type { get; set; }

        /// <summary>
        ///     长度
        /// </summary>
        public string Width { get; set; }
    }

    /// <summary>
    ///     字段组
    /// </summary>
    public class FieldGroup
    {
        /// <summary>
        ///     分组名称
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        ///     设置列表
        /// </summary>
        public IList<FormFieldProperty> Items { get; set; } = new List<FormFieldProperty>();
    }

    /// <summary>
    ///     表单字段属性
    /// </summary>
    public class FormFieldProperty
    {
        /// <summary>
        ///     字段英文名字
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        ///     Gets or sets the name of the field.
        ///     字段的中文名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the name of the field.
        ///     field value
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        ///     控件类型，输入框类型
        /// </summary>
        public ControlsType ControlsType { get; set; }

        /// <summary>
        /// 控件类型，用于前台数据渲染
        /// </summary>

        public string Type {
            get { return this.ControlsType.ToString().ToLower(); }
        }

        /// <summary>
        ///     必填
        /// </summary>
        public bool Required { get; set; } = false;

        /// <summary>
        ///     控件的数据源
        /// </summary>
        public string DataSource { get; set; }

        /// <summary>
        ///     验证方式
        ///     远程验证方式
        /// </summary>
        public ValidType ValidType { get; set; }

        /// <summary>
        ///     提示文字
        /// </summary>
        public string PlaceHolder { get; set; }

        /// <summary>
        ///     其他标志
        /// </summary>
        public string Mark { get; set; }

        /// <summary>
        ///     最大长度
        /// </summary>
        public long? Maxlength { get; set; }

        /// <summary>
        ///     最小长度
        /// </summary>
        public long? MinLength { get; set; }

        public string Width { get; set; } = "150";

        /// <summary>
        ///     字段排序，从小到大排列，默认值9999
        /// </summary>
        public long SortOrder { get; set; } = 9999;

        /// <summary>
        ///     是否显示在列表
        /// </summary>
        public bool ListShow { get; set; } = false;

        /// <summary>
        ///     是否编辑
        /// </summary>
        public bool EditShow { get; set; } = false;

        /// <summary>
        ///     帮助文档
        /// </summary>
        public string HelpBlock { get; set; }

        /// <summary>
        ///     控件类型，json类型
        /// </summary>
        public IList<FieldGroup> JsonItems { get; set; } = new List<FieldGroup>();
    }
}