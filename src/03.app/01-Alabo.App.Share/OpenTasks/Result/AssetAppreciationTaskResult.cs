using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Alabo.App.Share.OpenTasks.Base;
using Alabo.App.Share.OpenTasks.Parameter;
using Alabo.Data.People.Users.Domain.Repositories;
using Alabo.Data.Things.Orders.Extensions;
using Alabo.Data.Things.Orders.ResultModel;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Framework.Tasks.Queues.Models;

namespace Alabo.App.Share.OpenTasks.Result {

    public class AssetAppreciationTaskResult : ITaskResult {
        public TaskContext Context { get; private set; }

        public long UserId { get; set; }

        public decimal Amount { get; set; }

        public Guid SourceMoneyTypeId { get; set; }

        public FenRunResultParameter Parameter { get; set; }

        public IList<AssetsRule> RuleItems { get; set; }

        public AssetAppreciationTaskResult(TaskContext context) {
            Context = context;
        }

        public ExecuteResult Update() {
            var repositoryContext = Alabo.Helpers.Ioc.Resolve<IUserRepository>().RepositoryContext;
            var transaction = repositoryContext.BeginNativeDbTransaction();
            try {
                var sql = "";
                foreach (var item in RuleItems) {
                    var addAmount = item.Ratio * Amount;
                    sql = $"update Asset_Account set Amount=Amount+{addAmount}, HistoryAmount=HistoryAmount+{addAmount}, ModifiedTime=GetDate() where MoneyTypeId='{item.MoneyTypeId}' and UserId={UserId}";
                    if (repositoryContext.ExecuteNonQuery(transaction, sql) <= 0) {
                        return ExecuteResult.Fail($"sql script:\"{sql}\" execute with 0 line update.");
                    }
                    //此处继续加入Share_Reward与Asset_Bill表的记录
                    sql = @"insert into Share_Reward(AfterAcount, ConfirmTime, CreateTime, ExtraDate, Fee, FenRunDate, Intro, IsAccount, [Level], ModifiedTime, ModuleId, ModuleName, MoneyTypeId, MoneyTypeName, OrderId,OrderPrice, OrderSerial, OrderUserId, OrderUserNick, Price, Remark, Serial, SortOrder, State, Status, UserId, UserNikeName, TriggerType, ModuleConfigId, BonusId)
                        values(@afteracount, GETDATE(), GETDATE(), '', 0, GETDATE(), @intro, 1, 0, GETDATE(), @moduleid, @modulename, @moneytypeid, @moneytypename, 0, 0, '', 0, '', 0, NULL, '', 1000, 3, 0, @userid, @usernickname, @triggertype, @moduleconfigid, @bonusid)";
                    IList<DbParameter> parameterList = new List<DbParameter>()
                    {
                        repositoryContext.CreateParameter("@afteracount", Parameter.Amount),
                        repositoryContext.CreateParameter("@intro", Parameter.Summary),
                        repositoryContext.CreateParameter("@moduleid", Parameter.ModuleId),
                        repositoryContext.CreateParameter("@modulename", Parameter.ModuleName),
                        repositoryContext.CreateParameter("@moneytypeid", Parameter.MoneyTypeId),
                        repositoryContext.CreateParameter("@moneytypename", ""),
                        repositoryContext.CreateParameter("@userid", Parameter.ReceiveUserId),
                        repositoryContext.CreateParameter("@usernickname", Parameter.ReceiveUserName),
                        repositoryContext.CreateParameter("@triggertype", Parameter.TriggerType),
                        repositoryContext.CreateParameter("@moduleconfigid", Parameter.ModuleConfigId),
                        repositoryContext.CreateParameter("@bonusid", Parameter.BonusId)
                    };
                    repositoryContext.ExecuteNonQuery(transaction, sql, parameterList.ToArray());
                }
                transaction.Commit();
            } catch {
                transaction.Rollback();
                throw;
            } finally {
                transaction.Dispose();
            }
            return ExecuteResult.Success();
        }
    }
}