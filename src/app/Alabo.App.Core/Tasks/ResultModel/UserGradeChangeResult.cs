using System;
using Alabo.App.Core.Tasks.Extensions;

namespace Alabo.App.Core.Tasks.ResultModel {

    public class UserGradeChangeResult : ITaskResult {

        public UserGradeChangeResult() {
        }

        public UserGradeChangeResult(TaskContext context) {
            Context = context;
        }

        /// <summary>
        ///     等级改变
        /// </summary>
        public GradeChangeResult Result { get; set; }

        public TaskContext Context { get; }

        public ExecuteResult Update() {
            //try {
            //    var memberTypeConfig = Alabo.Helpers.Ioc.Resolve<IAutoConfigService>()
            //        .GetList<UserTypeConfig>(e => e.TypeClass == UserTypeEnum.Member)
            //        .FirstOrDefault();
            //    if (memberTypeConfig == null)
            //        return ExecuteResult<ITaskResult[]>.Cancel($"there is no member type config in system.");
            //    var findUserType = Alabo.Helpers.Ioc.Resolve<IUserTypeService>().GetSingle(UserId, memberTypeConfig.Id);
            //    if (findUserType == null)
            //        return ExecuteResult<ITaskResult[]>.Cancel($"user with id {UserId} do not has a user typeindex with MEMBER type.");
            //    if (findUserType.GradeId == GradeId)
            //        return ExecuteResult<ITaskResult[]>.Success("user with id {UserId} has no grade upgrade.");
            //    findUserType.GradeId = GradeId;
            //    Alabo.Helpers.Ioc.Resolve<IUserTypeService>().Update(findUserType);
            //    return ExecuteResult.Success();
            //} catch (Exception e) {
            //    return ExecuteResult.Error(e);
            //}
            return ExecuteResult.Success();
        }
    }

    public class GradeChangeResult {

        /// <summary>
        ///     用户Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        ///     升级前的等级Id
        /// </summary>
        public Guid OldGradeId { get; set; }

        /// <summary>
        ///     用户要等级Id
        /// </summary>
        public Guid GradeId { get; set; }

        /// <summary>
        ///     队列Id
        /// </summary>
        public long QueueId { get; set; }

        /// <summary>
        ///     要升级的用户类型Id
        /// </summary>
        public Guid UserTypeId { get; set; }

        /// <summary>
        ///     用户名
        /// </summary>
        public string UserName { get; set; }
    }
}