using System;
using System.Linq;
using Alabo.App.Core.Finance.Domain.CallBacks;
using Alabo.App.Core.Finance.Domain.Entities;
using Alabo.App.Core.Finance.Domain.Repositories;
using Alabo.App.Core.Finance.ViewModels.Bill;
using Alabo.App.Core.Tasks.Domain.Entities;
using Alabo.App.Core.Tasks.Domain.Services;
using Alabo.App.Core.User;
using Alabo.App.Core.User.Domain.Services;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Framework.Basic.AutoConfigs.Domain.Configs;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Schedules;

namespace Alabo.App.Core.Finance.Domain.Services {

    /// <summary>
    /// </summary>
    public class BillService : ServiceBase<Bill, long>, IBillService {

        public BillService(IUnitOfWork unitOfWork, IRepository<Bill, long> repository) : base(unitOfWork, repository) {
        }

        /// <summary>
        ///     会员中心资产明细
        /// </summary>
        /// <param name="query"></param>
        public PagedList<ViewHomeBill> GetUserPage(object query) {
            var user = query.ToUserObject();
            var resultList = Resolve<IBillService>().GetPagedList<ViewHomeBill>(query, r => r.UserId == user.Id);
            var moneyTypes = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
            var users = Resolve<IUserService>().GetList();
            resultList.ForEach(r => {
                // r.MoneyTypeName = moenyTypes.FirstOrDefault(u => u.Id == r.MoneyTypeId)?.Name;
                r.OtherUser = users.FirstOrDefault(u => u.Id == r.OtherUserId);
                if (r.OtherUser != null) {
                    r.OtherUserName = r.OtherUser.UserName;
                }

                r.MoneyTypeName = moneyTypes.FirstOrDefault(e => e.Id == r.MoneyTypeId)?.Name;
                r.FlowAmoumtStr = r.Flow.GetHtmlName();
                r.BillTypeName = r.Type.GetHtmlName();
                r.CreatimeStr = r.CreateTime.ToString();
            });
            return resultList;
        }

        #region 转账

        /// <summary>
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="targetUser"></param>
        /// <param name="config"></param>
        /// <param name="amount"></param>
        public ServiceResult Transfer(Users.Entities.User user, Users.Entities.User targetUser,
            TransferConfig config, decimal amount) {

            #region 转账安全判断，确保数据准备

            var result = ServiceResult.Success;
            if (amount <= 0) {
                return ServiceResult.FailedWithMessage("转账金额必须大于0");
            }

            if (user == null) {
                return ServiceResult.FailedWithMessage("付款用户不存在");
            }

            if (targetUser == null) {
                return ServiceResult.FailedWithMessage("收款用户不存在");
            }

            if (config == null) {
                return ServiceResult.FailedWithMessage("转账设置不存在，请在控制面板中先配置");
            }

            if (config.Status != Status.Normal) {
                return ServiceResult.FailedWithMessage("转账配置以删除或冻结中");
            }

            var moneyTypes = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
            var outTypeConfig = moneyTypes.FirstOrDefault(r => r.Id == config.OutMoneyTypeId);
            if (outTypeConfig == null) {
                return ServiceResult.FailedWithMessage("转出货币类型不存在");
            }

            var inTypeConfig = moneyTypes.FirstOrDefault(r => r.Id == config.InMoneyTypeId);
            if (inTypeConfig == null) {
                return ServiceResult.FailedWithMessage("收款货币类型不存在");
            }

            if (user.Id == targetUser.Id && outTypeConfig.Id == inTypeConfig.Id) {
                return ServiceResult.FailedWithMessage("自己不能给自己相同的货币类型账户转账");
            }

            if (outTypeConfig.Status != Status.Normal) {
                return ServiceResult.FailedWithMessage("付款货币类型已删除或被冻结");
            }

            if (inTypeConfig.Status != Status.Normal) {
                return ServiceResult.FailedWithMessage("收款货币类型已删除或被冻结");
            }

            var userAccount = Resolve<IAccountService>().GetAccount(user.Id, outTypeConfig.Id);
            if (userAccount == null) {
                return ServiceResult.FailedWithMessage("付款账户不存在，转账账户未创建");
            }

            if (amount > userAccount.Amount) {
                return ServiceResult.FailedWithMessage("付款金额大于转账用户账户的余额，请重新输入");
            }

            var targetUserAccount = Resolve<IAccountService>().GetAccount(targetUser.Id, inTypeConfig.Id);
            if (targetUserAccount == null) {
                return ServiceResult.FailedWithMessage("收款账户不存在,目标转账未创建");
            }

            if (!config.CanTransferOther) {
                if (user.Id != targetUser.Id) {
                    return ServiceResult.FailedWithMessage("不能转账给它人");
                }
            }

            if (!config.CanTransferSelf) {
                if (user.Id == targetUser.Id) {
                    return ServiceResult.FailedWithMessage("不能转账给自己");
                }
            }

            #endregion 转账安全判断，确保数据准备

            //转账用户金额减少
            userAccount.Amount = userAccount.Amount - amount;

            var userBill = new Bill {
                Type = BillActionType.TransferOut,
                AfterAmount = userAccount.Amount,
                Amount = amount,
                UserId = user.Id,
                OtherUserId = targetUser.Id,
                MoneyTypeId = outTypeConfig.Id,
                Flow = AccountFlow.Spending,
                Intro =
                    $@"{user.GetUserName()}{outTypeConfig.Name}转账给{targetUser.GetUserName()}{inTypeConfig.Name}账户，金额为{
                            amount
                        }"
            };
            targetUserAccount.Amount = targetUserAccount.Amount + amount * config.Fee;
            targetUserAccount.HistoryAmount += amount * config.Fee;
            var targetBill = new Bill {
                Type = BillActionType.TransferIn,
                AfterAmount = targetUserAccount.Amount,
                Amount = amount * config.Fee,
                UserId = targetUser.Id,
                OtherUserId = user.Id,
                MoneyTypeId = inTypeConfig.Id,
                Flow = AccountFlow.Income,
                Intro =
                    $@"{targetUser.GetUserName()}{inTypeConfig.Name}收到{user.GetUserName()}的{outTypeConfig.Name}转账，金额为{
                            amount * config.Fee
                        }"
            };

            var context = Repository<IBillRepository>().RepositoryContext;
            context.BeginTransaction();
            try {
                Resolve<IAccountService>().Update(userAccount);
                Resolve<IAccountService>().Update(targetUserAccount);
                Add(userBill);
                Add(targetBill);
                context.SaveChanges();
                context.CommitTransaction();
            } catch (Exception ex) {
                context.RollbackTransaction();
                return ServiceResult.FailedWithMessage("更新失败:" + ex.Message);
            } finally {
                context.DisposeTransaction();
            }

            return result;
        }

        public ServiceResult Transfer(Users.Entities.User user, Users.Entities.User targetUser,
            MoneyTypeConfig typeConfig, MoneyTypeConfig targetTypeConfig, decimal amount) {
            var transferConfigs = Resolve<IAutoConfigService>().GetList<TransferConfig>();
            var config = transferConfigs.FirstOrDefault(r =>
                r.OutMoneyTypeId == typeConfig.Id && r.InMoneyTypeId == targetTypeConfig.Id);
            if (config == null) {
                return ServiceResult.FailedWithMessage("该转账类型不存，请在转账配置中进行设置");
            }

            return Transfer(user, targetUser, config, amount);
        }

        #endregion 转账

        #region 冻结与解冻

        /// <summary>
        ///     解冻账户金额
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="currency"></param>
        /// <param name="amount"></param>
        /// <param name="Intro"></param>
        public ServiceResult DeductTreeze(Users.Entities.User user, Currency currency, decimal amount,
            string Intro) {
            if (Convert.ToInt16(currency) < 0) {
                return ServiceResult.FailedWithMessage("操作货币类型不合法");
            }

            var moneyTypes = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
            var typeConfig = moneyTypes.FirstOrDefault(r => r.Currency == currency);

            if (typeConfig == null) {
                return ServiceResult.FailedWithMessage("操作货币类型不存在");
            }

            return Treeze(user, typeConfig, amount, Intro);
        }

        /// <summary>
        ///     解冻账户金额
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="typeConfig"></param>
        /// <param name="amount"></param>
        /// <param name="Intro"></param>
        public ServiceResult DeductTreeze(Users.Entities.User user, MoneyTypeConfig typeConfig, decimal amount,
            string Intro) {
            var result = ServiceResult.Success;
            var userAcount = Resolve<IAccountService>().GetAccount(user.Id, typeConfig.Id);

            if (user == null) {
                return ServiceResult.FailedWithMessage("操作用户不存在");
            }

            var account = Resolve<IAccountService>().GetAccount(user.Id, typeConfig.Id);
            if (account == null) {
                return ServiceResult.FailedWithMessage("操作账户不存在");
            }

            if (typeConfig.Status != Status.Normal) {
                return ServiceResult.FailedWithMessage("操作币种状态不正常");
            }

            if (userAcount.FreezeAmount < amount) {
                return ServiceResult.FailedWithMessage("冻结金额不足");
            }

            if (amount <= 0) {
                return ServiceResult.FailedWithMessage("解冻金额不能为0");
            }

            userAcount.Amount = userAcount.Amount + amount;
            userAcount.FreezeAmount = userAcount.FreezeAmount - amount;

            var userBill = CreateBill(userAcount, amount, BillActionType.DeductTreeze, Intro);
            var context = Repository<IBillRepository>().RepositoryContext;
            context.BeginTransaction();
            try {
                Resolve<IAccountService>().Update(userAcount);
                Add(userBill);
                context.SaveChanges();
                context.CommitTransaction();
            } catch (Exception ex) {
                context.RollbackTransaction();
                return ServiceResult.FailedWithMessage("更新失败:" + ex.Message);
            } finally {
                context.DisposeTransaction();
            }

            return result;
        }

        /// <summary>
        ///     冻结账户金额
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="typeConfig"></param>
        /// <param name="amount"></param>
        /// <param name="Intro"></param>
        public ServiceResult Treeze(Users.Entities.User user, MoneyTypeConfig typeConfig, decimal amount,
            string Intro) {
            var result = ServiceResult.Success;
            if (user == null) {
                return ServiceResult.FailedWithMessage("操作用户不存在");
            }

            var userAcount = Resolve<IAccountService>()
                .GetSingle(r => r.UserId == user.Id && r.MoneyTypeId == typeConfig.Id);
            if (userAcount == null) {
                return ServiceResult.FailedWithMessage("操作账户不存在");
            }

            if (typeConfig.Status != Status.Normal) {
                return ServiceResult.FailedWithMessage("操作币种状态不正常");
            }

            if (userAcount.Amount < amount) {
                return ServiceResult.FailedWithMessage("冻结金额不足");
            }

            if (amount <= 0) {
                return ServiceResult.FailedWithMessage("冻结金额不能为0");
            }

            userAcount.Amount = userAcount.Amount - amount;
            userAcount.FreezeAmount = userAcount.FreezeAmount + amount;

            var userBill = CreateBill(userAcount, -amount, BillActionType.Treeze, Intro);
            var context = Repository<IBillRepository>().RepositoryContext;
            context.BeginTransaction();
            try {
                Resolve<IAccountService>().Update(userAcount);
                Add(userBill);

                context.SaveChanges();
                context.CommitTransaction();
            } catch (Exception ex) {
                context.RollbackTransaction();
                return ServiceResult.FailedWithMessage("更新失败:" + ex.Message);
            } finally {
                context.DisposeTransaction();
            }

            return result;
        }

        /// <summary>
        ///     冻结单条记录，没有使用事物
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="typeConfig"></param>
        /// <param name="amount"></param>
        /// <param name="Intro"></param>
        public ServiceResult TreezeSingle(Users.Entities.User user, MoneyTypeConfig typeConfig, decimal amount,
            string Intro) {
            var result = ServiceResult.Success;
            if (user == null) {
                return ServiceResult.FailedWithMessage("操作用户不存在");
            }

            var userAcount = Resolve<IAccountService>()
                .GetSingle(r => r.UserId == user.Id && r.MoneyTypeId == typeConfig.Id);
            if (userAcount == null) {
                return ServiceResult.FailedWithMessage("操作账户不存在");
            }

            if (typeConfig.Status != Status.Normal) {
                return ServiceResult.FailedWithMessage("操作币种状态不正常");
            }

            if (userAcount.Amount < amount) {
                return ServiceResult.FailedWithMessage("冻结金额不足");
            }

            if (amount <= 0) {
                return ServiceResult.FailedWithMessage("冻结金额不能为0");
            }

            userAcount.Amount = userAcount.Amount - amount;
            userAcount.FreezeAmount = userAcount.FreezeAmount + amount;

            var userBill = Resolve<IBillService>().CreateBill(userAcount, -amount, BillActionType.Treeze, Intro);
            if (!Resolve<IAccountService>().Update(userAcount)) {
                return ServiceResult.FailedWithMessage("冻结失败");
            }

            if (!Add(userBill)) {
                return ServiceResult.FailedWithMessage("账单记录添加失败");
            }

            return result;
        }

        /// <summary>
        ///     冻结账户金额
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="currency"></param>
        /// <param name="amount"></param>
        /// <param name="Intro"></param>
        public ServiceResult Treeze(Users.Entities.User user, Currency currency, decimal amount, string Intro) {
            if (Convert.ToInt16(currency) < 0) {
                return ServiceResult.FailedWithMessage("操作货币类型不合法");
            }

            var moneyTypes = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
            var typeConfig = moneyTypes.FirstOrDefault(r => r.Currency == currency);

            if (typeConfig == null) {
                return ServiceResult.FailedWithMessage("操作货币类型不存在");
            }

            return Treeze(user, typeConfig, amount, Intro);
        }

        #endregion 冻结与解冻

        #region 充值提现

        /// <summary>
        ///     减少账户金额
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="typeConfig"></param>
        /// <param name="amount"></param>
        /// <param name="Intro"></param>
        public ServiceResult Reduce(Users.Entities.User user, MoneyTypeConfig typeConfig, decimal amount,
            string Intro) {
            var result = ServiceResult.Success;
            if (user == null) {
                return ServiceResult.FailedWithMessage("操作用户不存在");
            }
            if (amount <= 0) {
                return ServiceResult.FailedWithMessage("金额不能为0");
            }

            var userAcount = Resolve<IAccountService>().GetAccount(user.Id, typeConfig.Id);

            var account = Resolve<IAccountService>().GetAccount(user.Id, typeConfig.Id);
            if (account == null) {
                return ServiceResult.FailedWithMessage("操作账户不存在");
            }

            if (typeConfig.Status != Status.Normal) {
                return ServiceResult.FailedWithMessage("操作币种状态不正常");
            }

            if (userAcount.Amount < amount) {
                return ServiceResult.FailedWithMessage("账户余额不足，扣除失败");
            }

            userAcount.Amount = userAcount.Amount - amount;

            var userBill = CreateBill(userAcount, -amount, BillActionType.SystemReduce, Intro);
            var context = Repository<IBillRepository>().RepositoryContext;
            context.BeginTransaction();
            try {
                Resolve<IAccountService>().Update(userAcount);
                Add(userBill);

                context.SaveChanges();
                context.CommitTransaction();
            } catch (Exception ex) {
                context.RollbackTransaction();
                return ServiceResult.FailedWithMessage("更新失败:" + ex.Message);
            } finally {
                context.DisposeTransaction();
            }

            return result;
        }

        /// <summary>
        ///     减少账户金额
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="currency"></param>
        /// <param name="amount"></param>
        /// <param name="Intro"></param>
        public ServiceResult Reduce(Users.Entities.User user, Currency currency, decimal amount, string Intro) {
            if (Convert.ToInt16(currency) < 0) {
                return ServiceResult.FailedWithMessage("操作货币类型不合法");
            }

            var moneyTypes = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
            var typeConfig = moneyTypes.FirstOrDefault(r => r.Currency == currency);

            if (typeConfig == null) {
                return ServiceResult.FailedWithMessage("操作货币类型不存在");
            }

            return Reduce(user, typeConfig, amount, Intro);
        }

        /// <summary>
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="typeConfig"></param>
        /// <param name="amount"></param>
        /// <param name="Intro"></param>
        public ServiceResult Increase(Users.Entities.User user, MoneyTypeConfig typeConfig, decimal amount,
            string Intro) {
            if (user == null) {
                return ServiceResult.FailedWithMessage("操作用户不存在");
            }

            var result = ServiceResult.Success;
            var userAcount = Resolve<IAccountService>().GetAccount(user.Id, typeConfig.Id);
            if (userAcount == null) {
                Resolve<IAccountService>().CreateAccount(user.Id, typeConfig);
            }

            userAcount = Resolve<IAccountService>().GetAccount(user.Id, typeConfig.Id);

            var account = Resolve<IAccountService>().GetAccount(user.Id, typeConfig.Id);
            if (account == null) {
                return ServiceResult.FailedWithMessage("操作账户不存在");
            }

            if (typeConfig.Status != Status.Normal) {
                return ServiceResult.FailedWithMessage("操作币种状态不正常");
            }

            if (amount <= 0) {
                return ServiceResult.FailedWithMessage("金额不能为0");
            }
            //string Intro = $@"管理员增加{user.GetUserName()}用户的{userAcount.Currency.GetDisplayName()}，增加金额为{amount}";

            userAcount.Amount = userAcount.Amount + amount;
            userAcount.HistoryAmount = userAcount.HistoryAmount + amount;
            var userBill = CreateBill(userAcount, amount, BillActionType.PeopleIncrease, Intro);
            var context = Repository<IBillRepository>().RepositoryContext;
            context.BeginTransaction();
            try {
                Resolve<IAccountService>().Update(userAcount);
                Add(userBill);

                // 如果类型为升级点，则添加升级点队列
                if (typeConfig.Currency == Currency.UpgradePoints) {
                    var taskQueue = new TaskQueue {
                        UserId = user.Id,
                        ModuleId = TaskQueueModuleId.UserUpgradeByUpgradePoints // 自动升级维度的guid，固定不变
                    };
                    Resolve<ITaskQueueService>().Add(taskQueue);
                }

                context.SaveChanges();
                context.CommitTransaction();
            } catch (Exception ex) {
                context.RollbackTransaction();
                return ServiceResult.FailedWithMessage("更新失败:" + ex.Message);
            } finally {
                context.DisposeTransaction();
            }

            return result;
        }

        /// <summary>
        ///     增加账户金额
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="currency"></param>
        /// <param name="amount"></param>
        /// <param name="Intro"></param>
        public ServiceResult Increase(Users.Entities.User user, Currency currency, decimal amount, string Intro) {
            if (Convert.ToInt16(currency) < 0) {
                return ServiceResult.FailedWithMessage("操作货币类型不合法");
            }

            var moneyTypes = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
            var typeConfig = moneyTypes.FirstOrDefault(r => r.Currency == currency);

            if (typeConfig == null) {
                return ServiceResult.FailedWithMessage("操作货币类型不存在");
            }

            return Increase(user, typeConfig, amount, Intro);
        }

        /// <summary>
        /// </summary>
        /// <param name="account"></param>
        /// <param name="changeAmount"></param>
        /// <param name="actionType"></param>
        /// <param name="intro"></param>
        /// <param name="targetUserId"></param>
        /// <param name="orderSerial"></param>
        /// <param name="remark"></param>
        public Bill CreateBill(Account account, decimal changeAmount, BillActionType actionType, string intro = null,
            long targetUserId = 0, string orderSerial = null, string remark = null) {
            var moneyConfigList = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
            var moneyConfig =
                moneyConfigList.FirstOrDefault(r => r.Id == account.MoneyTypeId && r.Status == Status.Normal);
            if (moneyConfig == null) {
                throw new ArgumentNullException("account moneyconfig is null or status is not normal");
            }

            var flow = AccountFlow.Spending;
            if (changeAmount >= 0) {
                flow = AccountFlow.Income;
            }

            if (intro.IsNullOrEmpty()) {
                var baseUser = Resolve<IUserService>().GetSingle(account.UserId);
                if (baseUser == null) {
                    throw new ArgumentNullException("user is not exist");
                }

                intro =
                    $@"用户{baseUser.GetUserName()}完成{actionType.GetDisplayName()}操作,{moneyConfig.Name}{
                            flow.GetDisplayName()
                        }金额{changeAmount}";
            }

            var bill = new Bill {
                UserId = account.UserId,
                Amount = changeAmount,
                AfterAmount = account.Amount,
                Type = actionType,
                MoneyTypeId = account.MoneyTypeId,
                Flow = flow,
                Intro = intro
                //Remark = remark
            };
            return bill;
        }

        /// <summary>
        /// </summary>
        /// <param name="account"></param>
        /// <param name="changeAmount"></param>
        /// <param name="actionType"></param>
        /// <param name="prefixIntro"></param>
        public Bill CreateBill(Account account, decimal changeAmount, BillActionType actionType, string prefixIntro) {
            var moneyConfigList = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
            var moneyConfig =
                moneyConfigList.FirstOrDefault(r => r.Id == account.MoneyTypeId && r.Status == Status.Normal);
            if (moneyConfig == null) {
                throw new ArgumentNullException("account moneyconfig is null or status is not normal");
            }

            var baseUser = Resolve<IUserService>().GetSingle(r => r.Id == account.UserId);
            if (baseUser == null) {
                throw new ArgumentNullException("user is not exist");
            }

            var flow = AccountFlow.Spending;
            if (changeAmount >= 0) {
                flow = AccountFlow.Income;
            }

            var intro =
                $"{prefixIntro},用户{baseUser.GetUserName()}的{moneyConfig.Name}{flow.GetDisplayName()}金额{changeAmount}";
            return CreateBill(account, changeAmount, actionType, intro, 0, string.Empty, string.Empty);
        }

        #endregion 充值提现
    }
}