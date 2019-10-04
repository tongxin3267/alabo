namespace Alabo.Framework.Tasks.Queues.Models {

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