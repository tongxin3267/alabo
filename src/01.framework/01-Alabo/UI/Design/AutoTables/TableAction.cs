using Alabo.Extensions;
using Newtonsoft.Json;
using System;

namespace Alabo.UI.Design.AutoTables
{
    /// <summary>
    ///     表格操作
    /// </summary>
    public class TableAction
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="TableAction" /> class.
        /// </summary>
        public TableAction()
        {
        }

        public TableAction(string name, string url)
        {
            Name = name;
            Url = url;
            Type = ActionLinkType.Link;
            ActionType = TableActionType.ColumnAction;
            if (Name.Contains("删除")) {
                Type = ActionLinkType.Delete;
            }
        }

        public TableAction(string name, string url, TableActionType tableAction)
        {
            Name = name;
            Url = url;
            Type = ActionLinkType.Link;
            ActionType = tableAction;
        }

        public TableAction(string name, string url, ActionLinkType type)
        {
            Name = name;
            Url = url;
            Type = type;
        }

        /// <summary>
        ///     名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     URL
        ///     支持参数
        ///     删除的时候对应Api接口
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        ///     图标
        /// </summary>
        public string Icon
        {
            get
            {
                if (Convert.ToInt16(IconType) > 0) {
                    return IconType.GetIcon();
                }

                return string.Empty;
            }
            set { }
        }

        /// <summary>
        ///     图标名称
        /// </summary>
        [JsonIgnore]
        public Flaticon IconType
        {
            get
            {
                if (Name.Contains("编辑")) {
                    return Flaticon.Edit;
                }

                if (Name.Contains("删除")) {
                    return Flaticon.Delete;
                }

                return 0;
            }

            set { }
        }

        /// <summary>
        ///     视图类型
        /// </summary>
        public ActionLinkType Type { get; set; }

        /// <summary>
        ///     默认表格操作方式
        ///     表格相关的操作
        /// </summary>
        public TableActionType ActionType { get; set; } = TableActionType.ColumnAction;

        /// <summary>
        ///     表单弹窗类型，继承IAutoForm
        /// </summary>
        public string FormType { get; set; }
    }

    /// <summary>
    ///     表格相关的操作
    /// </summary>
    public enum TableActionType
    {
        /// <summary>
        ///     表格列操作方式
        /// </summary>
        ColumnAction = 1,

        /// <summary>
        ///     表格头部快捷操作操作方式
        ///     在表格的上方横排，最多不能操作三个
        /// </summary>
        QuickAction = 2,

        /// <summary>
        ///     预览链接，新窗口打开
        /// </summary>
        ViewAction = 3,

        /// <summary>
        ///     表格表单操作
        /// </summary>
        FormAction = 4
    }

    /// <summary>
    ///     链接类型
    /// </summary>
    public enum ActionLinkType
    {
        /// <summary>
        ///     链接方式
        /// </summary>
        Link = 1,

        /// <summary>
        ///     删除
        /// </summary>
        Delete = 2,

        /// <summary>
        ///     弹出
        /// </summary>
        Dialog = 3,

        /// <summary>
        ///     执行ApiPost请求
        /// </summary>
        ApiPost = 4
    }
}