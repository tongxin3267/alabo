namespace Alabo.App.Share.TaskExecutes.ResultModel {

    public abstract class TaskQueueParameterBase {

        public TaskQueueParameterBase(int configurationId) {
            ConfigurationId = configurationId;
        }

        /// <summary>
        ///     对应模块配置的主键Id
        /// </summary>
        public int ConfigurationId { get; set; }
    }
}