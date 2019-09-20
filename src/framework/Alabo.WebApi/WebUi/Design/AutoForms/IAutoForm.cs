using Alabo.Domains.Entities;

namespace Alabo.UI.AutoForms
{
    /// <summary>
    ///     自动表单接口
    ///     继承的类，必须以AutoForm结尾
    /// </summary>
    public interface IAutoForm : IUI
    {
        /// <summary>
        ///     通过Id获取自动视图，视图Id可以为空
        /// </summary>
        /// <param name="id"></param>
        /// <param name="autoModel"></param>
        /// <returns></returns>
        AutoForm GetView(object id, AutoBaseModel autoModel);

        /// <summary>
        ///     保存
        /// </summary>
        /// <returns></returns>
        ServiceResult Save(object model, AutoBaseModel autoModel);
    }
}