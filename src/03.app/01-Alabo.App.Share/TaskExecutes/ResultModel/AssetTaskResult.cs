using System;
using Alabo.App.Core.Finance.Domain.Services;
using Alabo.App.Core.Tasks.Extensions;
using Alabo.Helpers;

namespace Alabo.App.Core.Tasks.ResultModel {

    /// <summary>
    ///     资产变化结果
    /// </summary>
    public class AssetTaskResult : ITaskResult {

        public AssetTaskResult(TaskContext context) {
            Context = context;
        }

        public long UserId { get; set; }

        public long AccountId { get; set; }

        public decimal ChangeAmount { get; set; }
        public TaskContext Context { get; }

        public ExecuteResult Update() {
            if (ChangeAmount == 0) {
                return ExecuteResult.Success();
            }

            try {
                var findAccount = Ioc.Resolve<IAccountService>().GetAccount(AccountId);
                if (findAccount == null) {
                    return ExecuteResult.Fail($"not found account when UserId={UserId} and AccountId={AccountId}");
                }

                findAccount.Amount += ChangeAmount;
                if (ChangeAmount > 0) {
                    findAccount.HistoryAmount += ChangeAmount;
                }

                Ioc.Resolve<IAccountService>().Update(findAccount);
                //decimal historyChangeAmount = ChangeAmount > 0 ? ChangeAmount : 0;
                //string sql = $"update [Asset_Account] set [Amount]=[Amount]+{ChangeAmount}, [HistoryAmount]=[HistoryAmount]+{historyChangeAmount} where [UserId]={UserId} and [Id]={AccountId}";
                //Alabo.Helpers.Ioc.Resolve<IUserRepository>().RepositoryContext.ExecuteNonQuery(sql);
                return ExecuteResult.Success();
            } catch (Exception e) {
                return ExecuteResult.Error(e);
            }
        }
    }
}