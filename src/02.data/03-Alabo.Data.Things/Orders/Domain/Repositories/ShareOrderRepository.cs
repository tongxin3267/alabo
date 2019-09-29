using Alabo.App.Asset.Accounts.Domain.Repositories;
using Alabo.Cache;
using Alabo.Data.Things.Orders.Domain.Entities;
using Alabo.Data.Things.Orders.Domain.Entities.Extensions;
using Alabo.Data.Things.Orders.ResultModel;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Extensions;
using Alabo.Extensions;
using Alabo.Framework.Basic.Grades.Domain.Entities;
using Alabo.Framework.Basic.Grades.Domain.Enums;
using Alabo.Framework.Basic.Grades.Domain.Services;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Framework.Tasks.Queues.Domain.Entities;
using Alabo.Framework.Tasks.Queues.Domain.Enums;
using Alabo.Helpers;
using Alabo.Regexs;
using Alabo.Schedules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using ZKCloud.Open.Message.Models;
using Convert = System.Convert;

namespace Alabo.Data.Things.Orders.Domain.Repositories
{
    /// <summary>
    ///     Class ShareOrderRepository.
    /// </summary>
    public class ShareOrderRepository : RepositoryEfCore<ShareOrder, long>, IShareOrderRepository
    {
        public ShareOrderRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public void SuccessOrder(long shareOrderId)
        {
            var sql = "update Task_ShareOrder set Status=@Status ,UpdateTime=GETDATE() where Id=@Id";
            var parameters = new[]
            {
                RepositoryContext.CreateParameter("@Id", shareOrderId),
                RepositoryContext.CreateParameter("@Status", Convert.ToInt16(ShareOrderStatus.Handled))
            };
            RepositoryContext.ExecuteNonQuery(sql, parameters);
        }

        /// <summary>
        ///     获取单条原生Sql记录
        /// </summary>
        /// <param name="shareOrderId">The share order identifier.</param>
        public ShareOrder GetSingleNative(long shareOrderId)
        {
            var sql = $"select  * from Task_ShareOrder where id={shareOrderId}  "; //每次处理10条
            var shareOrder = new ShareOrder();
            using (var dr = RepositoryContext.ExecuteDataReader(sql))
            {
                if (dr.Read()) shareOrder = ReadShareOrder(dr);
            }

            return shareOrder;
        }

        /// <summary>
        ///     Errors the order.
        /// </summary>
        /// <param name="shareOrderId">The share order identifier.</param>
        /// <param name="message">The message.</param>
        public void ErrorOrder(long shareOrderId, string message)
        {
            var sql = "update Task_ShareOrder set Summary=@Summary ,Status=@Status ,UpdateTime=GETDATE() where Id=@Id";
            var parameters = new[]
            {
                RepositoryContext.CreateParameter("@Id", shareOrderId),
                RepositoryContext.CreateParameter("@Status", Convert.ToInt16(ShareOrderStatus.Error)),
                RepositoryContext.CreateParameter("@Summary", message)
            };
            RepositoryContext.ExecuteNonQuery(sql, parameters);
        }

        /// <summary>
        ///     获取s the un handled identifier list.
        /// </summary>
        public IList<long> GetUnHandledIdList()
        {
            var sql =
                $"select  top 10 Id from Task_ShareOrder where Status={(byte)ShareOrderStatus.Pending} order by id   "; //每次处理10条
            IList<long> list = new List<long>();
            using (var dr = RepositoryContext.ExecuteDataReader(sql))
            {
                while (dr.Read()) list.Add(dr.Read<long>("Id"));
            }

            return list;
        }

        /// <summary>
        ///     更新分润模块执行次数
        /// </summary>
        /// <param name="shareOrderId">The share order identifier.</param>
        /// <param name="count">The count.</param>
        public void UpdateExcuteCount(long shareOrderId, long count)
        {
            var sql = $"update Task_ShareOrder set ExecuteCount=ExecuteCount+{count}  where Id={shareOrderId}";
            RepositoryContext.ExecuteNonQuery(sql);
        }

        /// <summary>
        ///     更新分润执行结果
        /// </summary>
        /// <param name="resultList">The result list.</param>
        public void UpdatePriceTaskResult(IEnumerable<ShareResult> resultList)
        {
            var sqlList = new List<string>();
            var dbParameterList = new List<DbParameter[]>();

            var repositoryContext = RepositoryContext;
            long shareOrderId = 0;
            var sql = string.Empty;
            DbParameter[] parameters = null;

            IList<long> shareUsreIds = new List<long>();
            foreach (var shareResult in resultList) shareUsreIds.Add(shareResult.ShareUser.Id);

            //TODO 9月重构注释
            var shareUsreAccounts =
                Ioc.Resolve<IAccountRepository>()
                    .GetAccountByUserIds(shareUsreIds); //获取所有分润用户的资产账户

            foreach (var shareResult in resultList)
            {
                if (shareOrderId == 0) shareOrderId = shareResult.ShareOrder.Id;

                var account = shareUsreAccounts.FirstOrDefault(r =>
                    r.MoneyTypeId == shareResult.MoneyTypeId && r.UserId == shareResult.ShareUser.Id);
                if (account == null) break;

                var afterAccount = account.Amount + shareResult.Amount; //账户金额
                //更新资产
                sql =
                    "update Asset_Account set Amount=Amount+@Amount, HistoryAmount=HistoryAmount+@Amount  where MoneyTypeId=@MoneyTypeId and  UserId=@UserId";
                parameters = new[]
                {
                    repositoryContext.CreateParameter("@UserId", shareResult.ShareUser.Id),
                    repositoryContext.CreateParameter("@MoneyTypeId", shareResult.MoneyTypeId),
                    repositoryContext.CreateParameter("@Amount", shareResult.Amount)
                };
                sqlList.Add(sql);
                dbParameterList.Add(parameters);

                //更新财务记录bill表
                sql =
                    @"INSERT INTO [dbo].[Asset_Bill]([UserId],[OtherUserId] ,[EntityId] ,[Type] ,[Flow],[MoneyTypeId],[Amount],[AfterAmount],[Intro],[CreateTime])
                                                    VALUES (@UserId,@OtherUserId ,@EntityId ,@Type ,@Flow,@MoneyTypeId,@Amount,@AfterAmount,@Intro,@CreateTime)";
                parameters = new[]
                {
                    repositoryContext.CreateParameter("@UserId", shareResult.ShareUser.Id),
                    repositoryContext.CreateParameter("@OtherUserId", shareResult.OrderUser.Id),
                    repositoryContext.CreateParameter("@EntityId", shareResult.ShareOrder.Id),
                    repositoryContext.CreateParameter("@Type", Convert.ToInt16(BillActionType.FenRun)),
                    repositoryContext.CreateParameter("@Flow", Convert.ToInt16(AccountFlow.Income)),
                    repositoryContext.CreateParameter("@MoneyTypeId", shareResult.MoneyTypeId),
                    repositoryContext.CreateParameter("@Amount", shareResult.Amount),
                    repositoryContext.CreateParameter("@AfterAmount", afterAccount),
                    repositoryContext.CreateParameter("@Intro", shareResult.Intro),
                    repositoryContext.CreateParameter("@CreateTime", DateTime.Now)
                };
                sqlList.Add(sql);
                dbParameterList.Add(parameters);

                //添加分润记录
                sql =
                    @"INSERT INTO [dbo].[Share_Reward] ([UserId] ,[OrderUserId],[OrderId],[MoneyTypeId],[Amount] ,[AfterAmount],[ModuleId],[ModuleConfigId],[Intro],[CreateTime],[Status])
                             VALUES (@UserId ,@OrderUserId,@OrderId,@MoneyTypeId,@Amount ,@AfterAmount,@ModuleId,@ModuleConfigId,@Intro,@CreateTime,@Status)";
                parameters = new[]
                {
                    repositoryContext.CreateParameter("@UserId", shareResult.ShareUser.Id),
                    repositoryContext.CreateParameter("@OrderUserId", shareResult.OrderUser.Id),
                    repositoryContext.CreateParameter("@OrderId", shareResult.ShareOrder.Id),
                    repositoryContext.CreateParameter("@MoneyTypeId", shareResult.MoneyTypeId),
                    repositoryContext.CreateParameter("@Amount", shareResult.Amount),
                    repositoryContext.CreateParameter("@AfterAmount", afterAccount),
                    repositoryContext.CreateParameter("@ModuleId", shareResult.ModuleId),
                    repositoryContext.CreateParameter("@ModuleConfigId", shareResult.ModuleConfigId),
                    repositoryContext.CreateParameter("@Intro", shareResult.Intro),
                    repositoryContext.CreateParameter("@Status", 3), // 分润状态暂时设定为成功
                    repositoryContext.CreateParameter("@CreateTime", DateTime.Now)
                };
                sqlList.Add(sql);
                dbParameterList.Add(parameters);

                // 更新货币类型的账号金额，否则多个账号增加金额时会导致账号金额一样
                shareUsreAccounts.Foreach(r =>
                {
                    if (r.MoneyTypeId == shareResult.MoneyTypeId) r.Amount += shareResult.Amount;
                });

                //添加的短信队列
                if (shareResult.SmsNotification)
                {
                    if (RegexHelper.CheckMobile(shareResult.ShareUser.Mobile) && !shareResult.SmsIntro.IsNullOrEmpty())
                    {
                        sql =
                            @"INSERT INTO [dbo].[Basic_MessageQueue] ([TemplateCode],[Mobile],[Content] ,[Parameters] ,[Status],[Message] ,[Summary],[IpAdress],[RequestTime],[SendTime])
                                VALUES (@TemplateCode,@Mobile,@Content ,@Parameters ,@Status,@Message ,@Summary,@IpAdress,@RequestTime,@SendTime)";
                        parameters = new[]
                        {
                            repositoryContext.CreateParameter("@TemplateCode", 0),
                            repositoryContext.CreateParameter("@Mobile", shareResult.ShareUser.Mobile),
                            repositoryContext.CreateParameter("@Content", shareResult.SmsIntro),
                            repositoryContext.CreateParameter("@Parameters", string.Empty),
                            repositoryContext.CreateParameter("@Status", Convert.ToInt16(MessageStatus.Pending)),
                            repositoryContext.CreateParameter("@Message", string.Empty),
                            repositoryContext.CreateParameter("@Summary", string.Empty),
                            repositoryContext.CreateParameter("@IpAdress", "127.0.0.3"),
                            repositoryContext.CreateParameter("@RequestTime", DateTime.Now),
                            repositoryContext.CreateParameter("@SendTime", DateTime.Now)
                        };
                        sqlList.Add(sql);
                        dbParameterList.Add(parameters);
                    }

                    Ioc.Resolve<IObjectCache>().Set("MessageIsAllSend_Cache", false);
                }
            }

            #region //获取得到升级点的用户，并加入升级队列

            var upgradePointsUserIds = resultList
                .Where(r => r.MoneyTypeId == Guid.Parse("E97CCD1E-1478-49BD-BFC7-E73A5D699006"))
                .Select(r => r.ShareUser.Id).ToList();
            if (upgradePointsUserIds.Count > 0)
                foreach (var userId in upgradePointsUserIds)
                {
                    var taskQueue = new TaskQueue
                    {
                        UserId = userId,
                        ModuleId = TaskQueueModuleId.UserUpgradeByUpgradePoints,
                        Type = TaskQueueType.Once
                    };
                    sql =
                        "INSERT INTO [dbo].[Task_TaskQueue] ([UserId]  ,[Type],[ModuleId] ,[Parameter],[ExecutionTimes] ,[ExecutionTime],[CreateTime] ,[HandleTime] ,[MaxExecutionTimes],[Status] ,[Message]) " +
                        "VALUES (@UserId  ,@Type,@ModuleId ,@Parameter,@ExecutionTimes ,@ExecutionTime,@CreateTime ,@HandleTime ,@MaxExecutionTimes,@Status ,@Message)";
                    parameters = new[]
                    {
                        repositoryContext.CreateParameter("@UserId", taskQueue.UserId),
                        repositoryContext.CreateParameter("@Type", taskQueue.Type), // 升级只需执行一次
                        repositoryContext.CreateParameter("@ModuleId", taskQueue.ModuleId),
                        repositoryContext.CreateParameter("@Parameter", taskQueue.Parameter),
                        repositoryContext.CreateParameter("@ExecutionTimes", taskQueue.ExecutionTimes),
                        repositoryContext.CreateParameter("@ExecutionTime", taskQueue.ExecutionTime),
                        repositoryContext.CreateParameter("@CreateTime", taskQueue.CreateTime),
                        repositoryContext.CreateParameter("@HandleTime", taskQueue.HandleTime),
                        repositoryContext.CreateParameter("@MaxExecutionTimes", taskQueue.MaxExecutionTimes),
                        repositoryContext.CreateParameter("@Status", taskQueue.Status),
                        repositoryContext.CreateParameter("@Message", taskQueue.Message)
                    };
                    sqlList.Add(sql);
                    dbParameterList.Add(parameters);
                }

            #endregion //获取得到升级点的用户，并加入升级队列

            // //更新ShareOrder状态
            sql = "update Task_ShareOrder set Summary='sucess sql' ,Status=@Status ,UpdateTime=GETDATE() where Id=@Id";
            parameters = new[]
            {
                repositoryContext.CreateParameter("@Id", shareOrderId),
                repositoryContext.CreateParameter("@Status", Convert.ToInt16(ShareOrderStatus.Handled))
            };
            sqlList.Add(sql);
            dbParameterList.Add(parameters);

            try
            {
                sql = $"select Status from Task_ShareOrder where Id={shareOrderId}";
                //Thread.Sleep(1); // 停留1，防止重复触发
                var shareOrderStatus = repositoryContext.ExecuteScalar(sql).ConvertToInt();
                // 如果订单状态==1，在执行数据操作
                if (shareOrderStatus == 1) repositoryContext.ExecuteBatch(sqlList, dbParameterList);
            }
            catch (Exception ex)
            {
                sql = "update Task_ShareOrder set Summary=@Summary ,Status=@Status ,UpdateTime=GETDATE() where Id=@Id";
                parameters = new[]
                {
                    repositoryContext.CreateParameter("@Id", shareOrderId),
                    repositoryContext.CreateParameter("@Status", Convert.ToInt16(ShareOrderStatus.Error)),
                    repositoryContext.CreateParameter("@Summary", ex.Message)
                };
                repositoryContext.ExecuteNonQuery(sql, parameters);
            }
        }

        /// <summary>
        ///     执行分润执行结果
        /// </summary>
        /// <param name="resultList">The result list.</param>
        public void UpdateUpgradeTaskResult(IEnumerable<UserGradeChangeResult> resultList)
        {
            var sqlList = new List<string>();
            var dbParameterList = new List<DbParameter[]>();
            var repositoryContext = RepositoryContext;

            IList<long> shareUsreIds = new List<long>();

            foreach (var gradeResult in resultList)
            {
                //更新等级
                var sql = "update User_User set GradeId=@GradeId  where  Id=@Id";
                var parameters = new[]
                {
                    repositoryContext.CreateParameter("@Id", gradeResult.Result.UserId),
                    repositoryContext.CreateParameter("@GradeId", gradeResult.Result.GradeId)
                };
                sqlList.Add(sql);
                dbParameterList.Add(parameters);

                #region 添加的短信队列

                //添加的短信队列
                //if (shareResult.SmsNotification) {
                //    if (RegexHelper.CheckMobile(shareResult.ShareUser.Mobile) && !shareResult.SmsIntro.IsNullOrEmpty()) {
                //        sql = @"INSERT INTO [dbo].[Basic_MessageQueue] ([TemplateCode],[Mobile],[Content] ,[Parameters] ,[Status],[Message] ,[Summary],[IpAdress],[RequestTime],[SendTime])
                //                VALUES (@TemplateCode,@Mobile,@Content ,@Parameters ,@Status,@Message ,@Summary,@IpAdress,@RequestTime,@SendTime)";
                //        parameters = new[]
                //        {
                //            repositoryContext.CreateParameter("@TemplateCode", 0),
                //            repositoryContext.CreateParameter("@Mobile", shareResult.ShareUser.Mobile),
                //            repositoryContext.CreateParameter("@Content", shareResult.SmsIntro),
                //            repositoryContext.CreateParameter("@Parameters",string.Empty),
                //            repositoryContext.CreateParameter("@Status", Convert.ToInt16(MessageStatus.Pending)),
                //            repositoryContext.CreateParameter("@Message", string.Empty),
                //            repositoryContext.CreateParameter("@Summary", string.Empty),
                //            repositoryContext.CreateParameter("@IpAdress", "127.0.0.3"),
                //            repositoryContext.CreateParameter("@RequestTime", DateTime.Now),
                //            repositoryContext.CreateParameter("@SendTime",DateTime.Now),
                //     };
                //        sqlList.Add(sql);
                //        dbParameterList.Add(parameters);
                //    }
                //}

                #endregion 添加的短信队列

                #region 团队等级自动更新模块

                //等级触发时，添加推荐等级，间接推荐等级，团队等级自动更新
                var taskQueue = new TaskQueue
                {
                    UserId = gradeResult.Result.UserId,
                    ModuleId = TaskQueueModuleId.TeamUserGradeAutoUpdate, //团队等级自动更新模块
                    Type = TaskQueueType.Once
                };
                sql =
                    "INSERT INTO [dbo].[Task_TaskQueue] ([UserId]  ,[Type],[ModuleId] ,[Parameter],[ExecutionTimes] ,[ExecutionTime],[CreateTime] ,[HandleTime] ,[MaxExecutionTimes],[Status] ,[Message]) " +
                    "VALUES (@UserId  ,@Type,@ModuleId ,@Parameter,@ExecutionTimes ,@ExecutionTime,@CreateTime ,@HandleTime ,@MaxExecutionTimes,@Status ,@Message)";
                parameters = new[]
                {
                    repositoryContext.CreateParameter("@UserId", taskQueue.UserId),
                    repositoryContext.CreateParameter("@Type", taskQueue.Type), // 升级只需执行一次
                    repositoryContext.CreateParameter("@ModuleId", taskQueue.ModuleId),
                    repositoryContext.CreateParameter("@Parameter", taskQueue.Parameter),
                    repositoryContext.CreateParameter("@ExecutionTimes", taskQueue.ExecutionTimes),
                    repositoryContext.CreateParameter("@ExecutionTime", taskQueue.ExecutionTime),
                    repositoryContext.CreateParameter("@CreateTime", taskQueue.CreateTime),
                    repositoryContext.CreateParameter("@HandleTime", taskQueue.HandleTime),
                    repositoryContext.CreateParameter("@MaxExecutionTimes", taskQueue.MaxExecutionTimes),
                    repositoryContext.CreateParameter("@Status", taskQueue.Status),
                    repositoryContext.CreateParameter("@Message", taskQueue.Message)
                };
                sqlList.Add(sql);
                dbParameterList.Add(parameters);

                #endregion 团队等级自动更新模块

                //清除用户缓存，不然会员查看不到等级的变化
                //TODO 9月重构注释
                // Ioc.Resolve<IUserService>()
                //     .DeleteUserCache(gradeResult.Result.UserId, gradeResult.Result.UserName);

                // 添加升级记录
                var upgradeRecord = new UpgradeRecord
                {
                    UserId = gradeResult.Result.UserId,
                    BeforeGradeId = gradeResult.Result.OldGradeId,
                    AfterGradeId = gradeResult.Result.GradeId,
                    Type = UpgradeType.UpgradePoint
                };
                Ioc.Resolve<IUpgradeRecordService>().Add(upgradeRecord);
            }

            if (sqlList.Count > 0)
                try
                {
                    var excuteResult = repositoryContext.ExecuteBatch(sqlList, dbParameterList);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
        }

        public List<ShareOrder> GetList(List<long> EntityIds)
        {
            var shareOrders = new List<ShareOrder>();
            var strSql =
                $"SELECT T.Id,T.EntityId,T.UserId,T.CreateTime FROM Task_ShareOrder T WHERE EntityId IN ({EntityIds.ToSqlString()}) ";
            using (var dr = RepositoryContext.ExecuteDataReader(strSql))
            {
                while (dr.Read()) shareOrders.Add(ShareOrder(dr));
            }

            return shareOrders;
        }

        private ShareOrder ReadShareOrder(IDataReader reader)
        {
            var shareOrder = new ShareOrder
            {
                Id = reader["Id"].ConvertToLong(0),
                EntityId = reader["EntityId"].ConvertToLong(0),
                UserId = reader["UserId"].ConvertToLong(0),
                ExecuteCount = reader["ExecuteCount"].ConvertToLong(0),
                UpdateTime = reader["UpdateTime"].ConvertToDateTime(),
                CreateTime = reader["CreateTime"].ConvertToDateTime(),
                Summary = reader["Summary"].ToString(),
                Amount = reader["Amount"].ConvertToDecimal(0),
                Extension = reader["Extension"].ToString(),
                Parameters = reader["Parameters"].ToString(),
                SystemStatus = (ShareOrderSystemStatus)reader["SystemStatus"].ConvertToInt(0),
                TriggerType = (TriggerType)reader["TriggerType"].ConvertToInt(0),
                Status = (ShareOrderStatus)reader["Status"].ConvertToInt(0)
            };
            if (!shareOrder.Extension.IsNullOrEmpty())
                shareOrder.ShareOrderExtension = shareOrder.Extension.ToObject<ShareOrderExtension>();

            return shareOrder;
        }

        private ShareOrder ShareOrder(IDataReader reader)
        {
            var shareOrder = new ShareOrder
            {
                Id = reader["Id"].ConvertToLong(0),
                EntityId = reader["EntityId"].ConvertToLong(0),
                UserId = reader["UserId"].ConvertToLong(0),
                CreateTime = reader["CreateTime"].ConvertToDateTime()
            };
            return shareOrder;
        }
    }
}