namespace Alabo.UI.Design.AutoPreviews
{
    /// <summary>
    ///     预览组件，对应移动端zk-preview
    ///     https://weui.io/#preview
    /// </summary>
    public interface IAutoPreview
    {
        /// <summary>
        ///     获取预览图
        /// </summary>
        /// <param name="id"></param>
        /// <param name="autoModel"></param>
        /// <returns></returns>
        AutoPreview GetPreview(string id, AutoBaseModel autoModel);
    }
}