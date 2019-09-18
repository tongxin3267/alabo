using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.App.Core.Tasks.Domain.Entities;
using Alabo.App.Core.Tasks.Domain.Services;
using Alabo.App.Core.Tasks.Extensions;
using Alabo.App.Core.Tasks.ResultModel;
using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Core.UserType.Domain.Services;
using Alabo.App.Share.Tasks.Base;
using Alabo.Extensions;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.App.Share.Tasks {

    using User = Alabo.App.Core.User.Domain.Entities.User;

    /// <summary>
    /// Class UserAssetsModuleBase.
    /// </summary>
    public abstract class UserAssetsModuleBase<TConfiguration> : TaskModuleBase
        where TConfiguration : ShareBaseConfig {

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        public TConfiguration Configuration { get; private set; }

        /// <summary>
        /// 分润订单，交易订单
        /// </summary>
        protected ShareOrder ShareOrder { get; set; }

        /// <summary>
        /// 订单用户,分润触发用户，已经完成检查
        /// 包括等级，用户类型，状态，为空，等检查
        /// </summary>
        protected User ShareOrderUser { get; set; }

        /// <summary>
        /// 分润比例，最多支持三级
        /// </summary>
        protected IList<string> Ratios { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserAssetsModuleBase{TConfiguration}"/> class.
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="configuration">The configuration.</param>
        public UserAssetsModuleBase(TaskContext context, TConfiguration configuration)
            : base(context) {
            Configuration = configuration;
        }

        /// <summary>
        /// Executes the specified parameter.
        /// </summary>
        /// <param name="parameter">参数</param>
        public override ExecuteResult<ITaskResult[]> Execute(TaskParameter parameter) {
            if (Configuration == null) {
                return ExecuteResult<ITaskResult[]>.Fail("configuration is null.");
            }
            //进行参数判断
            if (parameter == null) {
                throw new ArgumentNullException(nameof(parameter));
            }

            //判断通用交易订单
            if (!parameter.TryGetValue("ShareOrderId", out long shareOrderId)) {
                return ExecuteResult<ITaskResult[]>.Fail("分润订单ShareOrderId未找到.");
            }
            var shareOrder = Alabo.Helpers.Ioc.Resolve<IShareOrderService>().GetSingle(r => r.Id == shareOrderId);
            if (shareOrder == null) {
                return ExecuteResult<ITaskResult[]>.Fail($"分润订单为空，shareorder with id {shareOrderId} is null.");
            }
            if (shareOrder.Status != Core.Tasks.Domain.Enums.ShareOrderStatus.Pending) {
                return ExecuteResult<ITaskResult[]>.Fail($"分润订单状态不是代理状态，不触发分润.");
            }
            if (shareOrder.Amount <= 0) {
                return ExecuteResult<ITaskResult[]>.Fail($"分润订单金额小于0，shareorder with amount is less than zero");
            }
            if (Configuration.PriceLimitType == PriceLimitType.OrderPrice) {
                if (shareOrder.Amount > Configuration.BaseRule.MaxAmount && Configuration.BaseRule.MaxAmount > 0) {
                    return ExecuteResult<ITaskResult[]>.Fail($"分润订单金额{ shareOrder.Amount} > 最大触发金额{ Configuration.BaseRule.MinimumAmount}, 退出模块");
                }
                if (shareOrder.Amount < Configuration.BaseRule.MinimumAmount && Configuration.BaseRule.MinimumAmount > 0) {
                    return ExecuteResult<ITaskResult[]>.Fail($"分润订单金额{ shareOrder.Amount} <= 最小触发金额{ Configuration.BaseRule.MinimumAmount}, 退出模块");
                }
            }
            this.ShareOrder = shareOrder;

            //判断交易用户
            var user = Alabo.Helpers.Ioc.Resolve<IUserService>().GetSingle(shareOrder.UserId);
            if (user == null) {
                return ExecuteResult<ITaskResult[]>.Fail($"shareorder with id {shareOrderId} ,shareorder user is null.");
            }
            ShareOrderUser = user;
            //检查分润用户
            var gradeResult = CheckOrderUserTypeAndGrade();
            if (gradeResult.Status != ResultStatus.Success) {
                return gradeResult;
            }

            //检查分润比例
            var distriRatio = Configuration.DistriRatio.Split(',');
            if (distriRatio == null || distriRatio.Length == 0) {
                return ExecuteResult<ITaskResult[]>.Cancel("模块需要设置分润比例但未设置.");
            }
            Ratios = distriRatio.ToList();
            return ExecuteResult<ITaskResult[]>.Success();
        }

        #region 检查分润订单用户的类型与等级，检查分润用户

        /// <summary>
        /// 检查分润订单用户的类型与等级，检查分润用户
        /// </summary>

        public virtual ExecuteResult<ITaskResult[]> CheckOrderUserTypeAndGrade() {
            //检查分润订单用户类型
            if (Configuration.OrderUser.IsLimitOrderUserType) {
                if (!Configuration.OrderUser.OrderUserTypeId.IsGuidNullOrEmpty()) {
                    if (Configuration.OrderUser.OrderUserTypeId == Guid.Parse("71BE65E6-3A64-414D-972E-1A3D4A365000")) {
                        //如果是会员，检查会员等级
                        if (Configuration.OrderUser.IsLimitOrderUserGrade) {
                            if (Configuration.OrderUser.OrderUserGradeId != ShareOrderUser.GradeId) {
                                return ExecuteResult<ITaskResult[]>.Cancel($"user with id {ShareOrder.UserId} not match UserGradeid:{Configuration.OrderUser.OrderUserTypeId}, exit module"); //会员等级不符合grade要求，直接退出
                            }
                        }
                    } else {
                        //检查用户类型
                        var userType = Alabo.Helpers.Ioc.Resolve<IUserTypeService>().GetSingle(ShareOrderUser.Id, Configuration.OrderUser.OrderUserTypeId);
                        if (userType == null) {
                            return ExecuteResult<ITaskResult[]>.Cancel($"user with id {ShareOrder.UserId} has not  userType:{Configuration.OrderUser.OrderUserTypeId}, exit module"); //该会员不存在该类型的用户类型
                        } else {
                            //检查用户类型等级
                            if (Configuration.OrderUser.IsLimitOrderUserGrade) {
                                if (Configuration.OrderUser.OrderUserGradeId != userType.GradeId) {
                                    return ExecuteResult<ITaskResult[]>.Cancel($"userType with UserId {ShareOrder.UserId} not match UserTypeGradeid:{Configuration.OrderUser.OrderUserTypeId}, exit module"); //会员等级不符合grade要求，直接退出
                                }
                            }
                        }
                    }
                } else {
                    return ExecuteResult<ITaskResult[]>.Cancel($"OrderUserTypeId is null"); //userTypeId 为空
                }
            }
            return ExecuteResult<ITaskResult[]>.Success();
        }

        #endregion 检查分润订单用户的类型与等级，检查分润用户

        /// <summary>
        /// 检查分润会员，得到收益的会员
        /// 可以在插入数据看的时候，检查
        /// </summary>
        /// <param name="userId">会员Id</param>
        /// <param name="shareUser">The share 会员.</param>

        public virtual ExecuteResult<ITaskResult[]> GetShareUser(long userId, out User shareUser) {
            shareUser = null;
            var _shareUser = Alabo.Helpers.Ioc.Resolve<IUserService>().GetSingle(userId);
            if (_shareUser == null) {
                return ExecuteResult<ITaskResult[]>.Fail($"the shareuser is null.with userid {userId}");
            }
            //检查分润会员的状态
            if (_shareUser.Status != Alabo.Domains.Enums.Status.Normal) {
                return ExecuteResult<ITaskResult[]>.Fail($"the shareuser status is not normal .with userid {userId}");
            }
            //检查分润订单用户类型
            if (Configuration.ShareUser.IsLimitShareUserType) {
                if (!Configuration.ShareUser.ShareUserTypeId.IsGuidNullOrEmpty()) {
                    if (Configuration.ShareUser.ShareUserTypeId == Guid.Parse("71BE65E6-3A64-414D-972E-1A3D4A365000")) {
                        //如果是会员，检查会员等级
                        if (Configuration.ShareUser.IsLimitShareUserGrade) {
                            if (Configuration.ShareUser.ShareUserGradeId != _shareUser.GradeId) {
                               
                                return ExecuteResult<ITaskResult[]>.Fail($"user with id {userId} not match UserGradeid:{Configuration.ShareUser.ShareUserTypeId}, exit module"); //会员等级不符合grade要求，直接退出
                            }
                        }
                    } else {
                        //检查用户类型
                        var userType = Alabo.Helpers.Ioc.Resolve<IUserTypeService>().GetSingle(userId, Configuration.ShareUser.ShareUserTypeId);
                        if (userType == null) {
                            return ExecuteResult<ITaskResult[]>.Fail($"share user with id {userId} has not  userType:{Configuration.ShareUser.ShareUserTypeId}, exit module"); //该会员不存在该类型的用户类型
                        } else {
                            //检查用户类型等级
                            if (Configuration.ShareUser.IsLimitShareUserGrade) {
                                if (Configuration.ShareUser.ShareUserGradeId != userType.GradeId) {
                                    return ExecuteResult<ITaskResult[]>.Fail($"share userType with UserId {userId} not match UserTypeGradeid:{Configuration.ShareUser.ShareUserTypeId}, exit module"); //会员等级不符合grade要求，直接退出
                                }
                            }
                        }
                    }
                } else {
                    return ExecuteResult<ITaskResult[]>.Fail($"ShareUserTypeId is null"); //userTypeId 为空
                }
            }
            shareUser = _shareUser;

            return ExecuteResult<ITaskResult[]>.Success();
        }
    }
}