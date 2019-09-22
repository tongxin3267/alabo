namespace Alabo.App.Core.Tasks.ResultModel {

    public interface IModuleConfig {

        /// <summary>
        ///     配置存入数据库的主键Id
        /// </summary>
        long Id { get; set; }

        /// <summary>
        ///     配置名称
        /// </summary>
        string Name { get; set; }
    }
}