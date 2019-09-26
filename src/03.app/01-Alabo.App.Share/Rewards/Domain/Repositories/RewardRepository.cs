using System;
using System.Collections.Generic;
using System.Data;
using Alabo.App.Share.Rewards.Domain.Entities;
using Alabo.App.Share.Rewards.Domain.Enums;
using Alabo.App.Share.Rewards.Dtos;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Extensions;
using Alabo.Extensions;

namespace Alabo.App.Share.Rewards.Domain.Repositories
{
    internal class RewardRepository : RepositoryEfCore<Reward, long>, IRewardRepository
    {
        public RewardRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IList<Reward> GetRewardList(RewardInput userInput, out long count)
        {
            if (userInput.PageIndex < 0)
                throw new ArgumentNullException("pageIndex", "pageindex has to be greater than 1");

            if (userInput.PageSize > 100) userInput.PageSize = 100;

            var sqlWhere = string.Empty;
            //if (userInput.BeginAmount.HasValue)
            //    sqlWhere = $"{sqlWhere} AND BeginAmount={(decimal)userInput.BeginAmount}";
            //if (userInput.EndAmount.HasValue)
            //    sqlWhere = $"{sqlWhere} AND EndAmount={(decimal)userInput.EndAmount}";
            //if (userInput.EneTime.HasValue)
            //    sqlWhere = $"{sqlWhere} AND CreateTime> '{userInput.EneTime}' ";

            //if (!userInput.MoneyTypeId.IsGuidNullOrEmpty())
            //    sqlWhere = $"{sqlWhere} AND MoneyTypeId= '{userInput.MoneyTypeId}' ";
            //if (userInput.Serial.IsNullOrEmpty())
            //    sqlWhere = $"{sqlWhere} AND Serial='{userInput.Serial}' ";
            if (userInput.UserId > 0) sqlWhere = $"{sqlWhere} AND UserId='{userInput.UserId}' ";

            if (userInput.OrderId > 0) sqlWhere = $"{sqlWhere} AND OrderId='{userInput.OrderId}' ";

            if (!userInput.ModuleId.IsGuidNullOrEmpty()) sqlWhere = $"{sqlWhere} AND ModuleId='{userInput.ModuleId}' ";

            if (userInput.ModuleConfigId > 0) sqlWhere = $"{sqlWhere} AND ModuleConfigId='{userInput.ModuleConfigId}' ";

            var sqlCount = $"SELECT COUNT(Id) [Count] FROM [Share_Reward] where 1=1 {sqlWhere}";
            count = RepositoryContext.ExecuteScalar(sqlCount)?.ConvertToLong() ?? 0;

            var result = new List<Reward>();
            var sql = $@"SELECT TOP {userInput.PageSize} * FROM (
                        SELECT  ROW_NUMBER() OVER (ORDER BY id desc) AS RowNumber,[Id] ,[UserId] ,[OrderUserId] ,[OrderId],[MoneyTypeId],[Amount] ,[AfterAmount]
                      ,[ModuleId],[ModuleConfigId] ,[Intro],[CreateTime] ,[Status] FROM [Share_Reward] where 1=1 {sqlWhere}
                               ) as A
                        WHERE RowNumber > {userInput.PageSize}*({userInput.PageIndex}-1)  ";
            using (var dr = RepositoryContext.ExecuteDataReader(sql))
            {
                while (dr.Read()) result.Add(ReadReward(dr));
            }

            return result;
        }

        private Reward ReadReward(IDataReader dr)
        {
            var result = new Reward
            {
                Id = dr.Read<long>("Id"),
                UserId = dr.Read<long>("UserId"),
                OrderUserId = dr.Read<long>("OrderUserId"),
                OrderId = dr.Read<long>("OrderId"),
                MoneyTypeId = dr.Read<Guid>("MoneyTypeId"),
                ModuleConfigId = dr.Read<long>("ModuleConfigId"),
                Amount = dr.Read<decimal>("Amount"),
                AfterAmount = dr.Read<decimal>("AfterAmount"),
                ModuleId = dr.Read<Guid>("ModuleId"),
                Intro = dr.Read<string>("Intro"),
                Status = (FenRunStatus) dr.Read<int>("Status"),
                CreateTime = dr.Read<DateTime>("CreateTime")
            };
            return result;
        }
    }
}