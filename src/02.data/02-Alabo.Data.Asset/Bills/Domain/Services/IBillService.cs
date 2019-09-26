using Alabo.App.Asset.Accounts.Domain.Entities;
using Alabo.App.Asset.Bills.Domain.Entities;
using Alabo.App.Asset.Transfers.Domain.Configs;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.Framework.Basic.AutoConfigs.Domain.Configs;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Users.Entities;

namespace Alabo.App.Asset.Bills.Domain.Services
{
    /// <summary>
    ///     财务记录
    /// </summary>
    public interface IBillService : IService<Bill, long>
    {
        /// <summary>
        ///     创建账单
        /// </summary>
        /// <param name="account">要更改的账户</param>
        /// <param name="changeAmount">更改的金额，可以为正，也可以为负</param>
        /// <param name="actionType">操作类型</param>
        /// <param name="intro">账单简介，不提供是系统默认生成</param>
        /// <param name="tragetUserId">The traget user identifier.</param>
        /// <param name="orderSerial">关联订单号</param>
        /// <param name="remark">备注</param>
        /// <returns>Bill.</returns>
        Bill CreateBill(Account account, decimal changeAmount, BillActionType actionType, string intro = null,
            long tragetUserId = 0, string orderSerial = null, string remark = null);

        /// <summary>
        ///     Creates the bill.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <param name="changeAmount">The change amount.</param>
        /// <param name="actionType">Type of the action.</param>
        /// <param name="prefixIntro">The prefix intro.</param>
        /// <returns>Bill.</returns>
        Bill CreateBill(Account account, decimal changeAmount, BillActionType actionType, string prefixIntro);

        /// <summary>
        ///     转账
        ///     根据系统转账配置转账
        /// </summary>
        /// <param name="user">付款用户</param>
        /// <param name="targetUser">收款用户用户</param>
        /// <param name="config">用户转账设置，在系统控制面板中可配置</param>
        /// <param name="amount">转账金额,必须大于0</param>
        /// <returns>ServiceResult.</returns>
        ServiceResult Transfer(User user, User targetUser,
            TransferConfig config, decimal amount);

        // <summary>
        /// <summary>
        ///     Transfers the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="targetUser">The target user.</param>
        /// <param name="typpeConfig">The typpe configuration.</param>
        /// <param name="tragetTypeConfig">The traget type configuration.</param>
        /// <param name="amount">The amount.</param>
        /// <returns>ServiceResult.</returns>
        ServiceResult Transfer(User user, User targetUser,
            MoneyTypeConfig typpeConfig, MoneyTypeConfig tragetTypeConfig, decimal amount);

        /// <summary>
        ///     冻结账户金额
        ///     事物处理方式
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="typeConfig">系统货币类型</param>
        /// <param name="amount">冻结金额,必须大于0</param>
        /// <param name="Intro">The intro.</param>
        /// <returns>ServiceResult.</returns>
        ServiceResult Treeze(User user, MoneyTypeConfig typeConfig, decimal amount, string Intro);

        /// <summary>
        ///     冻结账户金额
        ///     非事物处理
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="typeConfig">The type configuration.</param>
        /// <param name="amount">The amount.</param>
        /// <param name="Intro">The intro.</param>
        /// <returns>ServiceResult.</returns>
        ServiceResult TreezeSingle(User user, MoneyTypeConfig typeConfig, decimal amount,
            string Intro);

        /// <summary>
        ///     冻结账户金额
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="currency">系统货币类型</param>
        /// <param name="amount">冻结金额,必须大于0</param>
        /// <param name="Intro">The intro.</param>
        /// <returns>ServiceResult.</returns>
        ServiceResult Treeze(User user, Currency currency, decimal amount, string Intro);

        /// <summary>
        ///     解冻账户金额
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="typeConfig">系统货币类型</param>
        /// <param name="amount">解冻金额,必须大于0</param>
        /// <param name="Intro">The intro.</param>
        /// <returns>ServiceResult.</returns>
        ServiceResult DeductTreeze(User user, MoneyTypeConfig typeConfig, decimal amount,
            string Intro);

        /// <summary>
        ///     解冻账户金额
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="currency">系统货币类型</param>
        /// <param name="amount">解冻金额,必须大于0</param>
        /// <param name="Intro">The intro.</param>
        /// <returns>ServiceResult.</returns>
        ServiceResult DeductTreeze(User user, Currency currency, decimal amount, string Intro);

        /// <summary>
        ///     减少账户金额
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="typeConfig">系统货币类型</param>
        /// <param name="amount">解冻金额,必须大于0</param>
        /// <param name="Intro">The intro.</param>
        /// <returns>ServiceResult.</returns>
        ServiceResult Reduce(User user, MoneyTypeConfig typeConfig, decimal amount, string Intro);

        /// <summary>
        ///     减少账户金额
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="currency">系统货币类型</param>
        /// <param name="amount">解冻金额,必须大于0</param>
        /// <param name="Intro">The intro.</param>
        /// <returns>ServiceResult.</returns>
        ServiceResult Reduce(User user, Currency currency, decimal amount, string Intro);

        /// <summary>
        ///     增加账户余额
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="typeConfig">系统货币类型</param>
        /// <param name="amount">解冻金额,必须大于0</param>
        /// <param name="Intro">The intro.</param>
        /// <returns>ServiceResult.</returns>
        ServiceResult Increase(User user, MoneyTypeConfig typeConfig, decimal amount,
            string Intro);

        /// <summary>
        ///     增加账户金额
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="currency">系统货币类型</param>
        /// <param name="amount">解冻金额,必须大于0</param>
        /// <param name="Intro">The intro.</param>
        /// <returns>ServiceResult.</returns>
        ServiceResult Increase(User user, Currency currency, decimal amount, string Intro);
    }
}