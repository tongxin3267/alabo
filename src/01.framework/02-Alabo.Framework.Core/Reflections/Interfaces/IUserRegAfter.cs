namespace Alabo.Core.Reflections.Interfaces {

    /// <summary>
    /// 用户注册后时间
    /// </summary>
    public interface IUserRegAfter {

        /// <summary>
        /// 执行顺序，越小越在前面
        /// </summary>
        long SortOrder {
            get; set;
        }

        /// <summary>
        /// 执行事件
        /// </summary>
        /// <param name="user">用户</param>
        void Excecute(Users.Entities.User user);
    }
}