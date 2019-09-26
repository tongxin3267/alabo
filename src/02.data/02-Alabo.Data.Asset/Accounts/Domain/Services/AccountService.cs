using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Alabo.App.Core.Finance.Domain.CallBacks;
using Alabo.App.Core.Finance.Domain.Dtos.Account;
using Alabo.App.Core.Finance.Domain.Entities;
using Alabo.App.Core.Finance.Domain.Repositories;
using Alabo.App.Core.Finance.ViewModels.Account;
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
using Alabo.Mapping;
using Alabo.Randoms;

namespace Alabo.App.Core.Finance.Domain.Services {

    /// <summary>
    ///     Class AccountService.
    /// </summary>
    public class AccountService : ServiceBase<Account, long>, IAccountService {

        public AccountService(IUnitOfWork unitOfWork, IRepository<Account, long> repository) : base(unitOfWork,
            repository) {
        }

        /// <summary>
        ///     获取用户的资产账户
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="moneyTypeId">货币类型MoneyType ID</param>
        public Account GetAccount(long userId, Guid moneyTypeId) {
            return Repository<IAccountRepository>().GetAccount(userId, moneyTypeId);
        }

        /// <summary>
        ///     获取s the account.
        /// </summary>
        /// <param name="transation">The transation.</param>
        /// <param name="userid">The userid.</param>
        /// <param name="currency">The currency.</param>
        public Account GetAccount(DbTransaction transation, long userid, Currency currency) {
            var moneyConfigList = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
            var moneyConfig = moneyConfigList.FirstOrDefault(r => r.Currency == currency && r.Status == Status.Normal);
            if (moneyConfig == null) {
                throw new ArgumentNullException("currency has not moneyconfig ");
            }

            return GetAccount(transation, userid, moneyConfig.Id);
        }

        /// <summary>
        ///     获取s the account.
        /// </summary>
        /// <param name="transation">The transation.</param>
        /// <param name="userId">会员Id</param>
        /// <param name="moneyTypeId">The money 类型 identifier.</param>
        public Account GetAccount(DbTransaction transation, long userId, Guid moneyTypeId) {
            return Repository<IAccountRepository>().GetAccount(transation, userId, moneyTypeId);
        }

        /// <summary>
        ///     获取s the account.
        /// </summary>
        /// <param name="id">Id标识</param>
        public Account GetAccount(long id) {
            return Repository<IAccountRepository>().GetAccount(id);
        }

        /// <summary>
        ///     获取s the account.
        /// </summary>
        /// <param name="userid">The userid.</param>
        /// <param name="currency">The currency.</param>
        public Account GetAccount(long userid, Currency currency) {
            var moneyConfigList = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
            var moneyConfig = moneyConfigList.FirstOrDefault(r => r.Currency == currency && r.Status == Status.Normal);
            if (moneyConfig == null) {
                throw new ArgumentNullException("currency has not moneyconfig ");
            }

            return GetAccount(userid, moneyConfig.Id);
        }

        /// <summary>
        ///     获取所有用户的正常激活账户
        /// </summary>
        /// <param name="userId">会员Id</param>
        public IList<Account> GetUserAllAccount(long userId) {
            var moneyTypes = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>()
                .Where(r => r.Status == Status.Normal).OrderBy(r => r.SortOrder).ToList();
            var moneyTypeIds = moneyTypes.Select(r => r.Id).ToList().JoinToString("','");
            ;
            return Repository<IAccountRepository>().GetUserAllAccount(userId, moneyTypeIds);
        }

        /// <summary>
        ///     获取账户的金额
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="moneyTypeId">货币类型ID</param>
        public decimal GetAccountAmount(long userId, Guid moneyTypeId) {
            return Repository<IAccountRepository>().GetAccountAmount(userId, moneyTypeId);
        }

        /// <summary>
        ///     更改账户的金额
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="moneyTypeId">货币类型ID</param>
        /// <param name="changedAmount">更加的金额，可以为正，也可以为负数，减少的时候为负数</param>
        public bool ChangeAccountAmount(long userId, Guid moneyTypeId, decimal changedAmount) {
            return Repository<IAccountRepository>().ChangeAccountAmount(userId, moneyTypeId, changedAmount);
        }

        /// <summary>
        ///     更新账户的金额
        /// </summary>
        /// <param name="userId">会员Id</param>
        /// <param name="moneyTypeId">The money 类型 identifier.</param>
        /// <param name="amount">The amount.</param>
        public bool UpdateAccountAmount(long userId, Guid moneyTypeId, decimal amount) {
            return Repository<IAccountRepository>().ChangeAccountAmount(userId, moneyTypeId, amount);
        }

        #region 初始化用户帐户

        /// <summary>
        ///     初始化用户帐户 需要调试
        /// </summary>
        /// <param name="userId">用户编号</param>
        public void InitSingleUserAccount(long userId) {
            //找到所有的资产类型
            var moneyTypes = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
            moneyTypes = moneyTypes.Where(r => r.Status == Status.Normal).ToList();

            //获取所有的资产类型编号
            var moneyTypeIds = moneyTypes.Select(r => r.Id).ToList();

            var accountList = GetUserAllAccount(userId);
            //比对是否有新增的资产类型
            var add = moneyTypes.Select(o => o.Id).Except(accountList.Select(o => o.MoneyTypeId)); //待添加账户
            //比对是否有修改过的资产类型（禁用或删除）
            var update = moneyTypes.Select(o => o.Id).Intersect(accountList.Select(o => o.MoneyTypeId));
            //获取所有的用户
            foreach (var item in add) {
                CreateAccount(userId, moneyTypes.Where(o => o.Id == item).FirstOrDefault());
            }

            //foreach (var item in update) {
            //    UpdateAccount(userId, moneyTypes.Where(o => o.Id == item).FirstOrDefault());
            //}
        }

        /// <summary>
        ///     创建用户资产账户
        /// </summary>
        /// <param name="userId">会员Id</param>
        /// <param name="config">The configuration.</param>
        public ServiceResult CreateAccount(long userId, MoneyTypeConfig config) {
            var result = ServiceResult.Success;
            if (config == null) {
                return ServiceResult.FailedWithMessage("货币类型不存在");
            }

            if (config.Id.IsNull()) {
                return ServiceResult.FailedWithMessage("货币类型不合法");
            }

            if (config.Status != Status.Normal) {
                return ServiceResult.FailedWithMessage("货币类型状态不正常，不能添加");
            }

            var account = new Account {
                UserId = userId,
                MoneyTypeId = config.Id
            };
            account.Token = GetToken(userId, config);
            Add(account);
            return result;
        }

        public string GetToken(long userId, MoneyTypeConfig config) {
            if (config == null) {
                return null;
            }

            var begin = Convert.ToInt32(config.Currency).ToString(); // 3位数
            if (config.Currency == Currency.Cny) {
                begin = RandomHelper.Number(11, 99) + "0";
            }

            if (begin.Length > 3) {
                begin = begin.Substring(0, 3);
            }

            if (begin.Length == 1) {
                begin += RandomHelper.Number(10, 99).ToString();
            }

            if (begin.Length == 2) {
                begin += RandomHelper.Number(0, 9).ToString();
            }

            var lastMoneyId = config.Id.ToString().Replace("-", "");

            var userIdToken = userId.ToString().ToMd5HashString();

            var middle = RandomHelper.RandomString(19); // 25位数

            var token = begin + middle +
                        lastMoneyId.Substring(3, 2).ToLower() +
                        userIdToken.Substring(5, 1).ToUpper() +
                        RandomHelper.RandomString(2) +
                        lastMoneyId.Substring(10, 2).ToUpper() +
                        RandomHelper.RandomString(2) +
                        userIdToken.Substring(10, 1).ToLower() +
                        RandomHelper.RandomString(2);
            return token;
        }

        public void InitToken() {
            var list = GetList(r => r.Token.Length < 10 || r.Token == null);
            foreach (var item in list) {
                var config = Resolve<IAutoConfigService>().MoneyTypes().FirstOrDefault(r => r.Id == item.MoneyTypeId);
                item.Token = GetToken(item.UserId, config);
                if (item.Token == null) {
                    item.Token = RandomHelper.RandomString(34);
                }

                Update(item);
            }
        }

        /// <summary>
        ///     重新
        /// </summary>
        /// <param name="query"></param>
        public PagedList<ViewBlockChain> GetBlockChainList(object query) {
            var accountList = GetPagedList(query, r => r.MoneyTypeId != Currency.UpgradePoints.GetFieldId());
            var result = new List<ViewBlockChain>();
            var moneyConfigs = Resolve<IAutoConfigService>().MoneyTypes();
            foreach (var item in accountList) {
                var viewBlockChain = AutoMapping.SetValue<ViewBlockChain>(item);
                var confing = moneyConfigs.FirstOrDefault(r => r.Id == item.MoneyTypeId);
                viewBlockChain.MoneyTypeName = confing?.Name;
                result.Add(viewBlockChain);
            }

            return PagedList<ViewBlockChain>.Create(result, accountList.RecordCount, accountList.PageSize,
                accountList.PageIndex);
        }

        public void InitAllUserIdsWidthOutAccount() {
            var userIds = Repository<IAccountRepository>().GetAllUserIdsWidthOutAccount();
            foreach (var item in userIds) {
                InitSingleUserAccount(item);
            }
        }

        #endregion 初始化用户帐户

        /// <summary>
        /// 储值
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ServiceResult Add(RechargeAccountInput input) {

            #region 安全验证

            if (input == null) {
                return ServiceResult.FailedWithMessage("对象不能为空");
            }
            if (input.Money <= 0) {
                return ServiceResult.FailedWithMessage("金额不能小于等于0!");
            }
            if (input.UserId.IsNullOrEmpty()) {
                return ServiceResult.FailedWithMessage("用户id不能为空!");
            }
            var user = Resolve<IUserService>().GetSingle(input.UserId);
            if (user == null) {
                return ServiceResult.FailedWithMessage("用户不存在");
            }

            #endregion 安全验证

            var moneyConfigs = Resolve<IAutoConfigService>().MoneyTypes();
            var moneyConfig = moneyConfigs.FirstOrDefault(s => s.Id == MoneyTypeConfig.CNY);

            var result = Resolve<IBillService>().Increase(user, moneyConfig, input.Money, "会员通过支付宝储值充值");
            if (result.Succeeded) {
                return ServiceResult.SuccessWithObject("充值成功");
            }
            return ServiceResult.FailedWithMessage("充值失败");
        }

        /// <summary>
        /// 储值
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public void AddVoid(RechargeAccountInput input) {

            #region 安全验证

            if (input == null) {
                Resolve<IPayService>().Log("参数不能为空!");
                return;
            }
            if (input.Money <= 0) {
                Resolve<IPayService>().Log("金额必须大于0!");
                return;
            }
            if (input.UserId.IsNullOrEmpty()) {
                Resolve<IPayService>().Log("用户id不能为空!");
                return;
            }
            var user = Resolve<IUserService>().GetSingle(input.UserId);
            if (user == null) {
                Resolve<IPayService>().Log("用户不存在!");
                return;
            }

            #endregion 安全验证

            var moneyConfigs = Resolve<IAutoConfigService>().MoneyTypes();
            var moneyConfig = moneyConfigs.FirstOrDefault(s => s.Id == MoneyTypeConfig.CNY);

            var res = Resolve<IBillService>().Increase(user, moneyConfig, input.Money, "会员通过支付宝储值充值");
            Resolve<IPayService>().Log("储值执行结果!" + res.ReturnMessage);
        }
    }
}