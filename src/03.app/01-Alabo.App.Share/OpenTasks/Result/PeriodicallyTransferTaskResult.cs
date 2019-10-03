using Alabo.App.Share.OpenTasks.Base;
using Alabo.App.Share.OpenTasks.Parameter;
using Alabo.Data.People.Users.Domain.Repositories;
using Alabo.Data.Things.Orders.Extensions;
using Alabo.Data.Things.Orders.ResultModel;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Framework.Basic.AutoConfigs.Domain.Configs;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Framework.Tasks.Queues.Models;
using Alabo.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Convert = System.Convert;

namespace Alabo.App.Share.OpenTasks.Result
{
    public class PeriodicallyTransferTaskResult : ITaskResult
    {
        public PeriodicallyTransferTaskResult(TaskContext context)
        {
            Context = context;
        }

        public long UserId { get; set; }

        public decimal Amount { get; set; }

        public Guid SourceMoneyTypeId { get; set; }

        public FenRunResultParameter Parameter { get; set; }

        public IList<AssetsRule> RuleItems { get; set; }
        public TaskContext Context { get; }

        public ExecuteResult Update()
        {
            var moneyType = Ioc.Resolve<IAutoConfigService>()
                .GetList<MoneyTypeConfig>()
                .FirstOrDefault(e => e.Id == Parameter.MoneyTypeId);
            var repositoryContext = Ioc.Resolve<IUserRepository>().RepositoryContext;
            var transaction = repositoryContext.BeginNativeDbTransaction();
            try
            {
                var sql =
                    $"update Asset_Account set Amount=Amount-{Amount}, ModifiedTime=GetDate() where MoneyTypeId='{SourceMoneyTypeId}' and UserId={UserId} and Amount>={Amount}";
                //判断更新语句是否影响了数据库
                if (repositoryContext.ExecuteNonQuery(transaction, sql) > 0) {
                    foreach (var item in RuleItems)
                    {
                        var addAmount = item.Ratio * Amount;
                        sql =
                            $"declare @amount decimal(18, 2);update Asset_Account set Amount=Amount+{addAmount}, @amount=Amount+{addAmount}, HistoryAmount=HistoryAmount+{addAmount}, ModifiedTime=GetDate() where MoneyTypeId='{item.MoneyTypeId}' and UserId={UserId};select @amount";
                        var result = repositoryContext.ExecuteScalar(transaction, sql);
                        if (result == null || result == DBNull.Value) {
                            return ExecuteResult.Fail($"sql script:\"{sql}\" execute with 0 line update.");
                        }

                        var afterAmount = Convert.ToDecimal(result);
                        //此处继续加入Share_Reward与Asset_Bill表的记录
                        sql =
                            @"insert into Share_Reward(AfterAcount, ConfirmTime, CreateTime, ExtraDate, Fee, FenRunDate, Intro, IsAccount, [Level], ModifiedTime, ModuleId, ModuleName, MoneyTypeId, MoneyTypeName, OrderId,OrderPrice, OrderSerial, OrderUserId, OrderUserNick, Price, Remark, Serial, SortOrder, State, Status, UserId, UserNikeName, TriggerType, ModuleConfigId, BonusId)
                            values(@afteracount, GETDATE(), GETDATE(), '', 0, GETDATE(), @intro, 1, 0, GETDATE(), @moduleid, @modulename, @moneytypeid, @moneytypename, 0, 0, '', 0, '', 0, NULL, '', 1000, 3, 0, @userid, @usernickname, @triggertype, @moduleconfigid, @bonusid)";
                        IList<DbParameter> parameterList = new List<DbParameter>
                        {
                            repositoryContext.CreateParameter("@afteracount", afterAmount),
                            repositoryContext.CreateParameter("@intro", Parameter.Summary),
                            repositoryContext.CreateParameter("@moduleid", Parameter.ModuleId),
                            repositoryContext.CreateParameter("@modulename", Parameter.ModuleName),
                            repositoryContext.CreateParameter("@moneytypeid", Parameter.MoneyTypeId),
                            repositoryContext.CreateParameter("@moneytypename", moneyType.Name),
                            repositoryContext.CreateParameter("@userid", Parameter.ReceiveUserId),
                            repositoryContext.CreateParameter("@triggertype", Parameter.TriggerType),
                            repositoryContext.CreateParameter("@moduleconfigid", Parameter.ModuleConfigId),
                            repositoryContext.CreateParameter("@bonusid", Parameter.BonusId)
                        };
                        repositoryContext.ExecuteNonQuery(transaction, sql, parameterList.ToArray());
                        sql =
                            @"insert into Asset_Bill(ActionType, AfterAmount,Amount, BillStatus, BillTypeId, BillTypeName, CheckAmount, CreateTime, Currency, ExtraDate, FailuredReason, Flow, Intro, ModifiedTime, MoneyTypeId, MoneyTypeName, OrderSerial, OtherUserId, OtherUserName, PayTime, ReceiveTime, Remark, Serial, ServiceAmount, ServiceMoneyTypeId, SortOrder, [Status], UserId, UserName, UserRemark)
                                values(@actiontype, @afteramount, @amount, @billstatus, @billtypeid, @billtypename, @checkamount, GETDATE(), @currency, NULL, NULL, 0, @intro, GETDATE(), @moneytypeid, @moneytypename, NULL, 0, NULL,GETDATE(), GETDATE(), NULL, NULL, 0, 0, 1000, 0, @userid, @UserName, NULL)";
                        parameterList = new List<DbParameter>
                        {
                            /*epositoryContext.CreateParameter("@actiontype", fenRunBillTypeConfig.ActionType),*/
                            repositoryContext.CreateParameter("@afteramount", afterAmount),
                            repositoryContext.CreateParameter("@amount", addAmount),
                            repositoryContext.CreateParameter("@billstatus", Parameter.BillStatus),
                            //repositoryContext.CreateParameter("@billtypeid", fenRunBillTypeConfig.Id),
                            //repositoryContext.CreateParameter("@billtypename", fenRunBillTypeConfig.Name),
                            repositoryContext.CreateParameter("@checkamount", addAmount),
                            repositoryContext.CreateParameter("@currency", moneyType.Currency),
                            repositoryContext.CreateParameter("@intro", Parameter.Summary),
                            repositoryContext.CreateParameter("@moneytypeid", moneyType.Id),
                            repositoryContext.CreateParameter("@moneytypename", moneyType.Name),
                            repositoryContext.CreateParameter("@userid", Parameter.ReceiveUserId),
                            repositoryContext.CreateParameter("@UserName", Parameter.ReceiveUserName)
                        };
                        repositoryContext.ExecuteNonQuery(transaction, sql, parameterList.ToArray());
                    }
                }

                transaction.Commit();
                return ExecuteResult.Success();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                return ExecuteResult.Error(e);
            }
            finally
            {
                transaction.Dispose();
            }
        }
    }
}