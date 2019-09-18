using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.UI.AutoForms
{
    /// <summary>
    ///     form信息框
    /// </summary>
    public class FromMessage
    {
        /// <summary>
        ///     表单提示信息构造函数
        /// </summary>
        /// <param name="formMessageType">提示方式，失败或成功</param>
        /// <param name="message">提示信息</param>
        public FromMessage(FromMessageType formMessageType, string message)
        {
            Type = Type;
            Message = message;
            Link = new ViewLink("返回首页", "pages/index");
        }

        /// <summary>
        ///     表单提示信息构造函数
        /// </summary>
        /// <param name="formMessageType">提示方式，失败或成功</param>
        /// <param name="message">提示信息</param>
        /// <param name="linkName">主链接文字</param>
        /// <param name="linkUrl">主链接Url</param>
        public FromMessage(FromMessageType formMessageType, string message, string linkName, string linkUrl)
        {
            Type = Type;
            Message = message;
            Link = new ViewLink(linkName, linkUrl);
        }

        /// <summary>
        ///     返回信息类型
        /// </summary>
        public FromMessageType Type { get; set; }

        /// <summary>
        ///     提示信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     主操链接
        ///     第一个操作为返回
        /// </summary>

        public ViewLink Link { get; set; } = new ViewLink();
    }

    /// <summary>
    ///     返回信息类型
    /// </summary>
    [ClassProperty(Name = "返回信息类型")]
    public enum FromMessageType
    {
        /// <summary>
        ///     操作成功
        /// </summary>
        Success = 1,

        /// <summary>
        ///     信息
        ///     如审核中
        /// </summary>
        Info = 2,

        /// <summary>
        ///     操作失败
        /// </summary>
        Error = 3
    }
}