namespace Alabo.UI.Design.Widgets {

    /// <summary>
    ///     通用Widget获取接口
    ///     一个模块一个接口
    /// </summary>
    public interface IWidget {

        /// <summary>
        ///     获取数据
        /// </summary>
        /// <param name="json">json格式</param>
        /// <returns></returns>
        object Get(string json);

        //   object Get(object query, AutoBaseModel autoModel)
    }
}