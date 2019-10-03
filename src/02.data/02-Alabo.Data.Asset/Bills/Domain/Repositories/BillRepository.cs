using Alabo.App.Asset.Accounts.Domain.Entities;
using Alabo.App.Asset.Bills.Domain.Entities;
using Alabo.App.Asset.Bills.Dtos;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Extensions;
using Alabo.Extensions;
using Alabo.Framework.Core.Enums.Enum;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Alabo.App.Asset.Bills.Domain.Repositories
{
    public class BillRepository : RepositoryEfCore<Bill, long>, IBillRepository
    {
        public BillRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public void AddSingleNative(Bill bill)
        {
            if (bill == null) {
                throw new ArgumentNullException(nameof(bill));
            }

            var sql = @"INSERT INTO dbo.Asset_Bill
            ([Serial],[UserId],[OtherUserId],[Type],MoneyTypeId
            ,MoneyTypeName,Flow, Amount ,AfterAmount ,
            OrderSerial ,CreateTime,Intro ,CreateTime,[Remark])
            VALUES
    (@Serial,@UserId,@OtherUserId,@Type,MoneyTypeId,
             MoneyTypeName,@Flow, @Amount ,@AfterAmount ,
            @OrderSerial ,@CreateTime,@Intro,GETDATE(),@Remark) ;SELECT @@IDENTITY;";
            var parameters = new[]
            {
                RepositoryContext.CreateParameter("@AfterAmount", bill.AfterAmount),
                RepositoryContext.CreateParameter("@Type", bill.Type),
                RepositoryContext.CreateParameter("@Amount", bill.Amount),
                RepositoryContext.CreateParameter("@CreateTime", bill.CreateTime),
                RepositoryContext.CreateParameter("@Flow", bill.Flow),
                RepositoryContext.CreateParameter("@Intro", bill.Intro),
                RepositoryContext.CreateParameter("@MoneyTypeId", bill.MoneyTypeId),
                RepositoryContext.CreateParameter("@OtherUserId", bill.OtherUserId),
                // RepositoryContext.CreateParameter("@Remark", bill.Remark),
                RepositoryContext.CreateParameter("@Serial", bill.Serial),
                RepositoryContext.CreateParameter("@UserId", bill.UserId)
            };
            var result = RepositoryContext.ExecuteScalar(sql, parameters);
            if (result != null && result != DBNull.Value) {
                bill.Id = Convert.ToInt32(result);
            }
        }

        public IList<Bill> GetBillList(BillInput userInput, out long count)
        {
            if (userInput.PageIndex < 0) {
                throw new ArgumentNullException("pageIndex", "pageindex has to be greater than 1");
            }

            // TODO: ??? why set this at first??
            //if (userInput.PageSize > 100) {
            //    userInput.PageSize = 100;
            //}

            var sqlWhere = string.Empty;
            if (userInput.Flow.HasValue) {
                sqlWhere = $"{sqlWhere} AND Flow={(int)userInput.Flow}";
            }

            if (userInput.OtherUserId > 0) {
                sqlWhere = $"{sqlWhere} AND ParentId={userInput.OtherUserId}";
            }

            if (userInput.Id > 0) {
                sqlWhere = $"{sqlWhere} AND Id= '{userInput.Id}' ";
            }

            if (!userInput.MoneyTypeId.IsGuidNullOrEmpty()) {
                sqlWhere = $"{sqlWhere} AND MoneyTypeId= '{userInput.MoneyTypeId}' ";
            }

            if (userInput.UserId > 0) {
                sqlWhere = $"{sqlWhere} AND UserId='{userInput.UserId}' ";
            }

            var sqlCount = $"SELECT COUNT(Id) [Count] FROM [Asset_Bill] where 1=1 {sqlWhere}";
            count = RepositoryContext.ExecuteScalar(sqlCount)?.ConvertToLong() ?? 0;

            var result = new List<Bill>();
            var sql = $@"SELECT TOP {userInput.PageSize} * FROM (
                        SELECT  ROW_NUMBER() OVER (ORDER BY id desc) AS RowNumber,[Id] ,[UserId] ,[OtherUserId] ,[Type],[Flow],[MoneyTypeId],[Amount] ,[AfterAmount] ,[Intro] ,[CreateTime] FROM [Asset_Bill] where 1=1 {
                    sqlWhere
                }
                               ) as A
                        WHERE RowNumber > {userInput.PageSize}*({userInput.PageIndex}-1)  ";
            using (var dr = RepositoryContext.ExecuteDataReader(sql))
            {
                while (dr.Read()) {
                    result.Add(ReadBill(dr));
                }
            }

            return result;
        }

        public IList<Bill> GetApiBillList(BillApiInput billApiInput, out long count)
        {
            if (billApiInput.PageIndex < 0) {
                throw new ArgumentNullException("pageIndex", "pageindex has to be greater than 1");
            }

            if (billApiInput.PageSize > 100) {
                billApiInput.PageSize = 100;
            }

            var sqlWhere = string.Empty;
            if (billApiInput.Flow.HasValue) {
                sqlWhere = $"{sqlWhere} AND Flow={(int)billApiInput.Flow}";
            }

            if (!billApiInput.MoneyTypeId.IsGuidNullOrEmpty()) {
                sqlWhere = $"{sqlWhere} AND MoneyTypeId= '{billApiInput.MoneyTypeId}' ";
            }

            if (billApiInput.LoginUserId > 0) {
                sqlWhere = $"{sqlWhere} AND UserId='{billApiInput.LoginUserId}' ";
            }

            var sqlCount = $"SELECT COUNT(Id) [Count] FROM [Asset_Bill] where 1=1 {sqlWhere}";
            count = RepositoryContext.ExecuteScalar(sqlCount)?.ConvertToLong() ?? 0;

            var result = new List<Bill>();
            var sql = $@"SELECT TOP {billApiInput.PageSize} * FROM (
                        SELECT  ROW_NUMBER() OVER (ORDER BY id desc) AS RowNumber,[Id]  ,[Type],[Flow],AfterAmount,[MoneyTypeId],[Amount],[CreateTime] FROM [Asset_Bill] where 1=1 {
                    sqlWhere
                }
                               ) as A
                        WHERE RowNumber > {billApiInput.PageSize}*({billApiInput.PageIndex}-1)  ";
            using (var dr = RepositoryContext.ExecuteDataReader(sql))
            {
                while (dr.Read())
                {
                    var item = new Bill
                    {
                        Id = dr.Read<long>("Id"),
                        Amount = dr.Read<decimal>("Amount"),
                        AfterAmount = dr.Read<decimal>("AfterAmount"),
                        Type = (BillActionType)dr["Type"].ToInt16(),
                        Flow = (AccountFlow)dr["Flow"].ToInt16(),
                        CreateTime = dr.Read<DateTime>("CreateTime"),
                        MoneyTypeId = dr.Read<Guid>("MoneyTypeId")
                    };
                    result.Add(item);
                }
            }

            return result;
        }

        public void Pay(DbTransaction transaction, Bill bill, Account account)
        {
            var sqlAccount = $@"UPDATE [dbo].[Asset_Account]
                                    SET [Amount] = {account.Amount}
                                       ,[FreezeAmount] = {account.FreezeAmount}
                                       ,[HistoryAmount] = {account.HistoryAmount}
                                  WHERE Id={account.Id}";
            var sqlBill =
                $@"INSERT INTO [dbo].[Asset_Bill]([UserId] ,[OtherUserId] ,[Type]  ,[Flow] ,[MoneyTypeId],[Amount] ,[AfterAmount],[Intro] ,[CreateTime] ,[EntityId])
                                 VALUES
                                       ({bill.UserId}
                                       ,{bill.OtherUserId}
                                       ,{(int)bill.Type}
                                       ,{(int)bill.Flow}
                                       ,'{bill.MoneyTypeId}'
                                       ,{bill.Amount}
                                       ,{bill.AfterAmount}
                                       ,'{bill.Intro}'
                                       ,'{bill.CreateTime}'
                                       ,{bill.EntityId})";
            RepositoryContext.ExecuteNonQuery(transaction, sqlBill);
            RepositoryContext.ExecuteNonQuery(transaction, sqlAccount);
        }

        private Bill ReadBill(IDataReader dr)
        {
            var result = new Bill
            {
                Id = dr.Read<long>("Id"),
                UserId = dr.Read<long>("UserId"),
                OtherUserId = dr.Read<long>("OtherUserId"),
                Amount = dr.Read<decimal>("Amount"),
                AfterAmount = dr.Read<decimal>("AfterAmount"),
                Intro = dr.Read<string>("Intro"),
                Type = (BillActionType)dr["Type"].ToInt16(),
                Flow = (AccountFlow)dr["Flow"].ToInt16(),
                CreateTime = dr.Read<DateTime>("CreateTime"),
                MoneyTypeId = dr.Read<Guid>("MoneyTypeId")
            };
            return result;
        }
    }
}