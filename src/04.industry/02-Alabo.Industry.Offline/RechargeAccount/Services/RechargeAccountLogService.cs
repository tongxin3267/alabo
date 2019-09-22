using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.App.Offline.RechargeAccount.Entities;
using Alabo.App.Offline.RechargeAccount.Domain.Dtos;
using Alabo.Domains.Entities;
using Alabo.Extensions;
using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.Finance.Domain.CallBacks;
using Alabo.App.Core.Finance.Domain.Services;
using Alabo.App.Offline.RechargeAccount.Domain.Callbacks;

namespace Alabo.App.Offline.RechargeAccount.Services
{
    public class RechargeAccountLogService : ServiceBase<RechargeAccountLog, ObjectId>, IRechargeAccountLogService
    {
        public RechargeAccountLogService(IUnitOfWork unitOfWork, IRepository<RechargeAccountLog, ObjectId> repository) : base(unitOfWork, repository)
        {
        }

        public new ServiceResult Add(RechargeAccountInput rechargeAccount) {

            #region  安全验证
            if (rechargeAccount == null) {
                return ServiceResult.FailedWithMessage("对象不能为空");
            }
            if (rechargeAccount.UserName.IsNullOrEmpty()) {
                return ServiceResult.FailedWithMessage("用户名不能为空!");
            }
            var user = Resolve<IUserService>().GetSingleByUserNameOrMobile(rechargeAccount.UserName);
            if (user == null) {
                return ServiceResult.FailedWithMessage("用户不存在");
            }
            var config = Resolve<IAutoConfigService>().GetList<RechargeAccountConfig>();
            var find = config.FirstOrDefault(r => r.Id == rechargeAccount.Id);
            if (find == null) {
                return ServiceResult.FailedWithMessage("配置不存在");
            }
            #endregion

            var moneyConfigs = Resolve<IAutoConfigService>().MoneyTypes();

            var moneyConfig = moneyConfigs.FirstOrDefault(p=>p.Id == MoneyTypeConfig.CNY);
            var result = Resolve<IBillService>().Increase(user, moneyConfig, find.ArriveAmount, "储值充值");
            if (result.Succeeded)
            {
                // 积分，优惠券，消费额
                if (find.DiscountAmount > 0)
                {
                    moneyConfig = moneyConfigs.FirstOrDefault(p => p.Id == MoneyTypeConfig.Point);
                    Resolve<IBillService>().Increase(user, moneyConfig, find.DiscountAmount, "储值充值赠送积分");
                }

                if (find.GiveChangeAmount > 0)
                {
                    moneyConfig = moneyConfigs.FirstOrDefault(p => p.Id == MoneyTypeConfig.ShopAmount);
                    Resolve<IBillService>().Increase(user, moneyConfig, find.GiveChangeAmount, "储值充值赠送消费额");
                }

                if (find.GiveBuyAmount > 0)
                {
                    moneyConfig = moneyConfigs.FirstOrDefault(p => p.Id == MoneyTypeConfig.Preferential);
                    Resolve<IBillService>().Increase(user, moneyConfig, find.GiveBuyAmount, "储值充值赠送优惠券");
                }

                RechargeAccountLog rechargeModel = new RechargeAccountLog()
                {
                    ArriveAmount= find.ArriveAmount,
                    DiscountAmount=find.DiscountAmount,
                    GiveBuyAmount=find.GiveBuyAmount,
                    GiveChangeAmount=find.GiveChangeAmount,
                    CreateTime=DateTime.Now,
                    StoreAmount=find.StoreAmount,
                    UserId=user.Id
                };
                Resolve<IRechargeAccountLogService>().Add(rechargeModel);

                return ServiceResult.SuccessWithObject("充值成功");
            }
            return ServiceResult.FailedWithMessage("充值失败");
        }
    }
}
